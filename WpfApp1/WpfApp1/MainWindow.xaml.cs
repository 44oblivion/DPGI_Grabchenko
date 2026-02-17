using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeCommands();
        }

        // Метод для реєстрації команд
        private void InitializeCommands()
        {
            // 1. Команда Save (для кнопки ОК)
            CommandBinding saveCommand = new CommandBinding(ApplicationCommands.Save, execute_Save, canExecute_Save);
            this.CommandBindings.Add(saveCommand);

            // 2. Команда Open (для кнопки Відкрити)
            CommandBinding openCommand = new CommandBinding(ApplicationCommands.Open, execute_Open, canExecute_Open);
            this.CommandBindings.Add(openCommand);

            // 3. Команда Delete (для кнопки Стерти)
            CommandBinding deleteCommand = new CommandBinding(ApplicationCommands.Delete, execute_Delete, canExecute_Delete);
            this.CommandBindings.Add(deleteCommand);
        }

        //Логіка Save
        void canExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            // Кнопка активна, тільки якщо в полі є текст
            if (txtEditor != null && txtEditor.Text.Trim().Length > 0)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        void execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                File.WriteAllText("myFile.txt", txtEditor.Text);
                MessageBox.Show("Файл збережено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження: " + ex.Message);
            }
        }

        //Логіка Open
        void canExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true; // Завжди доступна
        }

        void execute_Open(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (File.Exists("myFile.txt"))
                {
                    txtEditor.Text = File.ReadAllText("myFile.txt");
                }
                else
                {
                    MessageBox.Show("Файл не знайдено.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка відкриття: " + ex.Message);
            }
        }

        //логіка Delete
        void canExecute_Delete(object sender, CanExecuteRoutedEventArgs e)
        {
            // Активна, якщо є текст
            if (txtEditor != null && txtEditor.Text.Length > 0)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        void execute_Delete(object sender, ExecutedRoutedEventArgs e)
        {
            txtEditor.Clear();
        }
    }
}