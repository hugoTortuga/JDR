using JDR.Vue.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace JDR.Vue.Utils
{
    public class CustomPolygon : ViewModelBase
    {
        private PointCollection _Points;
        private bool _IsSelected;

        public PointCollection Points
        {
            get { return _Points; }
            set
            {
                _Points = value;
                OnPropertyChanged(nameof(Points));
            }
        }

        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
    }
}
