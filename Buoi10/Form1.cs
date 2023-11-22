using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Buoi10
{
    public partial class Form1 : Form
    {
        private List<CauHoi> danhSachCauHoi;
        private int currentIndex;
        private string selectedAnswer;
        private string connectionString = "Data Source=LAPTOP-L0J0D79V\\WIPPOO;Initial Catalog=QuizDatabase;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();

            danhSachCauHoi = new List<CauHoi>
            {
                new CauHoi("1+1=?", new List<string> { "A. 1", "B. 2", "C. 3", "D. 4" }),
                 new CauHoi("4+6=?", new List<string> { "A. 123", "B. 10", "C. 41", "D. 12" }),
                  new CauHoi("34+62=?", new List<string> { "A. 96", "B. 26", "C. 93", "D. 81" }),
            };

            currentIndex = 0;
            HienThiCauHoi();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void HienThiCauHoi()
        {
            labelCauHoi.Text = danhSachCauHoi[currentIndex].NoiDung;
            string savedAnswer = LayKetQua("PlayerName", currentIndex);
            switch (savedAnswer)
            {
                case "A":
                    radioButtonA.Checked = true;
                    break;
                case "B":
                    radioButtonB.Checked = true;
                    break;
                case "C":
                    radioButtonC.Checked = true;
                    break;
                case "D":
                    radioButtonD.Checked = true;
                    break;
                default:
                    break;
            }
        }

        private void LuuKetQua(string playerName, int questionIndex, string selectedAnswer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                string checkQuery = "SELECT COUNT(*) FROM Questions WHERE ID = @QuestionIndex";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@QuestionIndex", questionIndex);

                int rowCount = (int)checkCommand.ExecuteScalar();

                if (rowCount > 0)
                {
                    string insertQuery = "INSERT INTO QuizResults (PlayerName, QuestionIndex, SelectedAnswer) VALUES (@PlayerName, @QuestionIndex, @SelectedAnswer)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@PlayerName", playerName);
                    insertCommand.Parameters.AddWithValue("@QuestionIndex", questionIndex);
                    insertCommand.Parameters.AddWithValue("@SelectedAnswer", selectedAnswer);

                    insertCommand.ExecuteNonQuery();
                }
                
            }
        }


        private string LayKetQua(string playerName, int questionIndex)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT SelectedAnswer FROM QuizResults WHERE PlayerName = @PlayerName AND QuestionIndex = @QuestionIndex";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlayerName", playerName);
                command.Parameters.AddWithValue("@QuestionIndex", questionIndex);

                object result = command.ExecuteScalar();
                return result != null ? result.ToString() : null;
            }
        }

        private void buttonNext_Click_1(object sender, EventArgs e)
        {
            LuuKetQua("PlayerName", currentIndex, selectedAnswer);

            if (currentIndex < danhSachCauHoi.Count - 1)
            {
                currentIndex++;
                HienThiCauHoi();
            }
            else
            {
            }
            
            radioButtonA.Checked = false;
            radioButtonB.Checked = false;
            radioButtonC.Checked = false;
            radioButtonD.Checked = false;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                HienThiCauHoi();
                
                if (danhSachCauHoi[currentIndex].DapAn.Any(d => d.Contains("Checked")))
                {
                    string selectedAnswer = danhSachCauHoi[currentIndex].DapAn.First(d => d.Contains("Checked"));
                }
            }
        }

        public class CauHoi
        {
            public string NoiDung { get; set; }
            public List<string> DapAn { get; set; }

            public CauHoi(string noiDung, List<string> dapAn)
            {
                NoiDung = noiDung;
                DapAn = dapAn;
            }
        }

        private void labelCauHoi_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonA_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonA.Checked)
            {
                selectedAnswer = "A";
                LuuKetQua("PlayerName", currentIndex, selectedAnswer);
            }
        }

        private void radioButtonB_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonB.Checked)
            {
                selectedAnswer = "B";
                LuuKetQua("PlayerName", currentIndex, selectedAnswer);
            }
        }

        private void radioButtonC_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonC.Checked)
            {
                selectedAnswer = "C";
                LuuKetQua("PlayerName", currentIndex, selectedAnswer);
            }
        }

        private void radioButtonD_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonD.Checked)
            {
                selectedAnswer = "D";
                LuuKetQua("PlayerName", currentIndex, selectedAnswer);
            }
        }
    }
}
