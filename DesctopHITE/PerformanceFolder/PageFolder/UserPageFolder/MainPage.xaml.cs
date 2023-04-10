﻿///----------------------------------------------------------------------------------------------------------
/// Данная страница является "главной" и открывается при авторизации пользователя.
/// На странице реализовано то, что считается нужным быть на главной странице.
/// Данная страница берёт некоторую информацию из класса TimeClass
///----------------------------------------------------------------------------------------------------------

using DesctopHITE.AppDateFolder.ClassFolder;
using System;
using System.Windows;
using System.Data.Entity;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Linq;
using DesctopHITE.AppDateFolder.ModelFolder;

namespace DesctopHITE.PerformanceFolder.PageFolder.UserPageFolder
{
    public partial class MainPage : Page
    {
        DateTime ToDay = DateTime.Now;
        public static TimeClass GetTimeClass = new TimeClass();
        public static HolidayClass GetDayClass = new HolidayClass();

        DispatcherTimer GetTimer;

        public MainPage()
        {
            InitializeComponent();
        }
        #region Действие
        private void GetTimer_Tick(object sender, EventArgs e) // Действие, которое будет происходит в определённый промежуток времени
        {
            HelloyTextBlock.Text = GetTimeClass.WhatTimeIsIt.ToString(); // Текст приветствие
            NowTimeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss"); // xx:xx:xx
            NowDateTextBlock.Text = DateTime.Now.ToString("dd MMMM" + "(MM) " + "yyyy"); // xx Month(xx) xxxx
            BirthdayTextBlock.Text = GetDayClass.HappyBirthdayGreetings.ToString(); // Поздравление с днём рождения
            NowHolidayTextBlock.Text = GetDayClass.WhatDayIsIt.ToString(); // Поздравление с текущим праздником

            if (GetDayClass.HappyBirthdayGreetings == "")
            {
                BirthdayTextBlock.Visibility = Visibility.Collapsed;
            }
            if (GetDayClass.WhatDayIsIt == "Сегодня нет праздников")
            {
                NowHolidayTextBlock.FontSize = 18;
                NowHolidayTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(42, 42, 42));
            }

            var EmployeesObjects = AppConnectClass.DataBase.WorkerTabe.Include(WorkerPassport => WorkerPassport.PassportTable).Where(
                Birthday => Birthday.PassportTable.DateOfBrich_Passport.Day == ToDay.Day &&
                            Birthday.PassportTable.DateOfBrich_Passport.Month == ToDay.Month);

            EmployeesWhoHaveBirthdayToday.ItemsSource = EmployeesObjects.ToList();
            }
        #endregion

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                // Свойства для Таймера
                GetTimer = new DispatcherTimer();
                GetTimer.Tick += new EventHandler(GetTimer_Tick);
                GetTimer.Interval = TimeSpan.FromSeconds(1);
                GetTimer.Start();
            }
            else
            {
                GetTimer.Stop();
            }
        }
    }
}
