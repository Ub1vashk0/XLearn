using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Transactions;
using Xamarin.Forms;
using XLearn.DataConn;

namespace XLearn.TestContent
{
    public class TestInfo
    {
        public int MaxScore { get; set; }
        public int Score { get; set; }
        public int PassingScore { get; set; }
        public int Result { get; set; }
        public int CountQuestion { get; set; }
        private int IDSubtheme { get; set; }

        public TestInfo(int idSubtheme) 
        { 
            IDSubtheme = idSubtheme;
            Result = 0;

            ConnectionDBcs.OpenDB();
            var DRcountQuestion = ConnectionDBcs.GetDataReader("SELECT count(*) FROM Question where IDSubtheme = " + IDSubtheme);
            if (DRcountQuestion.Read())
               CountQuestion = DRcountQuestion.GetInt32(0);
            var DRScore = ConnectionDBcs.GetDataReader("SELECT PassingScore FROM Subthemes where IDSubtheme = " + IDSubtheme);
            if (DRScore.Read())
            {
                PassingScore = DRScore.GetInt32(0);
            }
            var DRMaxScore = ConnectionDBcs.GetDataReader("SELECT sum(Score) from Question where IDSubtheme = " + IDSubtheme);
            if (DRMaxScore.Read())
            {
                MaxScore = DRMaxScore.GetInt32(0);
            }
            ConnectionDBcs.CloseDB();
        }
        public TestInfo() { }
        public void AddScore(int idQuestion)
        {
            int score = 0;
            ConnectionDBcs.OpenDB();
            var dr = ConnectionDBcs.GetDataReader("SELECT Score from Question where IDQuestion = " + idQuestion);
            if(dr.Read())
                score = dr.GetInt32(0);
            Result += score;
            ConnectionDBcs.CloseDB();
        }
        public void ReturnResult(Label label)
        {
            ConnectionDBcs.OpenDB();
            if (Result > PassingScore)
            {
                label.Text = "Тест складено";
                label.TextColor = System.Drawing.Color.GreenYellow;
                ConnectionDBcs.UpdateValueInTable($"UPDATE Subthemes SET Status = 1, Score = {Result} WHERE IDSubtheme ={IDSubtheme}");
                CheckSubthemes();
            }
            else
            {
                label.Text = "Teст не складено";
                label.TextColor = System.Drawing.Color.Red;
            }
            ConnectionDBcs.CloseDB();
        }
        public void CheckSubthemes()
        {
            int corect = 0;
            int all = 0;
            int IDTheme = 0;

            ConnectionDBcs.OpenDB();
            var dr = ConnectionDBcs.GetDataReader($"SELECT IDTheme from Subthemes where IDSubtheme = {IDSubtheme}");
            if(dr.Read())
                IDTheme = dr.GetInt32(0);
            dr.Close();
            dr = ConnectionDBcs.GetDataReader($"SELECT count(Status) from Subthemes WHERE IDTheme = {IDTheme} and Status = 1");
            if(dr.Read())
                corect = dr.GetInt32(0);
            dr.Close();
            dr = ConnectionDBcs.GetDataReader($"SELECT count(Status) from Subthemes WHERE IDTheme = {IDTheme}");
            if(dr.Read())
                all = dr.GetInt32(0);
            dr.Close();
            if(corect == all)
                ConnectionDBcs.UpdateValueInTable($"UPDATE Themes SET Status = 1 WHERE IDTheme ={IDTheme}");
            ConnectionDBcs.CloseDB();
        }
    }
}
