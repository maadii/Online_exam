using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Components;
using Components.Data;
using Components.IO;
using DataModel.Entitys;
using Microsoft.Win32;
using static Components.DBInstanc;
using static DataModel.DBModel;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Random rng = new Random();
        string SpeakingAdreseePath= null;
        string SpeakingFileName = null;
        Dictionary<int, string> Answers = new Dictionary<int, string>();


        public MainWindow()
        {
            InitializeComponent();
            LoadExamInfo();
            ExamGenerator.Instance.GetQustion();
            //Bind the DataGrid to the  result
            LoadResultGird();
          
            Gendercbx.Items.Add("Male");
            Gendercbx.Items.Add("Female");
            Gendercbx.Items.Add("Other");
            Gendercbx.SelectedIndex = 0;
        }
        public void LoadExamInfo()
        {
            var Examinfo = DBInstanc.Instance.GetExamInfos();
            Autorlbl.Content += " " + Examinfo.Autor;
            CenterNamelbl.Content += " " + Examinfo.CenterName;
            DateTimelbl.Content += " " + Examinfo.ExamDate;
            PhoneNumberlbl.Content += " " + Examinfo.Phonenamber;
            Titlelbl.Content += " " + Examinfo.Title;
        }
        public void LoadResultGird()
        {
            DataTable dataSource = Instance.GetResult();
            dataGrid.ItemsSource = dataSource.DefaultView;
        }
        private void MouseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                 Nametbx.IsEnabled = false;
                lastNametbx.IsEnabled = false;
                Nationaltbx.IsEnabled = false;
                BrithDate.IsEnabled = false;
                Gendercbx.IsEnabled = false;
                Examini();
                Countdown(180, TimeSpan.FromSeconds(1), cur => Timerlbl.Content = TimeSpan.FromSeconds(cur).ToString("mm':'ss"));
            }
            catch (Exception)
            {
                throw;
            }          
        }
       
        public void Examini()
        {
           List< QuestionView> q= ExamGenerator.Instance.GetQustion();
            Q1Tiltle.Content = q[0].Titel.ToString();
            Q1Tiltle.Tag = q[0].ID.ToString();
            var shuffledanswerd = q[0].Answers.OrderBy(a => rng.Next()).ToList();
            Q1A1.Content= shuffledanswerd[0].ToString();
            Q1A2.Content = shuffledanswerd[1].ToString();
            Q1A3.Content = shuffledanswerd[2].ToString();
            Q1A4.Content = shuffledanswerd[3].ToString();

            Q2Tiltle.Content = q[1].Titel.ToString();
            Q2Tiltle.Tag = q[1].ID.ToString();
            var shuffledanswerd1 = q[1].Answers.OrderBy(a => rng.Next()).ToList();
            Q2A1.Content = shuffledanswerd1[0].ToString();
            Q2A2.Content = shuffledanswerd1[1].ToString();
            Q2A3.Content = shuffledanswerd1[2].ToString();
            Q2A4.Content = shuffledanswerd1[3].ToString();

            Q3Tiltle.Content = q[2].Titel.ToString();
            Q3Tiltle.Tag = q[2].ID.ToString();
            var shuffledanswerd2 = q[1].Answers.OrderBy(a => rng.Next()).ToList();
            Q3A1.Content = shuffledanswerd2[0].ToString();
            Q3A2.Content = shuffledanswerd2[1].ToString();
            Q3A3.Content = shuffledanswerd2[2].ToString();
            Q3A4.Content = shuffledanswerd2[3].ToString();

            var d = ExamGenerator.Instance.GetQustion(QuestionType.Descriptive);
            TitleDlbl.Content = d.Titel;
            HintDlbl.Content = d.Hints;

            var s = ExamGenerator.Instance.GetQustion(QuestionType.Speaking);
            TitleSlbl.Content = s.Titel;
            Hintslbl.Content = s.Hints;
        }
        void Countdown(int count, TimeSpan interval, Action<int> ts)
        {
            var dt = new System.Windows.Threading.DispatcherTimer();
            dt.Interval = interval;
            dt.Tick += (_, a) =>
            {
                if (count-- == 0)
                    dt.Stop();
                else
                    ts(count);
            };
            ts(count);
            dt.Start();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio files (*.Mp3;*.ogg;*.WAV)|*.Mp3;*.ogg;*.WAV";
            if (openFileDialog.ShowDialog() == true)

            {
                SpeakingAdreseePath = openFileDialog.FileName;
                SpeakingFileName = openFileDialog.SafeFileName;
                openFileDialog.Reset();
                
            }
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            int Sresult = 0;
            SaveAnswers saveanswers = new SaveAnswers();
            saveanswers.WriteAnswer(Nametbx.Text + lastNametbx.Text, Nationaltbx.Text, DateTimelbl.Content.ToString(), "gjg", SpeakingAdreseePath,SpeakingFileName);
            using (var ase = new QuickAssessment(Answers))
            {
                Sresult = ase.Getresults();
                MessageBox.Show("Your quick result of the multiple choices is " + " " + Sresult.ToString() + " the ultimate result will be announced soon.", "Your results", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Result R = new Result()
            {
                ResultDate = DateTime.Now,
                ResultNumber = Sresult,
                SpendTime = DateTime.Parse(Timerlbl.Content.ToString()).Subtract(DateTime.Parse("03:00")).ToString(),
                Student = new Student()
                {
                    Name = Nametbx.Text,
                    LastName = lastNametbx.Text,
                    NationalCode = Convert.ToInt32(Nationaltbx.Text),
                    Birthdate = BrithDate.SelectedDate,
                    Genders = GenderType.Female
                }
            };
            Instance.SetResults(R);
            LoadResultGird();
        }
          

        private void ItemQ1Selected(object sender, RoutedEventArgs e)
        {
            int key = Convert.ToInt32(Q1Tiltle.Tag);
            string value = ((RadioButton)sender).Content.ToString();
            if (Answers.ContainsKey(key))
            {
                Answers[key] = value;
            }
            else

            {
                Answers.Add(key, value);
            }
        }
        private void ItemQ2Selected(object sender, RoutedEventArgs e)
        {
            int key = Convert.ToInt32(Q2Tiltle.Tag);
            string value = ((RadioButton)sender).Content.ToString();
            if (Answers.ContainsKey(key))
            {
                Answers[key] = value;
            }
            else

            {
                Answers.Add(key, value);
            }

        }
        private void ItemQ3Selected(object sender, RoutedEventArgs e)
        {
            int key = Convert.ToInt32(Q3Tiltle.Tag);
            string value = ((RadioButton)sender).Content.ToString();
            if (Answers.ContainsKey(key))
            {
                Answers[key] = value;
            }
            else

            {
                Answers.Add(key, value);
            }

        }
    } 
}