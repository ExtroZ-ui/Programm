using Project.net;
using Project.net.Class;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Project
{
    public partial class TestLesson : Window
    {

        private int idTest;
        public TestLesson(int tempIdTest)
        {
            InitializeComponent();
            idTest = tempIdTest;
        }

        private TimeSpan time;
        public static TimeSpan timeDb;
        private DateTime timeTest;
        private int i;
        private int countQuestion;
        private int[] idQust;
        public string[] textSel;
        public static int[] idQustError;
        public static int gradeTes;
        private TextBox tempTextBox;
        Random ran = new Random();

        private string nameTestAll;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.DBConnect();
            countQuestion = Convert.ToInt32(MainWindow._context.Test.Where(i => i.id == idTest).Select(t => t.countQuestion).FirstOrDefault());
            lableQues.Content = $"Количество вопросов {i} из {countQuestion}";
            nameTestAll = MainWindow._context.Test.Where(i => i.id == idTest).Select(t => t.name).FirstOrDefault();
            lableNameTest.Content = nameTestAll;
            GoToTimer();
            int temp = Convert.ToInt32(MainWindow._context.Test.Where(i => i.id == idTest).Select(t => t.time).FirstOrDefault());
            time = TimeSpan.FromMinutes(temp);
            timeTest = timeTest.AddMinutes(temp);
            idQust = MainWindow._context.Question.Where(i => i.idTest == idTest).Select(p => p.id).ToArray();
            idQust = idQust.OrderBy(x => ran.Next()).ToArray();
            textSel = new string[countQuestion];
            idQustError = new int[countQuestion];
            textSel = MainWindow._context.Question.Where(p => p.idTest == idTest).Select(t => t.answerTrue).ToArray();
            CreationTest();
        }
        public void GoToTimer()
        {

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            void timer_Tick(object sender, EventArgs e)
            {
                if (time == TimeSpan.Zero)
                {
                    MessageBox.Show("Время закончилось");
                    gradeTes = 2;
                    timer.Stop();
                }
                else
                {
                    time = time.Add(TimeSpan.FromSeconds(-1));
                    timeDb = timeDb.Add(TimeSpan.FromSeconds(1));
                    textBlockTimer.Text = ($"{time.Minutes}:{time.Seconds}");

                }
            }
        }

        public void GetGredy()
        {

        }

        public void CreationTest()
        {
            lableQuestion.Text = MainWindow._context.Question.Where(p => p.id == idQust.ToList()[i]).Select(s => s.question).FirstOrDefault();
            lableAnswerOne.Text = MainWindow._context.Question.Where(p => p.id == idQust.ToList()[i]).Select(s => s.answerOne).FirstOrDefault();
            lableAnswerTwo.Text = MainWindow._context.Question.Where(p => p.id == idQust.ToList()[i]).Select(s => s.answerTwo).FirstOrDefault();
            lableAnswerThree.Text = MainWindow._context.Question.Where(p => p.id == idQust.ToList()[i]).Select(s => s.answerThree).FirstOrDefault();
            lableAnswerFour.Text = MainWindow._context.Question.Where(p => p.id == idQust.ToList()[i]).Select(s => s.answerFour).FirstOrDefault();
            lableQues.Content = $"Количество вопросов {i} из {countQuestion}";
        }

        public void BackTest()
        {
            lableQuestion.Text = MainWindow._context.Question.Where(p => p.id == idQust.ToList()[i]).Select(s => s.question).FirstOrDefault();
            lableAnswerOne.Text = MainWindow._context.Question.Where(p => p.id == idQust.ToList()[i]).Select(s => s.answerOne).FirstOrDefault();
            lableAnswerTwo.Text = MainWindow._context.Question.Where(p => p.id == idQust.ToList()[i]).Select(s => s.answerTwo).FirstOrDefault();
            lableAnswerThree.Text = MainWindow._context.Question.Where(p => p.id == idQust.ToList()[i]).Select(s => s.answerThree).FirstOrDefault();
            lableAnswerFour.Text = MainWindow._context.Question.Where(p => p.id == idQust.ToList()[i]).Select(s => s.answerFour).FirstOrDefault();
            lableQues.Content = $"Количество вопросов {i} из {countQuestion}";
        }


        public void ClearBrushAnswer()
        {
            lableAnswerOne.BorderThickness = new Thickness(1, 1, 1, 1);
            lableAnswerTwo.BorderThickness = new Thickness(1, 1, 1, 1);
            lableAnswerThree.BorderThickness = new Thickness(1, 1, 1, 1);
            lableAnswerFour.BorderThickness = new Thickness(1, 1, 1, 1);
            lableOne.BorderThickness = new Thickness(1, 1, 0, 1);
            lableTwo.BorderThickness = new Thickness(1, 1, 0, 1);
            lableThree.BorderThickness = new Thickness(1, 1, 0, 1);
            lableFour.BorderThickness = new Thickness(1, 1, 0, 1);
            lableAnswerOne.BorderBrush = Brushes.Black;
            lableAnswerTwo.BorderBrush = Brushes.Black;
            lableAnswerThree.BorderBrush = Brushes.Black;
            lableAnswerFour.BorderBrush = Brushes.Black;
            lableOne.BorderBrush = Brushes.Black;
            lableTwo.BorderBrush = Brushes.Black;
            lableThree.BorderBrush = Brushes.Black;
            lableFour.BorderBrush = Brushes.Black;
        }

        private string colorBorder = "#FF08FB00";
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            ClearBrushAnswer();
            switch (e.Key)
            {
                case Key.D1:
                    lableAnswerOne.BorderThickness = new Thickness(5, 5, 5, 5);
                    lableAnswerOne.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    lableOne.BorderThickness = new Thickness(5, 5, 0, 5);
                    lableOne.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    tempTextBox = lableAnswerOne;
                    break;
                case Key.D2:
                    lableAnswerTwo.BorderThickness = new Thickness(5, 5, 5, 5);
                    lableAnswerTwo.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    lableTwo.BorderThickness = new Thickness(5, 5, 0, 5);
                    lableTwo.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    tempTextBox = lableAnswerTwo;
                    break;
                case Key.D3:
                    lableAnswerThree.BorderThickness = new Thickness(5, 5, 5, 5);
                    lableAnswerThree.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    lableThree.BorderThickness = new Thickness(5, 5, 0, 5);
                    lableThree.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    tempTextBox = lableAnswerThree;
                    break;
                case Key.D4:
                    lableAnswerFour.BorderThickness = new Thickness(5, 5, 5, 5);
                    lableAnswerFour.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    lableFour.BorderThickness = new Thickness(5, 5, 0, 5);
                    lableFour.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    tempTextBox = lableAnswerFour;
                    break;
                case Key.NumPad1:
                    lableAnswerOne.BorderThickness = new Thickness(5, 5, 5, 5);
                    lableAnswerOne.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    lableOne.BorderThickness = new Thickness(5, 5, 0, 5);
                    lableOne.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    tempTextBox = lableAnswerOne;
                    break;
                case Key.NumPad2:
                    lableAnswerTwo.BorderThickness = new Thickness(5, 5, 5, 5);
                    lableAnswerTwo.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    lableTwo.BorderThickness = new Thickness(5, 5, 0, 5);
                    lableTwo.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    tempTextBox = lableAnswerTwo;
                    break;
                case Key.NumPad3:
                    lableAnswerThree.BorderThickness = new Thickness(5, 5, 5, 5);
                    lableAnswerThree.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    lableThree.BorderThickness = new Thickness(5, 5, 0, 5);
                    lableThree.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    tempTextBox = lableAnswerThree;
                    break;
                case Key.NumPad4:
                    lableAnswerFour.BorderThickness = new Thickness(5, 5, 5, 5);
                    lableAnswerFour.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    lableFour.BorderThickness = new Thickness(5, 5, 0, 5);
                    lableFour.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    tempTextBox = lableAnswerFour;
                    break;
                default:
                    break;
            }
        }


        private void lableAnswerOne_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetSelect(1);
        }

        private void lableAnswerTwo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetSelect(2);
        }

        private void lableAnswerThree_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetSelect(3);
        }

        private void lableAnswerFour_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetSelect(4);
        }

        public void SetSelect(int temp)
        {
            ClearBrushAnswer();
            switch (temp)
            {
                case 1:
                    lableAnswerOne.BorderThickness = new Thickness(3, 3, 3, 3);
                    lableAnswerOne.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    lableOne.BorderThickness = new Thickness(3, 3, 0, 3);
                    lableOne.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    tempTextBox = lableAnswerOne;
                    break;
                case 2:
                    lableAnswerTwo.BorderThickness = new Thickness(3, 3, 3, 3);
                    lableAnswerTwo.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    lableTwo.BorderThickness = new Thickness(3, 3, 0, 3);
                    lableTwo.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    tempTextBox = lableAnswerTwo;
                    break;
                case 3:
                    lableAnswerThree.BorderThickness = new Thickness(3, 3, 3, 3);
                    lableAnswerThree.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    lableThree.BorderThickness = new Thickness(3, 3, 0, 3);
                    lableThree.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    tempTextBox = lableAnswerThree;
                    break;
                case 4:
                    lableAnswerFour.BorderThickness = new Thickness(3, 3, 3, 3);
                    lableAnswerFour.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    lableFour.BorderThickness = new Thickness(3, 3, 0, 3);
                    lableFour.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    tempTextBox = lableAnswerFour;
                    break;
            }
        }

        private int p;

        private void NextQustion()
        {
            if (tempTextBox.Text != null)
            {
                textSel[i] = tempTextBox.Text;
                if (i != countQuestion)
                {
                    if (MainWindow._context.Question.Where(p => p.id == idQust.ToList()[i]).Select(g => g.answerTrue).FirstOrDefault() == tempTextBox.Text)
                    {
                        // tempId += $"{MainWindow._context.Question.Local.ToObservableCollection().Where(p => p.id == idQust.ToList()[i]).Select(g => g.id).FirstOrDefault()} ";
                        idQustError[p] = MainWindow._context.Question.Where(p => p.id == idQust.ToList()[i]).Select(g => g.id).FirstOrDefault();
                        p++;

                    }
                    if (i == 19)
                    {
                        buttNext.Content = "Завершить тест";
                    }

                    i++;

                    if (i != 0)
                    {
                        buttBack.Visibility = Visibility.Visible;
                    }
                    CreationTest();
                }
                else
                {
                    DateTime date = DateTime.Now;
                    idQustError = idQustError.Distinct().ToArray();
                    idQustError = idQustError.Take(idQustError.Count() - 1).ToArray();


                    if (idQustError.Count() <= 2)
                    {

                        gradeTes = 4;
                    }
                    if (idQustError.Count() <= 1)
                    {
                        gradeTes = 5;
                    }
                    else
                    {
                        gradeTes = 2;
                    }


                    MainWindow._context.StateTest.Add(new StateTest()
                    {
                        data = date.ToString("dd.MM.yyyy"),
                        nameTest = nameTestAll,
                        grade = gradeTes,
                        error = idQustError.Count(),
                        familiy = MainWindow.nameUser,
                        discipline = "0",
                        speciality = MainWindow.special,
                        namePK = Environment.MachineName,
                        nomVopr = "-",
                        time = $"{timeDb.Minutes}:{timeDb.Seconds}",
                        vzdov = MainWindow.vzvod,
                        rota = MainWindow.rota,
                        idUser = MainWindow.idUser,

                    });
                    MainWindow._context.SaveChanges();

                    new ShowResul().Show();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Выберите ответ");
            }
        }

        private void buttNext_Click(object sender, RoutedEventArgs e)
        {
            NextQustion();
        }

        private void buttExit_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            //new Performance(Lessen.nameTema, Lessen.nameLess).Show();
            Close();
        }

        private void buttBack_Click(object sender, RoutedEventArgs e)
        {

            if (i != 0)
            {
                // i--;

                i--;

                if (i == 0)
                {
                    buttBack.Visibility = Visibility.Collapsed;
                }

                if (MainWindow._context.Question.Where(p => p.id == idQust.ToList()[i]).Select(g => g.id).FirstOrDefault() == idQust.ToList()[i])
                {
                    idQustError.Take(idQustError.Count() - 1).ToArray();
                }

                BackTest();

                if (textSel[i] == lableAnswerOne.Text)
                {
                    SetSelect(1);
                }
                else if (textSel[i] == lableAnswerTwo.Text)
                {
                    SetSelect(2);
                }
                else if (textSel[i] == lableAnswerThree.Text)
                {
                    SetSelect(3);
                }
                else if (textSel[i] == lableAnswerFour.Text)
                {
                    SetSelect(4);
                }

            }
        }

        private void buttNext_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                NextQustion();
            }
        }
    }
}
