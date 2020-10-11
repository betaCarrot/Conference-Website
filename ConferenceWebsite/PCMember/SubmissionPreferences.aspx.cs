using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ConferenceWebsite.App_Code;
using static ConferenceWebsite.Global;

namespace ConferenceWebsite.PCMember
{
    public partial class SubmissionPreferences : System.Web.UI.Page
    {
        //***********************
        // Uses TODO 24, 25, 32 *
        //***********************

        private ConferenceDBAccess myConferenceDB = new ConferenceDBAccess();
        private HelperMethods myHelpers = new HelperMethods();

        /***** Private Methods *****/

        private bool DisplaySubmissionsWithPreference(string personId)
        {
            pnlPreferencesSpecified.Visible = false;
            pnlPreferencesNotSpecified.Visible = false;
            bool result = true;
            //***************
            // Uses TODO 24 *
            //***************
            DataTable dtSubmissionPreferences = myConferenceDB.GetSubmissionsWithPreference(personId);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "PREFERENCE", "SUBMISSIONNO", "TITLE", "ABSTRACT", "SUBMISSIONTYPE" };

            // Determine if the query is valid.
            if (myHelpers.IsQueryResultValid("TODO 24", dtSubmissionPreferences, attributeList, lblResultMessage))
            {
                if (dtSubmissionPreferences.Rows.Count != 0)
                {
                    gvPreferenceSpecified.DataSource = dtSubmissionPreferences;
                    gvPreferenceSpecified.DataBind();
                    gvPreferenceSpecified.Visible = true;
                }
                else // Nothing to display.
                {
                    myHelpers.DisplayMessage(lblResultWithPreferenceMessage, "You have not specified a preference for any submission.");
                    gvPreferenceSpecified.Visible = false;
                }
                pnlPreferencesSpecified.Visible = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        private void DisplaySubmissionsWithNoPreference(string personId)
        {
            //***************
            // Uses TODO 25 *
            //***************
            DataTable dtSubmissionPreferences = myConferenceDB.GetSubmissionsWithNoPreference(personId);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "SUBMISSIONNO", "TITLE", "ABSTRACT", "SUBMISSIONTYPE" };

            // Determine if the query is valid.
            if (myHelpers.IsQueryResultValid("TODO 25", dtSubmissionPreferences, attributeList, lblResultMessage))
            {
                if (dtSubmissionPreferences.Rows.Count != 0)
                {
                    gvNoPreferenceSpecified.DataSource = dtSubmissionPreferences;
                    gvNoPreferenceSpecified.DataBind();
                    pnlNoPreferenceSpecifiedResult.Visible = true;
                }
                else // Nothing to display.
                {
                    myHelpers.DisplayMessage(lblResultWithNoPreferenceMessage, "You have specified a preference for all submissions.");
                    pnlNoPreferenceSpecifiedResult.Visible = false;
                }
                pnlPreferencesNotSpecified.Visible = true;
            }
        }

        private void LoadPapers()
        {
            lblResultWithNoPreferenceMessage.Visible = false;
            lblResultWithPreferenceMessage.Visible = false;
            lblUpdateMessage.Visible = false;
            if (DisplaySubmissionsWithPreference(loginPersonId))
            {
                DisplaySubmissionsWithNoPreference(loginPersonId);
            }
        }

        /***** Protected Methods *****/

        protected void BtnUpdatePreferences_Click(object sender, EventArgs e)
        {
            bool isSubmssionSelected = false;
            int submissionNoColumn = myHelpers.GetColumnIndexByName(gvNoPreferenceSpecified, "SUBMISSION", lblResultMessage);
            if (submissionNoColumn != -1)
            {
                // For each submission for which a preference has been specified, get the preference and insert it into the database.
                for (int i = 0; i < gvNoPreferenceSpecified.Rows.Count; i++)
                {
                    DropDownList listPreference = (DropDownList)gvNoPreferenceSpecified.Rows[i].FindControl("ddlPreference");
                    string submissionNo = gvNoPreferenceSpecified.Rows[i].Cells[submissionNoColumn].Text;
                    if (listPreference.SelectedIndex != 0)
                    {
                        isSubmssionSelected = true;
                        string preference = listPreference.SelectedItem.Value;

                        //***************
                        // Uses TODO 32 *
                        //***************
                        if (!myConferenceDB.CreatePreferenceForSubmission(submissionNo, loginPersonId, preference))
                        {
                            // An SQL error occurred.
                            myHelpers.DisplayMessage(lblResultMessage, sqlError);
                            return;
                        }
                    }
                }
                // Determine if any preference was updated.
                if (isSubmssionSelected)
                {
                    LoadPapers();
                    myHelpers.DisplayMessage(lblResultWithPreferenceMessage, "Your preferences have been updated.");
                }
                else
                {
                    myHelpers.DisplayMessage(lblUpdateMessage, "You have not selected any preference.");
                    lblUpdateMessage.Visible = true;
                }
            }
        }

        protected void GvNoPreferenceSpecified_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 5)
            {
                int submissionNoColumn = myHelpers.GetGridViewColumnIndexByName(sender, "SUBMISSIONNO", lblResultMessage) + 1;
                if (submissionNoColumn != -1)
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

        protected void GvPreferenceSpecified_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 5)
            {
                int submissionNoColumn = myHelpers.GetGridViewColumnIndexByName(sender, "SUBMISSIONNO", lblResultMessage);
                int preferenceColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PREFERENCE", lblResultMessage);
                if (submissionNoColumn != -1 && preferenceColumn != -1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "SUBMISSIONNO", "SUBMISSION");
                        myHelpers.RenameGridViewColumn(e, "SUBMISSIONTYPE", "TYPE");
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[submissionNoColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[preferenceColumn].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPapers();
            }
        }
    }
}