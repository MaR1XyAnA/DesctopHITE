﻿using DesctopHITE.AppDateFolder.ClassFolder;
using DesctopHITE.AppDateFolder.ModelFolder;
using DesctopHITE.PerformanceFolder.WindowsFolder;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DesctopHITE.PerformanceFolder.PageFolder.MenuPageFolder
{
    public partial class ListMenuCashPage : Page
    {
        int personalNumberCategory;
        MenuTable menuSelection;

        public ListMenuCashPage()
        {
            try
            {
                InitializeComponent();
                AppConnectClass.connectDataBase_ACC = new DesctopHiteEntities();
            }
            catch (Exception exMenuCashPage)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                    textMessage: $"Событие ListMenuCashPage в ListMenuCashPage:\n\n " +
                    $"{exMenuCashPage.Message}");
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                personalNumberCategory = AppConnectClass.rememberTheSelectedCategory_ACC;

                var listMenuCategory = AppConnectClass.connectDataBase_ACC.MenuTable.Where(dataMenu => 
                    dataMenu.pnMenuCategory_Menu == personalNumberCategory);
                ListMenuListView.ItemsSource = listMenuCategory.ToList();
            }
            catch (Exception exMenuCashPage)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                    textMessage: $"Событие Page_IsVisibleChanged в ListMenuCashPage:\n\n " +
                    $"{exMenuCashPage.Message}");
            }
        }
        #region _SelectionChanged
        private void ListMenuListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                menuSelection = ListMenuListView.SelectedItem as MenuTable;
                Event_ChooseFood();
            }
            catch (Exception exMenuCashPage)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                    textMessage: $"Событие ListMenuListView_SelectionChanged в ListMenuCashPage:\n\n " +
                    $"{exMenuCashPage.Message}");
            }
        }
        #endregion
        #region Event_
        private void Event_ChooseFood()
        {
            try
            {
                if (menuSelection != null)
                {
                    InfoMenuCashWindow infoMenuCashWindow = new InfoMenuCashWindow(menuSelection);
                    infoMenuCashWindow.ShowDialog();

                    menuSelection = null;
                    ListMenuListView.SelectedItem = null;
                }
            }
            catch (Exception exEvent_ChooseFood)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                    textMessage: $"Событие Event_ChooseFood в ListMenuCashPage:\n\n " +
                    $"{exEvent_ChooseFood.Message}");
            }
        }
        #endregion
    }
}
