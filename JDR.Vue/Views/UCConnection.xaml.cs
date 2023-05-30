using JDR.Vue.ViewModels;
using NAudio.CoreAudioApi;
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

namespace JDR.Vue.Views
{
    /// <summary>
    /// Logique d'interaction pour UCConnection.xaml
    /// </summary>
    public partial class UCConnection : UserControl
    {
        private MainWindow _MainWindow;
        private ConnectionViewModel _ViewModel;
        public UCConnection(MainWindow window)
        {
            _ViewModel = new ConnectionViewModel();
            DataContext = _ViewModel;
            _MainWindow = window;
            InitializeComponent();
        }

        private void GoToSubscribtion(object sender, RoutedEventArgs e)
        {
            _MainWindow.GoToSubscribtion();
        }

        private void Connect(object sender, RoutedEventArgs e)
        {
            var success = _ViewModel.TryToConnect();
            if (success) _MainWindow.GoToMainMenu();
        }
    }
}
