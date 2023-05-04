﻿///----------------------------------------------------------------------------------------------------------
/// Просто главное окно, которое нужно для того, чтоб туда выгружать страницы, с
///     которыми уже и взаимодействует пользователь.
/// На данный момент, окно представляет из себя пустышку, с 2 "Frame" и всёёё....
///----------------------------------------------------------------------------------------------------------

using DesctopHITE.AppDateFolder.ClassFolder;
using DesctopHITE.PerformanceFolder.PageFolder.PanelMenuFolder;
using System;
using System.Windows;

namespace DesctopHITE.PerformanceFolder.WindowsFolder
{
    public partial class MainCashWindow : Window
    {
        public MainCashWindow()
        {
            try
            {
                InitializeComponent();
                FrameNavigationClass.MunuCash_FNC = MenuCashFrame;
                FrameNavigationClass.BodyCash_FNC = BodyCashFrame;
            }
            catch (Exception ex)
            {
                MessageBoxClass.ExceptionMessage(
                        textMessage: $"Событие MainCashWindow в MainCashWindow:\n\n " +
                        $"{ex.Message}");
            }
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (Visibility == Visibility.Visible)
                {
                    FrameNavigationClass.MunuCash_FNC.Navigate(new MenuCashPage());
                }
            }
            catch (Exception exWindow_IsVisibleChanged)
            {
                MessageBoxClass.ExceptionMessage(
                        textMessage: $"Событие Window_IsVisibleChanged в MainCashWindow:\n\n " +
                        $"{exWindow_IsVisibleChanged.Message}");
            }
        }
    }
}
