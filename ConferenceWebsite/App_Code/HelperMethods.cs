using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ConferenceWebsite.Models;
using static ConferenceWebsite.Global;

namespace ConferenceWebsite.App_Code
{
    public class HelperMethods : Page
    {
        private readonly ConferenceDBAccess myConferenceDB = new ConferenceDBAccess();

        public string CleanInput(string input)
        {
            // Replace single quote by two quotes and remove leading and trailing spaces.
            return input.Replace("'", "''").Trim();
        }

        public void DisplayMessage(Label labelControl, string message)
        {
            if (message != "")
            {
                if (message.Substring(0, 3) == "***" || message.Substring(0, 6) == "Please") // Error message.
                {
                    labelControl.ForeColor = System.Drawing.Color.Red;
                }
                else // Information message.
                {
                    labelControl.ForeColor = System.Drawing.Color.Blue; // "#FF0000FF"
                }
                labelControl.Text = message;
            }
            else // Error in displaying message.
            {
                labelControl.Text = "*** Internal system error getting error message. Please contact 3311rep.";
            }
            labelControl.Visible = true;
        }

        public string EmptyResultForTODO(string TODO)
        {
            return "*** SQL error in TODO " + TODO + ": The query did not return any result.";
        }

        public int GetColumnIndexByName(GridView grid, string attributeName, Label labelControl)
        {
            for (int i = 0; i < grid.Rows[0].Cells.Count; i++)
            {
                if (grid.HeaderRow.Cells[i].Text.ToLower().Trim() == attributeName.ToLower().Trim())
                {
                    return i;
                }
            }
            DisplayMessage(labelControl, "*** SQL error: The attribute " + attributeName + " is missing in the query result.");
            return -1;
        }

        public string GetEmailFromUsername(string username, Label labelControl)
        {
            string result = null;
            DataTable dtEmail = myConferenceDB.GetEmailFromUsername(username);

            // Attributes expected to be returned by the query result.
            var attributeList = new List<string> { "EMAIL" };

            // Determine if the query is valid.
            if (IsQueryResultValid("GetEmailFromUsername", dtEmail, attributeList, labelControl))
            {
                if (dtEmail.Rows.Count != 0)
                {
                    result = dtEmail.Rows[0]["EMAIL"].ToString();
                }
                else // No email found for username => invalid username.
                {
                    labelControl.Text = "Invalid username.";
                }
            }
            return result;
        }

        public int GetGridViewColumnIndexByName(object sender, string attributeName, Label labelControl)
        {
            DataTable dt = ((DataTable)((GridView)sender).DataSource);
            if (dt != null)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName.ToUpper().Trim() == attributeName.ToUpper().Trim())
                    {
                        return i;
                    }
                }
                DisplayMessage(labelControl, "*** SQL error: The attribute " + attributeName + " is missing in the query result.");
            }
            return -1;
        }

        public string GetNextTableIdValue(string tableName, string tableId)
        {
            string id = "";
            decimal maxId = myConferenceDB.GetMaxTableId(tableName, tableId);
            if (maxId != -1)
            {
                id = (maxId + 1).ToString();
            }
            else // An SQL error occurred.
            {
                sqlError = sqlError + " - When getting next " + tableId + " for " + tableName + " table. Please contact 3311rep.";
            }
            return id;
        }

        public bool IsQueryResultValid(string TODO, DataTable datatableToCheck, List<string> columnsNames, Label labelControl)
        {
            bool isQueryResultValid = true;
            if (datatableToCheck != null)
            {
                if (datatableToCheck.Columns != null && datatableToCheck.Columns.Count == columnsNames.Count)
                {
                    foreach (string columnName in columnsNames)
                    {
                        if ((!datatableToCheck.Columns.Contains(columnName)) && columnName != "ANYNAME")
                        {
                            DisplayMessage(labelControl, "*** The SELECT statement of " + TODO + " does not retrieve the attribute " + columnName + ".");
                            isQueryResultValid = false;
                            break;
                        }
                    }
                }
                else
                {
                    DisplayMessage(labelControl, "*** The SELECT statement of " + TODO + " retrieves " + datatableToCheck.Columns.Count + " attributes while the required number is " + columnsNames.Count + ".");
                    isQueryResultValid = false;
                }
            }
            else // An SQL error occurred.
            {
                DisplayMessage(labelControl, sqlError);
                isQueryResultValid = false;
            }
            return isQueryResultValid;
        }

        public void RenameGridViewColumn(GridViewRowEventArgs e, string fromName, string toName)
        {
            for (int i = 0; i < e.Row.Controls.Count; i++)
            {
                if (e.Row.Cells[i].Text.ToUpper().Trim() == fromName.ToUpper().Trim())
                {
                    e.Row.Cells[i].Text = toName;
                }
            }
        }

        public bool SynchLoginAndApplicationDatabases(string username, string email, ConferenceRole role, Label labelControl)
        {
            // Synchronize login and application database email.
            bool synchResult = true;
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindByName(username);

            // Person is in Asp.Net and in application database => possibly update email.
            if (user != null && email != null)
            {
                // If the role is None, the user is registering and should be removed from the Asp.Net database as his/her
                // email was not found in the application database. The user will be created by the Register method.
                if (role == ConferenceRole.None)
                {
                    manager.Delete(user);
                }
                if (user.Email != email)
                {
                    user.Email = email;
                    manager.Update(user);
                }
            }
            // Person is in Asp.Net, but not in application database => delete Asp.Net user.
            else if (user != null && email == null)
            {
                manager.Delete(user);
            }
            // Person is not in Asp.Net, but is in application database => create user in Asp.Net,
            // but only if the user role is not None as in this case the user will be created in the Login method.
            else if (user == null && email != null && role != ConferenceRole.None)
            {
                user = new ApplicationUser() { UserName = username, Email = email };
                IdentityResult result = manager.Create(user, "Conference1#");
                if (result.Succeeded)
                {
                    IdentityResult roleResult = manager.AddToRole(user.Id, role.ToString());
                    if (!roleResult.Succeeded)
                    {
                        manager.Delete(user);
                        labelControl.Text = "*** Cannot create role " + role.ToString() + " for user with email '" + email + "'. Please contact 3311rep.";
                        synchResult = false;
                    }
                }
                else
                {
                    labelControl.Text = "*** " + ((string[])result.Errors)[0] + " Please contact 3311rep.";
                    synchResult = false;
                }
            }
            return synchResult;
        }
    }
}
      
