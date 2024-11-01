using System.Windows;
using AdminWPFWork.View; // Додайте цей простір імен для використання вашого View

namespace AdminWPFWork.Windows
{
    /// <summary>
    /// Interaction logic for WindowMain.xaml
    /// </summary>
    public partial class WindowMain : Window
    {
        public WindowMain()
        {
            InitializeComponent();
            //MainFrame.Navigate(new UserView()); // Перехід на початкову вкладку
            //var userView = new UserView();
            //userView.Show(); // Відкриття UserView як окремого вікна під час запуску
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Закриваємо програму
        }

        private void ManageUsersMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var userView = new UserView();
            userView.Show(); // Відкриття UserView як окремого вікна
            //MainFrame.Navigate(new UserView()); // Перехід на вкладку управління користувачами
        }

        //private void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    MainFrame.Navigate(new SettingsView()); // Перехід на вкладку налаштувань
        //}
    }
}
