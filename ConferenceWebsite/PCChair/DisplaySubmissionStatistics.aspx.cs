using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ConferenceWebsite.App_Code;

namespace ConferenceWebsite.PCChair
{
    public partial class DisplaySubmissionStatistics : System.Web.UI.Page
    {
        //***************
        // Uses TODO 22 *
        //***************

        private ConferenceDBAccess myConferenceDB = new ConferenceDBAccess();
        private HelperMethods myHelpers = new HelperMethods();

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            //***************
            // Uses TODO 22 *
            //***************
            DataTable dtSubmissionStatistics = myConferenceDB.GetSubmissionStatistics();

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "SUBMISSIONNO", "TITLE", "SUBMISSIONTYPE", "NAME", "ANYNAME", "ANYNAME", "ANYNAME", "STATUS" };

            // Determine if the query is valid.
            if (myHelpers.IsQueryResultValid("TODO 22", dtSubmissionStatistics, attributeList, lblResultMessage))
            {
                if (dtSubmissionStatistics.Rows.Count != 0)
                {
                    gvSubmissionStatistics.DataSource = dtSubmissionStatistics;
                    gvSubmissionStatistics.DataBind();
                    pnlSubmissionStatistics.Visible = true;
                }
                else // Nothing to display
                {
                    myHelpers.DisplayMessage(lblResultMessage, "There are no submissions.");
                }
            }
        }

        protected void GvSubmissionStatistics_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 8)
            {
                int submissionNoColumn = myHelpers.GetGridViewColumnIndexByName(sender, "SUBMISSIONNO", lblResultMessage);
                int titleColumn = myHelpers.GetGridViewColumnIndexByName(sender, "TITLE", lblResultMessage);
                int submissionTypeColumn = myHelpers.GetGridViewColumnIndexByName(sender, "SUBMISSIONTYPE", lblResultMessage);
                int nameColumn = myHelpers.GetGridViewColumnIndexByName(sender, "NAME", lblResultMessage);
                int statusColumn = myHelpers.GetGridViewColumnIndexByName(sender, "STATUS", lblResultMessage);
                if (submissionNoColumn != -1 && titleColumn != -1 && submissionTypeColumn != -1 && nameColumn != -1 && statusColumn != -1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "SUBMISSIONNO", "SUBMISSION");
                        myHelpers.RenameGridViewColumn(e, "SUBMISSIONTYPE", "TYPE");
                        myHelpers.RenameGridViewColumn(e, "NAME", "CONTACT&nbsp;AUTHOR");
                        e.Row.Cells[4].Text = "#AUTHORS";
                        e.Row.Cells[5].Text = "#REVIEWERS";
                        e.Row.Cells[6].Text = "#COMPLETED";
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[submissionNoColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[submissionTypeColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
        }
    }
}