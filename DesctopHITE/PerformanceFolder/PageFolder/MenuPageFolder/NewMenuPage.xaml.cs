﻿///----------------------------------------------------------------------------------------------------------
///
///----------------------------------------------------------------------------------------------------------

using DesctopHITE.AppDateFolder.ClassFolder;
using DesctopHITE.AppDateFolder.ModelFolder;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DesctopHITE.PerformanceFolder.PageFolder.MenuPageFolder
{
    public partial class NewMenuPage : Page
    {
        List<IngredientsTable> ingredientsList = new List<IngredientsTable>();

        byte[] imageData;

        int personlNumberMenu;

        string pathImage = "";
        string messageNull;
        string messageValidData;

        public NewMenuPage(MenuTable menuTable)
        {
            try
            {
                InitializeComponent();

                if (menuTable != null)
                {
                    DataContext = menuTable;
                    personlNumberMenu = menuTable.PersonalNumber_Menu;

                    AppConnectClass.connectDataBase_ACC.MenuTable.Include(ingredients => ingredients.IngredientsTable).Load();
                    var ingredientsMenu = AppConnectClass.connectDataBase_ACC.MenuTable.Find(personlNumberMenu).IngredientsTable;

                    SelectionIngredientsListListView.Items.Refresh();
                    ingredientsList = ingredientsMenu.ToList();

                    SelectionIngredientsListListView.ItemsSource = ingredientsMenu.ToList();
                }
                else
                {
                    ImageSource userImage = new BitmapImage(new Uri("/ContentFolder/ImageFolder/NoImage.png", UriKind.RelativeOrAbsolute)); // Вывод стандартного фото
                    MenuPhotoImage.Source = userImage;
                }

                AppConnectClass.connectDataBase_ACC = new DesctopHiteEntities();
                pnCategoryMenuComboBox.ItemsSource = AppConnectClass.connectDataBase_ACC.MenuCategoryTable.ToList();
                pnSystemSIComboBox.ItemsSource = AppConnectClass.connectDataBase_ACC.SystemSITable.ToList();
                AllIngredientsListListView.ItemsSource = AppConnectClass.connectDataBase_ACC.IngredientsTable.ToList();
                AllIngredientsListListView.Items.SortDescriptions.Add(new SortDescription("Name_Ingredients", ListSortDirection.Ascending));
            }
            catch (Exception exNewMenuPage)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                        textMessage: $"Событие NewMenuPage в NewMenuPage:\n\n " +
                        $"{exNewMenuPage.Message}");
            }
        }

        #region _Click
        private void NewMenuButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                messageNull = "";
                Event_MessageNull();

                if (messageNull != "")
                {
                    MessageBoxClass.FailureMessageBox_MBC(textMessage: $"Ошибка добавления нового блюда\n\n{messageNull}");
                }
                else
                {
                    messageValidData = "";
                    Event_MessageNull();

                    if (messageValidData != "")
                    {
                        MessageBoxClass.FailureMessageBox_MBC(textMessage: $"Ошибка добавления нового блюда\n\n{messageValidData}");
                    }
                    else
                    {
                        Event_NewDataMenu();
                    }
                }
            }
            catch (Exception exNewMenuButton_Click)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                        textMessage: $"Событие NewMenuButton_Click в NewMenuPage:\n\n " +
                        $"{exNewMenuButton_Click.Message}");
            }
        }

        private void NewMenuImageButton_Click(object sender, RoutedEventArgs e) // При нажатии на кнопку открываем FileDialog и получаем путь к картинке
        {
            try
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"; // Выбираем в OpenFileDialog формат файла

                if (openFileDialog.ShowDialog() == true)
                {
                    pathImage = openFileDialog.FileName;
                    MenuPhotoImage.Source = new BitmapImage(new Uri(openFileDialog.FileName)); // Вставить фото в пользовательский элемент управления
                }
            }
            catch (Exception exNewMenuImageButton_Click)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                         textMessage: $"Событие NewMenuImageButton_Click в NewMenuPage:\n\n " +
                         $"{exNewMenuImageButton_Click.Message}");
            }
        }
        #endregion
        #region Event_
        public void Event_NewDataMenu() // Добавление нового меню в базу данных
        {
            try
            {
                // Объявляем таблицы
                var addEditMenuTable = new MenuTable();
                var addEditImageMenuTable = new ImageMenuTable();

                // Работа с основной таблицей меню
                addEditMenuTable.Name_Menu = NameMenuTextBox.Text;
                addEditMenuTable.Description_Menu = DescriptionMenuTextBox.Text;
                addEditMenuTable.pnMenuCategory_Menu = (pnCategoryMenuComboBox.SelectedItem as MenuCategoryTable).PersonalNumber_MenuCategory;
                addEditMenuTable.pnSystemSI = (pnSystemSIComboBox.SelectedItem as SystemSITable).PersonalNumber_SystemSI;
                addEditMenuTable.Prise_Menu = Convert.ToInt32(PriseMenuTextBox.Text);
                addEditMenuTable.Weight_Menu = Convert.ToInt32(WeightMenuTextBox.Text);

                if (DataContext != null)
                {
                    addEditMenuTable.PersonalNumber_Menu = personlNumberMenu;
                }

                // Работа с фото
                if (pathImage != "")
                {
                    // Конвертация изображения в байты
                    using (FileStream fs = new FileStream(pathImage, FileMode.Open, FileAccess.Read))
                    {
                        imageData = new byte[fs.Length];
                        fs.Read(imageData, 0, imageData.Length);
                    }
                    if (DataContext != null)
                    {
                        addEditImageMenuTable.PersonalNumber_ImageMenu = addEditMenuTable.pnImageMunu_Menu;
                    }

                    addEditImageMenuTable.Name_ImageMenu = $"{NameMenuTextBox.Text}";
                    addEditImageMenuTable.Image_ImageMenu = imageData;
                    AppConnectClass.connectDataBase_ACC.ImageMenuTable.AddOrUpdate(addEditImageMenuTable);

                    addEditMenuTable.pnImageMunu_Menu = addEditImageMenuTable.PersonalNumber_ImageMenu;
                }

                if (DataContext == null)
                {
                    if (pathImage == "")
                    {
                        addEditMenuTable.pnImageMunu_Menu = 404;
                    }
                }

                AppConnectClass.connectDataBase_ACC.MenuTable.AddOrUpdate(addEditMenuTable);
                AppConnectClass.connectDataBase_ACC.SaveChanges();

                personlNumberMenu = addEditMenuTable.PersonalNumber_Menu;

                Event_SelectedIngredients();
            }
            catch (Exception exEvent_NewDataMenu)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                         textMessage: $"Событие Event_NewDataMenu в NewMenuPage:\n\n " +
                         $"{exEvent_NewDataMenu.Message}");
            }
        }

        private void Event_SelectedIngredients() // Добавление списка в связь многие ко многим
        {
            try
            {
                var objectMenu = AppConnectClass.connectDataBase_ACC.MenuTable.Find(personlNumberMenu);

                // Цикл для того, чтоб добавить несколько элементов в таблицу
                foreach (var ingredient in SelectionIngredientsListListView.Items)
                {
                    var objectListIngredients = ingredient as IngredientsTable;
                    objectMenu.IngredientsTable.Add(objectListIngredients);
                }

                AppConnectClass.connectDataBase_ACC.SaveChanges();

                var nameMenu = AppConnectClass.connectDataBase_ACC.MenuTable.Find(personlNumberMenu);
                MessageBoxClass.GoodMessageBox_MBC(textMessage: $"{nameMenu.Name_Menu} успешно добавлено");
                FrameNavigationClass.bodyMenu_FNC.Navigate(new NewMenuPage(null));
            }
            catch (Exception exEvent_SelectedIngredients)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                        textMessage: $"Событие Event_SelectedIngredients в NewMenuPage:\n\n " +
                        $"{exEvent_SelectedIngredients.Message}");
            }
        }

        private void Event_MessageNull() // Event_ на проверки полей на валидность данных 
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NameMenuTextBox.Text)) messageNull += "Вы не указали 'Название'\n";
                if (string.IsNullOrWhiteSpace(DescriptionMenuTextBox.Text)) messageNull += "Вы не указали 'Описание'\n";
                if (string.IsNullOrWhiteSpace(pnCategoryMenuComboBox.Text)) messageNull += "Вы не указали 'Категорию'\n";
                if (string.IsNullOrWhiteSpace(PriseMenuTextBox.Text)) messageNull += "Вы не указали 'Стоимость'\n";
                if (string.IsNullOrWhiteSpace(WeightMenuTextBox.Text)) messageNull += "Вы не указали 'Вес (грамм)'";

                if (NameMenuTextBox.Text.Length <= 1) messageValidData += "'Название' не может быть меньше или быть равным 1 символу\n";
                if (DescriptionMenuTextBox.Text.Length <= 5) messageValidData += "'Описание' не может быть меньше или быть равным 5 символам\n";
                if (pnCategoryMenuComboBox.Text == "") messageValidData += "'Категория' не выбрана\n";
                if (PriseMenuTextBox.Text.Length <= 1) messageValidData += "'Стоимость' не может быть меньше или быть равным 1 символу\n";
                if (WeightMenuTextBox.Text.Length <= 1) messageValidData += "'Вес (грамм)' не может быть меньше или быть равным 1 символу\n";
            }
            catch (Exception exEvent_MessageNull)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                        textMessage: $"Событие Event_MessageNull в NewMenuPage:\n\n " +
                        $"{exEvent_MessageNull.Message}");
            }
        }
        #endregion
        #region _MouseDoubleClick _PreviewKeyDown
        private void AllIngredientsListListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var allIngredients = (IngredientsTable)AllIngredientsListListView.SelectedItem; // Получаем информацию

                if (allIngredients != null)
                {
                    if (!ingredientsList.Contains(allIngredients))
                    {
                        ingredientsList.Add(allIngredients);
                    }
                    else
                    {
                        ingredientsList.Remove(allIngredients);
                    }

                    SelectionIngredientsListListView.ItemsSource = ingredientsList;

                    AllIngredientsListListView.Items.SortDescriptions.Add(new SortDescription("Name_Ingredients", ListSortDirection.Ascending));
                    SelectionIngredientsListListView.Items.SortDescriptions.Add(new SortDescription("Name_Ingredients", ListSortDirection.Ascending));
                }
            }
            catch (Exception exAllIngredientsListListView_MouseDoubleClick)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                        textMessage: $"Событие AllIngredientsListListView_MouseDoubleClick в NewMenuPage:\n\n " +
                        $"{exAllIngredientsListListView_MouseDoubleClick.Message}");
            }
        }

        private void SelectionIngredientsListListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var selectionIngredients = (IngredientsTable)SelectionIngredientsListListView.SelectedItem; // Получаем информацию

                if (selectionIngredients != null)
                {
                    ingredientsList.Remove(selectionIngredients);
                    SelectionIngredientsListListView.ItemsSource = ingredientsList;

                    AllIngredientsListListView.Items.SortDescriptions.Add(new SortDescription("Name_Ingredients", ListSortDirection.Ascending));
                    SelectionIngredientsListListView.Items.SortDescriptions.Add(new SortDescription("Name_Ingredients", ListSortDirection.Ascending));
                }
            }
            catch (Exception exSelectionIngredientsListListView_MouseDoubleClick)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                        textMessage: $"Событие SelectionIngredientsListListView_MouseDoubleClick в NewMenuPage:\n\n " +
                        $"{exSelectionIngredientsListListView_MouseDoubleClick.Message}");
            }
        }

        private void CtrlV_PreviewKeyDown(object sender, KeyEventArgs e) // Запретить использовать Ctrl + v в некоторых TextBox
        {
            try
            {
                if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
                {
                    e.Handled = true;
                }
            }
            catch (Exception exCtrlV_PreviewKeyDown)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                         textMessage: $"Событие CtrlV_PreviewKeyDown в NewMenuPage:\n\n " +
                         $"{exCtrlV_PreviewKeyDown.Message}");
            }
        }
        #endregion
        #region ValidData // Просто для валидность данных (В одних TextBox разрешить писать только цифры и т.д.)
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Regex NumberRegex = new Regex("[^0-9]");
                e.Handled = NumberRegex.IsMatch(e.Text);
            }
            catch (Exception exNumberValidationTextBox)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                        textMessage: $"Событие NumberValidationTextBox в NewMenuPage:\n\n " +
                        $"{exNumberValidationTextBox.Message}");
            }
        }
        private void PriseValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Regex NumberRegex = new Regex("[^0-9]");
                e.Handled = NumberRegex.IsMatch(e.Text);
            }
            catch (Exception exPriseValidationTextBox)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                        textMessage: $"Событие PriseValidationTextBox в NewMenuPage:\n\n " +
                        $"{exPriseValidationTextBox.Message}");
            }
        }
        #endregion

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameNavigationClass.bodyMenu_FNC.Navigate(new ListMenuPage());
            }
            catch (Exception exGoBackButton_Click)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                        textMessage: $"Событие GoBackButton_Click в NewMenuPage:\n\n " +
                        $"{exGoBackButton_Click.Message}");
            }
        }

        private void NewIngridientToggleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NewIngridientToggleButton.IsChecked == true)
                {
                    AllIngridientBorder.Visibility = Visibility.Visible;
                    NewIngridientToggleButton.Content = "Скрыть список всех ингредиентов";
                    NewIngridientToggleButton.ToolTip = "Скрыть список всех ингредиентов";
                }
                else
                {
                    AllIngridientBorder.Visibility = Visibility.Collapsed;

                    NewIngridientToggleButton.Content = "Показать список всех ингредиентов";
                    NewIngridientToggleButton.ToolTip = "Показать список всех ингредиентов";
                }
            }
            catch (Exception exNewIngridientToggleButton_Click)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                        textMessage: $"Событие NewIngridientToggleButton_Click в NewMenuPage:\n\n " +
                        $"{exNewIngridientToggleButton_Click.Message}");
            }
        }
    }
}
