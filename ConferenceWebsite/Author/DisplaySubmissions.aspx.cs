using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using ConferenceWebsite.App_Code;
using static ConferenceWebsite.Global;

namespace ConferenceWebsite.Author
{
    public partial class DisplaySubmission : System.Web.UI.Page
    {
        //*******************
        // Uses TODO 09, 12 *
        //*******************

        private ConferenceDBAccess myConferenceDB = new ConferenceDBAccess();
        private HelperMethods myHelpers = new HelperMethods();

        /***** Private Methods *****/
        private void PopulateSubmissions()
        {
            lblResultMessage.Visible = false;
            pnlSearchResult.Visible = false;

            //***************
            // Uses TODO 09 *
            //***************
            DataTable dtSubmissions = myConferenceDB.GetSubmissionsForAuthor(loginPersonId);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "SUBMISSIONNO", "TITLE", "ABSTRACT", "SUBMISSIONTYPE", "STATUS", "CONTACTAUTHOR" };

            // Determine if the query is valid.
            if (myHelpers.IsQueryResultValid("TODO 09", dtSubmissions, attributeList, lblResultMessage))
            {
                if (dtSubmissions.Rows.Count != 0)
                {
                    gvSubmission.DataSource = dtSubmissions;
                    gvSubmission.DataBind();
                    pnlSearchResult.Visible = true;
                }
                else // There are no submissions.
                {
                    myHelpers.DisplayMessage(lblResultMessage, "You do not have any submissions.");
                }
            }
        }

        /***** Protected Methods *****/

        protected void GvSubmission_SelectedIndexChanged(object sender, EventArgs e)
        {
            int row = gvSubmission.SelectedIndex;
            int submissionNo = myHelpers.GetGridViewColumnIndexByName(sender, "SUBMISSIONNO", lblResultMessage) + 2;
            if (submissionNo != -1)
            {
                //***************
                // Uses TODO 12 *
                //***************
                if (myConferenceDB.UpdateSubmissionStatus(gvSubmission.Rows[row].Cells[submissionNo].Text, "withdrawn"))
                {
                    PopulateSubmissions();
                    myHelpers.DisplayMessage(lblResultMessage, "Submission " + gvSubmission.Rows[row].Cells[submissionNo].Text + " has been withdrawn.");
                }
                else // An SQL error occurred.
                {
                    myHelpers.DisplayMessage(lblResultMessage, sqlError);
                }
            }
        }

        protected void GvSubmission_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 8)
            {
                // Hide the contactAuthor. Offset by 2 due to Edit and Withdraw columns.
                int submissionNoColumn = myHelpers.GetGridViewColumnIndexByName(sender, "SUBMISSIONNO", lblResultMessage) + 2;
                int abstractColumn = myHelpers.GetGridViewColumnIndexByName(sender, "ABSTRACT", lblResultMessage) + 2;
                int submissionTypeColumn = myHelpers.GetGridViewColumnIndexByName(sender, "SUBMISSIONTYPE", lblResultMessage) + 2;
                int statusColumn = myHelpers.GetGridViewColumnIndexByName(sender, "STATUS", lblResultMessage) + 2;
                int contactAuthorColumn = myHelpers.GetGridViewColumnIndexByName(sender, "CONTACTAUTHOR", lblResultMessage) + 2;
                if (submissionNoColumn != 1 && submissionTypeColumn != 1 && statusColumn != 1 && contactAuthorColumn != 1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        e.Row.Cells[abstractColumn].Visible = false;
                        myHelpers.RenameGridViewColumn(e, "SUBMISSIONNO", "SUBMISSION");
                        myHelpers.RenameGridViewColumn(e, "SUBMISSIONTYPE", "TYPE");
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[abstractColumn].Visible = false;
                        e.Row.Cells[submissionNoColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[submissionTypeColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[statusColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[contactAuthorColumn].HorizontalAlign = HorizontalAlign.Center;
                        // Hide the Edit and Withdraw links if the logged in user is not the contact author or a staus has been assigned.
                        if (e.Row.Cells[contactAuthorColumn].Text != loginPersonId || e.Row.Cells[statusColumn].Text != "&nbsp;")
                        {
                            e.Row.Cells[0].Text = "Edit";
                            e.Row.Cells[0].ForeColor = System.Drawing.Color.Gray;
                            e.Row.Cells[1].Text = "Withdraw";
                            e.Row.Cells[1].ForeColor = System.Drawing.Color.Gray;
                        }
                        if (e.Row.Cells[contactAuthorColumn].Text == loginPersonId)
                        {
                            e.Row.Cells[contactAuthorColumn].Text = "Yes";
                        }
                        else
                        {
                            e.Row.Cells[contactAuthorColumn].Text = "No";
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateSubmissions();
        }
    }
}