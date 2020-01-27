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
using System.Data.SqlClient;



public partial class _Default : System.Web.UI.Page
{

    SqlConnection cs;   //allows user to connect with database
    SqlCommand cmd;     //allows user to insert data into database
    SqlDataReader sdr;  //allows user to search database

    //connection string to connect to database
    //to work on native device, change this connection string to link to prefered database
    string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dforb_000\source\repos\Registration_v2\Registration_v2\App_Data\Registration.mdf;Integrated Security=True;Connect Timeout=30";

    protected void Page_Load(object sender, EventArgs e)
    {
        output.Text = "";
  
    }

    //Checks if all information fields have been filled out
    bool ValidInput(string fName, string lName, string email, string tName)
    {
        return !(fName == string.Empty || lName == string.Empty || email == string.Empty || tName == string.Empty);
    }

    //Checks if flag color is selected
    bool CheckFlag(string flag)
    {
        return (flag == "Default");
    }

    //Checks if email already exists in database
    bool CheckEmail(string email)
    {
        bool found = false;

        using (cs = new SqlConnection(ConnectionString))
        {
            cs.Open();

            cmd = new SqlCommand("Select * FROM Members WHERE email = @email", cs);
            cmd.Parameters.AddWithValue("@email", email);

            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (Convert.ToString(sdr["email"]) == email)
                {
                    found = true;
                    break;
                }
            }

        }
        return found;
    }

    //Adds new team to database
    public void CreateTeam(string team, string flag)
    {
        using (cs = new SqlConnection(ConnectionString))
        {
            //Set up SQL statement to insert new team into Teams table
            cmd = new SqlCommand("INSERT INTO Teams VALUES(@teamName, @flagColor)", cs);
            cmd.Parameters.AddWithValue("@teamName", team);
            cmd.Parameters.AddWithValue("@flagColor", flag);

            cs.Open();

            try
            {
                //display this message if registration was successful
                if (cmd.ExecuteNonQuery() != 0)
                    output.Text += "Team " + team + " successfully registered.<br/>";
            }
            catch(SqlException)
            {
                //display this message if SqlError occurs
                output.Text += "Error: Team " + team + " already exists.<br/>";
            }

        }
    }
    
    //Adds new team member to database
    public void AddMember(string fName, string lName, string email, string tName)
    {
        using (cs = new SqlConnection(ConnectionString))
        {

            //Set up SQL statement to insert new member into Members table
            cmd = new SqlCommand("INSERT INTO Members VALUES(@firstName, @lastName, @email, @teamName)", cs);
            cmd.Parameters.AddWithValue("@firstName", fName);
            cmd.Parameters.AddWithValue("@lastName", lName);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@teamName", tName);

            cs.Open();

            //executes command
            try
            {
                if (cmd.ExecuteNonQuery() != 0)
                    output.Text += fName + " " + lName + " added to team " + tName + ".<br/>";
            }
            //if command is unsuccessful, proper error message is handled here
            catch (SqlException Ex)
            {
                switch (Ex.Number)
                {
                    //catches foreign key error;
                    case 547:
                        output.Text += "Error: Team " + tName + " does not exist.<br/>";
                        break;
                    //catches unique key error; email value already exists in database and cannot be entered again.
                    case 2627:
                        output.Text += "Error: email " + email + " already registered. Team members may have been previously added.\n";
                        break;
                    //any other error falls here.
                    default:
                        output.Text += "Error: System failed to register " + fName + " " + lName + ".<br/>";
                        break;
                }
            }
        }

    }


    /* Event Handling */
    protected void ftButton_Click(object sender, EventArgs e)
    {
        //assigns variables to respective ID from  "formTeam" in Default.aspx
        string firstName = firstName1.Text;
        string lastName = lastName1.Text;
        string email = Email1.Text;
        string teamName = teamName1.Text;
        string flagColor = teamColor.SelectedValue;

        output.Text = ""; // reset output string

        if (!ValidInput(firstName, lastName, email, teamName) || CheckFlag(flagColor))
            output.Text = "Error: please fill out all fields.";

        else
        {
            // registrtion cancelled if email is found in database
            if(CheckEmail(email) == true)
                output.Text = firstName + " " + lastName + " already registered and cannot create a team.";

            //adds team and team member to database
            else
            {
                CreateTeam(teamName, flagColor);
                AddMember(firstName, lastName, email, teamName);
            }

        }

    }

    protected void jtButton_Click(object sender, EventArgs e)
    {
        //assigns variables to respective ID from "joinTeam" in Default.aspx
        string firstName = firstName2.Text;
        string lastName = lastName2.Text;
        string email = Email2.Text;
        string teamName = teamName2.Text;

        output.Text = "";

        if (!ValidInput(firstName, lastName, email, teamName))
            output.Text = "Error: please fill out all fields.";

        else
        {
            AddMember(firstName, lastName, email, teamName);
        }
        
    }
}