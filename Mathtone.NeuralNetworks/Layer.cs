using Mathtone.NeuralNetworks.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathtone.NeuralNetworks {
	/// <summary>
	/// A layer of neurons.
	/// </summary>
	public class Layer {

		/// <summary>
		/// Gets or sets the neurons contained in the layer.
		/// </summary>
		/// <value>The neurons.</value>
		public NeuronBase[] Neurons { get; protected set; }

		/// <summary>
		/// Output value for the layer.
		/// </summary>
		/// <value>The output.</value>
		public double[] Output { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Layer"/> class.
		/// </summary>
		/// <param name="neurons">Neurons contained in this layer.</param>
		public Layer(NeuronBase[] neurons) {
			this.Neurons = neurons;
			this.Output = new double[neurons.Length];
		}

		/// <summary>
		/// Computes this layer's output for the specified input values.
		/// </summary>
		/// <param name="inputs">Input values.</param>
		/// <returns>System.Double[].</returns>
		public virtual double[] Compute(double[] inputs) {

			for (int i = 0; i < Neurons.Length; i++) {
				Output[i] = Neurons[i].Compute(inputs);
			}

			return Output;
		}

		/// <summary>
		/// Randomizes input values for all neurons in this layer.
		/// </summary>
		/// <param name="min">Minimum input weight value.</param>
		/// <param name="max">Maximum input weight value.</param>
		public void Scramble(double min = 0, double max = 1) {
			foreach (var n in Neurons) {
				n.Scramble(min, max);
			}
		}
	}
}