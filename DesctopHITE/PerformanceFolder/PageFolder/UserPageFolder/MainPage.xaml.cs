﻿using DesctopHITE.AppDateFolder.ClassFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesctopHITE.PerformanceFolder.PageFolder.UserPageFolder
{
    public partial class MainPage : Page
    {
        public static TimeClass GetTimeClass = new TimeClass();
        public MainPage()
        {
            InitializeComponent();
        }
        #region Действие
        private void PartOfTheDay()
        {

        }
        #endregion

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                string ToDayDate = GetTimeClass.WhatTimeIsIt + " " + GetTimeClass.WhatDayIsIt;
                ttt.Text = ToDayDate;
            }
        }
    }
}
