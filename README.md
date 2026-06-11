# Mathtone.NeuralNetworkExplorer

A C# neural network library and WPF visualization application for exploring self-organizing map (SOM) training.

## Projects

### Mathtone.NeuralNetworks

A class library implementing core neural network primitives:

- **`NeuralNetwork`** — Composes a sequence of layers and drives forward computation.
- **`Layer`** — A collection of neurons; computes outputs for a given input vector and supports weight randomization.
- **`NeuronBase`** — Abstract base for all neuron types; owns the input weight array and delegates computation to subclasses.
- **`ActivatorNeuron`** — Abstract neuron that computes a weighted sum of inputs and passes it through an activation function.
- **`SigmoidActivatorNeuron`** — Concrete activator using the sigmoid function; exposes a configurable `Alpha` (steepness) parameter.
- **`DeltaNeuron`** — Outputs the total absolute difference between its weights and the input vector; used as the distance metric in SOM layers.
- **`SOMTrainer`** — Self-Organizing Map trainer that implements competitive (winner-takes-neighborhood) Hebbian learning.

### Mathtone.NeuralNetworkExplorer

A WPF desktop application that demonstrates SOM color clustering:

- Builds a 2-D grid of `DeltaNeuron`s and trains it with random RGB vectors.
- Renders the trained weight map as a `WriteableBitmap` with per-channel visibility controls (red, green, blue, grayscale).
- Exposes training parameters (map width, iterations, learning rate, cluster radius, refresh rate) through a MVVM interface backed by [Mathtone.MIST](https://github.com/DeadProjectsSociety) property notification and [Prism](https://github.com/PrismLibrary/Prism) commands.

## Getting Started

1. Open `Mathtone.NeuralNetworkExplorer.sln` in Visual Studio.
2. Restore NuGet packages.
3. Set **Mathtone.NeuralNetworkExplorer** as the startup project and run.
4. Click **Open** to initialize the SOM, then **Train** to start learning. Use **Stop** and **Reset** as needed.

## License

MIT — see [LICENSE.txt](LICENSE.txt).
