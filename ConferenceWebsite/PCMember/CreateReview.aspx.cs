using System;
using System.Data;
using ConferenceWebsite.App_Code;
using static ConferenceWebsite.Global;

namespace ConferenceWebsite.PCMember
{
    public partial class CreateReview : System.Web.UI.Page
    {
        //*****************************
        // Uses TODO 33, 07(S), 08(S) *
        //*****************************
        private ConferenceDBAccess myConferenceDB = new ConferenceDBAccess();
        private HelperMethods myHelpers = new HelperMethods();
        private SharedMethods mySharedMethods = new SharedMethods();

        /***** Private Methods *****/

        private bool GetAuthors(string submissionNo)
        {
            bool result = false;
            lblResultMessage.Visible = false;

            // Uses TODO 08 in App_Code\SharedMethods.cs.
            DataTable dtAuthors = mySharedMethods.GetSubmissionAuthors(submissionNo, lblResultMessage);

            // Determine if the query is valid.
            if (dtAuthors != null)
            {
                // There is an error in the query if the query result is empty.
                if (dtAuthors.Rows.Count != 0)
                {
                    // Set the submission authors for display.
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
                    // Set the submission title for display.
                    txtTitle.Text = dtSubmission.Rows[0]["TITLE"].ToString();
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

        protected void BtnCreateReview_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Collect the information required to add a review.
                string submissionNo = Request["SUBMISSIONNO"];
                string relevant = ddlRelevant.SelectedValue;
                string technicallyCorrect = ddlTechnicallyCorrect.SelectedValue;
                string lengthAndContent = ddlLengthAndContent.SelectedValue;
                string originality = ddlOriginality.SelectedValue;
                string impact = ddlImpact.SelectedValue;
                string presentation = ddlPresentation.SelectedValue;
                string technicalDepth = ddlTechnicalDepth.SelectedValue;
                string overallRating = ddlOverallRating.SelectedValue;
                string confidence = ddlConfidence.SelectedValue;
                string mainContribution = myHelpers.CleanInput(txtMainContributions.Text);
                string strongPoints = myHelpers.CleanInput(txtStrongPoints.Text);
                string weakPoints = myHelpers.CleanInput(txtWeakPoints.Text);
                string overallSummary = myHelpers.CleanInput(txtOverallSummary.Text);
                string detailedComments = myHelpers.CleanInput(txtDetailedComments.Text);
                string confidentialComments = myHelpers.CleanInput(txtConfidentialComments.Text);

                //***************
                // Uses TODO 33 *
                //***************
                if (myConferenceDB.CreateReview(submissionNo, loginPersonId, relevant, technicallyCorrect, lengthAndContent,
                    confidence, originality, impact, presentation, technicalDepth, overallRating, mainContribution, strongPoints,
                    weakPoints, overallSummary, detailedComments, confidentialComments))
                {
                    myHelpers.DisplayMessage(lblResultMessage, "Your review for submission " + submissionNo + " was successfully created.");
                    pnlSubmission.Visible = false;
                    pnlAuthors.Visible = false;
                    pnlReview.Visible = false;
                }
                else // An SQL error occurred.
                {
                    myHelpers.DisplayMessage(lblResultMessage, sqlError);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (GetSubmission(Request["SUBMISSIONNO"]))
                {
                    if (GetAuthors(Request["SUBMISSIONNO"]))
                    {
                        pnlReview.Visible = true;
                    }
                }
            }
        }
    }
}