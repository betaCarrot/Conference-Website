using System;
using System.Data;
using System.Collections.Generic;
using ConferenceWebsite.App_Code;
using static ConferenceWebsite.Global;


namespace ConferenceWebsite.PCMember
{
    public partial class DisplayReview : System.Web.UI.Page
    {
        //*****************************
        // Uses TODO 28, 07(S), 08(S) *
        //*****************************
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
            if (dtAuthors!= null)
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
        }

        private bool GetReview(string submissionNo, string personId)
        {
            bool result = false;

            //***************
            // Uses TODO 28 * 
            //***************
            DataTable dtReview = myConferenceDB.GetReview(submissionNo, personId);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "PERSONID", "SUBMISSIONNO", "RELEVANT", "TECHNICALLYCORRECT",
                "LENGTHANDCONTENT", "ORIGINALITY", "IMPACT", "PRESENTATION", "TECHNICALDEPTH", "OVERALLRATING",
                "CONFIDENCE", "MAINCONTRIBUTION", "STRONGPOINTS", "WEAKPOINTS", "OVERALLSUMMARY", "DETAILEDCOMMENTS",
                "CONFIDENTIALCOMMENTS" };

            // Determine if the query is valid.
            if (myHelpers.IsQueryResultValid("TODO 28", dtReview, attributeList, lblResultMessage))
            {
                if (dtReview.Rows.Count != 0)
                {
                    // Set the review information for display.
                    txtRelevant.Text = dtReview.Rows[0]["relevant"].ToString();
                    txtTechnicallyCorrect.Text = dtReview.Rows[0]["technicallyCorrect"].ToString();
                    txtLengthAndContent.Text = dtReview.Rows[0]["lengthAndContent"].ToString();
                    txtOriginality.Text = dtReview.Rows[0]["originality"].ToString();
                    txtImpact.Text = dtReview.Rows[0]["impact"].ToString();
                    txtPresentation.Text = dtReview.Rows[0]["presentation"].ToString();
                    txtTechnicalDepth.Text = dtReview.Rows[0]["technicalDepth"].ToString();
                    txtOverallRating.Text = dtReview.Rows[0]["overallRating"].ToString();
                    txtConfidence.Text = dtReview.Rows[0]["confidence"].ToString();
                    txtMainContributions.Text = dtReview.Rows[0]["mainContribution"].ToString();
                    txtStrongPoints.Text = dtReview.Rows[0]["strongPoints"].ToString();
                    txtWeakPoints.Text = dtReview.Rows[0]["weakPoints"].ToString();
                    txtOverallSummary.Text = dtReview.Rows[0]["overallSummary"].ToString();
                    txtDetailedComments.Text = dtReview.Rows[0]["detailedComments"].ToString();
                    txtConfidentialComments.Text = dtReview.Rows[0]["confidentialComments"].ToString();
                    result = true;
                }
                else // SQL error - no review.
                {
                    myHelpers.DisplayMessage(lblResultMessage, myHelpers.EmptyResultForTODO("28"));
                }
            }
            return result;
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
                    txtTitle.Text = dtSubmission.Rows[0]["TITLE"].ToString();
                    pnlSubmission.Visible = true;
                    result = true;
                }
                else // SQL error - no submission.
                {
                    myHelpers.DisplayMessage(lblResultMessage, myHelpers.EmptyResultForTODO("GetSubmission"));
                }
            }
            return result;
        }

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (GetSubmission(Request["SUBMISSIONNO"]))
                {
                    if (GetAuthors(Request["SUBMISSIONNO"]))
                    {
                        if (GetReview(Request["SUBMISSIONNO"], loginPersonId))
                        {
                            pnlReview.Visible = true;
                        }
                    }
                }
            }
        }
    }
}