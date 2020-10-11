using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ConferenceWebsite.Models;
using ConferenceWebsite.App_Code;
using static ConferenceWebsite.Global;

namespace ConferenceWebsite.Account
{
    public partial class Register : Page
    {
        private readonly ConferenceDBAccess myConferenceDB = new ConferenceDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string username = myHelpers.CleanInput(txtUsername.Text);
                string email = myHelpers.CleanInput(Email.Text);

                // Synchronize users in AspNetUsers and Conference databases.
                if (myHelpers.SynchLoginAndApplicationDatabases(username, email, ConferenceRole.None, lblResultMessage))
                {
                    loginPersonId = myConferenceDB.CreateRegisteredPerson(username, ddlTitle.SelectedValue, myHelpers.CleanInput(txtName.Text), 
                        myHelpers.CleanInput(txtInstitution.Text), myHelpers.CleanInput(txtCountry.Text), email);
                    if (loginPersonId != "0")
                    {
                        var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                        var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
                        var user = new ApplicationUser() { UserName = username, Email = email };
                        IdentityResult result = manager.Create(user, Password.Text);
                        if (result.Succeeded)
                        {
                            IdentityResult roleResult = manager.AddToRole(user.Id, "AuthorOf");
                            if (roleResult.Succeeded)
                            {
                                loginRole = ConferenceRole.AuthorOf;
                                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
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
            }
        }

        protected void CvIsDuplicateEmail_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            decimal result = myConferenceDB.IsAttributeValueUnique("Person", "email", myHelpers.CleanInput(Email.Text));
            if (result != 0)
            {
                if (result == -1)
                {
                    myHelpers.DisplayMessage(lblResultMessage, sqlError);
                }
                else
                {
                    cvEmail.ErrorMessage = "Duplicate email.";
                }
                args.IsValid = false;
            }
        }

        protected void CvIsDuplicateUsername_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            decimal result = myConferenceDB.IsAttributeValueUnique("Person", "username", myHelpers.CleanInput(txtUsername.Text));
            if (result != 0)
            {
                if (result == -1)
                {
                    myHelpers.DisplayMessage(lblResultMessage, sqlError);
                }
                else
                {
                    cvUsername.ErrorMessage = "Duplicate username.";
                }
                args.IsValid = false;
            }
        }
    }
}