﻿using JDR.Core;
using JDR.Model;
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
using System.Windows.Shapes;

namespace JDR.Vue.Views
{
    /// <summary>
    /// Logique d'interaction pour WinMusicSelection.xaml
    /// </summary>
    public partial class WinMusicSelection : Window
    {
        public WinMusicSelection(IMusicStorage musicStorage, IList<Music> musics)
        {
            DataContext = new MusicSelectionViewModel(musicStorage, musics, new Action(Close));
            InitializeComponent();
        }
    }
}
