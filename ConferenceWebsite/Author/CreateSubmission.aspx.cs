using System;
using System.Data;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.UI.WebControls;
using ConferenceWebsite.Models;
using ConferenceWebsite.App_Code;
using static ConferenceWebsite.Global;

namespace ConferenceWebsite.Author
{
    public partial class CreateSubmission : System.Web.UI.Page
    {
        //**********************************
        // Uses TODO 02, 03, 10, 13, 01(S) *
        //**********************************

        private ConferenceDBAccess myConferenceDB = new ConferenceDBAccess();
        private HelperMethods myHelpers = new HelperMethods();
        private SharedMethods mySharedMethods = new SharedMethods();
        private DataTable dtAuthors;
        private DataTable dtNewPersons;
        private string username = HttpContext.Current.User.Identity.Name;

        /***** Private Methods *****/

        private void AddSubmissionAuthor(string personId)
        {
            // Add author information to the list of authors and save in ViewState.
            DataRow drAuthors = dtAuthors.NewRow();
            drAuthors["PERSONID"] = personId;
            if (ddlTitle.SelectedValue == "None") { drAuthors["TITLE"] = ""; }
            else { drAuthors["TITLE"] = ddlTitle.SelectedValue; }
            drAuthors["NAME"] = myHelpers.CleanInput(txtAuthorName.Text);
            drAuthors["INSTITUTION"] = myHelpers.CleanInput(txtInstitution.Text);
            drAuthors["COUNTRY"] = myHelpers.CleanInput(txtCountry.Text);
            drAuthors["EMAIL"] = myHelpers.CleanInput(txtEmail.Text);
            dtAuthors.Rows.Add(drAuthors);
            ViewState["dtAuthorsDataTable"] = dtAuthors;

            // Reset and hide input form.
            ResetAuthorInput();
            txtEmail.Text = "";
            pnlAuthorInput.Visible = false;

            // Display authors list.
            gvCoauthors.DataSource = dtAuthors;
            gvCoauthors.DataBind();
        }

        private string CreatePerson()
        {
            string personId = myHelpers.GetNextTableIdValue("PERSON", "PERSONID");

            if (personId != "")
            {
                //***************
                // Uses TODO 02 *
                //***************
                if (myConferenceDB.CreatePerson(personId, "", ddlTitle.SelectedValue, myHelpers.CleanInput(txtAuthorName.Text),
                    myHelpers.CleanInput(txtInstitution.Text), myHelpers.CleanInput(txtCountry.Text), myHelpers.CleanInput(txtEmail.Text)))
                {
                    ViewState["PERSONID"] = personId;
                }
                else // An SQL error occurred.
                {
                    personId = "";
                    myHelpers.DisplayMessage(lblResultMessage, sqlError);
                }
            }
            else
            {
                myHelpers.DisplayMessage(lblResultMessage, sqlError);
            }
            return personId;
        }

        private bool PopulateAddAuthor(string personId)
        {
            bool result = false;
            // Uses TODO 01 in App_Code\SharedMethods.cs.
            DataTable dtAuthor = mySharedMethods.GetPerson(personId, lblResultMessage);

            // Display the query result if it is valid.
            if (dtAuthor != null)
            {
                if (dtAuthor.Rows.Count != 0)
                {
                    string title = dtAuthor.Rows[0]["TITLE"].ToString().Trim();
                    if (title == "")
                    {
                        ddlTitle.SelectedValue = "None";
                    }
                    else
                    {
                        ddlTitle.SelectedValue = title;
                    }
                    txtAuthorName.Text = dtAuthor.Rows[0]["NAME"].ToString();
                    txtInstitution.Text = dtAuthor.Rows[0]["INSTITUTION"].ToString();
                    txtCountry.Text = dtAuthor.Rows[0]["COUNTRY"].ToString();

                    // Disable editing of author data.
                    ddlTitle.Enabled = false;
                    txtAuthorName.ReadOnly = true;
                    txtInstitution.ReadOnly = true;
                    txtCountry.ReadOnly = true;
                    result = true;
                }
                else // No person record.
                {
                    myHelpers.DisplayMessage(lblResultMessage, myHelpers.EmptyResultForTODO("01"));
                }
            }
            return result;
        }

        private void ResetAuthorInput() //X
        {
            ddlTitle.SelectedIndex = 0;
            txtAuthorName.Text = "";
            txtInstitution.Text = "";
            txtCountry.Text = "";

            // Enable editing of author data.
            ddlTitle.Enabled = true;
            txtAuthorName.ReadOnly = false;
            txtInstitution.ReadOnly = false;
            txtCountry.ReadOnly = false;
        }

        private bool SetAuthorOfRole()
        {
            bool result = true;
            // Set AuthorOf role for contact author.
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindByName(username);
            if (!manager.IsInRole(user.Id, "AuthorOf"))
            {
                IdentityResult roleResult = manager.AddToRole(user.Id, "AuthorOf");
                if (!roleResult.Succeeded)
                {
                    result = false;
                    myHelpers.DisplayMessage(lblResultMessage, "*** Cannot create role AuthorOf for contact author. Please contact 3311rep.");
                }
            }
            return result;
        }

        private void SetContactAuthor()
        {
            // Uses TODO 01 in App_Code\SharedMethods.cs.
            DataTable dtAuthors = mySharedMethods.GetPerson(loginPersonId, lblResultMessage);

            if (dtAuthors != null)
            {
                if (dtAuthors.Rows.Count != 0)
                {
                    ViewState["dtAuthorsDataTable"] = dtAuthors;
                    gvContactAuthor.DataSource = dtAuthors;
                    gvContactAuthor.DataBind();
                }
                else // No person record.
                {
                    myHelpers.DisplayMessage(lblResultMessage, myHelpers.EmptyResultForTODO("01"));
                }
            }
        }

        /***** Protected Methods *****/

        protected void BtnAddAuthor_Click(object sender, EventArgs e)
        {
            lblResultMessage.Visible = false;
            lblSearchResult.Visible = false;
            Page.Validate("SubmissionValidation");
            Page.Validate("AuthorValidation");

            if (IsValid)
            {
                pnlCoauthors.Visible = true;

                string personId;

                if (hfIsNewPerson.Value == "true") // Create a new person record.
                {
                    personId = CreatePerson();
                    if (personId != "")
                    {
                        // Add the person to the list of new persons. Needed to remove person.
                        DataRow drNewPersons = dtNewPersons.NewRow();
                        drNewPersons["PERSONID"] = personId;
                        dtNewPersons.Rows.Add(drNewPersons);
                        ViewState["dtNewPersonsDataTable"] = dtNewPersons;
                    }
                }
                else // Get the person id from ViewState.
                {
                    personId = ViewState["PERSONID"].ToString();
                }

                // An SQL error occurred creating a Person record if the person id is empty.
                if (personId != "")
                {
                    AddSubmissionAuthor(personId);
                }
            }
        }

        protected void BtnSearchForAuthor_Click(object sender, EventArgs e)
        {
            Page.Validate("SubmissionValidation");
            Page.Validate("EmailValidation");

            if (IsValid)
            {
                lblSearchResult.Visible = false;

                string personId = mySharedMethods.GetPersonId(myHelpers.CleanInput(txtEmail.Text), lblResultMessage);

                if (personId != null) // An SQL error occured if the person id is null.
                {
                    // The Person record already exists in the Person table.
                    if (personId != "")
                    {
                        hfIsNewPerson.Value = "false";
                        if (PopulateAddAuthor(personId))
                        {
                            lblSearchResult.Text = "The following author information was found.";
                            ViewState["PERSONID"] = personId;
                            lblSearchResult.Visible = true;
                            pnlAuthorInput.Visible = true;
                        }
                    }
                    else // New Person record.
                    {
                        hfIsNewPerson.Value = "true";
                        ResetAuthorInput();
                        lblSearchResult.Text = "No author information found; please input the following information.";
                        lblSearchResult.Visible = true;
                        pnlAuthorInput.Visible = true;
                    }
                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Page.Validate("SubmissionValidation");

            if (IsValid)
            {
                lblResultMessage.Visible = false;
                dtAuthors = ViewState["dtAuthorsDataTable"] as DataTable;
                dtNewPersons = ViewState["NewPersonsDataTable"] as DataTable;

                // Set the submission information for insertion from the web form controls.
                string submissionNo = myHelpers.GetNextTableIdValue("SUBMISSION", "SUBMISSIONNO").ToString();
                if (submissionNo != "")
                {
                    string submissionTitle = myHelpers.CleanInput(txtTitle.Text);
                    string submissionAbstract = myHelpers.CleanInput(txtAbstract.Text);
                    string submissionType = ddlSubmissionType.SelectedValue;

                    if (SetAuthorOfRole())
                    {
                        //*******************
                        // Uses TODO 10, 13 *
                        //*******************
                        if (myConferenceDB.CreateNewSubmission(submissionNo, submissionTitle, submissionAbstract, submissionType, "",
                        loginPersonId, dtAuthors))
                        {
                            myHelpers.DisplayMessage(lblResultMessage, "Your submission '" + submissionTitle + "' with submission number " +
                                submissionNo + " was successfully submitted.");
                            pnlSubmissionInfo.Visible = false;
                        }
                        else // An SQL error occurred.
                        {
                            // Remove any newly created Person records.
                            foreach (DataRow row in dtNewPersons.Rows)
                            {
                                //***************
                                // Uses TODO 03 *
                                //***************
                                if (!myConferenceDB.RemovePerson(row["PERSONID"].ToString(), null))
                                {
                                    // An SQL error occurred.
                                    myHelpers.DisplayMessage(lblResultMessage, sqlError);
                                    return;
                                }
                            }
                            myHelpers.DisplayMessage(lblResultMessage, sqlError);
                        }
                    }
                }
                else // An SQL error occurred.
                {
                    myHelpers.DisplayMessage(lblResultMessage, sqlError);
                }
            }
        }

        protected void CvIsDuplicateAuthor_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (IsValid)
            {
                if (dtAuthors != null)
                {
                    if (dtAuthors.Select("EMAIL='" + txtEmail.Text.Trim() + "'").Length != 0)
                    {
                        cvIsDuplicateAuthor.ErrorMessage = "This author has already been added.";
                        args.IsValid = false;
                    }
                }
                else // Error in database.
                {
                    cvIsDuplicateAuthor.ErrorMessage = "There are no authors for this submission. Please check your database.";
                    args.IsValid = false;
                }
            }
        }

        protected void CvIsEmailUnique_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (hfIsNewPerson.Value == "true")
            {
                decimal result = myConferenceDB.IsAttributeValueUnique("Person", "email", myHelpers.CleanInput(txtEmail.Text));
                if (result != 0)
                {
                    if (result == -1)
                    {
                        myHelpers.DisplayMessage(lblResultMessage, sqlError);
                    }
                    else
                    {
                        cvIsEmailUnique.ErrorMessage = "Duplicate email.";
                    }
                    args.IsValid = false;
                }
            }
        }

        protected void GvCoauthors_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 8)
            {
                // Hide personId and username. Offset by 1 due to Delete button.
                int personIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PERSONID", lblResultMessage) + 1;
                int usernameColumn = myHelpers.GetGridViewColumnIndexByName(sender, "USERNAME", lblResultMessage) + 1;
                if (personIdColumn != -1 && usernameColumn != -1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        e.Row.Controls[personIdColumn].Visible = false;
                        e.Row.Controls[usernameColumn].Visible = false;
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Controls[personIdColumn].Visible = false;
                        e.Row.Controls[usernameColumn].Visible = false;
                        // Hide the contact author row in the coauthors table.
                        if (e.Row.Cells[personIdColumn].Text == loginPersonId) { e.Row.Visible = false; }
                    }
                }
            }
        }

        protected void GvCoauthors_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string personId = dtAuthors.Rows[e.RowIndex]["PERSONID"].ToString();

            // First, remove the person from the Authors DataTable.
            for (int i = dtAuthors.Rows.Count - 1; i >= 0; i--)
            {
                DataRow row = dtAuthors.Rows[i];
                if (row["PERSONID"].ToString() == personId)
                {
                    dtAuthors.Rows.Remove(row);
                }
            }
            ViewState["AuthorsDataTable"] = dtAuthors;

            // Second, if this is a new person, then remove him/her from the NewPersons DataTable and also,
            // third, remove him/her from the Person table since he/she has already been inserted into the Person table.
            for (int i = dtNewPersons.Rows.Count - 1; i >= 0; i--)
            {
                DataRow row = dtNewPersons.Rows[i];
                if (row["PERSONID"].ToString() == personId)
                {
                    //***************
                    // Uses TODO 03 *
                    //***************
                    if (myConferenceDB.RemovePerson(personId, null))
                    {
                        dtNewPersons.Rows.Remove(row);
                    }
                    else // An SQL error occurred.
                    {
                        myHelpers.DisplayMessage(lblResultMessage, sqlError);
                    }
                }
            }
            ViewState["NewPersonsDataTable"] = dtNewPersons;
            gvCoauthors.DataSource = dtAuthors;
            gvCoauthors.DataBind();

            if (dtAuthors.Rows.Count == 1) { pnlCoauthors.Visible = false; }
        }

        protected void GvContactAuthor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 7)
            {
                // Hide personId.
                int personIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PERSONID", lblResultMessage);
                int usernameColumn = myHelpers.GetGridViewColumnIndexByName(sender, "USERNAME", lblResultMessage);
                if (personIdColumn != -1 && usernameColumn != -1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        e.Row.Controls[personIdColumn].Visible = false;
                        e.Row.Controls[usernameColumn].Visible = false;
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Controls[personIdColumn].Visible = false;
                        e.Row.Controls[usernameColumn].Visible = false;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Set the contact author as the logged in person.
                SetContactAuthor();

                // Create a DataTable to store new person information and store it in ViewState.
                dtNewPersons = new DataTable();
                dtNewPersons.Columns.Add("PERSONID");
                ViewState["NewPersonsDataTable"] = dtNewPersons;
            }
            else
            {
                dtAuthors = (DataTable)ViewState["dtAuthorsDataTable"];
                dtNewPersons = (DataTable)ViewState["NewPersonsDataTable"];
            }
        }
    }
}