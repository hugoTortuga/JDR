using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JDR.Vue.ViewModels {
	public class MainViewModel : ViewModelBase {


        private ContentControl _CurrentControl;
        public ContentControl CurrentControl {
            get {
                return (_CurrentControl);
            }
            set {
                _CurrentControl = value;
                OnPropertyChanged(nameof(CurrentControl));
            }
        }

        public MainViewModel()
        {
            CurrentControl = new UCMainMenu();
        }

    }
}
