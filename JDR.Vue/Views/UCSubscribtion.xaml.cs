using JDR.Vue.ViewModels;
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
    /// Logique d'interaction pour UCSubscribtion.xaml
    /// </summary>
    public partial class UCSubscribtion : UserControl
    {
        private MainWindow _MainWindow;
        private ConnectionViewModel _ViewModel;

        public UCSubscribtion(MainWindow mainWindow)
        {
            _MainWindow = mainWindow;
            _ViewModel = new ConnectionViewModel();
            DataContext = _ViewModel;
            InitializeComponent();
        }

        private void GoToConnection(object sender, RoutedEventArgs e)
        {
            _MainWindow.GoToConnection();
        }

        private void Subscribe(object sender, RoutedEventArgs e)
        {
            var success = _ViewModel.TryToSubscribe();
            if (success) _MainWindow.GoToConnection();

        }
    }
}
