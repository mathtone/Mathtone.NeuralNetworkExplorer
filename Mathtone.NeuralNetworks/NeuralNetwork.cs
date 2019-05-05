using Mathtone.NeuralNetworks.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathtone.NeuralNetworks {
	/// <summary>
	/// A neural network.
	/// </summary>
	public class NeuralNetwork {

		/// <summary>
		/// Geta the layers present in this network.
		/// </summary>
		/// <value>The layers.</value>
		public IList<Layer> Layers { get; }

		/// <summary>
		/// Gets the output of th elast layer.
		/// </summary>
		/// <value>The output.</value>
		public double[] Output => Layers.Last().Output;

		/// <summary>
		/// Initializes a new instance of the <see cref="NeuralNetwork"/> class.
		/// </summary>
		public NeuralNetwork() {
			this.Layers = new List<Layer>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NeuralNetwork"/> class.
		/// </summary>
		/// /// <param name="layers">Layers to be included in the network.</param>
		public NeuralNetwork(params Layer[] layers) {
			this.Layers = layers.ToList();
		}

		/// <summary>
		/// Randomizes input weights for all neirons in the network.
		/// </summary>
		/// <param name="min">Minimum input weight value.</param>
		/// <param name="max">Maximum input weight value.</param>
		public virtual void Scramble(double min = 0, double max = 1) {
			foreach (var layer in Layers) {
				layer.Scramble(min, max);
			}
		}

		/// <summary>
		/// Computes the network's output from the specified input.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>System.Double[].</returns>
		public virtual double[] Compute(double[] input) {
			var output = input;

			foreach (var layer in Layers) {
				output = layer.Compute(output);
			}

			return output;
		}
	}
}
