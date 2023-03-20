﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Net;
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
using DesctopHITE.AppDateFolder.ClassFolder;
using DesctopHITE.AppDateFolder.ModelFolder;

namespace DesctopHITE.PerformanceFolder.PageFolder.WorkerPageFolder
{
    public partial class ListWorkerPage : Page
    {
        public ListWorkerPage()
        {
            InitializeComponent();
            AppConnectClass.DataBase = new DesctopHiteEntities();
            ListWorkerListBox.ItemsSource = AppConnectClass.DataBase.WorkerTabe.ToList();
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                EditButton.IsEnabled = false;
                DeliteButton.IsEnabled = false;
            }
        }
        #region Click
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeliteButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        #region SelectionChanged_MouseDoubleClick
        private void ListWorkerListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        private void ListWorkerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditButton.IsEnabled = true;
            DeliteButton.IsEnabled = true;
        }
        private void SearchTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "")
            {
                HintSearchTextBlock.Visibility = Visibility.Visible;
                ListWorkerListBox.ItemsSource = AppConnectClass.DataBase.WorkerTabe.ToList();
            }
            else
            {
                HintSearchTextBlock.Visibility = Visibility.Collapsed;

                var workers = AppConnectClass.DataBase.WorkerTabe.Include(w => w.PassportTable).ToList();

                var searchResults = workers.Where(worker =>
                    worker.PassportTable.Surname_Passport.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                    worker.PassportTable.Name_Passport.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                    worker.PassportTable.Middlename_Passport.ToLower().Contains(SearchTextBox.Text.ToLower()));

                ListWorkerListBox.ItemsSource = searchResults.ToList();
            }
        }
        #endregion
    }
}
