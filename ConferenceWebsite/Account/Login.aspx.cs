using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity.Owin;
using ConferenceWebsite.App_Code;
using static ConferenceWebsite.Global;

namespace ConferenceWebsite.Account
{
    public partial class Login : Page
    {
        private ConferenceDBAccess myConferenceDB = new ConferenceDBAccess();
        private HelperMethods myHelpers = new HelperMethods();
        private SharedMethods mySharedMethods = new SharedMethods();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Get email from username.
                string email = myHelpers.GetEmailFromUsername(Username.Text, lblResultMessage);
                if (email == null) { myHelpers.DisplayMessage(lblResultMessage, lblResultMessage.Text); return; }
                // Set the user's role.
                if (Enum.TryParse(rblRole.SelectedValue, out ConferenceRole role))
                {
                    loginRole = role;
                }
                else // Error parsing role value.
                {
                    myHelpers.DisplayMessage(lblResultMessage, "*** Cannot set the user's role. Please contact 3311rep.");
                    return;
                }

                //  Get person id for user.
                loginPersonId = mySharedMethods.GetPersonId(email, lblResultMessage);
                if (loginPersonId == null)
                {
                    myHelpers.DisplayMessage(lblResultMessage, sqlError);
                    return;
                }

                // Determine if person is in selected role if the role is not AuthorOf. 
                if (loginRole != ConferenceRole.AuthorOf)
                {
                    decimal roleResult = myConferenceDB.IsPersonInRole(loginPersonId, loginRole.ToString());
                    if (roleResult == 0)
                    {
                        myHelpers.DisplayMessage(lblResultMessage, "You are not authorized for this role.");
                        return;
                    }
                    else if (roleResult == -1) // An SQL error occurred.
                    {
                        myHelpers.DisplayMessage(lblResultMessage, sqlError);
                        return;
                    }
                }

                // Synchronize users in AspNetUsers and Conference databases.
                if (myHelpers.SynchLoginAndApplicationDatabases(Username.Text, email, loginRole, lblResultMessage))
                {
                    var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger lockout, change to shouldLockout: true
                    var result = signinManager.PasswordSignIn(Username.Text, Password.Text, false, shouldLockout: false);

                    switch (result)
                    {
                        case SignInStatus.Success:
                            IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                            break;
                        case SignInStatus.LockedOut:
                            Response.Redirect("/Account/Lockout");
                            break;
                        case SignInStatus.RequiresVerification:
                            Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                            Request.QueryString["ReturnUrl"], false), false);
                            break;
                        case SignInStatus.Failure:
                        default:
                            myHelpers.DisplayMessage(lblResultMessage, "Invalid username.");
                            break;
                    }
                }
                else // Failed to synchronize.
                {
                    myHelpers.DisplayMessage(lblResultMessage, lblResultMessage.Text);
                }
            }
        }
    }
}