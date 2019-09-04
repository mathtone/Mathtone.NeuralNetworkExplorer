using Mathtone.MIST;
using Prism.Mvvm;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mathtone.NeuralNetworkExplorer.ViewModels {

	public class ViewModel : BindableBase {

		[NotifyTarget]
		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			base.OnPropertyChanged(propertyName);
		}
	}
}
