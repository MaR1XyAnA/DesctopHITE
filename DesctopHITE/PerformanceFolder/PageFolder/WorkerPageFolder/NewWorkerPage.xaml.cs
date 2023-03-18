﻿using DesctopHITE.AppDateFolder.ClassFolder;
using DesctopHITE.AppDateFolder.ModelFolder;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesctopHITE.PerformanceFolder.PageFolder.WorkerPageFolder
{
    public partial class NewWorkerPage : Page
    {
        string PathImage = "";
        DateTime ToDayDate = DateTime.Today; // Получаем сегодняшнюю дату

        string MessagePassportNull;
        string MessagePlaceResidenceNull;
        string MessageMedicalBookNull;
        string MessageSnilsNull;
        string MessageINNNull;
        string MessageSalaryCardNull;
        string MessageGeneralDataNull;
        string MessageValidData;

        public NewWorkerPage()
        {
            try
            {
                InitializeComponent();
                AppConnectClass.DataBase = new DesctopHiteEntities(); // Даём взаиможействовать этой странице с базой данных
                pnGenderComboBox.ItemsSource = AppConnectClass.DataBase.GenderTable.ToList(); // Выгружаем список Гендера в pnGenderComboBox    
                pnRoleWorkerComboBox.ItemsSource = AppConnectClass.DataBase.RoleTable.ToList(); // Выгружаем список Гендера в pnRoleWorkerComboBox    
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Ошибка (Error - E-001)",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) // Еслм страница видна
        {
            if (Visibility == Visibility.Visible)
            {
                PassportToggleButton.IsChecked = true;
                PassportBorder.Visibility = Visibility.Visible;
            }
        }

        #region Click
        private void PassportToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (PassportToggleButton.IsChecked == true)
            {
                PassportBorder.Visibility = Visibility.Visible;
            }
            else
            {
                PassportBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void PlaceResidenceToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlaceResidenceToggleButton.IsChecked == true)
            {
                PlaceResidenceBorder.Visibility = Visibility.Visible;
            }
            else
            {
                PlaceResidenceBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void MedicalBookToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (MedicalBookToggleButton.IsChecked == true)
            {
                MedicalBookBorder.Visibility = Visibility.Visible;
            }
            else
            {
                MedicalBookBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void SnilsToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (SnilsToggleButton.IsChecked == true)
            {
                SnilsBorder.Visibility = Visibility.Visible;
            }
            else
            {
                SnilsBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void INNToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (INNToggleButton.IsChecked == true)
            {
                INNBorder.Visibility = Visibility.Visible;
            }
            else
            {
                INNBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void SalaryCardToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (SalaryCardToggleButton.IsChecked == true)
            {
                SalaryCardBorder.Visibility = Visibility.Visible;
            }
            else
            {
                SalaryCardBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void GeneralDataToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (GeneralDataToggleButton.IsChecked == true)
            {
                GeneralDataBorder.Visibility = Visibility.Visible;
            }
            else
            {
                GeneralDataBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void NewWorkerButton_Click(object sender, RoutedEventArgs e) // Выполняем ряд действий, после чего добавляем нового сотрудника в базу данных
        {
            try
            {
                MessagePassportNull = "";
                MessagePlaceResidenceNull = "";
                MessageMedicalBookNull = "";
                MessageSnilsNull = "";
                MessageINNNull = "";
                MessageSalaryCardNull = "";
                MessageGeneralDataNull = "";

                MessageNull();

                if (MessagePassportNull != "" || MessagePlaceResidenceNull != "" || MessageMedicalBookNull != "" || MessageSnilsNull != "" || MessageINNNull != "" || MessageSalaryCardNull != "" || MessageGeneralDataNull != "")
                {
                    MessageBox.Show(
                        MessagePassportNull + MessagePlaceResidenceNull + MessageMedicalBookNull + MessageSnilsNull + MessageINNNull + MessageSalaryCardNull + MessageGeneralDataNull,
                        "Ошибка добавления нового сотрудника (Error - E-003)",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    if (MessageValidData != "")
                    {
                        MessageBox.Show(
                            MessageValidData,
                            "Ошибка добавления нового сотрудника (Error - E-005)",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                    else
                    {
                        AddDataDatabase();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Ошибка добавления (Error - E-004)",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void NewPhotoButton_Click(object sender, RoutedEventArgs e) // При нажатии на кнопку открываем FileDialog и получаем путь к картинке
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"; // Выбираем в OpenFileDialog формат файла
            if (openFileDialog.ShowDialog() == true) // Если пользователь выбрал содержимое
            {
                PathImage = openFileDialog.FileName; // Получение пути к выбранному файлу и записываем в переменную
                UserPhotoImage.Source = new BitmapImage(new Uri(openFileDialog.FileName)); ; // Вставить фото в пользовательский элемент управления
            }
        }
        #endregion
        #region Действие
        private void AddDataDatabase() // Метод для добавления нового сотрудника в базу данных
        {
            try
            {
                PassportTable AddPassport = new PassportTable()
                {
                    Series_Passport = SeriesPassportTextBox.Text,
                    Number_Passport = NumberPassportTextBox.Text,
                    Surname_Passport = SurnamePassportTextBox.Text,
                    Name_Passport = NamePassportTextBox.Text,
                    Middlename_Passport = MiddlenamePassportTextBox.Text,
                    pnGender_Passport = (pnGenderComboBox.SelectedItem as GenderTable).PersonalNumber_Gender,
                    DateOfBrich_Passport = Convert.ToDateTime(DateOfBrichPassportTextBox.Text),
                    LocationOfBrich_Passport = LocationOfBrichPassportTextBox.Text,
                    Issued_Passport = IssuedPassportTextBox.Text,
                    DateIssued_Passport = Convert.ToDateTime(DateIssuedPassportTextBox.Text),
                    DivisionCode_Passport = DivisionCodePassportTextBox.Text
                };
                if (PathImage == "")
                {
                    AddPassport.Image_Passport = null;
                }
                else
                {
                    // Конвертация изображения в байты
                    byte[] imageData;
                    using (FileStream fs = new FileStream(PathImage, FileMode.Open, FileAccess.Read))
                    {
                        imageData = new byte[fs.Length];
                        fs.Read(imageData, 0, imageData.Length);
                    }

                    AddPassport.Image_Passport = imageData;
                }
                AppConnectClass.DataBase.PassportTable.Add(AddPassport);

                PlaceResidenceTable AddPlaceResidence = new PlaceResidenceTable()
                {
                    PersonalNumber_PlaceResidence = SeriesPassportTextBox.Text + NumberPassportTextBox.Text,
                    RegistrationDate_PlaceResidence = Convert.ToDateTime(RegistrationDatePlaceResidenceTextBox.Text),
                    Region_PlaceResidence = RegionPlaceResidenceTextBox.Text,
                    District_PlaceResidence = DistrictPlaceResidenceTextBox.Text,
                    Point_PlaceResidence = PointPlaceResidenceTextBox.Text,
                    Street_PlaceResidence = StreetPlaceResidenceTextBox.Text,
                    House_PlaceResidence = HousePlaceResidenceTextBox.Text,
                    Flat_PlaceResidence = FlatPlaceResidenceTextBox.Text
                };
                AppConnectClass.DataBase.PlaceResidenceTable.Add(AddPlaceResidence);

                MedicalBookTable AddMedicalBook = new MedicalBookTable()
                {
                    PersonalNumber_MedicalBook = PersonalNumberMedicalBookTextBox.Text,
                    Issue_MedicalBook = IssueMedicalBookTextBox.Text,
                    SNMDirector_MedicalBook = SNMDirectorMedicalBookTextBox.Text,
                    DateIssue_MedicalBook = Convert.ToDateTime(DateIssueMedicalBookTextBox.Text),
                    HomeAdress_MedicalBook = HomeAdressMedicalBookTextBox.Text,
                    Role_MedicalBook = RoleMedicalBookTextBox.Text,
                    Organization_MedicalBook = OrganizationMedicalBookTextBox.Text
                };
                AppConnectClass.DataBase.MedicalBookTable.Add(AddMedicalBook);

                SnilsTable AddSnils = new SnilsTable()
                {
                    PersonalNumber_Snils = PersonalNumberSnilsTextBox.Text,
                    DateRegistration_Snils = Convert.ToDateTime(DateRegistrationSnilsTextBox.Text)
                };
                AppConnectClass.DataBase.SnilsTable.Add(AddSnils);

                INNTable AddINN = new INNTable()
                {
                    PersonalNumber_INN = PersonalNumberINNTextBox.Text,
                    TaxAuthority_INN = TaxAuthorityINNTextBox.Text,
                    NumberTaxAuthority_INN = NumberTaxAuthorityINNTextBox.Text,
                    Date_INN = Convert.ToDateTime(DateINNTextBox.Text)
                };
                AppConnectClass.DataBase.INNTable.Add(AddINN);

                SalaryCardTable AddSalaryCard = new SalaryCardTable()
                {
                    PersonalNumber_SalaryCard = PersonalNumberSalaryCardTextBox.Text,
                    NameEnd_SalaryCard = NameEndSalaryCardTextBox.Text,
                    SurnameEng_SalaryCard = SurnameEngSalaryCardTextBox.Text,
                    YearEnd_SalaryCard = YearEndSalaryCardTextBox.Text,
                    Month_SalaryCard = MonthSalaryCardTextBox.Text,
                    Code_SalaryCard = CodeSalaryCardTextBox.Text
                };
                AppConnectClass.DataBase.SalaryCardTable.Add(AddSalaryCard);

                WorkerTabe AddWorker = new WorkerTabe()
                {
                    Phone_Worker = PhoneWorkerTextBox.Text,
                    Login_Worker = LoginWorkerTextBox.Text,
                    Email_Worker = EmailWorkerTextBox.Text,
                    Password_Worker = PasswordWorkerTextBox.Text,
                    pnRole_Worker = (pnRoleWorkerComboBox.SelectedItem as RoleTable).PersonalNumber_Role,
                    SeriesPassport_Worker = AddPassport.Series_Passport,
                    NumberPassport_Worker = AddPassport.Number_Passport,
                    pnPlaceResidence_Worker = AddPlaceResidence.PersonalNumber_PlaceResidence,
                    pnMedicalBook_Worker = AddMedicalBook.PersonalNumber_MedicalBook,
                    pnSalaryCard_Worker = AddSalaryCard.PersonalNumber_SalaryCard,
                    DateWord_Worker = ToDayDate,
                    pnStatus_Worker = 2,
                    pnINN_Worker = AddINN.PersonalNumber_INN,
                    pnSnils_Worker = AddSnils.PersonalNumber_Snils
                };
                AppConnectClass.DataBase.WorkerTabe.Add(AddWorker);

                AppConnectClass.DataBase.SaveChanges();
                MessageBox.Show(
                        "Новый сотрудник добавлен в базу данных",
                        "Сохранение",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
        }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Ошибка добавления (Error - E-002)",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
}

        private void MessageNull()
        {
            #region MessagePassportNull
            if (string.IsNullOrWhiteSpace(SeriesPassportTextBox.Text)) MessagePassportNull += "Вы не указали 'Серию' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(NumberPassportTextBox.Text)) MessagePassportNull += "Вы не указали 'Номер' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(SurnamePassportTextBox.Text)) MessagePassportNull += "Вы не указали 'Фамилию' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(NamePassportTextBox.Text)) MessagePassportNull += "Вы не указали 'Имя' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(pnGenderComboBox.Text)) MessagePassportNull += "Вы не указали 'Пол' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(DateOfBrichPassportTextBox.Text)) MessagePassportNull += "Вы не указали 'Дату рождения' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(LocationOfBrichPassportTextBox.Text)) MessagePassportNull += "Вы не указали 'Место рождения' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(IssuedPassportTextBox.Text)) MessagePassportNull += "Вы не указали 'Кем выдан' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(DateIssuedPassportTextBox.Text)) MessagePassportNull += "Вы не указали 'Дату выдачи' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(DivisionCodePassportTextBox.Text)) MessagePassportNull += "Вы не указали 'Код подразделения' в 'Паспорт'\n";
            #endregion
            #region MessagePlaceResidenceNull
            if (string.IsNullOrWhiteSpace(RegistrationDatePlaceResidenceTextBox.Text)) MessagePlaceResidenceNull += "Вы не указали 'Дату регистрации' в 'Место жительства'\n";
            if (string.IsNullOrWhiteSpace(RegionPlaceResidenceTextBox.Text)) MessagePlaceResidenceNull += "Вы не указали 'Регион' в 'Место жительства'\n";
            if (string.IsNullOrWhiteSpace(DistrictPlaceResidenceTextBox.Text)) MessagePlaceResidenceNull += "Вы не указали 'Район' в 'Место жительства'\n";
            if (string.IsNullOrWhiteSpace(PointPlaceResidenceTextBox.Text)) MessagePlaceResidenceNull += "Вы не указали 'Пункт' в 'Место жительства'\n";
            if (string.IsNullOrWhiteSpace(StreetPlaceResidenceTextBox.Text)) MessagePlaceResidenceNull += "Вы не указали 'Улицу' в 'Место жительства'\n";
            if (string.IsNullOrWhiteSpace(HousePlaceResidenceTextBox.Text)) MessagePlaceResidenceNull += "Вы не указали 'Дом' в 'Место жительства'\n";
            if (string.IsNullOrWhiteSpace(HousePlaceResidenceTextBox.Text)) MessagePlaceResidenceNull += "Вы не указали 'Квартиру' в 'Место жительства'\n";
            #endregion
            #region MessageMedicalBookNull
            if (string.IsNullOrWhiteSpace(PersonalNumberMedicalBookTextBox.Text)) MessageMedicalBookNull += "Вы не указали 'Номер' в 'Медецинская книжка'\n";
            if (string.IsNullOrWhiteSpace(IssueMedicalBookTextBox.Text)) MessageMedicalBookNull += "Вы не указали 'Личная медецинская книжка выдана' в 'Медецинская книжка'\n";
            if (string.IsNullOrWhiteSpace(SNMDirectorMedicalBookTextBox.Text)) MessageMedicalBookNull += "Вы не указали 'ФИО руководителя' в 'Медецинская книжка'\n";
            if (string.IsNullOrWhiteSpace(DateIssueMedicalBookTextBox.Text)) MessageMedicalBookNull += "Вы не указали 'Дату выдачи' в 'Медецинская книжка'\n";
            if (string.IsNullOrWhiteSpace(HomeAdressMedicalBookTextBox.Text)) MessageMedicalBookNull += "Вы не указали 'Домашний адресс' в 'Медецинская книжка'\n";
            if (string.IsNullOrWhiteSpace(RoleMedicalBookTextBox.Text)) MessageMedicalBookNull += "Вы не указали 'Должность' в 'Медецинская книжка'\n";
            if (string.IsNullOrWhiteSpace(OrganizationMedicalBookTextBox.Text)) MessageMedicalBookNull += "Вы не указали 'Организацию (индивидуальный предприниматель)' в 'Медецинская книжка'\n";
            #endregion
            #region MessageSnilsNull
            if (string.IsNullOrWhiteSpace(PersonalNumberSnilsTextBox.Text)) MessageSnilsNull += "Вы не указали 'Номер' в 'СНИЛС'\n";
            if (string.IsNullOrWhiteSpace(DateRegistrationSnilsTextBox.Text)) MessageSnilsNull += "Вы не указали 'Дату выдачи' в 'СНИЛС'\n";
            #endregion
            #region MessageINNNull
            if (string.IsNullOrWhiteSpace(PersonalNumberINNTextBox.Text)) MessageINNNull += "Вы не указали 'Номер' в 'ИНН'\n";
            if (string.IsNullOrWhiteSpace(TaxAuthorityINNTextBox.Text)) MessageINNNull += "Вы не указали 'Налоговый огран' в 'ИНН'\n";
            if (string.IsNullOrWhiteSpace(NumberTaxAuthorityINNTextBox.Text)) MessageINNNull += "Вы не указали 'Номер налогового органа' в 'ИНН'\n";
            if (string.IsNullOrWhiteSpace(DateINNTextBox.Text)) MessageINNNull += "Вы не указали 'Дату выдачи' в 'ИНН'\n";
            #endregion
            #region MessageSalaryCardNull
            if (string.IsNullOrWhiteSpace(PersonalNumberSalaryCardTextBox.Text)) MessageSalaryCardNull += "Вы не указали 'Номер' в 'Заработная карта'\n";
            if (string.IsNullOrWhiteSpace(NameEndSalaryCardTextBox.Text)) MessageSalaryCardNull += "Вы не указали 'Имя (Eng)' в 'Заработная карта'\n";
            if (string.IsNullOrWhiteSpace(SurnameEngSalaryCardTextBox.Text)) MessageSalaryCardNull += "Вы не указали 'Фамилия (Eng)' в 'Заработная карта'\n";
            if (string.IsNullOrWhiteSpace(YearEndSalaryCardTextBox.Text)) MessageSalaryCardNull += "Вы не указали 'Год' в 'Заработная карта'\n";
            if (string.IsNullOrWhiteSpace(MonthSalaryCardTextBox.Text)) MessageSalaryCardNull += "Вы не указали 'Месяц' в 'Заработная карта'\n";
            if (string.IsNullOrWhiteSpace(CodeSalaryCardTextBox.Text)) MessageSalaryCardNull += "Вы не указали 'Код' в 'Заработная карта'\n";
            #endregion
            #region MessageGeneralDataNull
            if (string.IsNullOrWhiteSpace(PhoneWorkerTextBox.Text)) MessageGeneralDataNull += "Вы не указали 'Номер телефона' в 'Общие данные'\n";
            if (string.IsNullOrWhiteSpace(LoginWorkerTextBox.Text)) MessageGeneralDataNull += "Вы не указали 'Login' в 'Общие данные'\n";
            if (string.IsNullOrWhiteSpace(EmailWorkerTextBox.Text)) MessageGeneralDataNull += "Вы не указали 'Электронную почту' в 'Общие данные'\n";
            if (string.IsNullOrWhiteSpace(PasswordWorkerTextBox.Text)) MessageGeneralDataNull += "Вы не указали 'Password' в 'Общие данные'\n";
            if (string.IsNullOrWhiteSpace(pnRoleWorkerComboBox.Text)) MessageGeneralDataNull += "Вы не указали 'Должность' в 'Общие данные'\n";
            #endregion
            #region MessageValidData
            if (PhoneWorkerTextBox.Text.Length > 0) MessageGeneralDataNull += "Вы не указали 'Номер телефона' в 'Общие данные'\n";
            #endregion
        }
        #endregion
        #region ValidData
        private void DateValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex DateRegex = new Regex("[^0-9/.]");
            e.Handled = DateRegex.IsMatch(e.Text);
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex NumberRegex = new Regex("[^0-9,]");
            e.Handled = NumberRegex.IsMatch(e.Text);
        }
        private void DivisionCodeValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex DivisionCodeRegex = new Regex("[^0-9,]");
            e.Handled = DivisionCodeRegex.IsMatch(e.Text);
        }
        #endregion
    }
}
