using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ConferenceWebsite.App_Code;
using static ConferenceWebsite.Global;

namespace ConferenceWebsite.PCChair
{
    public partial class AssignSubmissionToPCMember : System.Web.UI.Page
    {
        //**************************************
        // Uses TODO 15, 16, 17, 18, 19, 07(S) *
        //**************************************

        private ConferenceDBAccess myConferenceDB = new ConferenceDBAccess();
        private HelperMethods myHelpers = new HelperMethods();
        private SharedMethods mySharedMethods = new SharedMethods();

        /***** Private Methods *****/

        private void PopulateSubmissionNumbers()
        {
            //***************
            // Uses TODO 15 *
            //***************
            DataTable dtSubmissionNumbers = myConferenceDB.GetSubmissionNumbers();

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "SUBMISSIONNO" };

            // Determine if the query is valid.
            if (myHelpers.IsQueryResultValid("TODO 15", dtSubmissionNumbers, attributeList, lblResultMessage))
            {
                if (dtSubmissionNumbers.Rows.Count != 0)
                {
                    ddlSubmissionNumbers.DataSource = dtSubmissionNumbers;
                    ddlSubmissionNumbers.DataValueField = "SUBMISSIONNO";
                    ddlSubmissionNumbers.DataTextField = "SUBMISSIONNO";
                    ddlSubmissionNumbers.DataBind();
                    ddlSubmissionNumbers.Items.Insert(0, "Select");
                    ddlSubmissionNumbers.Items[0].Value = "none selected";
                }
                else // Nothing to display.
                {
                    myHelpers.DisplayMessage(lblResultMessage, "There are no submissions.");
                    pnlSelectSubmission.Visible = false;
                }
            }
        }

        private void PopulateCurrentlyAssigned(string submissionNo)
        {
            lblResultMessage.Visible = false;
            pnlCurrentlyAssigned.Visible = false;
            pnlAvailableForAssignment.Visible = false;

            // Uses TODO 07 in App_Code/SharedMethods.
            DataTable dtSubmission = mySharedMethods.GetSubmission(submissionNo, lblResultMessage);

            // Determine if the query is valid.
            if (dtSubmission != null)
            {
                if (dtSubmission.Rows.Count != 0)
                {
                    lblTitle.Text = "<b>Title: </b>" + dtSubmission.Rows[0]["TITLE"].ToString();
                    lblTitle.Visible = true;

                    //***************
                    // Uses TODO 16 *
                    //***************
                    DataTable dtCurrentlyAssigned = myConferenceDB.GetPCMembersAssignedToSubmission(submissionNo);

                    // Attributes expected to be returned by the query result.
                    var attributeList = new List<string> { "NAME", "PREFERENCE" };

                    // Determine if the query is valid.
                    if (myHelpers.IsQueryResultValid("TODO 16", dtCurrentlyAssigned, attributeList, lblResultMessage))
                    {
                        if (dtCurrentlyAssigned.Rows.Count != 0)
                        {
                            gvCurrentlyAssigned.DataSource = dtCurrentlyAssigned;
                            gvCurrentlyAssigned.DataBind();
                            ShowCurrentlyAssigned();
                        }
                        else // No one assigned to this submission.
                        {
                            myHelpers.DisplayMessage(lblCurrentlyAssignedResult, "There are no PC members assigned to this submission.");
                            pnlCurrentlyAssigned.Visible = true;
                            gvCurrentlyAssigned.Visible = false;
                        }
                    }
                }
                else // Cannot get submission title.
                {
                    myHelpers.DisplayMessage(lblResultMessage, myHelpers.EmptyResultForTODO("16"));
                }
            }
        }

        private void PopulateAvailableForAssignment(string submissionNo, string preference)
        {
            lblResultMessage.Visible = false;
            pnlAvailableForAssignment.Visible = false;
            DataTable dtAvailableForAssignment;
            string TODO;
            if (preference != "None")
            {
                //***************
                // Uses TODO 17 *
                //***************
                dtAvailableForAssignment = myConferenceDB.GetPCMemberAvailableForAssignmentWithSpecifiedPreference(submissionNo, preference);
                TODO = "17";
            }
            else
            {
                //***************
                // Uses TODO 18 *
                //***************
                dtAvailableForAssignment = myConferenceDB.GetPCMemberAvailableForAssignmentWithNoSpecifiedPreference(submissionNo);
                TODO = "18";
            }

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "PERSONID", "NAME", "PREFERENCE", "ANYNAME" };

            // Determine if the query is valid.
            if (myHelpers.IsQueryResultValid("TODO " +  TODO, dtAvailableForAssignment, attributeList, lblResultMessage))
            {
                if (dtAvailableForAssignment.Rows.Count != 0)
                {
                    gvAvailableForAssignment.DataSource = dtAvailableForAssignment;
                    gvAvailableForAssignment.DataBind();
                    lblAvailableForAssignmentResult.Visible = false;
                    pnlAvailableForAssignment.Visible = true;
                }
                else
                {
                    if (ddlMinimumPreference.SelectedValue == "None")
                    {
                        myHelpers.DisplayMessage(lblAvailableForAssignmentResult, "All PC members have specified a preference for this submission.");
                    }
                    else
                    {
                        myHelpers.DisplayMessage(lblAvailableForAssignmentResult, "There are no assignable PC members that have specified a preference >= "
                            + ddlMinimumPreference.SelectedValue + " for this submission.");
                    }
                }
            }
        }

        private void ShowCurrentlyAssigned()
        {
            lblCurrentlyAssignedResult.Visible = false;
            pnlCurrentlyAssigned.Visible = true;
            gvCurrentlyAssigned.Visible = true;
            ddlMinimumPreference.SelectedIndex = 0;
        }

        /***** Protected Methods *****/

        protected void BtnAssignPCMember_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string personId = "";
                int personIdColumn = myHelpers.GetColumnIndexByName(gvAvailableForAssignment, "PERSONID", lblResultMessage);
                if (personIdColumn != -1)
                {
                    string submissionNo = ddlSubmissionNumbers.SelectedValue;
                    // Determine if any pc member was selected for this submission.
                    foreach (GridViewRow row in gvAvailableForAssignment.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chkSelected") as CheckBox);
                            if (chkRow != null && chkRow.Checked)
                            {
                                // Get the PC members person id.
                                personId = row.Cells[personIdColumn].Text;
                                //***************
                                // Uses TODO 19 *
                                //***************
                                if (!myConferenceDB.AssignPCMemberToReviewSubmission(submissionNo, personId))
                                {
                                    myHelpers.DisplayMessage(lblResultMessage, sqlError);
                                    return;
                                }
                            }
                        }
                    }

                    // Show result message and refresh the web form.
                    if (personId != "")
                    {
                        PopulateCurrentlyAssigned(submissionNo);
                        ShowCurrentlyAssigned();
                        pnlAvailableForAssignment.Visible = false;
                    }
                    else
                    {
                        myHelpers.DisplayMessage(lblAssignmentResult, "No PC member was selected for assignment.");
                    }
                }
            }
        }

        protected void DdlMinimumPreference_SelectedIndexChanged(object sender, EventArgs e)
        {
            rfvDdlMinumumPreference.Visible = true;
            if (ddlMinimumPreference.SelectedIndex != 0)
            {
                PopulateAvailableForAssignment(ddlSubmissionNumbers.SelectedValue, ddlMinimumPreference.SelectedValue);
            }
            else
            {
                pnlAvailableForAssignment.Visible = false;
                lblResultMessage.Visible = false;
            }
        }

        protected void DdlSubmission_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubmissionNumbers.SelectedIndex != 0)
            {
                rfvDdlMinumumPreference.Visible = false;
                lblAvailableForAssignmentResult.Visible = false;
                PopulateCurrentlyAssigned(ddlSubmissionNumbers.SelectedValue);
            }
            else
            {
                lblTitle.Visible = false;
                pnlCurrentlyAssigned.Visible = false;
                pnlAvailableForAssignment.Visible = false;
            }
        }

        protected void GvAvailableForAssignment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 5)
            {
                int personIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PERSONID", lblResultMessage) + 1;
                int preferenceColumn = e.Row.Cells.Count - 2;
                int submissionsAssignedColumn = e.Row.Cells.Count - 1;
                if (personIdColumn != -1 && preferenceColumn >=0 && submissionsAssignedColumn >=0)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        e.Row.Cells[personIdColumn].Visible = false;
                        e.Row.Cells[preferenceColumn].Text = "PREFERENCE";
                        e.Row.Cells[submissionsAssignedColumn].Text = "SUBMISSIONS&nbsp;ASSIGNED";
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[personIdColumn].Visible = false;
                        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[preferenceColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[submissionsAssignedColumn].HorizontalAlign = HorizontalAlign.Center;
                        if (e.Row.Cells[preferenceColumn].Text == "&nbsp;")
                        { e.Row.Cells[preferenceColumn].Text = "&ndash;"; }
                    }
                }
            }
        }

        protected void GvCurrentlyAssigned_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 2)
            {
                int preferenceColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PREFERENCE", lblResultMessage);
                if (preferenceColumn != -1)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[preferenceColumn].HorizontalAlign = HorizontalAlign.Center;
                        if (e.Row.Cells[preferenceColumn].Text == "&nbsp;")
                        { e.Row.Cells[preferenceColumn].Text = "&ndash;"; }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PopulateSubmissionNumbers();
                pnlCurrentlyAssigned.Visible = false;
                pnlAvailableForAssignment.Visible = false;
            }
        }
    }
}