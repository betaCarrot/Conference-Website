using System;
using System.Diagnostics.Tracing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using static ConferenceWebsite.Global;

namespace ConferenceWebsite
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Hide contact author menu items.
            liCreateSubmission.Visible = false;
            liDisplaySubmissions.Visible = false;
            // Hide PC chair menu items
            liAssignSubmissions.Visible = false;
            liStatistics.Visible = false;
            liReviewerStatistics.Visible = false;
            liSubmissionStatistics.Visible = false;
            liManagePCDropdown.Visible = false;
            liCreatePCMember.Visible = false;
            liDisplayPCMembers.Visible = false;
            // Hide PC member menu items.
            liReviewingAssignments.Visible = false;
            liSubmissionPreferences.Visible = false;

            string userId = HttpContext.Current.User.Identity.GetUserId();
            //var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            if (userId == null) { loginRole = ConferenceRole.None; }

            switch (loginRole)
            {
                case ConferenceRole.AuthorOf:
                    // Show contact author menu items.
                    liCreateSubmission.Visible = true;
                    liDisplaySubmissions.Visible = true;
                    break;
                case ConferenceRole.PCChair:
                    // Show PC chair menu items.
                    liAssignSubmissions.Visible = true;
                    liStatistics.Visible = true;
                    liReviewerStatistics.Visible = true;
                    liSubmissionStatistics.Visible = true;
                    liAssignSubmissions.Visible = true;
                    liManagePCDropdown.Visible = true;
                    liCreatePCMember.Visible = true;
                    liDisplayPCMembers.Visible = true;
                    break;
                case ConferenceRole.PCMember:
                    // Show PC member menu items.
                    liReviewingAssignments.Visible = true;
                    liSubmissionPreferences.Visible = true;
                    break;
                case ConferenceRole.None:
                    break;
            }
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }

}