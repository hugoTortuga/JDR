using JDR.Model;
using JDR.Vue.ViewModels;
using System;
using System.Windows;

namespace JDR.Vue.Views {

	public partial class WinCharacterSheet : Window {
		public WinCharacterSheet(Character selectedPlayer) {
			DataContext = new CharacterSheetViewModel(selectedPlayer);
			InitializeComponent();
		}
	}
}
