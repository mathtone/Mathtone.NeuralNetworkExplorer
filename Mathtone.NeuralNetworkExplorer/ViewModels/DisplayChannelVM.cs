using Mathtone.MIST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathtone.NeuralNetworkExplorer.ViewModels {
	[Notifier(NotificationMode.Implicit)]
	public class DisplayChannelVM : ViewModel {
		public bool ShowRed { get; set; } = true;
		public bool ShowGreen { get; set; } = true;
		public bool ShowBlue { get; set; } = true;
		public bool ShowGrayscale { get; set; } = false;
	}
}
