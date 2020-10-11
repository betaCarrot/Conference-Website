using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using ConferenceWebsite.App_Code;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using static ConferenceWebsite.Global;

namespace ConferenceWebsite.Account
{
    public partial class Manage : System.Web.UI.Page
    {
        //**************************
        // Uses TODO 04, 05, 01(S) *
        //**************************

        private ConferenceDBAccess myConferenceDB = new ConferenceDBAccess();
        private HelperMethods myHelpers = new HelperMethods();
        private SharedMethods mySharedMethods = new SharedMethods();

        /***** Private Methods *****/

        private bool PersonEmailIsChanged(string userName, string newEmail)
        {
            bool result = false;
            string oldEmail = ViewState["oldEmail"].ToString();

            if (oldEmail != newEmail)
            {
                //***************
                // Uses TODO 05 * 
                //***************
                if (myConferenceDB.UpdatePersonEmail(loginPersonId, newEmail))
                {
                    var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var user = manager.FindByName(userName);
                    user.Email = newEmail;
                    manager.Update(user);
                    result = true;
                }
                else // An SQL error occurred.
                {
                    myHelpers.DisplayMessage(lblResultMessage, sqlError);
                }
            }
            return result;
        }

        private bool PersonInfoIsChanged(string newTitle, string newName, string newInstitution, string newCountry)
        {
            DataTable dtPerson = ViewState["PersonInfo"] as DataTable;

            if (Equals(dtPerson.Rows[0]["TITLE"].ToString().Trim(), newTitle) &&
                Equals(dtPerson.Rows[0]["NAME"].ToString().Trim(), newName) &&
                Equals(dtPerson.Rows[0]["INSTITUTION"].ToString().Trim(), newInstitution) &&
                Equals(dtPerson.Rows[0]["COUNTRY"].ToString().Trim(), newCountry))
            {
                return false;
            }
            return true;
        }

        private void PopulatePerson(string personId)
        {
            // Uses TODO 01 in App_Code\SharedMethods.cs.
            DataTable dtPerson = mySharedMethods.GetPerson(personId, lblResultMessage);

            // Display the query result if it is valid.
            if (dtPerson != null)
            {
                if (dtPerson.Rows.Count != 0)
                {
                    string title = dtPerson.Rows[0]["TITLE"].ToString().Trim();
                    if (title == "")
                    {
                        ddlTitle.SelectedValue = "None";
                    }
                    else
                    {
                        ddlTitle.SelectedValue = title;
                    }
                    txtUsername.Text = dtPerson.Rows[0]["USERNAME"].ToString().Trim();
                    txtName.Text = dtPerson.Rows[0]["NAME"].ToString();
                    txtInstitution.Text = dtPerson.Rows[0]["INSTITUTION"].ToString();
                    txtCountry.Text = dtPerson.Rows[0]["COUNTRY"].ToString();
                    ViewState["oldEmail"] = txtEmail.Text = dtPerson.Rows[0]["EMAIL"].ToString();
                    ViewState["PersonInfo"] = dtPerson;
                    pnlCreatePerson.Visible = true;
                }
                else // No person record retrieved for the specified person id. Should not happen!
                {
                    myHelpers.DisplayMessage(lblResultMessage, myHelpers.EmptyResultForTODO("01"));
                }
            }
        }

        /***** Protected Methods *****/

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string userName = myHelpers.CleanInput(txtUsername.Text);
                string title = ddlTitle.SelectedValue;
                string name = myHelpers.CleanInput(txtName.Text);
                string institution = myHelpers.CleanInput(txtInstitution.Text);
                string country = myHelpers.CleanInput(txtCountry.Text);
                string email = myHelpers.CleanInput(txtEmail.Text);

                if (PersonInfoIsChanged(title, name, institution, country))
                {
                    //***************
                    // Uses TODO 04 * 
                    //***************
                    if (myConferenceDB.UpdatePerson(loginPersonId, title, name, institution, country))
                    {
                        myHelpers.DisplayMessage(lblResultMessage, "Your information has been updated.");
                        pnlCreatePerson.Visible = false;
                    }
                    else // An SQL error occurred.
                    {
                        myHelpers.DisplayMessage(lblResultMessage, sqlError);
                    }
                }
                else if (PersonEmailIsChanged(userName, email))
                {
                    myHelpers.DisplayMessage(lblResultMessage, "Your information has been updated.");
                    pnlCreatePerson.Visible = false;
                }
                else // Nothing was updated.
                {
                    myHelpers.DisplayMessage(lblUpdateMessage, "You did not update any information.");
                }
            }
        }

        protected void CvIsEmailUnique_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string oldEmail = ViewState["oldEmail"].ToString();
            if (oldEmail != myHelpers.CleanInput(txtEmail.Text))
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

        protected void Page_Load()
        {
            if (!IsPostBack)
            {
                PopulatePerson(loginPersonId);
            }

        }
    }
}