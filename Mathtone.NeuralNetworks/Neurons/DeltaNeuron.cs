using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathtone.NeuralNetworks.Neurons {
	/// <summary>
	/// Outputs a difference (delta) between current and new input weights.
	/// </summary>
	/// <seealso cref="Mathtone.NeuralNetworks.Neurons.NeuronBase" />
	public class DeltaNeuron : NeuronBase {

		/// <summary>
		/// Initializes a new instance of the <see cref="DeltaNeuron"/> class.
		/// </summary>
		/// <param name="inputs">Number of input weights.</param>
		public DeltaNeuron(int inputs) :
			base(inputs) { }

		/// <summary>
		/// Computes the output as a difference (delta) between current and new input weights.
		/// </summary>
		/// <param name="inputs">The inputs.</param>
		/// <returns>System.Double.</returns>
		protected override double ComputeOutput(double[] inputs) {
			var rtn = 0.0;

			// compute difference between inputs and weights
			for (var i = 0; i < InputWeights.Length; i++) {
				rtn += Math.Abs(InputWeights[i] - inputs[i]);
			}
			return rtn;
		}
	}
}