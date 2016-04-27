using Mathtone.MIST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathtone.NeuralNetworkExplorer.ViewModels {
	[Notifier(NotificationMode.Implicit)]
	public class ApplicationVM : ViewModel {

		public string Title { get; protected set; } = "C# Neural Networks";

		public ClusteringDemoVM ClusteringDemo { get; } = new ClusteringDemoVM();
	}
}