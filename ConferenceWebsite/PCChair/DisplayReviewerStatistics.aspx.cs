using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ConferenceWebsite.App_Code;

namespace ConferenceWebsite.PCChair
{
    public partial class DisplayReviewerStatistics : System.Web.UI.Page
    {
        //***************
        // Uses TODO 23 *
        //***************

        private ConferenceDBAccess myConferenceDB = new ConferenceDBAccess();
        private HelperMethods myHelpers = new HelperMethods();

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            //***************
            // Uses TODO 23 *
            //***************
            DataTable dtPCMembers = myConferenceDB.GetReviewingStatistics();

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "NAME", "ANYNAME", "ANYNAME", "ANYNAME" };

            // Determine if the query is valid.
            if (myHelpers.IsQueryResultValid("TODO 23", dtPCMembers, attributeList, lblResultMessage))
            {
                if (dtPCMembers.Rows.Count != 0)
                {
                    gvReviewingStatistics.DataSource = dtPCMembers;
                    gvReviewingStatistics.DataBind();
                    pnlReviewingStatistics.Visible = true;
                }
                else // Nothing to display
                {
                    myHelpers.DisplayMessage(lblResultMessage, "There are no reviews.");
                }
            }
        }

        protected void GvReviewingStatistics_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 4)
            {
                int nameColumn = myHelpers.GetGridViewColumnIndexByName(sender, "NAME", lblResultMessage);
                if (nameColumn != -1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "NAME", "PC&nbsp;MEMBER");
                        e.Row.Cells[1].Text = "#ASSIGNED";
                        e.Row.Cells[2].Text = "#REVIEWED";
                        e.Row.Cells[3].Text = "#NOT&nbsp;REVIEWED";
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[nameColumn].Text = e.Row.Cells[nameColumn].Text.Replace(" ", "&nbsp;");
                        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
        }
    }
}