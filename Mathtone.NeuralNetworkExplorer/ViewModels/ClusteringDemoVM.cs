using Mathtone.MIST;
using Mathtone.NeuralNetworks;
using Mathtone.NeuralNetworks.Neurons;
using Mathtone.NeuralNetworks.Training;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mathtone.NeuralNetworkExplorer.ViewModels {



	[Notifier(NotificationMode.Implicit)]
	public class ClusteringDemoVM : ViewModel {

		protected object locker = new object();
		protected NeuralNetwork network;

		public int MapWidth { get; set; } = 100;
		public double Iterations { get; set; } = 5000;
		public double LearningRate { get; set; } = 0.2;
		public int ClusterRadius { get; set; } = 7;
		public string Status { get; protected set; }
		public int CurrentIteration { get; protected set; } = 0;
		public bool ReadyToTrain { get; protected set; } = false;
		public bool Training { get; protected set; } = false;
		public bool IsRunning { get; protected set; } = false;
		public WriteableBitmap BitMap { get; protected set; }
		public DelegateCommand OpenCommand { get; protected set; }
		public DelegateCommand TrainCommand { get; protected set; }
		public DelegateCommand ResetCommand { get; protected set; }
		public DelegateCommand StopCommand { get; protected set; }

		public DisplayChannelVM DisplayChannels { get; } = new DisplayChannelVM();

		public ClusteringDemoVM() {
			DisplayChannels.PropertyChanged += DisplayChannels_PropertyChanged;
			OpenCommand = new DelegateCommand(Initialize);
			TrainCommand = new DelegateCommand(TrainNetwork);
			ResetCommand = new DelegateCommand(Reset);
			StopCommand = new DelegateCommand(Stop);
		}

		void Initialize() {
			lock (locker) {

				Status = "Initializing";
				CurrentIteration = 0;
				ReadyToTrain = false;

				//Initialize neural network
				network = new NeuralNetwork();
				var l1 = new Layer(new DeltaNeuron[MapWidth * MapWidth]);
				for (var i = 0; i < l1.Neurons.Length; i++) {
					l1.Neurons[i] = new DeltaNeuron(3);
				}
				l1.Scramble(0, 255);
				network.Layers.Add(l1);

				//Create a bitmap to which we can draw
				var width = (int)Math.Sqrt(network.Layers[0].Neurons.Length);
				Application.Current.Dispatcher.Invoke(() => {
					BitMap = new WriteableBitmap(width, width, 96, 96, PixelFormats.Rgb24, null);
					UpdateMap();
				});

				Status = "Ready";
				ReadyToTrain = true;
			}
		}

		void UpdateMap() {

			BitMap.Lock();
			unsafe
			{
				byte* pbuff = (byte*)BitMap.BackBuffer.ToPointer();
				var bpp = BitMap.Format.BitsPerPixel / 8;
				var h = BitMap.Height;
				var w = BitMap.Width;
				var s = BitMap.BackBufferStride;

				var neurons = network.Layers[0].Neurons;
				var i = 0;

				//Go from left-right, top-bottom as per...

				for (var y = 0; y < h; y++) {
					for (var x = 0; x < w; x++, i++) {
						var loc = y * s + x * bpp;
						var weights = neurons[i].InputWeights;
						//Use weights for R, G & B values
						var r = DisplayChannels.ShowRed ? weights[0] : 0;
						var g = DisplayChannels.ShowGreen ? weights[1] : 0;
						var b = DisplayChannels.ShowBlue ? weights[2] : 0;

						if (DisplayChannels.ShowGrayscale) {
							var f = 1d / (Convert.ToInt32(DisplayChannels.ShowRed) + Convert.ToInt32(DisplayChannels.ShowGreen) + Convert.ToInt32(DisplayChannels.ShowBlue));
							var gray = (byte)(f * r + f * g + f * b);
							pbuff[loc] = gray;
							pbuff[loc + 1] = gray;
							pbuff[loc + 2] = gray;
						}
						else {
							pbuff[loc] = (byte)r;
							pbuff[loc + 1] = (byte)g;
							pbuff[loc + 2] = (byte)b;
						}
					}
				}
			}

			BitMap.AddDirtyRect(new Int32Rect(0, 0, (int)BitMap.Width, (int)BitMap.Height));
			BitMap.Unlock();
		}

		void TrainNetwork() {
			Task.Factory.StartNew(() => {
				lock (locker) {
					ReadyToTrain = false;
					Training = true;
					Status = "Training";
					var i = 0;
					var rand = new Random();
					var width = (int)Math.Sqrt(network.Layers[0].Neurons.Length);
					var trainer = new SOMTrainer(width, width, network);
					var input = new double[3];
					var fixedLearningRate = LearningRate / 10.0;
					var driftingLearningRate = fixedLearningRate * 9.0;
					var maxIterations = Iterations + CurrentIteration;

					IsRunning = true;

					while (CurrentIteration < maxIterations && IsRunning) {

						trainer.LearningRate = driftingLearningRate * (Iterations - i) / Iterations + this.LearningRate;
						trainer.LearningRadius = (double)ClusterRadius * (Iterations - i) / Iterations;

						input[0] = rand.Next(256);
						input[1] = rand.Next(256);
						input[2] = rand.Next(256);

						trainer.Run(input);

						if ((CurrentIteration % 10) == 0) {
							BitMap.Dispatcher.Invoke(UpdateMap);
						}
						CurrentIteration++;
						i++;
					}
					BitMap.Dispatcher.Invoke(UpdateMap);
					Status = "Ready";
					Training = false;
					ReadyToTrain = true;
				}
			});
		}

		void Reset() {
			Initialize();
		}

		void Stop() {
			IsRunning = false;
		}

		private void DisplayChannels_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			UpdateMap();
		}
	}
}