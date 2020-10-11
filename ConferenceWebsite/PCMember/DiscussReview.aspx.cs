using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ConferenceWebsite.App_Code;
using static ConferenceWebsite.Global;

namespace ConferenceWebsite.PCMember
{
    public partial class DiscussReview : System.Web.UI.Page
    {
        //*****************************************
        // Uses TODO 29, 30, 31, 34, 07(S), 08(S) *
        //*****************************************

        private ConferenceDBAccess myConferenceDB = new ConferenceDBAccess();
        private HelperMethods myHelpers = new HelperMethods();
        private SharedMethods mySharedMethods = new SharedMethods();

        /***** Private Methods *****/

        private bool GetAuthors(string submissionNo)
        {
            bool result = false;
            lblResultMessage.Visible = false;

            // Uses TODO 08 in App_Code/SharedMethods.cs.
            DataTable dtAuthors = mySharedMethods.GetSubmissionAuthors(submissionNo, lblResultMessage);

            // Determine if the query is valid.
            if (dtAuthors != null)
            {
                // There is an error in the query if the query result is empty.
                if (dtAuthors.Rows.Count != 0)
                {
                    // Set the submission title and authors for display.
                    txtAuthor.Text = "";
                    for (int i = 0; i < dtAuthors.Rows.Count; i++)
                    {
                        txtAuthor.Text += dtAuthors.Rows[i]["NAME"].ToString();
                        if (i < dtAuthors.Rows.Count - 1)
                        {
                            txtAuthor.Text += ", ";
                        }
                    }
                    pnlAuthors.Visible = true;
                    result = true;
                }
                else // SQL error - no authors.
                {
                    myHelpers.DisplayMessage(lblResultMessage, myHelpers.EmptyResultForTODO("08"));
                }
            }
            return result;
        } //

        private void GetDiscussion(string submissionNo)
        {
            pnlAddNewDiscussion.Visible = false;
            pnlDiscussion.Visible = false;

            //***************
            // Uses TODO 29 *
            //***************
            DataTable dtDiscussion = myConferenceDB.GetDiscussion(submissionNo);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "NAME", "COMMENTS" };

            // Determine if the query is valid.
            if (myHelpers.IsQueryResultValid("TODO 29", dtDiscussion, attributeList, lblResultMessage))
            {
                if (dtDiscussion.Rows.Count != 0)
                {
                    gvDiscussion.DataSource = dtDiscussion;
                    gvDiscussion.DataBind();
                    gvDiscussion.Visible = true;
                    lblDiscussionResultMessage.Visible = false;
                }
                else
                {
                    gvDiscussion.Visible = false;
                    myHelpers.DisplayMessage(lblDiscussionResultMessage, "There is no discussion for this submission.");
                }
                //btnViewDiscussion.Visible = false;
                pnlDiscussion.Visible = true;
                pnlAddNewDiscussion.Visible = true;
            }
        }

        private bool GetSubmissionReviewSummary(string submissionNo)
        {
            bool result = false;

            //***************
            // Uses TODO 31 * 
            //***************
            DataTable dtSubmissionReviewSummary = myConferenceDB.GetSubmissionReviewSummary(submissionNo);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "NAME", "RELEVANT", "TECHNICALLYCORRECT", "LENGTHANDCONTENT",
                "CONFIDENCE", "ORIGINALITY", "IMPACT", "PRESENTATION", "TECHNICALDEPTH", "OVERALLRATING", "OVERALLSUMMARY" };

            // Determine if the query is valid.
            if (myHelpers.IsQueryResultValid("TODO 31", dtSubmissionReviewSummary, attributeList, lblResultMessage))
            {
                if (dtSubmissionReviewSummary.Rows.Count >= 3)
                {
                    DataView dvSubmissionReviewSummary = new DataView(dtSubmissionReviewSummary);
                    // Display overall summary of each reviewer.
                    gvOverallSummary.DataSource = dvSubmissionReviewSummary.ToTable(false, "NAME", "OVERALLSUMMARY");
                    gvOverallSummary.DataBind();

                    // Display summary of reviews.
                    gvReviewSummary.DataSource = dtSubmissionReviewSummary;
                    gvReviewSummary.DataBind();

                    txtSpread.Text = GetSpread(dtSubmissionReviewSummary);
                    GetDiscussion(submissionNo);

                    result = true;
                }
                else // Not enough reviews for a discussion.
                {
                    myHelpers.DisplayMessage(lblResultMessage, "At least three reviews are required to open a discussion for a paper. This paper has " + dtSubmissionReviewSummary.Rows.Count.ToString() + " reviews.");
                }
            }
            return result;
        }

        private string GetSpread(DataTable dtSubmissionReviewSummary)
        {
            int max = int.Parse(dtSubmissionReviewSummary.Rows[0]["OVERALLRATING"].ToString());
            int min = int.Parse(dtSubmissionReviewSummary.Rows[0]["OVERALLRATING"].ToString());
            foreach (DataRow row in dtSubmissionReviewSummary.Rows)
            {
                int rating = int.Parse(row["OVERALLRATING"].ToString());
                if (rating < min) { min = rating;  }
                else if (rating > max) { max = rating; }
            }
            return (max - min).ToString();
        }

        private bool GetSubmission(string submissionNo)
        {
            bool result = false;
            lblResultMessage.Visible = false;

            // Uses TODO 07 in App_Code/SharedMethods.cs.
            DataTable dtSubmission = mySharedMethods.GetSubmission(submissionNo, lblResultMessage);

            // Determine if the query is valid.
            if (dtSubmission != null)
            {
                // There is an error in the query if the query result is empty.
                if (dtSubmission.Rows.Count != 0)
                {
                    txtSubmissionNo.Text = dtSubmission.Rows[0]["SUBMISSIONNO"].ToString();
                    txtTitle.Text = dtSubmission.Rows[0]["TITLE"].ToString();
                    txtStatus.Text = dtSubmission.Rows[0]["STATUS"].ToString();
                    pnlSubmission.Visible = true;
                    result = true;
                }
                else // SQL error - no submission.
                {
                    myHelpers.DisplayMessage(lblResultMessage, myHelpers.EmptyResultForTODO("07"));
                }
            }
            return result;
        }

        /***** Protected Methods *****/

        protected void BtnAddToDiscussion_Click(object sender, EventArgs e) // CreateDiscusssion
        {
            if (IsValid)
            {
                lblResultMessage.Visible = false;

                // Collect the information needed to insert a new discussion.
                string submissionNo = Request["SUBMISSIONNO"];
                string comments = myHelpers.CleanInput(txtNewDiscussion.Text);
                if (comments != "")
                {
                    //***************
                    // Uses TODO 30 *
                    //***************
                    decimal sequenceNumber = myConferenceDB.GetMaxSequenceNoForSubmission(submissionNo);

                    if (sequenceNumber != -1)
                    {
                        //***************
                        // Uses TODO 34 *
                        //***************
                        if (myConferenceDB.CreateDiscusssion((sequenceNumber + 1).ToString(), submissionNo, loginPersonId, comments))
                        {
                            // Refresh the discussion and reset the new discussion textbox.
                            GetDiscussion(submissionNo);
                            txtNewDiscussion.Text = "";
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
            }
        }

        protected void gvDiscussion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 2)
            {
                int nameColumn = myHelpers.GetGridViewColumnIndexByName(sender, "NAME", lblResultMessage);
                if (nameColumn != -1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "NAME", "PC&nbsp;MEMBER");
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[nameColumn].Text = e.Row.Cells[nameColumn].Text.Replace(" ", "&nbsp;");
                    }
                }
            }
        }

        protected void GvOverallSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 2)
            {
                int nameColumn = myHelpers.GetGridViewColumnIndexByName(sender, "NAME", lblResultMessage);
                int overallSummaryColumn = myHelpers.GetGridViewColumnIndexByName(sender, "OVERALLSUMMARY", lblResultMessage);
                if (nameColumn != -1 && overallSummaryColumn != -1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "NAME", "PC&nbsp;MEMBER");
                        myHelpers.RenameGridViewColumn(e, "OVERALLSUMMARY", "OVERALL&nbsp;SUMMARY");
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[nameColumn].Text = e.Row.Cells[nameColumn].Text.Replace(" ", "&nbsp;");
                    }
                }
            }
        }

        protected void GvReviewSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 11)
            {
                int nameColumn = myHelpers.GetGridViewColumnIndexByName(sender, "NAME", lblResultMessage);
                int relevanceColumn = myHelpers.GetGridViewColumnIndexByName(sender, "RELEVANT", lblResultMessage);
                int correctnessColumn = myHelpers.GetGridViewColumnIndexByName(sender, "TECHNICALLYCORRECT", lblResultMessage);
                int lengthContentColumn = myHelpers.GetGridViewColumnIndexByName(sender, "LENGTHANDCONTENT", lblResultMessage);
                int confidenceColumn = myHelpers.GetGridViewColumnIndexByName(sender, "CONFIDENCE", lblResultMessage);
                int originalityColumn = myHelpers.GetGridViewColumnIndexByName(sender, "ORIGINALITY", lblResultMessage);
                int impacColumn = myHelpers.GetGridViewColumnIndexByName(sender, "IMPACT", lblResultMessage);
                int presentationColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PRESENTATION", lblResultMessage);
                int technicalDepthColumn = myHelpers.GetGridViewColumnIndexByName(sender, "TECHNICALDEPTH", lblResultMessage);
                int overallRatingColumn = myHelpers.GetGridViewColumnIndexByName(sender, "OVERALLRATING", lblResultMessage);
                int overallSummaryColumn = myHelpers.GetGridViewColumnIndexByName(sender, "OVERALLSUMMARY", lblResultMessage);
                if (nameColumn != -1 && relevanceColumn != -1 && correctnessColumn != -1 && lengthContentColumn != -1 &&
                    originalityColumn != -1 && impacColumn != -1 && presentationColumn != -1 && technicalDepthColumn != -1 &&
                    overallRatingColumn != -1 && confidenceColumn != -1 && overallSummaryColumn != -1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        e.Row.Cells[relevanceColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[correctnessColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[lengthContentColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[confidenceColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[originalityColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[impacColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[presentationColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[technicalDepthColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[overallRatingColumn].HorizontalAlign = HorizontalAlign.Center;

                        e.Row.Cells[overallSummaryColumn].Visible = false;
                        myHelpers.RenameGridViewColumn(e, "NAME", "PC MEMBER");
                        myHelpers.RenameGridViewColumn(e, "RELEVANT", "RELE<br />VANCE");
                        myHelpers.RenameGridViewColumn(e, "TECHNICALLYCORRECT", "CORRECT<br />NESS");
                        myHelpers.RenameGridViewColumn(e, "LENGTHANDCONTENT", "LENGTH&<br />CONTENT");
                        myHelpers.RenameGridViewColumn(e, "CONFIDENCE", "CONFI<br />DENCE");
                        myHelpers.RenameGridViewColumn(e, "ORIGINALITY", "ORIGIN<br />ALITY");
                        myHelpers.RenameGridViewColumn(e, "PRESENTATION", "PRESENT<br />ATION");
                        myHelpers.RenameGridViewColumn(e, "TECHNICALDEPTH", "TECHNICAL<br />DEPTH");
                        myHelpers.RenameGridViewColumn(e, "OVERALLRATING", "OVERALL");
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[overallSummaryColumn].Visible = false;
                        e.Row.Cells[nameColumn].Text = e.Row.Cells[nameColumn].Text.Replace(" ", "&nbsp;");
                        e.Row.Cells[relevanceColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[correctnessColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[lengthContentColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[confidenceColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[originalityColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[impacColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[presentationColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[technicalDepthColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[overallRatingColumn].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string submissionNo = Request["SUBMISSIONNO"];
                if (GetSubmission(submissionNo))
                {
                    if (GetAuthors(submissionNo))
                    {
                        if (GetSubmissionReviewSummary(submissionNo))
                        {
                            pnlOverallSummary.Visible = true;
                        }
                    }
                }
            }
        }
    }
}