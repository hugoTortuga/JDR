using JDR.Core;
using JDR.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Vue.ViewModels
{
    public class CharacterCreationViewModel : ViewModelBase
    {

        public bool WasValidated { get; set; }

        private Character _Character;
        public Character Character
        {
            get
            {
                return (_Character);
            }
            set
            {
                _Character = value;
                OnPropertyChanged(nameof(Character));
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

        private ObservableCollection<Spell> _Spells;
        public ObservableCollection<Spell> Spells
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


        private Spell _SelectedSpell;
        public Spell SelectedSpell
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
        public InventoryItem SelectedItem
        {
            get
            {
                return (_SelecterItem);
            }
            set
            {
                _SelecterItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }


        private byte[] _PortraitData;
        public byte[] PortraitData
        {
            get
            {
                return (_PortraitData);
            }
            set
            {
                _PortraitData = value;
                OnPropertyChanged(nameof(PortraitData));
            }
        }


        private byte[] _TokenData;
        public byte[] TokenData
        {
            get
            {
                return (_TokenData);
            }
            set
            {
                _TokenData = value;
                OnPropertyChanged(nameof(TokenData));
            }
        }


        private ObservableCollection<InventoryItem> _CharacterItems;
        public ObservableCollection<InventoryItem> CharacterItems
        {
            get
            {
                return (_CharacterItems);
            }
            set
            {
                _CharacterItems = value;
                OnPropertyChanged(nameof(CharacterItems));
            }
        }

        private ObservableCollection<Spell> _CharacterSpells;
        public ObservableCollection<Spell> CharacterSpells
        {
            get
            {
                return (_CharacterSpells);
            }
            set
            {
                _CharacterSpells = value;
                OnPropertyChanged(nameof(CharacterSpells));
            }
        }





        private Action ClosingAction;

        public CharacterCreationViewModel(GameCore gameCore, Action closingAction)
        {
            WasValidated = false;
            CharacterSpells = new ObservableCollection<Spell>();
            CharacterItems = new ObservableCollection<InventoryItem>();
            Character = new Character();
            Spells = new ObservableCollection<Spell>();
            Items = new ObservableCollection<InventoryItem>(gameCore.GetAllItems());
            Races = new ObservableCollection<Race>(gameCore.GetAllRaces());
            ClosingAction = closingAction;
        }

        public void AddItem()
        {
            if (SelectedItem != null && !Character.Inventory.Objects.Contains(SelectedItem))
            {
                Character.Inventory.Objects.Add(SelectedItem);
                CharacterItems = new ObservableCollection<InventoryItem>(Character.Inventory.Objects);
                SelectedItem = null;
            }
        }
        public void AddSpell()
        {
            if (SelectedSpell != null && !Character.Spells.Contains(SelectedSpell))
            {
                Character.Spells.Add(SelectedSpell);
                CharacterSpells = new ObservableCollection<Spell>(Character.Spells);
                SelectedSpell = null;
            }
        }
        public void SelectAPortrait()
        {
            var portraitFilePath = GetFilePath();
            if (string.IsNullOrEmpty(portraitFilePath)) return;
            Character.Illustration = new Illustration
            (
                new FileInfo(portraitFilePath).Extension,
                Path.GetFileName(portraitFilePath)
            );
            PortraitData = Character.Illustration.Content;
        }
        public void SelectAToken()
        {
            var tokenFilePath = GetFilePath();
            if (string.IsNullOrEmpty(tokenFilePath)) return;
            Character.Token = new Illustration
            (
                new FileInfo(tokenFilePath).Extension,
                Path.GetFileName(tokenFilePath)
            );
            TokenData = Character.Token.Content;
        }
        public void Confirm()
        {
            WasValidated = true;
            ClosingAction.Invoke();
        }

        private string GetFilePath()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == true)
                return dialog.FileName;
            else return null;

        }

    }
}
