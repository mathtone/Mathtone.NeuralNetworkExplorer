using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathtone.NeuralNetworks.Neurons {
	/// <summary>
	/// A neuron that encapsulates an activation function.
	/// </summary>
	/// <seealso cref="Mathtone.NeuralNetworks.Neurons.NeuronBase" />
	public abstract class ActivatorNeuron : NeuronBase {

		/// <summary>
		/// Gets or sets the activation threshold.
		/// </summary>
		/// <value>Activation threshold value.</value>
		public double Threshold { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ActivatorNeuron"/> class.
		/// </summary>
		/// <param name="inputCount">The number of input values to this neuron.</param>
		public ActivatorNeuron(int inputCount = 1) :
			base(inputCount) {
		}

		/// <summary>
		/// Computes output value as the result off the activation function for the sum of tyhe input values.
		/// </summary>
		/// <param name="input">Input values.</param>
		/// <returns>Result of activation function.</returns>
		protected override double ComputeOutput(double[] input) {

			var sum = 0.0;

			// compute weighted sum of inputs
			
			for (var i = 0; i < this.InputWeights.Length; i++) {
				sum += InputWeights[i] * input[i];
			}
			sum += Threshold;

			return Activate(sum);
		}

		/// <summary>
		/// Processes the sum of neuron inputs as an activation function.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>Result of activation function.</returns>
		protected abstract double Activate(double input);
	}
}