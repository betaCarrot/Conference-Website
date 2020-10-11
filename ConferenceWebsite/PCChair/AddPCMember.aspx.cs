using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.UI.WebControls;
using ConferenceWebsite.Models;
using ConferenceWebsite.App_Code;
using static ConferenceWebsite.Global;


namespace ConferenceWebsite.PCChair
{
    public partial class CreatePCMember : Page
    {
        //******************************
        // Uses TODO 02, 06, 21, 01(S) *
        //******************************

        private ConferenceDBAccess myConferenceDB = new ConferenceDBAccess();
        private HelperMethods myHelpers = new HelperMethods();
        private SharedMethods mySharedMethods = new SharedMethods();
        private string personId;

        /***** Private Methods *****/

        private bool AddPCMember(string personId)
        {
            bool result = false;
            //***************
            // Uses TODO 21 * 
            //***************
            if (myConferenceDB.AddPCMember(personId))
            {
                myHelpers.DisplayMessage(lblResultMessage, txtName.Text + " has been added as a PC member.");
                result = true;
            }
            else // An SQL error occurred.
            {
                myHelpers.DisplayMessage(lblResultMessage, sqlError);
            }
            return result;
        }

        private bool RegisterUserAsPCMember(string username, string email)
        {
            bool registerResult = false;

            // Synchronize users in AspNetUsers and Conference databases.
            //if (myHelpers.SynchLoginAndApplicationDatabases(username, email, ConferenceRole.PCMember, lblResultMessage))
            {
                //***************
                // Uses TODO 02 *
                //***************
                loginPersonId = myConferenceDB.CreateRegisteredPerson(username, ddlTitle.SelectedValue, myHelpers.CleanInput(txtName.Text),
                    myHelpers.CleanInput(txtInstitution.Text), myHelpers.CleanInput(txtCountry.Text), email);
                if (loginPersonId != "0")
                {
                    var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var user = new ApplicationUser() { UserName = username, Email = email };
                    IdentityResult result = manager.Create(user, "Conference1#");
                    if (result.Succeeded)
                    {
                        IdentityResult roleResult = manager.AddToRole(user.Id, "PCMember");
                        if (roleResult.Succeeded)
                        {
                            registerResult = true;
                        }
                        else
                        {
                            myHelpers.DisplayMessage(lblResultMessage, "*** " + result.Errors.FirstOrDefault());
                        }
                    }
                    else
                    {
                        myHelpers.DisplayMessage(lblResultMessage, "*** " + result.Errors.FirstOrDefault());
                    }
                }
                else // An SQL error occurred.
                {
                    myHelpers.DisplayMessage(lblResultMessage, sqlError);
                }
            }
            return registerResult;
        }

        private void PopulateAddPCMember(string personId)
        {
            // Uses TODO 01 in App_Code\SharedMethods.cs.
            DataTable dtPerson = mySharedMethods.GetPerson(personId, lblResultMessage);

            // Display the query result if it is valid.
            if (dtPerson != null)
            {
                if (dtPerson.Rows.Count != 0)
                {
                    txtUsername.Text = dtPerson.Rows[0]["USERNAME"].ToString();
                    string title = dtPerson.Rows[0]["TITLE"].ToString().Trim();
                    if (title == "")
                    {
                        ddlTitle.SelectedValue = "None";
                    }
                    else
                    {
                        ddlTitle.SelectedValue = title;
                    }
                    txtName.Text = dtPerson.Rows[0]["NAME"].ToString();
                    txtInstitution.Text = dtPerson.Rows[0]["INSTITUTION"].ToString();
                    txtCountry.Text = dtPerson.Rows[0]["COUNTRY"].ToString();

                    // Disable editing of person data.
                    if (txtUsername.Text != "")
                    {
                        txtUsername.ReadOnly = true;
                        hfIsUsernameSet.Value = "true";
                    }
                    else
                    {
                        hfIsUsernameSet.Value = "false";
                    }
                    ddlTitle.Enabled = false;
                    txtName.ReadOnly = true;
                    txtInstitution.ReadOnly = true;
                    txtCountry.ReadOnly = true;
                }
                else // No person record retrieved for the specified person id. Should not happen!
                {
                    myHelpers.DisplayMessage(lblResultMessage, myHelpers.EmptyResultForTODO("01"));
                    lblSearchResult.Visible = false;
                    pnlPCMemberInput.Visible = false;
                }
            }
        }

        private void ResetPCMemberInput()
        {
            txtUsername.Text = "";
            ddlTitle.SelectedIndex = 0;
            txtName.Text = "";
            txtInstitution.Text = "";
            txtCountry.Text = "";

            // Enable editing of PC member data.
            txtUsername.ReadOnly = false;
            ddlTitle.Enabled = true;
            txtName.ReadOnly = false;
            txtInstitution.ReadOnly = false;
            txtCountry.ReadOnly = false;
        }

        /***** Protected Methods *****/

        protected void BtnAddPCMember_Click(object sender, EventArgs e)
        {
            Page.Validate("PCMemberValidation");

            if (IsValid)
            {
                string username = myHelpers.CleanInput(txtUsername.Text);
                string email = myHelpers.CleanInput(txtEmail.Text);

                if (hfIsNewPerson.Value == "true") // Create a new person record and register person.
                {
                    personId = myHelpers.GetNextTableIdValue("PERSON", "PERSONID");

                    if (personId != "")
                    {
                        //***************
                        // Uses TODO 02 *
                        //***************
                        if (RegisterUserAsPCMember(username, email) == true)
                        {
                            AddPCMember(personId);
                            pnlCreatePCMember.Visible = false;
                        }
                        else // An SQL error occurred.
                        {
                            myHelpers.DisplayMessage(lblResultMessage, sqlError);
                        }
                    }
                    else // An SQL error occurred.
                    {
                        myHelpers.DisplayMessage(lblResultMessage, sqlError);
                    }
                }
                else // Existing person.
                {
                    personId = ViewState["personId"].ToString();

                    // Set username if needed
                    if (hfIsUsernameSet.Value == "false")
                    {
                        //***************
                        // Uses TODO 06 *
                        //***************
                        if (!myConferenceDB.SetUsername(personId, username))
                        {
                            myHelpers.DisplayMessage(lblResultMessage, sqlError);
                            return;
                        }
                    }
                    // Add the PC member.
                    if (AddPCMember(personId))
                    {
                        // Add PCMember role for person.
                        var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                        ApplicationUser user = manager.FindByName(username);
                        IdentityResult roleResult = manager.AddToRole(user.Id, "PCMember");
                        if (roleResult.Succeeded)
                        {
                            pnlCreatePCMember.Visible = false;
                        }
                        else // Cannot create role.
                        {
                            myHelpers.DisplayMessage(lblResultMessage, "*** Cannot create role PCMember for user with email '" + txtEmail.Text + "'. Please contact 3311rep.");
                        }
                    }
                }
            }
        }

        protected void BtnSearchForPerson_Click(object sender, EventArgs e)
        {
            lblSearchResult.Visible = false;
            pnlPCMemberInput.Visible = false;
            Page.Validate("EmailValidation");

            if (IsValid)
            {
                lblSearchResult.Visible = true;
                pnlPCMemberInput.Visible = true;
                if (hfIsNewPerson.Value == "true")
                {
                    hfIsUsernameSet.Value = "false";
                    ResetPCMemberInput();
                    lblSearchResult.Text = "Please input the following PC member information.";
                }
                else
                {
                    personId = ViewState["personId"].ToString();
                    PopulateAddPCMember(personId);
                    lblSearchResult.Text = "The following information was found.";
                }
            }
        }

        protected void CvIsDuplicatePCMember_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (IsValid)
            {
                personId = mySharedMethods.GetPersonId(txtEmail.Text.Trim(), lblResultMessage);

                if (personId != null)
                {
                    if (personId != "") // A person record already exists.
                    {
                        hfIsNewPerson.Value = "false";
                        // Check if the person is already a PC member.
                        decimal result = myConferenceDB.IsPersonInRole(personId, "PCMember");
                        if (result == 0) // The person is not a PC member.
                        {
                            ViewState["personId"] = personId;
                        }
                        else if (result == 1) // The person is already a PC member.
                        {
                            cvIsDuplicatePCMember.ErrorMessage = "This person is a PC member.";
                            args.IsValid = false;
                        }
                        else if (result == -1) // An SQL error occurred.
                        {
                            args.IsValid = false;
                        }
                    }
                    else // Email not found => new person.
                    {
                        hfIsNewPerson.Value = "true";
                    }
                }
                else // An SQL error occurred.
                {
                    args.IsValid = false;
                }
            }
        }

        protected void CvIsDuplicateUsername_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (hfIsUsernameSet.Value == "false")
            {
                decimal result = myConferenceDB.IsAttributeValueUnique("PERSON", "USERNAME", myHelpers.CleanInput(txtUsername.Text));
                if (result == -1) // An SQL error occurred.
                {
                    myHelpers.DisplayMessage(lblResultMessage, sqlError);
                    args.IsValid = false;
                }
                else if (result != 0) // Duplicate username.
                {
                    CvIsDuplicateUsername.ErrorMessage = "This username is taken.";
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}