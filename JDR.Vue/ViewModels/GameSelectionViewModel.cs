using JDR.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Vue.ViewModels
{
    public class GameSelectionViewModel : ViewModelBase
    {

        private ObservableCollection<Game> _Games;
        public ObservableCollection<Game> Games
        {
            get
            {
                return _Games;
            }
            set
            {
                _Games = value;
                OnPropertyChanged(nameof(Games));
            }
        }

        private Game _SelectedGame;
        public Game SelectedGame
        {
            get { return _SelectedGame; }
            set
            {
                _SelectedGame = value; 
                OnPropertyChanged(nameof(SelectedGame));
            }
        }

        private MainWindow _MainWindow;
        public GameSelectionViewModel(MainWindow window, IList<Game> games)
        {
            _MainWindow = window;
            Games = new ObservableCollection<Game>(games);
        }

        public void GameSelected()
        {
            _MainWindow.OpenGame(SelectedGame);
        }


        public void BackToMenu()
        {
            _MainWindow.BackToMenu();
        }

    }
}
