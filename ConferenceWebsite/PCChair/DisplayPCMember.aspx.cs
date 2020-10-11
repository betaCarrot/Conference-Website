using System;
using System.Data;
using System.Collections.Generic;
using ConferenceWebsite.App_Code;

namespace ConferenceWebsite.PCChair
{
    public partial class DisplayPCMember : System.Web.UI.Page
    {
        //***************
        // Uses TODO 20 *
        //***************

        private ConferenceDBAccess myConferenceDB = new ConferenceDBAccess();
        private HelperMethods myHelpers = new HelperMethods();

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            //***************
            // Uses TODO 20 *
            //***************
            DataTable dtPCMember = myConferenceDB.GetAllPCMembers();

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "TITLE", "NAME", "INSTITUTION", "COUNTRY", "EMAIL" };

            // Determine if the query is valid.
            if (myHelpers.IsQueryResultValid("TODO 20", dtPCMember, attributeList, lblResultMessage))
            {
                if (dtPCMember.Rows.Count != 0)
                {
                    gvPCMember.DataSource = dtPCMember;
                    gvPCMember.DataBind();
                    pnlPCMemberInfo.Visible = true;
                }
                else // Nothing to display
                {
                    myHelpers.DisplayMessage(lblResultMessage, "There are no PC members.");
                }
            }
        }
    }
}