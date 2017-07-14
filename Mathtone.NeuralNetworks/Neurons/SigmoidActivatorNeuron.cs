using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathtone.NeuralNetworks.Neurons
{
    /// <summary>
    /// A <see cref="Mathtone.NeuralNetworks.Neurons.ActivatorNeuron"/> with a sigmoid activation function.
    /// </summary>
    /// <seealso cref="Mathtone.NeuralNetworks.Neurons.ActivatorNeuron" />
    public class SigmoidActivatorNeuron : ActivatorNeuron
    {

        /// <summary>
        /// Steepness of the sigmoid ramp.
        /// </summary>
        /// <value>The alpha.</value>
        public double Alpha { get; set; } = 2;

        /// <summary>
        /// Initializes a new instance of the <see cref="SigmoidActivatorNeuron"/> class.
        /// </summary>
        /// <param name="inputCount">The number of input values to this neuron.</param>
        public SigmoidActivatorNeuron(int inputCount = 1) :
            base(inputCount)
        {
        }

        /// <summary>
        /// Processes the sum of neuron inputs as the result of the sigmoid function.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Result of activation function.</returns>
        protected override double Activate(double input) =>
            (1 / (Math.Exp(-Alpha * input) + 1));
    }
}
