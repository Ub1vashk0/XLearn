using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;
using Android.Graphics.Drawables;
using Android.OS;
using Java.Sql;
using Microsoft.Data.Sqlite;
using Xamarin.Forms;
using static Android.InputMethodServices.InputMethodService;

namespace XLearn.DataConn
{
    static public class ConnectionDBcs
    {

        static string dbPath { get; } = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbCourseContent.db");
        static private SqliteConnection sqlConn = new SqliteConnection($"Data Source={dbPath};");

        static public void CheckDB()
        {
            if(!File.Exists(dbPath))
{
                using (var inputStream = Android.App.Application.Context.Assets.Open("dbCourseContent.db"))
                {
                    using (var outputStream = new FileStream(dbPath, FileMode.Create))
                    {
                        inputStream.CopyTo(outputStream);
                    }
                }
            }
        }

        static public DataTable GetDataTable(string command)
        {
            var datareader = GetDataReader(command);
            DataTable dt = new DataTable();
            dt.Load(datareader);
            return dt;
        }

        static public void OpenDB()
        {
            sqlConn.Open();
        }

        static public void CloseDB() 
        {
            sqlConn.Close();
        }

        static public SqliteDataReader GetDataReader(string command)
        {
            SqliteCommand cmd = new SqliteCommand(command, sqlConn);
            return cmd.ExecuteReader();
        }

        static public void UpdateValueInTable(string command)
        {
            SqliteCommand cmd = new SqliteCommand(command, sqlConn);
            cmd.ExecuteNonQuery();
        }
    }
}
