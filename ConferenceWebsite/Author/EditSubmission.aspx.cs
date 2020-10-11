using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ConferenceWebsite.App_Code;
using static ConferenceWebsite.Global;

namespace ConferenceWebsite.Author
{
    public partial class EditSubmission : Page
    {
        //************************************************
        // Uses TODO 02, 11, 13, 14, 01(S), 07(S), 08(S) *
        //************************************************

        private ConferenceDBAccess myConferenceDB = new ConferenceDBAccess();
        private HelperMethods myHelpers = new HelperMethods();
        private SharedMethods mySharedMethods = new SharedMethods();
        private DataTable dtSubmission;
        private DataTable dtAuthors;

        /***** Private Methods *****/

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
            else // An SQL error occurred.
            {
                myHelpers.DisplayMessage(lblResultMessage, sqlError);
            }
            return personId;
        }

        private bool CreateSubmissionAuthor(string submissionNo, string personId)
        {
            bool result = false;
            //***************
            // Uses TODO 13 *
            //***************
            if (myConferenceDB.AddAuthorOf(submissionNo, personId, null))
            {
                // Add author information to the list of authors.
                DataRow drAuthors = dtAuthors.NewRow();
                drAuthors["PERSONID"] = personId;
                if (ddlTitle.SelectedValue == "None") { drAuthors["TITLE"] = ""; }
                else { drAuthors["TITLE"] = ddlTitle.SelectedValue; }
                drAuthors["NAME"] = myHelpers.CleanInput(txtAuthorName.Text);
                drAuthors["INSTITUTION"] = myHelpers.CleanInput(txtInstitution.Text);
                drAuthors["COUNTRY"] = myHelpers.CleanInput(txtCountry.Text);
                drAuthors["EMAIL"] = myHelpers.CleanInput(txtEmail.Text);
                dtAuthors.Rows.Add(drAuthors);
                ViewState["AuthorsDataTable"] = dtAuthors;

                // Reset and hide input form.
                ResetAuthorInput();
                txtEmail.Text = "";
                pnlAuthorInput.Visible = false;

                // Display authors list.
                gvCoauthors.DataSource = dtAuthors;
                gvCoauthors.DataBind();
                result = true;
            }
            else// An SQL error occurred.
            {
                myHelpers.DisplayMessage(lblResultMessage, sqlError);
            }
            return result;
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
                else // No person record retrieved for the specified person id. Should not happen!
                {
                    myHelpers.DisplayMessage(lblResultMessage, myHelpers.EmptyResultForTODO("01"));
                }
            }
            return result;
        }

        private void PopulateAuthors(string submissionNo)
        {
            pnlCoauthors.Visible = false;

            // Uses TODO 08 in App_Code\SharedMethods.cs.
            dtAuthors = mySharedMethods.GetSubmissionAuthors(submissionNo, lblResultMessage);

            // Display the query result if it is valid.
            if (dtAuthors != null)
            {
                if (dtAuthors.Rows.Count != 0)
                {
                    // Set the contact author.
                    DataTable dtContactAuthor = dtAuthors.Select("PERSONID=" + loginPersonId).CopyToDataTable();
                    gvContactAuthor.DataSource = dtContactAuthor;
                    gvContactAuthor.DataBind();

                    // Populate the coauthors GridView.
                    if (dtAuthors.Rows.Count != 1)
                    {
                        gvCoauthors.DataSource = dtAuthors;
                        gvCoauthors.DataBind();
                        pnlCoauthors.Visible = true;
                    }
                    ViewState["AuthorsDataTable"] = dtAuthors;
                }
                else // Nothing retrieved.
                {
                    myHelpers.DisplayMessage(lblResultMessage, myHelpers.EmptyResultForTODO("08"));
                }
            }
        }

        private void PopulateSubmission()
        {
            string submissionNo = Request["SUBMISSIONNO"];
            pnlSubmissionInfo.Visible = false;

            // Uses TODO 07 in App_Code/SharedMethods.cs.*
            dtSubmission = mySharedMethods.GetSubmission(submissionNo, lblResultMessage);

            // Display the query result if it is valid.
            if (dtSubmission != null)
            {
                if (dtSubmission.Rows.Count != 0)
                {
                    txtTitle.Text = dtSubmission.Rows[0]["TITLE"].ToString().Trim();
                    txtAbstract.Text = dtSubmission.Rows[0]["ABSTRACT"].ToString().Trim();
                    ddlSubmissionType.SelectedValue = dtSubmission.Rows[0]["SUBMISSIONTYPE"].ToString().Trim();
                    ViewState["SubmissionDataTable"] = dtSubmission;
                    pnlSubmissionInfo.Visible = true;
                    PopulateAuthors(submissionNo);
                }
                else // Nothing retrieved.
                {
                    myHelpers.DisplayMessage(lblResultMessage, myHelpers.EmptyResultForTODO("07"));
                }
            }
        }

        private void ResetAuthorInput()
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

        private bool SubmissionInfoIsChanged(string newTitle, string newSubmissionAbstract, string newSubmissionType)
        {
            if (Equals(dtSubmission.Rows[0]["TITLE"].ToString().Trim(), newTitle) &&
                Equals(dtSubmission.Rows[0]["ABSTRACT"].ToString().Trim(), newSubmissionAbstract) &&
                Equals(dtSubmission.Rows[0]["SUBMISSIONTYPE"].ToString().Trim(), newSubmissionType))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /***** Protected Methods *****/

        protected void BtnAddAuthor_Click(object sender, EventArgs e)
        {
            lblResultMessage.Visible = false;
            lblSearchResult.Visible = false;
            Page.Validate("SubmissionValidation");
            Page.Validate("AuthorValidation");

            if (Page.IsValid)
            {
                pnlCoauthors.Visible = true;
                string personId;

                if (hfIsNewPerson.Value == "true") // Create a new person record.
                {
                    personId = CreatePerson();
                }
                else // Get the person id from ViewState.
                {
                    personId = ViewState["PERSONID"].ToString();
                }
                // An SQL error occurred creating a Person record if the person id is empty.
                if (personId != "")
                {
                    if (!CreateSubmissionAuthor(dtSubmission.Rows[0]["SUBMISSIONNO"].ToString(), personId))
                    {
                        myHelpers.DisplayMessage(lblResultMessage, sqlError);
                    }
                }
            }
        }

        protected void BtnSearchForAuthor_Click(object sender, EventArgs e)
        {
            lblSearchResult.Visible = false;
            Page.Validate("SubmissionValidation");
            Page.Validate("EmailValidation");

            if (IsValid)
            {
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
                    else // New person.
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

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            Page.Validate("SubmissionValidation");

            if (IsValid)
            {
                // Collect the submission information.
                string submissionTitle = myHelpers.CleanInput(txtTitle.Text);
                string submissionAbstract = myHelpers.CleanInput(txtAbstract.Text);
                string submissionType = ddlSubmissionType.SelectedValue;

                // Determine if anything has changed.
                if (SubmissionInfoIsChanged(submissionTitle, submissionAbstract, submissionType))
                {
                    //***************
                    // Uses TODO 11 *
                    //***************
                    if (myConferenceDB.UpdateSubmission(dtSubmission.Rows[0]["SUBMISSIONNO"].ToString().Trim(),
                        submissionTitle, submissionAbstract, submissionType))
                    {
                        // Repopulate the Submission DataTable.
                        dtSubmission.Rows[0]["TITLE"] = submissionTitle;
                        dtSubmission.Rows[0]["ABSTRACT"] = submissionAbstract;
                        dtSubmission.Rows[0]["SUBMISSIONTYPE"] = submissionType;
                        myHelpers.DisplayMessage(lblResultMessage, "The submission information has been updated.");
                        pnlSubmissionInfo.Visible = false;
                    }
                    else // An SQL error occurred.
                    {
                        myHelpers.DisplayMessage(lblResultMessage, sqlError);
                    }
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
            if (e.Row.Controls.Count == 7)
            {
                // Hide personId. Offset by 1 due to Delete column.
                int personIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PERSONID", lblResultMessage) + 1;
                if (personIdColumn != 0)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        e.Row.Controls[personIdColumn].Visible = false;
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        // Hide the person id and the contact author.
                        e.Row.Controls[personIdColumn].Visible = false;
                        if (e.Row.Cells[personIdColumn].Text == loginPersonId) { e.Row.Visible = false; }
                    }
                }
            }
        }

        protected void GvCoauthors_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string personId = dtAuthors.Rows[e.RowIndex]["PERSONID"].ToString();
            //***************
            // Uses TODO 14 *
            //***************
            if (myConferenceDB.DeleteSubmissionAuthor(dtSubmission.Rows[0]["SUBMISSIONNO"].ToString(), personId))
            {
                // Remove the author from the Authors DataTable.
                for (int i = dtAuthors.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow row = dtAuthors.Rows[i];
                    if (row["PERSONID"].ToString() == personId)
                    {
                        dtAuthors.Rows.Remove(row);
                    }
                }
                ViewState["AuthorsDataTable"] = dtAuthors;
                gvCoauthors.DataSource = dtAuthors;
                gvCoauthors.DataBind();

                if (dtAuthors.Rows.Count == 1) { pnlCoauthors.Visible = false; }
            }
            else // An SQL error occurred.
            {
                myHelpers.DisplayMessage(lblResultMessage, sqlError);
            }
        }

        protected void GvContactAuthor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 6)
            {
                // Hide personId.
                int personIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PERSONID", lblResultMessage);
                if (personIdColumn != -1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        e.Row.Controls[personIdColumn].Visible = false;
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Controls[personIdColumn].Visible = false;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateSubmission();
            }
            else
            {
                dtSubmission = (DataTable)ViewState["SubmissionDataTable"];
                dtAuthors = (DataTable)ViewState["AuthorsDataTable"];
            }
        }
    }
}