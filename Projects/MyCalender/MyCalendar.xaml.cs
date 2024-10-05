using FinalProjectWPF.Projects.MyCalendar.Models;
using FinalProjectWPF.Projects.MyCalender.UserControls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FinalProjectWPF.Projects.MyCalendar
{
    /// <summary>
    /// Interaction logic for Calender.xaml
    /// </summary>
    public partial class MyCalendar : Page
    {
        private DateTime _currentDay = DateTime.Today;
        private TaskFileManager taskFileManager = new TaskFileManager();
        private ObservableCollection<MyTask>? tasks;
        public MyCalendar()
        {
            InitializeComponent();
            InitializeTasksForDate(_currentDay);
            UpdateYearMonth(_currentDay);

        }

        private async Task InitializeTasksForDate(DateTime SelectedDate)
        {
            //    // Sample JSON parsing
            ObservableCollection<MyTask> tasks = taskFileManager.GetAllTasks();
            TaskTable.Children.Clear(); // Clear any existing items

            foreach (MyTask task in tasks)
            {
                DateTime taskStartTime = task.TaskStartTime;
                DateTime taskEndTime = task.TaskEndTime;
                string taskTime = taskStartTime.ToString() + " " + taskEndTime.ToString();
                var taskControl = new Item
                {
                    Title = task.Title,
                    Time = taskTime,
                    Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FBC9B1")) // Fixed to always use this color
                };
                taskControl.EditButtonClicked += (s, e) =>
                {
                    // This is where you can handle the event and get the task details
                    OpenEditPopup(task);
                };

                TaskTable.Children.Add(taskControl);
            }
        }

        private void PrevDayButton_Click(object sender, RoutedEventArgs e)
        {
            _currentDay = _currentDay.AddDays(-1);
            MainCalendar.DisplayDate = _currentDay;
            MainCalendar.SelectedDate = _currentDay;
            InitializeTasksForDate(_currentDay);
            //UpdateComboBoxes();
        }
        private void NextDayButton_Click(object sender, RoutedEventArgs e)
        {
            _currentDay = _currentDay.AddDays(1);
            MainCalendar.DisplayDate = _currentDay;
            MainCalendar.SelectedDate = _currentDay;
            InitializeTasksForDate(_currentDay);
            //UpdateComboBoxes();
        }

        private void UpdateYearMonth(DateTime day)
        {
            foreach (RadioButton r in YearBar.Children.OfType<RadioButton>())
            {
                if (day.Year.ToString() == r.Content.ToString())
                {
                    r.IsChecked = true;
                }
            }
            foreach (RadioButton r in MonthBar.Children.OfType<RadioButton>())
            {
                if (day.Month.ToString() == r.Content.ToString())
                {
                    r.IsChecked = true;
                }
            }
        }

        private void BackToToday_Click(object sender, RoutedEventArgs e)
        {
            _currentDay = DateTime.Today;
            InitializeTasksForDate(_currentDay);
            UpdateYearMonth(_currentDay);
        }
        private void EditTaskClicked()
        {

        }

        private void OpenEditPopup(MyTask task)
        {
            TaskNameBox.Text = task.Title;
            TaskDescriptionBox.Text = task.Description;
            TaskLocationBox.Text = task.Location;
            TaskDatePicker.SelectedDate = task.TaskStartTime.Date;
            string startHour = task.TaskStartTime.ToString("HH:mm");
            foreach (ComboBoxItem item in TaskStartTimeComboBox.Items)
            {
                if (item.Tag.ToString() == startHour)
                {
                    TaskStartTimeComboBox.SelectedItem = item;
                    break;
                }
            }

            // Set the selected end time
            string endHour = task.TaskEndTime.ToString("HH:mm");
            foreach (ComboBoxItem item in TaskEndTimeComboBox.Items)
            {
                if (item.Tag.ToString() == endHour)
                {
                    TaskEndTimeComboBox.SelectedItem = item;
                    break;
                }
            }
            TaskPopup.IsOpen = true;
        }

        private void ChangeMonth()
        { }
        private void ChangeYear()
        { }
    }
}
