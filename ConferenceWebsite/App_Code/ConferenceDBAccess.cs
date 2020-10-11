using System.Data;
using Oracle.DataAccess.Client;

namespace ConferenceWebsite.App_Code
{
    /// <summary>
    /// Student name: REN, Zhengtong
    /// Student id:
    /// </summary>

    public class ConferenceDBAccess
    {
        //******************************** IMPORTANT NOTE **********************************
        // For the web pages to display a query result correctly, the attribute names in   *
        // the query result table must be EXACTLY the same as that in the database tables. *
        //         Report problems with the website code to 3311rep@cse.ust.hk.            *
        //**********************************************************************************

        private readonly OracleDBAccess myOracleDBAccess = new OracleDBAccess();
        private string sql;

        #region TODOs

        #region SQL statements for Person

        public DataTable GetPerson(string personId)
        {
            //****************************************************
            // TODO 01: Used in App_Code\SharedMethods\GetPerson *
            // Construct an SQL SELECT statement to retrieve     *
            // a Person record identified by a person id.        *
            //****************************************************
            sql = "select * " +
                  "from Person " +
                  "where personId = " + personId;
            return myOracleDBAccess.GetData("TODO 01", sql);
        }

        public bool CreatePerson(string personId, string username, string title, string name,
            string institution, string country, string email)
        {
            //***************************************************************
            // TODO 02: Used in Author\CreateSubmission.aspx.cs,            *
            //          Author\EditSubmission.aspx,                         *
            //          PCChair\AddPCMember.aspx.cs                         *
            // Construct an SQL INSERT statement to insert a Person record. *
            //***************************************************************
            sql = "insert into Person values(" + personId +", '"+username+ "', '" +title+ "', '" +name+ "', '" +institution+ "', '" +country+ "', '" +email+"')";
            return UpdateData("TODO 02", sql, null);
        }

        public bool RemovePerson(string personId, OracleTransaction trans)
        {
            //************************************************
            // TODO 03: Used in Author\CreateSubmission.aspx *
            // Construct an SQL DELETE statement to remove a *
            // Person record identified by a person id.      *
            //************************************************
            sql = "delete from Person where personId = " + personId;
            return UpdateData("TODO 03", sql, trans);
        }

        public bool UpdatePerson(string personId, string title, string name, string institution, string country)
        {
            //**********************************************
            // TODO 04: Used in Account\Manage.aspx.cs     *
            // Construct an SQL UPDATE statement to update *
            // the title, name, institution and country of * 
            // a Person record identified by a person id.  *
            //**********************************************
            sql = "update Person set title='"+title+"', name='"+name+"',institution='"+institution+"',country='"+country+"' where personId="+personId;
            return UpdateData("TODO 04", sql, null);
        }

        public bool UpdatePersonEmail(string personId, string email)
        {
            //******************************************************
            // TODO 05: Used in Account\Manage.aspx.cs             *
            // Construct an SQL UPDATE statement to update the     *
            // email of a Person record identified by a person id. *
            //******************************************************
            sql = "update Person set email='"+email+"' where personId="+personId;
            return UpdateData("TODO 05", sql, null);
        }
        
        public bool SetUsername(string personId, string username)
        {
            //***************************************************
            // TODO 06: Used in AddPCMember.aspx.cs             *
            // Construct an SQL UPDATE statement to update the *
            // username of a person identified by a person id.  *
            //***************************************************
            sql = "update Person set username='" + username + "' where personId=" + personId;
            return UpdateData("TODO 06", sql, null);
        }

        #endregion SQL Statements for Person

        #region SQL statements for Author Functions

        public DataTable GetSubmission(string submissionNo)
        {
            //********************************************************
            // TODO 07: Used in App_Code\SharedMethods\GetSubmission *
            // Construct an SQL SELECT statement to retrieve a       *
            // submission identified by a submission number.         *
            //********************************************************
            sql = "select * " +
                  "from Submission "+
                  "where submissionNo="+submissionNo;
            return myOracleDBAccess.GetData("TODO 07", sql);
        }

        public DataTable GetSubmissionAuthors(string submissionNo)
        {
            //***************************************************************
            // TODO 08: Used in App_Code\SharedMethods\GetSubmissionAuthors *
            // Construct an SQL SELECT statement to retrieve the person id, *
            // title, name, institution, country and email for all authors  *
            // of a submission identified by a submission number.           *
            //***************************************************************
            sql = "select P.personId, P.title, P.name, P.institution, P.country, P.email " +
                  "from Person P, Submission S, AuthorOf A "+
                  "where P.personId = A.personId and A.submissionNo = S.submissionNo and S.submissionNo="+submissionNo;
            return myOracleDBAccess.GetData("TODO 08", sql);
        }

        public DataTable GetSubmissionsForAuthor(string personId)
        {
            //**************************************************************
            // TODO 09: Used in Author\DisplaySubmissions.aspx.cs          *
            // Construct an SQL SELECT statement to retrieve all the       *
            // submissions on which a person, identified by his/her person * 
            // id, is an author. Order the result by submission number.    *
            //**************************************************************
            sql = "select submissionNo, title, abstract, submissionType, status, contactAuthor "+
                  "from Submission natural join AuthorOf "+
                  "where personId="+personId;
            return myOracleDBAccess.GetData("TODO 09", sql);
        }

        public bool CreateSubmission(string submissionNo, string title, string submissionAbstract,
            string submissionType, string status, string contactAuthor, OracleTransaction trans)
        {
            //***********************************************************************
            // TODO 10: Used in Author\CreateSubmission.aspx.cs                     *
            // Construct an SQL INSERT statement to create a new submission record. *
            //***********************************************************************
            sql = "insert into Submission values("+submissionNo+", '"+title+"', '"+submissionAbstract+ "', '"+submissionType+ "', '"+status+"', "+contactAuthor+")";
            return UpdateData("TODO 10", sql, trans);
        }

        public bool UpdateSubmission(string submissionNo, string title, string submissionAbstract, string submissionType)
        {
            //**********************************************************************
            // TODO 11: Used in Author\EditSubmission.aspx.cs                      *
            // Construct an SQL UPDATE statement to update the title, abstract and *
            // submission type of a submission identified by a submission number.  *
            //**********************************************************************
            sql = "update Submission set title='"+title+"', abstract='"+submissionAbstract+"', submissionType='"+submissionType+"' where submissionNo="+submissionNo;
            return UpdateData("TODO 11", sql, null);
        }

        public bool UpdateSubmissionStatus(string submissionNo, string status)
        {
            //*********************************************************
            // TODO 12: Used in Author\DisplaySubmission.aspx.cs      *
            // Construct an SQL UPDATE statement to update the status *
            // of a submission identified by a submission number.     *
            //*********************************************************
            sql = "update Submission set status='"+status+"' where submissionNo="+submissionNo;
            return UpdateData("TODO 12", sql, null);
        }

        public bool AddAuthorOf(string submissionNo, string personId, OracleTransaction trans)
        {
            //********************************************************************
            // TODO 13: Used in Author\CreateSubmission.aspx.cs                  *
            //          Author\EditSubmission.aspx.cs                            *
            // Construct an SQL INSERT statement to add an author, identified by *
            // a person id, to a submission, identified by a submission number.  *
            //********************************************************************
            sql = "insert into AuthorOf values ("+submissionNo+", "+personId+")";
            return UpdateData("TODO 13", sql, trans);
        }

        public bool RemoveAuthorOf(string submissionNo, string personId, OracleTransaction trans)
        {
            //***********************************************************************
            // TODO 14: Used in EditSubmission.aspx.cs                              *
            // Construct an SQL DELETE statement to remove an author, identified by *
            // a person id, from a submission, identified by a submission number.   *
            //***********************************************************************
            sql = "delete from AuthorOf where submissionNo="+submissionNo+" and personId="+personId;
            return UpdateData("TODO 14", sql, trans);
        }

        #endregion SQL statements for Author Functions

        #region SQL statements for PC Chair Functions

        public DataTable GetSubmissionNumbers()
        {
            //*********************************************************************
            // TODO 15: Used in PCChair\AssignSubmissionToPCMember.aspx.cs        *
            // Construct an SQL SELECT statement to retrieve the submission       *
            // numbers of all submissions available for assignment to PC members. *
            //*********************************************************************
            sql = "select submissionNo "+
                  "from Submission "+
                  "where status is null";
            return myOracleDBAccess.GetData("TODO 15", sql);
        }

        public DataTable GetPCMembersAssignedToSubmission(string submissionNo)
        {
            //************************************************************************************
            // TODO 16: Used in PCChair\AssignSubmissionToPCMember.aspx.cs                       *
            // Construct an SQL SELECT statement to retrieve, for a submission identified by     *
            // a submission number, the names of the PC members and their preference for the     *
            // submission only for those PC members that are already assigned to the submission. *
            // If an assigned PC member has not indicated a preference for the submission,       *
            // then the preference value returned should be null. Order the result by name.      *
            //************************************************************************************
            sql = "select name, preference "+
                  "from (select * from PCMember natural join Assigned natural left outer join Prefers) A, Person P " +
                  "where A.personId = P.personId and A.submissionNo = "+submissionNo+" "+
                  "order by P.name";
            return myOracleDBAccess.GetData("TODO 16", sql);
        }

        public DataTable GetPCMemberAvailableForAssignmentWithSpecifiedPreference(string submissionNo, string preference)
        {
            //**********************************************************************************************
            // TODO 17: Used in PCChair\AssignSubmissionToPCMember.aspx.cs                                 *
            // Construct an SQL SELECT statement to retrieve, for a submission identified by a submission  *
            // number, those PC members available for assignment to the submission who have indicated a    *
            // preference for the submission greater than or equal to a specified preference. For each PC  *
            // member retrieve the person id, name, preference for the submission and the number of        *
            // submissions to which the PC member is already assigned. Order the result by name ascending. *
            //**********************************************************************************************
            sql = "with result(personId, cnt) as(select personId, count(submissionNo) from PCMember natural left outer join Assigned group by personId) " +
                  "select M.personId, P.name, F.preference, R.cnt " +
                  "from PCMember M, Prefers F, Submission S, Person P, result R " +
                  "where M.personId = R.personId and M.personId = P.personId and M.personId = F.personId and F.submissionNo = S.submissionNo and S.submissionNo=" + submissionNo + " and F.preference>=" + preference + " and M.personId not in (select personId from Assigned where submissionNo=" + submissionNo + ") " +
                  "order by P.name asc";
            return myOracleDBAccess.GetData("TODO 17", sql);
        }

        public DataTable GetPCMemberAvailableForAssignmentWithNoSpecifiedPreference(string submissionNo)
        {
            //**********************************************************************************************
            // TODO 18: Used in PCChair\AssignSubmissionToPCMember.aspx.cs                                 *
            // Construct an SQL SELECT statement to retrieve, for a submission identified by a submission  *
            // number, those PC members available for assignment to the submission who have not indicated  *
            // any preference for the submission. For each PC member retrieve the person id, name, and the *
            // preference for the submission, set to null, and the number of submissions to which the PC   *
            // member is already assigned. Order the result by name ascending.                             *
            //**********************************************************************************************
            sql = "with result(personId, cnt) as(select personId, count(submissionNo) from PCMember natural left outer join Assigned group by personId) " +
                  "select M.personId, P.name, null preference, R.cnt " +
                  "from PCMember M, Submission S, Person P, result R " +
                  "where M.personId = R.personId and M.personId = P.personId and S.submissionNo=" + submissionNo + " and M.personId not in (select personId from Assigned where submissionNo=" + submissionNo + ") and M.personId not in (select personId from Prefers where submissionNo=" + submissionNo + ") and M.personId not in (select personId from AuthorOf where submissionNo=" + submissionNo + ") " +
                  "order by P.name asc";
            return myOracleDBAccess.GetData("TODO 18", sql);
        }

        public bool AssignPCMemberToReviewSubmission(string submissionNo, string personId)
        {
            //**********************************************************************************
            // TODO 19: Used in PCChair\AssignSubmissionToPCMember.aspx.cs                     *
            // Construct an SQL INSERT statement to assign a PC member to review a submission. *
            //**********************************************************************************
            sql = "insert into Assigned values ("+submissionNo+", "+personId+")";
            return UpdateData("TODO 19", sql, null);
        }

        public DataTable GetAllPCMembers()
        {
            //******************************************************************************
            // TODO 20: Used in PCChair\DisplayPCMember.aspx.cs                            *
            // Construct an SQL SELECT statement to retrieve the title, name, institution, *
            // country and email of all PC members. Order the result by name ascending.    *
            //*****************************************************
            sql = "select title, name, institution, country, email " +
                "from Person natural join PCMember order by name asc";
            return myOracleDBAccess.GetData("TODO 20", sql);
        }

        public bool AddPCMember(string personId)
        {
            //************************************************************
            // TODO 21: Used in PCChair\AddPCMember.aspx.cs              *
            // Construct an SQL INSeRT statement to add a new PC member. *
            //************************************************************
            sql = "insert into PCMember values ("+personId+")";
            return UpdateData("TODO 21", sql, null);
        }

        public DataTable GetSubmissionStatistics()
        {
            //**********************************************************************
            // TODO 22: Used in PCChair\DisplaySubmissionStatistics.aspx.cs        *
            // Construct an SQL SELECT statement to retrieve, for all submissions, *
            // submission number, title, type, contact author name, number of      *
            // authors, number of PC members assigned to review the submission,    *
            // number of reviewers that have completed their review, and status.   *
            // Order the result by submission number ascending.                    *
            //**********************************************************************
            sql = "with result1(submissionNo, cnt1) as (select submissionNo, count(personId) from Submission natural join AuthorOf group by submissionNo), result2(submissionNo, cnt2) as (select submissionNo, count(personId) from Submission natural left outer join Assigned group by submissionNo), result3(submissionNo, cnt3) as (select submissionNo, count(personId) from Submission natural left outer join Review group by submissionNo) "+
                  "select S.submissionNo, S.title, S.submissionType, P.name, R1.cnt1, R2.cnt2, R3.cnt3, S.status " +
                  "from Submission S, result1 R1, result2 R2, result3 R3, Person P "+
                  "where S.submissionNo = R1.submissionNo and S.submissionNo = R2.submissionNo and S.submissionNo = R3.submissionNo and S.contactAuthor = P.personId "+
                  "order by S.submissionNo asc";
            return myOracleDBAccess.GetData("TODO 22", sql);
        }

        public DataTable GetReviewingStatistics()
        {
            //******************************************************************************
            // TODO 23: Used in PCChair\DisplayReviewingStatistics.aspx.cs                 *
            // Construct an SQL SELECT statement to retrieve for each PC member the name,  *
            // number of submissions assigned, number of submissions reviewd and number of *
            // submissions not yet reviewed. Order the result by PC member name ascending. *
            //******************************************************************************
            sql = "with result1(personId, cnt1) as (select personId, count(submissionNo) from PCMember natural left outer join Assigned group by personId), result2(personId, cnt2) as (select personId, count(submissionNo) from PCMember natural left outer join Review group by personId) " +
                  "select P.name, R1.cnt1, R2.cnt2, R1.cnt1-R2.cnt2 " +
                  "from PCMember M, Person P, result1 R1, result2 R2 " +
                  "where M.personId = P.personId and M.personId = R1.personId and M.personId = R2.personId " +
                  "order by P.name asc";
            return myOracleDBAccess.GetData("TODO 23", sql);
        }

        #endregion SQL statements for PC Chair Functions

        #region SQL statements for PC Member Functions

        public DataTable GetSubmissionsWithPreference(string personId)
        {
            //****************************************************************************
            // TODO 24: Used in PCMember\DisplaySubmissionPreferences.aspx.cs            *
            // Construct an SQL SELECT statement to retrieve the preference, submission  *
            // number, title, abstract and submission type for those submissions for     *
            // which a PC member, identified by a person id, has specified a preference. *
            // Order the result by preference ascending, submission number ascending.    *
            //****************************************************************************
            sql = "select preference, submissionNo, title, abstract, submissionType "+
                  "from PCMember natural join Prefers natural join Submission "+
                  "where (status is null or status = 'accept' or status = 'reject') and personId="+personId+" "+
                  "order by preference asc, submissionNo asc";
            return myOracleDBAccess.GetData("TODO 24", sql);
        }

        public DataTable GetSubmissionsWithNoPreference(string personId)
        {
            //************************************************************************************
            // TODO 25: Used in PCMember\DisplaySubmissionPreferences.aspx.cs                    *
            // Construct an SQL SELECT statement to retrieve the submission number, title,       *
            // abstract and submission type for those submissions for which a PC member,         *
            // identified by a person id, has not specified a preference and for which he/she    *
            // is able to specify a preference. Order the result by submission number ascending. *
            //************************************************************************************
            sql = "select submissionNo, title, abstract, submissionType " +
                  "from Submission " +
                  "where status is null and submissionNo not in (select submissionNo from Prefers where personId = " + personId + ") and submissionNo not in (select submissionNo from AuthorOf where personId = " + personId + ") and submissionNo not in (select submissionNo from AuthorOf where personId = " + personId + ")" +
                  "order by submissionNo asc";
            return myOracleDBAccess.GetData("TODO 25", sql);
        }

        public DataTable GetAssignedSubmissionsReviewed(string personId)
        {
            //****************************************************************
            // TODO 26: Used in PCMember\ReviewingAssignments.aspx.cs      *
            // Construct an SQL SELECT statement to retrieve the submission  *
            // number, title, submission type and status for the submissions *
            // that have been assigned for review to a PC member, identified *
            // by a person id, and for which the PC member has submitted a   *
            // review. Order the result by submission number ascending.      *
            //****************************************************************

            sql = "select submissionNo, title, submissionType, status "+
                  "from Submission natural join Review natural join PCMember "+
                  "where (status is null or status = 'accept' or status = 'reject') and personId = " + personId+" "+
                  "order by submissionNo asc";
            return myOracleDBAccess.GetData("TODO 26", sql);
        }

        public DataTable GetAssignedSubmissionsNotReviewed(string personId)
        {
            //***********************************************************************
            // TODO 27: Used in PCMember\ReviewingAssignments.aspx.cs               *
            // Construct an SQL SELECT statement to retrieve the submission number, *
            // title and submission type for the submissions that have been         *
            // assigned for review to a PC member, identified by a person id,       *
            // which require a review and for which the PC member has not submitted *
            // a review. Order the result by submission number ascending.           *
            //***********************************************************************

            sql = "select submissionNo, title, submissionType "+
                  "from Submission "+
                  "where status is null and submissionNo in (select submissionNo from Assigned where personId = "+personId+") and submissionNo not in (select submissionNo from Review where personId = "+personId+") "+
                  "order by submissionNo asc";
            return myOracleDBAccess.GetData("TODO 27", sql);
        }

        public DataTable GetReview(string submissionNo, string personId)
        {
            //*********************************************************
            // TODO 28: Used in PCMember\DisplayReview.aspx.cs        *
            // Construct an SQL SELECT statement to retrieve a review * 
            // for a submission, identified by a submission number,   *
            // by a PC member, identified by a person id.             *
            //*********************************************************
            sql = "select * "+
                  "from Review "+
                  "where submissionNo = "+submissionNo+" and personId = "+personId;
            return myOracleDBAccess.GetData("TODO 28", sql);
        }

        public DataTable GetDiscussion(string submissionNo)
        {
            //********************************************************************
            // TODO 29: Used in PCMember\DiscussReview.aspx.cs                   *
            // Construct an SQL SELECT statement to retrieve the PC member names *
            // and their discussion comments for a submission identified by a    *
            // submission number. Order the comments from earliest to latest.    *
            //********************************************************************
            sql = "select name, comments "+
                  "from PCMember natural join Person natural join Discussion "+
                  "where submissionNo = "+submissionNo+" "+
                  "order by sequenceNo asc";
            return myOracleDBAccess.GetData("TODO 29", sql);
        }

        public decimal GetMaxSequenceNoForSubmission(string submissionNo)
        {
            //******************************************************************************
            // TODO 30: Used in PCMember/DiscussReview.aspx.cs                             *
            // Construct an SQL SELECT statement to retrieve the current maximum sequence  *
            // number for the discssion of a submission identified by a submission number. *
            //******************************************************************************
            sql = "select max(sequenceNo) "+
                  "from Discussion "+
                  "where submissionNo = "+submissionNo;
            return myOracleDBAccess.GetAggregateValue("TODO 30", sql);
        }

        public DataTable GetSubmissionReviewSummary(string submissionNo)
        {
            //********************************************************************************* 
            // TODO 31: Used in  PCMember\DiscussReview.aspx.cs                               *
            // Construct an SQL SELECT statement to retrieve the PC member names and their    *
            // relevant, technically correct, length and content, confidence, originality,    *
            // impact, presentation, technical depth, overall rating and overall summary      *
            // values of all the reviews for a submission, identified by a submission number. *
            //*********************************************************************************
            sql = "select name, relevant, technicallyCorrect, lengthAndContent, confidence, originality, impact, presentation, technicalDepth, overallRating, overallSummary " +
                  "from PCMember natural join Person natural join Review "+
                  "where submissionNo = "+submissionNo;
            return myOracleDBAccess.GetData("TODO 31", sql);
        }

        public bool CreatePreferenceForSubmission(string submissionNo, string personId, string preference)
        {
            //**********************************************************************************
            // TODO 32: Used in PCMember\SubmissionPreferences.aspx.cs                         *
            // Construct an SQL INSERT statement to add a preference for a PC member,          *
            // identified by a person id, for a submission, identified by a submission number. *
            //**********************************************************************************
            sql = "insert into Prefers values ("+submissionNo+", "+personId+", "+preference+")";
            return UpdateData("TODO 32", sql, null);
        }

        public bool CreateReview(string submissionNo, string personId, string relevant,
            string technicallyCorrect, string lengthAndContent, string confidence,
            string originality, string impact, string presentation, string technicalDepth,
            string overallRating, string mainContribution, string strongPoints,
            string weakPoints, string overallSummary, string detailedComments,
            string confidentialComments)
        {
            //********************************************************************************
            // TODO 33: Used in PCMember\CreateReview.aspx.cs                                *
            // Construct an SQL INSERT statement to add a review for a PC member, identified *
            // by a person id, for a submission, identified by a submission number.          *
            //********************************************************************************
            sql = "insert into Review values ("+submissionNo+", "+personId+", '"+relevant+"', '"+technicallyCorrect+ "', '"+lengthAndContent+ "', "+confidence+", "+originality+", "+impact+", "+presentation+", "+technicalDepth+", "+overallRating+", '"+mainContribution+"', '"+strongPoints+"', '"+weakPoints+ "', '"+overallSummary+ "', '"+detailedComments+ "', '"+confidentialComments+"')";
            return UpdateData("TODO 33", sql, null);
        }

        public bool CreateDiscusssion(string sequenceNumber, string submissionNo, string personId, string comments)
        {
            //**********************************************************************************
            // TODO 34: Used in PCMember\DiscussReview.aspx.cs                                 *
            // Construct an SQL INSERT statement to add discussion comments for a submission,  *
            // identified by a submission number, from a PC member, identified by a person id. *
            //**********************************************************************************
            sql = "insert into Discussion values ("+sequenceNumber+", "+submissionNo+", "+personId+", '"+comments+"')";
            return UpdateData("TODO 34", sql, null);
        }

        #endregion SQL statements for PC Member Functions

        #endregion TODOS
        /*---------------------------------END OF TODOS---------------------------------*/

        #region *** DO NOT CHANGE THE METHODS BELOW THIS LINE. THEY ARE NOT TODOS!!! ***!

        public bool CreateNewSubmission(string submissionNo, string submissionTitle, string submissionAbstract,
            string submissionType, string status, string contactAuthor, DataTable dtAuthorsOf)
        {
            // First, create an Oracle transaction.
            OracleTransaction trans = myOracleDBAccess.BeginTransaction("CreateNewSubmission");
            if (trans == null) { return false; }

            // Second, create the Submission record.
            //***************
            // Uses TODO 10 *
            //***************
            if (!CreateSubmission(submissionNo, submissionTitle, submissionAbstract,
                submissionType, status, contactAuthor, trans))
            { return false; }

            // Third, create the AuthorOf records.
            foreach (DataRow row in dtAuthorsOf.Rows)
            {
                //***************
                // Uses TODO 13 *
                //***************
                if (!AddAuthorOf(submissionNo, row["PERSONID"].ToString(), trans))
                { return false; }
            }
            // Finally, commit the transaction.
            myOracleDBAccess.CommitTransaction("CreateNewSubmission", trans);
            return true;
        }

        public string CreateRegisteredPerson(string username, string title, string name,
            string institution, string country, string email)
        {
            string personId = (GetMaxTableId("PERSON", "PERSONID") + 1).ToString();
            if (personId != "0")
            {
                sql = "insert into Person values (" + personId + ",'" + username + "','" + title + "','" +
                name + "','" + institution + "','" + country + "','" + email + "')";
                if (!UpdateData("CreateRegisteredPerson", sql, null)) { personId = "0"; }
            }
            return personId;
        }

        public bool DeleteSubmissionAuthor(string submissionNo, string personId)
        {
            // First, create an Oracle transaction.
            OracleTransaction trans = myOracleDBAccess.BeginTransaction("DeleteSubmissionAuthor");
            if (trans == null) { return false; }

            // Second, delete the AuthorOf record. 
            //***************
            // Uses TODO 14 *
            //***************
            if (!RemoveAuthorOf(submissionNo, personId, trans)) { return false; }

            // Third, delete the Person record if the person is not an author or a PC member.
            //***************
            // Uses TODO 03 *
            //***************
            decimal isAuthor = IsPersonInRole(personId, "AuthorOf");
            decimal isPCMember = IsPersonInRole(personId, "PCMember");
            if (isAuthor == -1 || isPCMember == -1) { return false; }
            if (isAuthor == 0 && isPCMember == 0)
            {
                if (!RemovePerson(personId, trans)) { return false; }
            }

            // Finally, commit the transaction.
            myOracleDBAccess.CommitTransaction("DeleteSubmissionAuthor", trans);
            return true;
        }

        public DataTable GetEmailFromUsername(string username)
        {
            sql = "select email from Person where username='" + username + "'";
            return myOracleDBAccess.GetData("GetEmailFromUsername", sql);
        }

        public decimal GetMaxTableId(string tableName, string tableId)
        {
            sql = "select max(" + tableId + ") from " + tableName;
            return myOracleDBAccess.GetAggregateValue("GetMaxTableId", sql);
        }

        public DataTable GetPersonId(string email)
        {
            sql = "select personId from Person where email='" + email + "'";
            return myOracleDBAccess.GetData("GetPersonId", sql);
        }

        public decimal IsAttributeValueUnique(string tableName, string attributeName, string attributeValue)
        {
            sql = "select count(*) from " + tableName + " where " + attributeName + "='" + attributeValue + "'";
            return myOracleDBAccess.GetAggregateValue("IsAttributeValueUnique", sql);
        }

        public decimal IsPersonInRole(string personId, string tableName)
        {
            sql = "select count(*) from " + tableName + " where personId=" + personId;
            return myOracleDBAccess.GetAggregateValue("IsPersonInRole", sql);
        }

        private bool UpdateData(string TODO, string sql, OracleTransaction trans)
        {
            // If trans is null, then this is a single statement transaction.
            if (trans == null)
            {
                trans = myOracleDBAccess.BeginTransaction(TODO);
                if (trans == null) { return false; }  // Error creating the transaction.
                if (myOracleDBAccess.SetData(TODO, sql, trans))
                { myOracleDBAccess.CommitTransaction(TODO, trans); return true; } // The update succeeded.
                else
                { myOracleDBAccess.DisposeTransaction(TODO, trans); return false; } // The update failed.
            }
            else // This is an ongoing, multiple statement transaction.
            {
                if (myOracleDBAccess.SetData(TODO, sql, trans)) { return true; } // The update succeeded.
                else
                { myOracleDBAccess.DisposeTransaction(TODO, trans); return false; } // The update failed
            }
        }

        #endregion
    }
}