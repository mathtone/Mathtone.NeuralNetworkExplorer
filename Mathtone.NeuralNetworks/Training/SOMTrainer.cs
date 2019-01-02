using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathtone.NeuralNetworks.Training {

	/// <summary>
	/// Self-organizing map trainer.
	/// </summary>
	public class SOMTrainer {

		private readonly int width;
		private readonly int height;
		private NeuralNetwork network;

		public double LearningRate { get; set; } = 0.1;

		public double LearningRadius { get; set; } = 7;

		/// <summary>
		/// Initializes a new instance of the <see cref="SOMTrainer"/> class.
		/// </summary>
		/// <param name="width">Map width.</param>
		/// <param name="height">Map height.</param>
		/// <param name="network">Network to train.</param>
		public SOMTrainer(int width, int height, NeuralNetwork network) {
			this.width = width;
			this.height = height;
			this.network = network;
		}

		/// <summary>
		/// Gets the index of the lowest value.
		/// </summary>
		/// <param name="values">The values.</param>
		/// <returns>index of the lowest value.</returns>
		public int GetWinner(double[] values) {

			var min = values[0];
			var minIndex = 0;

			for (int i = 1, n = values.Length; i < n; i++) {
				if (values[i] < min) {
					min = values[i];
					minIndex = i;
				}
			}

			return minIndex;
		}

		/// <summary>
		/// Trains network with the specified input set.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>System.Double</returns>
		public double Run(double[] input) {

			var err = 0.0;
			var learningDiameter2 = 2 * LearningRadius * LearningRadius;
			var winner = GetWinner(network.Compute(input));
			var layer = network.Layers[0];

			if (LearningRadius == 0) {
				var neuron = layer.Neurons[winner];

				// update weight of the winner only
				for (int i = 0, n = neuron.InputWeights.Length; i < n; i++) {
					neuron.InputWeights[i] += (input[i] - neuron.InputWeights[i]) * LearningRate;
				}
			}
			else {

				var x = winner % width;
				var y = winner / height;
				var l = layer.Neurons.Length;

				for (int j = 0; j < l; j++) {

					var dx = (j % width) - x;
					var dy = (j / height) - y;
					var neuron = layer.Neurons[j];
					var factor = Math.Exp(-(double)(dx * dx + dy * dy) / learningDiameter2);

					for (int i = 0, n = neuron.InputWeights.Length; i < n; i++) {
						var e = (input[i] - neuron.InputWeights[i]) * factor;
						err += Math.Abs(e);
						neuron.InputWeights[i] += e * LearningRate;
					}
				}
			}
			return err;
		}

		/// <summary>
		/// Trains network with the specified input sets.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>System.Double.</returns>
		public double Run(double[][] input) {
			var err = 0.0;
			for (var i = 0; i < input.Length; i++) {
				err += Run(input[i]);
			}
			return err;
		}
	}
}