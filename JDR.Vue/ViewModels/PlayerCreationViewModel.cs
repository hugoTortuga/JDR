using JDR.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Vue.ViewModels
{
    public class PlayerCreationViewModel : ViewModelBase
    {

        public bool WasValidated { get; set; }

        private Player _Player;
        public Player Player
        {
            get
            {
                return (_Player);
            }
            set
            {
                _Player = value;
                OnPropertyChanged(nameof(Player));
            }
        }

        private ObservableCollection<Race> _Races;
        public ObservableCollection<Race> Races
        {
            get
            {
                return (_Races);
            }
            set
            {
                _Races = value;
                OnPropertyChanged(nameof(Races));
            }
        }

        private ObservableCollection<MagicCategory> _Spells;
        public ObservableCollection<MagicCategory> Spells
        {
            get
            {
                return (_Spells);
            }
            set
            {
                _Spells = value;
                OnPropertyChanged(nameof(Spells));
            }
        }


        private MagicCategory _SelectedSpell;
        public MagicCategory SelectedSpell
        {
            get
            {
                return (_SelectedSpell);
            }
            set
            {
                _SelectedSpell = value;
                OnPropertyChanged(nameof(SelectedSpell));
            }
        }

        private ObservableCollection<InventoryItem> _Items;
        public ObservableCollection<InventoryItem> Items
        {
            get
            {
                return (_Items);
            }
            set
            {
                _Items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        private InventoryItem _SelecterItem;
        public InventoryItem SelecterItem
        {
            get
            {
                return (_SelecterItem);
            }
            set
            {
                _SelecterItem = value;
                OnPropertyChanged(nameof(SelecterItem));
            }
        }

        public PlayerCreationViewModel()
        {
            WasValidated = false;
            Player = new Player();
        }

        public void AddItem() { }
        public void AddSpell() { }
        public void SelectAPortrait() { }
        public void SelectAToken() { }
        public void Confirm() { }

    }
}
