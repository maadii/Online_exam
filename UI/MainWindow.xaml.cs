using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using Components;
using Components.Data;
using Components.IO;
using Components.Tools;
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
        private static Random rand = new Random();
        private string SpeakingAdreseePath = null;
        private string SpeakingFileName = null;
        private Dictionary<int, string> Answers = new Dictionary<int, string>();
        int MinWords = Convert.ToInt32(ConfigurationManager.AppSettings["MinimumWord"]);
        int MaxQ = Convert.ToInt32(ConfigurationManager.AppSettings["QuestionNumber"]);
        readonly List<string> badWords = new List<string>(ConfigurationManager.AppSettings["LimitedWords"].Split(new char[] { ';' }));
        bool badWordInString = false;
        //For sync Exam timers
        bool ExamStart = true;
        string DateOfExam;

        public MainWindow()
        {
            InitializeComponent();
            //Bind the Exam info
            LoadExamInfo();
            //Bind the DataGrid to the  result
            LoadResultGird();

        }
        public void LoadExamInfo()
        {
            var Examinfo = DBInstanc.Instance.GetExamInfos();
            DateOfExam = CESTime.Instance.ChangeZone(Examinfo.ExamDate).ToString();
            Autorlbl.Content += " " + Examinfo.Autor;
            CenterNamelbl.Content += " " + Examinfo.CenterName;
            DateTimelbl.Content += " " + DateOfExam;
            PhoneNumberlbl.Content += " " + Examinfo.Phonenamber;
            Titlelbl.Content += " " + Examinfo.Title;
            ExamNumberlbl.Content += " " + Examinfo.ExamNumber;
            Gendercbx.ItemsSource = Enum.GetValues(typeof(GenderType)).Cast<GenderType>();
            Gendercbx.SelectedIndex = 0;
        }
        public void LoadResultGird()
        {
            DataTable dataSource = Instance.GetResult(MaxQ);

            dataGrid.ItemsSource = dataSource.DefaultView;
        }
        private void MouseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                NationalCodeValidation NCV = new NationalCodeValidation();
                if (NCV.IsValid(Nationaltbx.Text))
                {
                    if (string.IsNullOrEmpty(Nametbx.Text) || string.IsNullOrEmpty(lastNametbx.Text))
                    {
                        MessageBox.Show("Please fill  the balank ", "Blank name error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        Nametbx.IsEnabled = false;
                        lastNametbx.IsEnabled = false;
                        Nationaltbx.IsEnabled = false;
                        BrithDate.IsEnabled = false;
                        Gendercbx.IsEnabled = false;
                        Examini();
                        Countdown(180, TimeSpan.FromSeconds(1), cur => Timerlbl.Content = TimeSpan.FromSeconds(cur).ToString("mm':'ss"));
                    }
                }
                else
                {
                    MessageBox.Show("Please insert valid national code", "National code error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Get Qustion from exam Generator class
        public void Examini()
        {
            List<QuestionView> q = ExamGenerator.Instance.GetQustion();
            Q1Tiltle.Content = q[0].Titel.ToString();
            Q1Tiltle.Tag = q[0].ID.ToString();
            var shuffledanswerd = q[0].Answers.OrderBy(a => rand.Next()).ToList();
            Q1A1.Content = shuffledanswerd[0].ToString();
            Q1A2.Content = shuffledanswerd[1].ToString();
            Q1A3.Content = shuffledanswerd[2].ToString();
            Q1A4.Content = shuffledanswerd[3].ToString();

            Q2Tiltle.Content = q[1].Titel.ToString();
            Q2Tiltle.Tag = q[1].ID.ToString();
            var shuffledanswerd1 = q[1].Answers.OrderBy(a => rand.Next()).ToList();
            Q2A1.Content = shuffledanswerd1[0].ToString();
            Q2A2.Content = shuffledanswerd1[1].ToString();
            Q2A3.Content = shuffledanswerd1[2].ToString();
            Q2A4.Content = shuffledanswerd1[3].ToString();

            Q3Tiltle.Content = q[2].Titel.ToString();
            Q3Tiltle.Tag = q[2].ID.ToString();
            var shuffledanswerd2 = q[2].Answers.OrderBy(a => rand.Next()).ToList();
            Q3A1.Content = shuffledanswerd2[0].ToString();
            Q3A2.Content = shuffledanswerd2[1].ToString();
            Q3A3.Content = shuffledanswerd2[2].ToString();
            Q3A4.Content = shuffledanswerd2[3].ToString();
            Hardnes.Content = " The questions hardness are { " + q[0].Hardness + " ," + q[1].Hardness + " ," + q[2].Hardness + " }";

            var d = ExamGenerator.Instance.GetQustion(QuestionType.Descriptive);
            TitleDlbl.Content = d.Titel;
            HintDlbl.Content = d.Hints;
            DecHard.Content = " The question hardness is {" + d.Hardness + " }";

            var s = ExamGenerator.Instance.GetQustion(QuestionType.Speaking);
            TitleSlbl.Content = s.Titel;
            Hintslbl.Content = s.Hints;
            speakHard.Content = " The question hardness is { " + s.Hardness + " }";
            button.IsEnabled = false;
        }
        //Timer Count Down Metod 
        void Countdown(int count, TimeSpan interval, Action<int> ts)
        {

            var dt = new DispatcherTimer();
            dt.Interval = interval;
            dt.Tick += (_, a) =>
            {
                if (ExamStart)
                {
                    if (count-- == 0)
                    {
                        dt.Stop();
                        SubmitClick(null, new RoutedEventArgs());
                    }
                    else
                        ts(count);
                }
                else
                {
                    dt.Stop();

                }
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
        /*Submit answers*/
        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            if (!badWordInString)
            {
                using (var Validty = new TextValidation())
                {
                    string richText = new TextRange(Danswer.Document.ContentStart, Danswer.Document.ContentEnd).Text;
                    Validty.Text = richText;
                    Validty.ValidNumber = MinWords;

                    if (Validty.IsValid())
                    {
                        button1.IsEnabled = false;
                        button2.IsEnabled = false;
                        int Sresult = 0;
                        ExamStart = false;
                        SaveAnswers saveanswers = new SaveAnswers();

                        DateTime Edate = CESTime.Instance.ChangeZone(DateTime.Now);
                        using (var ase = new QuickAssessment(Answers))
                        {
                            Sresult = ase.Getresults();
                            MessageBox.Show("Your quick result of the multiple choices is " + " " + Sresult.ToString() + " the ultimate result will be announced soon.", "Your results", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        saveanswers.WriteAnswer(Nametbx.Text + lastNametbx.Text, Nationaltbx.Text, DateOfExam, richText, SpeakingAdreseePath, SpeakingFileName, Sresult);
                        Result R = new Result()
                        {
                            ResultDate = Edate,
                            ResultNumber = Sresult,
                            SpendTime = DateTime.Parse(Timerlbl.Content.ToString()).Subtract(DateTime.Parse("03:00")).ToString().Remove(0, 1),
                            Student = new Student()
                            {
                                Name = Nametbx.Text,
                                LastName = lastNametbx.Text,
                                NationalCode = Convert.ToInt32(Nationaltbx.Text),
                                Birthdate = BrithDate.SelectedDate.Value.Date,
                                Genders = (GenderType)Gendercbx.SelectedValue,
                            }
                        };
                        Instance.SetResults(R);
                        LoadResultGird();
                        ShowHardnes();
                    }

                    else
                    {
                        MessageBox.Show("The accepted answer is more than" + Validty.ValidNumber + " exclusive words", "number of exclusive words ", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void ShowHardnes()
        {
            Hardnes.Visibility = Visibility.Visible;
            DecHard.Visibility = Visibility.Visible;
            speakHard.Visibility = Visibility.Visible;

        }
        /* add question 1 seleted anwers to dictionary*/
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
        /* add question 2 seleted anwers to dictionary*/
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
        /* add question 3  seleted anwers to dictionary*/
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
        /*Check the text language*/
        private void EnglishValid(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[a-zA-Z]"))
            {
                e.Handled = true;
            }
        }
        /*Check for limited words*/
        private void ValidWord(object sender, TextChangedEventArgs e)
        {
            badWordInString = true;
            string richText = new TextRange(Danswer.Document.ContentStart, Danswer.Document.ContentEnd).Text;
            string i = null;
           
            foreach (var item in badWords)
            {
                Regex reg = new Regex(item, RegexOptions.IgnoreCase);
                foreach (Match find in reg.Matches(richText))
                {

                    badWordInString = true;
                    i += "," + item;
                }
            }
            if (badWordInString)
            {
                //string i= badWords.Find(richText.Contains);
                MessageBox.Show("please remove " + i, "Use limited Word", MessageBoxButton.OK, MessageBoxImage.Error);

            }


        }
    }
}
