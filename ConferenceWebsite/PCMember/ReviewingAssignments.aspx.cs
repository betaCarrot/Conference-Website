using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ConferenceWebsite.App_Code;
using static ConferenceWebsite.Global;

namespace ConferenceWebsite.PCMember
{
    public partial class ReviewingAssignments : System.Web.UI.Page
    {
        //*******************
        // Uses TODO 26, 27 *
        //*******************

        private ConferenceDBAccess myConferenceDB = new ConferenceDBAccess();
        private HelperMethods myHelpers = new HelperMethods();

        /***** Private Methods *****/

        private bool GetSubmissionsReviewed(string personId)
        {
            bool result = false;
            //***************
            // Uses TODO 26 *
            //***************
            DataTable dtSubmissions = myConferenceDB.GetAssignedSubmissionsReviewed(personId);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "SUBMISSIONNO", "TITLE", "SUBMISSIONTYPE", "STATUS" };

            // Determine if the query is valid.
            if (myHelpers.IsQueryResultValid("TODO 26", dtSubmissions, attributeList, lblResultMessage))
            {
                if (dtSubmissions.Rows.Count != 0)
                {
                    hfReviewedResult.Value = "some";
                    gvAssignmentsReviewed.DataSource = dtSubmissions;
                    gvAssignmentsReviewed.DataBind();
                    pnlSubmissionsReviewed.Visible = true;
                }
                else // No submissions reviewed.
                {
                    hfReviewedResult.Value = "none";
                    pnlSubmissionsReviewed.Visible = false;
                }
                result = true;
            }
            return result;
        }

        private bool GetSubmissionsNotReviewed(string personId)
        {
            pnlSubmissionsNotReviewed.Visible = true;
            bool result = false;
            //***************
            // Uses TODO 27 *
            //***************
            DataTable dtSubmissions = myConferenceDB.GetAssignedSubmissionsNotReviewed(personId);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "SUBMISSIONNO", "TITLE", "SUBMISSIONTYPE" };

            // Determine if the query is valid.
            if (myHelpers.IsQueryResultValid("TODO 27", dtSubmissions, attributeList, lblResultMessage))
            {
                if (dtSubmissions.Rows.Count != 0)
                {
                    hfNotReviewedResult.Value = "some";
                    gvAssignmentsNotReviewed.DataSource = dtSubmissions;
                    gvAssignmentsNotReviewed.DataBind();
                }
                else // No submissions to review.
                {
                    hfNotReviewedResult.Value = "none";
                    pnlSubmissionsNotReviewed.Visible = false;
                }
                result = true;
            }
            return result;
        }

        /***** Protected Methods *****/

        protected void GvAssignmentsNotReviewed_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 4)
            {
                // Offset by 1 due to Create column.
                int submissionNoColumn = myHelpers.GetGridViewColumnIndexByName(sender, "SUBMISSIONNO", lblResultMessage) + 1;
                if (submissionNoColumn != 0)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "SUBMISSIONNO", "SUBMISSION");
                        myHelpers.RenameGridViewColumn(e, "SUBMISSIONTYPE", "TYPE");
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[submissionNoColumn].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
        }

        protected void GvAssignmentsReviewed_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 6)
            {
                // Offset by 2 due to View and Discuss columns.
                int submissionNoColumn = myHelpers.GetGridViewColumnIndexByName(sender, "SUBMISSIONNO", lblResultMessage) + 2;
                if (submissionNoColumn != 1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "SUBMISSIONNO", "SUBMISSION");
                        myHelpers.RenameGridViewColumn(e, "SUBMISSIONTYPE", "TYPE");
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[submissionNoColumn].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblResultMessage.Visible = false;
            lblResultReviewedMessage.Visible = false;
            lblResulNotReviewedMessage.Visible = false;
            pnlSubmissionsNotReviewed.Visible = false;
            pnlSubmissionsReviewed.Visible = false;

            if (GetSubmissionsReviewed(loginPersonId))
            {
                if (GetSubmissionsNotReviewed(loginPersonId))
                {
                    // No submissions assigned for review.
                    if (hfReviewedResult.Value == "none" && hfNotReviewedResult.Value == "none")
                    {
                        myHelpers.DisplayMessage(lblResultMessage, "No submissions have been assigned to you for review.");
                    }
                    // Submissions assigned; no reviews completed.
                    else if (hfReviewedResult.Value == "none" && hfNotReviewedResult.Value == "some")
                    {
                        pnlSubmissionsNotReviewed.Visible = true;
                        pnlSubmissionsReviewed.Visible = true;
                        myHelpers.DisplayMessage(lblResultReviewedMessage, "No submissions assigned to you have been reviewed.");
                    }
                    // Submissions assigned; all reviews completed.
                    else if (hfReviewedResult.Value == "some" && hfNotReviewedResult.Value == "none")
                    {
                        pnlSubmissionsNotReviewed.Visible = true;
                        pnlSubmissionsReviewed.Visible = true;
                        myHelpers.DisplayMessage(lblResulNotReviewedMessage, "All submissions assigned to you have been reviewed.");
                    }
                    // Submissions assigned; some reviews completed, but not all.
                }
            }
        }
    }
}