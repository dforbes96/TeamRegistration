using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Security;
using System.Security.Permissions;



public partial class _Default : System.Web.UI.Page
{
    //file path for .csv file
    //to ensure file is properly created/written to, change this string to appropriate machine file path
    string filePath = @"C:\Users\dforb_000\source\repos\Registration_v1\Registration.csv";  
    Registration r;

    protected void Page_Load(object sender, EventArgs e)
    {
        /*Authorizes permission to read and write to file */
        var permissionSet = new PermissionSet(PermissionState.None);
        var writePermission = new FileIOPermission(FileIOPermissionAccess.Write, filePath);
        var readPermission = new FileIOPermission(FileIOPermissionAccess.Read, filePath);
        permissionSet.AddPermission(writePermission);
        permissionSet.AddPermission(readPermission);

        r = new Registration(filePath);
    }

    bool ValidInput(TextBox fName, TextBox lName, TextBox tName, DropDownList L)
    {
        return !(fName.Text == string.Empty || lName.Text == string.Empty || tName.Text == string.Empty || L.SelectedValue == "Default");
    }


    /* Event Handling */
    protected void ftButton_Click(object sender, EventArgs e)
    {
        if (!ValidInput(firstName1, lastName1, teamName1, teamColor))
            output.Text = "Error: Not all fields have been filled out.";

        else
        {
            if (r.TeamExists(teamName1.Text))
                output.Text = "Error: " + teamName1.Text + " already exists.";

            else
            {
                string[] data = { lastName1.Text, firstName1.Text, teamName1.Text, teamColor.Text };
                r.Insert(data);
                output.Text = firstName1.Text + " " + lastName1.Text + " created Team " + teamName1.Text;
            }
        }
    }

    protected void jtButton_Click(object sender, EventArgs e)
    {
        if (!ValidInput(firstName2, lastName2, teamName2, teamColor))
            output.Text = "Error: Not all fields have been filled out.";

        else
        {
            if (!r.TeamExists(teamName2.Text))
                output.Text = "Error: " + teamName2.Text + " does not exist.";

            else
            {
                if (r.MemberExists(firstName2.Text, lastName2.Text))
                    output.Text = "Error: " + firstName2.Text + " " + lastName2.Text + " already a member of Team " + r.GetTeam(firstName2.Text, lastName2.Text);

                else
                {
                    string[] data = { lastName2.Text, firstName2.Text, teamName2.Text, r.GetFlag(teamName2.Text) };
                    r.Insert(data);
                    output.Text = firstName2.Text + " " + lastName2.Text + " added to Team " + teamName2.Text;
                }
            }
        }
    }
}