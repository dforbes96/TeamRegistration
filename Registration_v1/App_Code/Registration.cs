using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;

/// <summary>
/// Summary description for Registration
/// </summary>
public class Registration
{
    public Registration(string fileName)
    {
        path = fileName;

        if (File.Exists(fileName))
            LoadCSV(fileName);

        else
        {
            outFile = new StreamWriter(fileName);
            outFile.WriteLine("lastName,firstName,teamName,flagColor");

            dt.Columns.Add("lastName");
            dt.Columns.Add("firstName");
            dt.Columns.Add("teamName");
            dt.Columns.Add("flagColor");

            outFile.Close();
        }

    }

    void LoadCSV(string fileName)
    {
        using (inFile = new StreamReader(fileName))
        {
            string[] header = inFile.ReadLine().Split(',');

            foreach (string h in header)
                dt.Columns.Add(h);

            while (!inFile.EndOfStream)
            {
                string[] rows = inFile.ReadLine().Split(',');
                DataRow dr = dt.NewRow();

                for (int i = 0; i < header.Length; i++)
                    dr[i] = rows[i];

                dt.Rows.Add(dr);
            }
        }
    }

    public void Insert(string[] data)
    {

        DataRow row = dt.NewRow();
        for (int i = 0; i < data.Length; i++)
            row[i] = data[i];

        dt.Rows.Add(row);

        using (outFile = File.AppendText(path))
        {
            for (int i = 0; i < data.Length; i++)
                outFile.Write(data[i] + ',');
            outFile.Write("\n");
        }
    }

    public void PrintTable()
    {
        foreach (DataRow dr in dt.Rows)
            Console.WriteLine(Convert.ToString(dr["lastName"]) + "\t" + Convert.ToString(dr["firstName"])
                + "\t" + Convert.ToString(dr["teamName"]) + "\t" + Convert.ToString(dr["flagColor"]));

    }

    public bool TeamExists(string tName)
    {
        bool exists = false;

        foreach (DataRow dr in dt.Rows)
        {
            if (Convert.ToString(dr["teamName"]) == tName)
            {
                exists = true;
                break;
            }
        }
        return exists;
    }

    public bool MemberExists(string fName, string lName)
    {
        bool exists = false;

        foreach (DataRow dr in dt.Rows)
        {
            if (Convert.ToString(dr["teamName"]) == fName && Convert.ToString(dr["teamName"]) == lName)
            {
                exists = true;
                break;
            }
        }
        return exists;
    }

    public string GetTeam(string fName, string lName)
    {
        string team = "";
        foreach (DataRow dr in dt.Rows)
        {
            if (Convert.ToString(dr["firstName"]) == fName && Convert.ToString(dr["lastName"]) == lName)
            {
                team = Convert.ToString(dr["teamName"]);
                break;
            }
        }

        return team;
    }

    public string GetFlag(string tName)
    {
        string flag = "";
        foreach (DataRow dr in dt.Rows)
        {
            if (Convert.ToString(dr["teamName"]) == tName)
            {
                flag = Convert.ToString(dr["flagColor"]);
                break;
            }
        }

        return flag;
    }


    StreamReader inFile;
    StreamWriter outFile;
    DataTable dt = new DataTable();
    string path;
}
