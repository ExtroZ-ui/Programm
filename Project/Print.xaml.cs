using Project.net;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
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
using Project.net.Class;
using System.Globalization;
using System.Data.Entity;

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для Print.xaml
    /// </summary>
    /// 
    
    public partial class Print : Window
    {
        public Print()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.DBConnect();
            GetAndSetInput();
            GoToTimer();
            GetTemp();
            LightButtontt(0);
        }


        private int time;
        private int progresbarMax;
        private int timeGrade;


        public void GetTemp()
        {
            int temp = MainWindow._context.Exercises
                                                             .Where(t => t.type_lesson == "печать" && t.id == 89)
                                                             .Select(te => te.temp)
                                                             .FirstOrDefault();

           /* int tempTime = MobileContext._context.Exercises.Local.ToObservableCollection()
                                                 .Where(t => t.type_lesson == "печать" && t.id == 89)
                                                 .Select(te => te.vremy)
                                                 .FirstOrDefault();*/

          //  string query = $"SELECT темп,vremy FROM Упражнения WHERE [Тип занятия] = 'печать' and [Номер_упражнения] = 40";
           
          //  using (OleDbCommand command = new OleDbCommand(query, MainWindow.db))
           // using (OleDbDataReader reader = command.ExecuteReader())
           // {
             //   reader.Read();
                textBlockMaxTemp.Text = temp.ToString();
                progressBaarTemp.Maximum = int.Parse(textBlockMaxTemp.Text);

                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(timer_Tick);
                timer.Start();

                void timer_Tick(object sender, EventArgs e)
                {
                    progressBaarTemp.Value++;
                    progresbarMax++;
                }
              
           // }

        }


        public void GetGrade()
        {
            int grTemp;

            if ( int.Parse(textBlockTemp.Text) <= int.Parse(textBlockMaxTemp.Text))
            {
                switch (int.Parse(textBlockError.Text))
                {
                    case 0:
                    case 1:
                        grTemp = 5;
                        break;
                    case 2:
                        grTemp = 4;
                        break;
                    case 3:
                        grTemp = 3;
                        break;
                    default:
                        grTemp = 2;
                        break;
                }
            }
            else
            {
                grTemp = 2;
            }


            DateTime date = DateTime.Now;
            MainWindow._context.ActionsAll.Add(new net.Class.Action()
            {
                type_action = 3,
                date = date.ToString("dd.MM.yyyy"),
                id_user = MainWindow.idUser,
                time = DateTime.Now,
                tema = Lessen.numburTema,
                less = Lessen.numburLess,
                time_work = textBlockTimer.Text,
                num_input = Convert.ToInt32(textBlockChar.Text),
                error = Convert.ToInt32(textBlockError.Text),
                grade = grTemp,
                num_exercise = 2,
                temp = Convert.ToInt32(textBlockTemp.Text),
                pc = Environment.MachineName,
            }) ;
            MainWindow._context.SaveChanges();
            new Lessen().Show();        
            
            Close();
        }

        public void GoToTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            void timer_Tick(object sender, EventArgs e)
            {
                time++;
                textBlockTimer.Text = ($"{time / 60}:{time % 60}");
                if (timeGrade.ToString() == (time / 60).ToString())
                {
                    GetGrade();
                    MessageBox.Show("Время закончилось");
                }
            }
             int tempTime = MainWindow._context.Exercises
                                      .Where(t => t.type_lesson == "печать" && t.tema == Lessen.numburTema && t.zanytie == Lessen.numburLess)
                                      .Select(te => te.vremy)
                                      .FirstOrDefault();

          //  string query = $"SELECT vremy FROM Упражнения WHERE [Тип занятия] = 'печать' and [Номер_упражнения] = 40";
          //  using (OleDbCommand command = new OleDbCommand(query, MainWindow.db))
          //  using (OleDbDataReader reader = command.ExecuteReader())
          //  {
                //reader.Read();
                timeGrade  = tempTime;
            //}          
        }
        
        int FileTemp;
        int i = 0;
        int p = 0;

        public void GetAndSetInput()
        {
            string tempText = MainWindow._context.Exercises
                         .Where(t => t.type_lesson == "печать" && t.tema == Lessen.numburTema && t.zanytie == Lessen.numburLess)
                         .Select(te => te.text)
                         .FirstOrDefault();

        //    string query = $"SELECT Текст FROM Упражнения WHERE [Тип занятия] = 'печать'";
          //  using (OleDbCommand command = new OleDbCommand(query, MainWindow.db))
          //  using (OleDbDataReader reader = command.ExecuteReader())
          //  {
           //     while (reader.Read())
            //    {
                    textBlockInput.Selection.Text = tempText;
           //     }
            //}
            FileTemp = Convert.ToInt32(textBlockError.Text);
        }

        TextPointer start;
        TextPointer end;

        TextPointer FindPointerAtTextOffset(TextPointer from, int offset, bool seekStart)
        {
            if (from == null)
                return null;

            TextPointer current = from;
            TextPointer end = from.DocumentEnd;
            int charsToGo = offset;

            while (current.CompareTo(end) != 0)
            {
                Run currentRun;
                if (current.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text &&
                    (currentRun = current.Parent as Run) != null)
                {
                    var remainingLengthInRun = current.GetOffsetToPosition(currentRun.ContentEnd);
                    if (charsToGo < remainingLengthInRun ||
                        (charsToGo == remainingLengthInRun && !seekStart))
                        return current.GetPositionAtOffset(charsToGo);
                    charsToGo -= remainingLengthInRun;
                    current = currentRun.ElementEnd;
                }
                else
                {
                    current = current.GetNextContextPosition(LogicalDirection.Forward);
                }
            }
            if (charsToGo == 0 && !seekStart)
                return end;
            return null;
        }
        public void GetPoint(int tempOne, int tempTwo)
        {
            TextRange range;
                    start = FindPointerAtTextOffset(textBlockInput.Document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward), tempOne, seekStart: true);
                    if (start == null)
                    { }

                    end = FindPointerAtTextOffset(start,  p + tempTwo, seekStart: false);
                    if (end == null)
                    { }
             range = new TextRange(start, end);
            range.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Green);
        }


        public void DefultButtonBackgroundColor(Button button)
        {

            string color = String.Concat(button.Name.ToString().Where(Char.IsLetter));

                switch (color)
                {
                    case "ButtRed":
                    button.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFF2323");
                    button.BorderThickness = new Thickness(1, 1, 1, 1);
                    button.BorderBrush = Brushes.Black;
                    break;
                    case "ButtYellow":
                    button.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFF44");
                    button.BorderThickness = new Thickness(1, 1, 1, 1);
                    button.BorderBrush = Brushes.Black;
                    break;
                    case "ButtGreen":
                    button.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF53FB53");
                    button.BorderThickness = new Thickness(1, 1, 1, 1);
                    button.BorderBrush = Brushes.Black;
                    break;
                    case "ButtBlue":
                    button.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFEC3CFF");
                    button.BorderThickness = new Thickness(1, 1, 1, 1);
                    button.BorderBrush = Brushes.Black;
                    break;
                    case "ButtPink":
                    button.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFF2323");
                    button.BorderThickness = new Thickness(1, 1, 1, 1);
                    button.BorderBrush = Brushes.Black;
                    break;
                        default:
                    button.Background = Brushes.LightGray;
                    button.BorderThickness = new Thickness(1, 1, 1, 1);
                    button.BorderBrush = Brushes.Black;
                    break;
                }
        }
        private string colorBorder = "#DF5A75";
        public  void LightButtontt(int temp)
        {
            
            var butt = canvasKey.Children.OfType<Button>().ToList();

            foreach (var item in butt)
            {
                DefultButtonBackgroundColor(item);
                foreach (var bu in item.Content.ToString().ToList())
                {
                    if (textBlockInput.Selection.Text.ToList()[i + temp].ToString() == bu.ToString() && bu.ToString() != " ")
                    {;
                        item.Background = Brushes.DarkViolet;
                        item.BorderThickness = new Thickness(2, 2, 2, 2);
                        item.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));

                    }
                    else if (textBlockInput.Selection.Text.ToList()[i + temp].ToString() == "\r" && item.Content.ToString() == "Enter" && bu.ToString() != " ")
                    {                    
                        item.Background = Brushes.DarkViolet;
                        item.BorderThickness = new Thickness(2, 2, 2, 2);
                        item.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    }
                    else if (item.Content.ToString() == "Space" && textBlockInput.Selection.Text.ToList()[i + temp].ToString() == " ")
                    {
                        item.Background = Brushes.DarkViolet;
                        item.BorderThickness = new Thickness(2, 2, 2, 2);
                        item.BorderBrush = (Brush)(new BrushConverter().ConvertFrom(colorBorder));
                    }
                }

            }
        }
        public async void LightButton(TextCompositionEventArgs e)
        {

            var butt = canvasKey.Children.OfType<Button>().ToList();
            foreach (var item in butt)
            {
                foreach (var bu in item.Content.ToString().ToList())
                {

                    if (e.Text == bu.ToString() && bu.ToString() != " ")
                    {
                        item.Background = Brushes.BlueViolet;
                        await Task.Delay(100);
                        item.Background = Brushes.LightGray;
                    }                  
                }
                if (e.Text == "\r" && item.Content.ToString() == "Enter")
                {
                    item.Background = Brushes.Yellow;
                    await Task.Delay(100);
                    item.Background = Brushes.LightGray;
                }
                if (item.Content.ToString() == "Space" && e.Text == " ")
                {
                    item.Background = Brushes.Yellow;
                    await Task.Delay(100);
                    item.Background = Brushes.LightGray;
                }
            }

        }

        private void Window_TextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (e.Text == textBlockInput.Selection.Text.ToList()[i].ToString())
                {
                    if (progresbarMax > int.Parse(textBlockTemp.Text))
                    {
                        textBlockTemp.Text = progresbarMax.ToString();
                    }
                    if (e.Text == "\r")
                    {
                        textBlockInputSet.Text += textBlockInput.Selection.Text.ToList()[i];
                        i++;
                        LightButtontt(1);
                        i++;
                        textBlockInputSet.Text += "\n";                       
                    }
                    else
                    {
                        LightButtontt(1);
                        GetPoint(0, 1);
                        textBlockInput.ScrollToEnd();
                        textBlockInputSet.Text += textBlockInput.Selection.Text.ToList()[i];
                        i++;
                        p++;
                    }
                    progresbarMax = 0;
                    progressBaarTemp.Value = 0;


                }
                else
                {
                    FileTemp++;
                    textBlockError.Text = FileTemp.ToString();
                }
                textBlockChar.Text = i.ToString();
            }
            catch
            {

            }
        }


        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            GetGrade();
        }
    }


    public static class FlowDocumentHelper
    {
        public static IEnumerable<TTextElement> WalkTextRange<TTextElement>(this FlowDocument doc, TextPointer start, TextPointer end) where TTextElement : TextElement
        {
            var lastVisited = new Dictionary<int, TTextElement>();
            foreach (var stack in doc.WalkTextHierarchy())
            {
                var element = stack.Peek() as TTextElement;
                if (element != null)
                {
                    TTextElement previous;
                    if (!lastVisited.TryGetValue(stack.Count - 1, out previous) || previous != element)
                    {
                        if (element.Overlaps(start, end))
                            yield return element;
                        lastVisited[stack.Count - 1] = element;
                    }
                }
            }
        }

        public static bool Overlaps(this TextElement element, TextPointer start, TextPointer end)
        {
            return element.ContentEnd.CompareTo(start) > 0 && element.ContentStart.CompareTo(end) < 0;
        }

        public static IEnumerable<Stack<DependencyObject>> WalkTextHierarchy(this FlowDocument doc)
        {
            if (doc == null)
                throw new ArgumentNullException();

            var stack = new Stack<DependencyObject>();

            TextPointer docEnd = doc.ContentEnd;


            for (var iterator = doc.ContentStart;
                (iterator != null) && (iterator.CompareTo(docEnd) < 0);
                iterator = iterator.GetNextContextPosition(LogicalDirection.Forward))
            {
                var parent = iterator.Parent;

                // Identify the type of content immediately adjacent to the text pointer.
                TextPointerContext context = iterator.GetPointerContext(LogicalDirection.Forward);

                switch (context)
                {
                    case TextPointerContext.ElementStart:
                    case TextPointerContext.EmbeddedElement:
                    case TextPointerContext.Text:
                        PushElement(stack, parent);
                        yield return stack;
                        break;

                    case TextPointerContext.ElementEnd:
                        break;

                    default:
                        throw new System.Exception("Unhandled TextPointerContext " + context.ToString());
                }

                switch (context)
                {
                    case TextPointerContext.ElementEnd:
                    case TextPointerContext.EmbeddedElement:
                    case TextPointerContext.Text:
                        PopToElement(stack, parent);
                        break;

                    case TextPointerContext.ElementStart:
                        break;

                    default:
                        throw new System.Exception("Unhandled TextPointerContext " + context.ToString());
                }
            }
        }

        static int IndexOf<T>(Stack<T> source, T value)
        {
            int index = 0;
            var comparer = EqualityComparer<T>.Default;
            foreach (T item in source)
            {
                if (comparer.Equals(item, value))
                    return index;
                index++;
            }
            return -1;
        }

        static void PopToElement<T>(Stack<T> stack, T item)
        {
            for (int index = IndexOf(stack, item); index >= 0; index--)
                stack.Pop();
        }

        static void PushElement<T>(Stack<T> stack, T item)
        {
            PopToElement(stack, item);
            stack.Push(item);
        }
    }
}
