using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;


namespace ConferenceWebsite.App_Code
{
    /// <summary>
    /// Project specific methods.
    /// </summary>

    public class SharedMethods
    {
        //***********************
        // Uses TODO 01, 07, 08 *
        //***********************

        private ConferenceDBAccess myConferenceDB = new ConferenceDBAccess();
        private HelperMethods myHelpers = new HelperMethods();

        public DataTable GetPerson(string personId, Label labelControl)
        {
            //***************
            // Uses TODO 01 *
            //***************
            DataTable dtPerson = myConferenceDB.GetPerson(personId);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "PERSONID", "USERNAME", "TITLE", "NAME", "INSTITUTION", "COUNTRY", "EMAIL" };

            // Display the query result if it is valid.
            if (myHelpers.IsQueryResultValid("TODO 01", dtPerson, attributeList, labelControl))
            {
                return dtPerson;
            }
            else // An SQL error occurred.
            {
                return null;
            }
        }

        public string GetPersonId(string email, Label resultMessage)
        {
            string personId = null;

            DataTable dtPerson = myConferenceDB.GetPersonId(email);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "PERSONID" };

            // Determine if the query is valid.
            if (myHelpers.IsQueryResultValid("GetPersonId", dtPerson, attributeList, resultMessage))
            {
                if (dtPerson.Rows.Count != 0)
                {
                    // The person already exists in the database.
                    personId = dtPerson.Rows[0]["PERSONID"].ToString();
                }
                else
                {
                    // The person does not exist in the database.
                    personId = "";
                }
            }
            return personId;
        }

        public DataTable GetSubmission(string submissionNo, Label labelControl)
        {
            //***************
            // Uses TODO 07 *
            //***************
            DataTable dtSubmission = myConferenceDB.GetSubmission(submissionNo);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "SUBMISSIONNO", "TITLE", "ABSTRACT", "SUBMISSIONTYPE", "STATUS", "CONTACTAUTHOR" };

            // Determine if the query is valid.
            if (myHelpers.IsQueryResultValid("TODO 07", dtSubmission, attributeList, labelControl))
            {
                return dtSubmission;
            }
            else //An SQL error occurred.
            {
                return null;
            }
        }

        public DataTable GetSubmissionAuthors(string submissionNo, Label labelControl)
        {
            //***************
            // Uses TODO 08 *
            //***************
            DataTable dtAuthors = myConferenceDB.GetSubmissionAuthors(submissionNo);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "PERSONID", "TITLE", "NAME", "INSTITUTION", "COUNTRY", "EMAIL" };

            // Determine if the query is valid.
            if (myHelpers.IsQueryResultValid("TODO 08", dtAuthors, attributeList, labelControl))
            {
                return dtAuthors;
            }
            else //An SQL error occurred.
            {
                return null;
            }
        }
    }
}