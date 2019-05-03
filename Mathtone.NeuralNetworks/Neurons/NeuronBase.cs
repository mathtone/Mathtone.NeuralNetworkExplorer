using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathtone.NeuralNetworks.Neurons {
	public abstract class NeuronBase {

		//protected Random random = new Random();

		public double[] InputWeights;

		public double Output { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NeuronBase"/> class.
		/// </summary>
		/// <param name="inputs">Number of input weights.</param>
		public NeuronBase(int inputs = 1) : this(new double[inputs]) {
			//InputWeights = new double[inputs];
			//Scramble();
		}

		public NeuronBase(params double[] inputWeights) {
			this.InputWeights = inputWeights;
			//Scramble();
		}

		/// <summary>
		/// Randomizes imput weights.
		/// </summary>
		/// <param name="min">Minimum input weight value.</param>
		/// <param name="max">Maximum input weight value.</param>
		//public virtual void Scramble(double min = 0, double max = 1) {
		//	var l = max - min;
		//	for (var i = 0; i < InputWeights.Length; i++) {
		//		InputWeights[i] = random.NextDouble() * l + min;
		//	}
		//}

		/// <summary>
		/// Computes and assigns output value for the specified input.
		/// </summary>
		/// <param name="input">Input values.</param>
		/// <returns>System.Double.</returns>
		public double Compute(double[] input) =>
			Output = ComputeOutput(input);

		/// <summary>
		/// Computes output value for the specified input.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>System.Double.</returns>
		protected abstract double ComputeOutput(double[] input);
	}
}