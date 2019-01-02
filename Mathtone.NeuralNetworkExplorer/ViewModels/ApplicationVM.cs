using Mathtone.MIST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathtone.NeuralNetworkExplorer.ViewModels {

	public interface IApplicationContext {

	}

	[Notifier(NotificationMode.Implicit)]
	public class ApplicationVM : ViewModel, IApplicationContext {

		public string Title { get; protected set; } = "C# Neural Networks";

		public ClusteringDemoVM ClusteringDemo { get; protected set; }

		public ApplicationVM() {
			ClusteringDemo = new ClusteringDemoVM(this);
		}
	}
}