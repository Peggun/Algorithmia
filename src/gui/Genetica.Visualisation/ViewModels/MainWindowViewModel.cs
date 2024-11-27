using Avalonia.Media.Imaging;
using Avalonia.Threading;

using SkiaSharp;

using Genetica;
using Genetica.Sinks;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System;
using System.Timers;
using System.Diagnostics;
using System.Windows.Input;
using System.Collections.Generic;

namespace Genetica.Visualisation.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private INoiseGenerator _noise;

        private int _mapHeight = 10;
        private int _mapWidth = 10;
        private float _scale = 0.01f;

        private NoiseType _selectedNoiseTypeIndex;
        private RotationType3D _selectedRotationTypeIndex;
        private int _seed = 12345;
        private double _frequency = 1.0;
        private FractalType _selectedFractalTypeIndex;
        private int _octaves = 1;
        private double _lacunarity = 2.0;
        private double _gain = 1.0;
        private double _weightedStrength = 0.5;
        private double _pingPongStrength = 0.0;
        private CellularDistanceFunction _selectedDistanceFunctionIndex;
        private CellularReturnType _selectedReturnTypeIndex;
        private double _jitter = 0.5;

        private int _windowHeight;
        private int _windowWidth;

        float[,] noiseData;

        private Bitmap? _generatedMapImage;

        private Timer _debounceTimer;

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsCellularNoiseSelected => SelectedNoiseTypeIndex == NoiseType.Cellular;

        ImageSink sink = new ImageSink();

        public MainWindowViewModel()
        {
            _noise = NoiseGeneratorFactory.CreateNoiseGenerator(NoiseType.Perlin);
            _debounceTimer = new Timer(200); // 200ms debounce interval
            _debounceTimer.Elapsed += (s, e) =>
            {
                _debounceTimer.Stop();
                Dispatcher.UIThread.InvokeAsync(UpdateMap); // Ensure UI update runs on main thread
            };
            UpdateMap(); // Generate initial map

            SaveMapCommand = new RelayCommand(() => CreateImageFromNoiseData(true, noiseData));
        }
        public ICommand SaveMapCommand { get; }

        // Update OnPropertyChanged to include the computed property
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(SelectedNoiseTypeIndex))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCellularNoiseSelected)));
            }

            // Reset debounce timer on any relevant property change
            if (propertyName != nameof(GeneratedMapImage))
            {
                _debounceTimer.Stop();
                _debounceTimer.Start();
            }
        }

        public Bitmap? GeneratedMapImage
        {
            get => _generatedMapImage;
            private set
            {
                if (_generatedMapImage != value)
                {
                    _generatedMapImage = value;
                    OnPropertyChanged();
                }
            }
        }

        private void UpdateMap()
        {
            Debug.WriteLine($"Map Height: {_mapHeight}\nMap Width: {_mapWidth}");

            // Generate noise data using the current settings
            noiseData = _noise.GenerateNoiseMap(_mapWidth, _mapHeight, _scale);

            // Create an image from the noise data
            GeneratedMapImage = CreateImageFromNoiseData(false, noiseData);
        }

        private Bitmap CreateImageFromNoiseData(bool saveToFile, float[,] noiseData = null)
        {
            int height = noiseData.GetLength(0);
            int width = noiseData.GetLength(1);

            // Create an SKBitmap with the required dimensions
            var bitmap = new SKBitmap(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);

            // Lock the pixels for direct access
            var pixels = bitmap.GetPixels();

            // Super fast image generation. Uses pointers. 
            // https://www.c-sharpcorner.com/article/pointers-in-C-Sharp/
            // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/unsafe-code
            // and a few more about unsafe code handling and pointers in SkiaSharp.
            
            // I spent 5 hours trying to speed this algorithm up. Research, AI. All lead to this.
            unsafe
            {
                byte* ptr = (byte*)pixels.ToPointer();

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        float noiseValue = noiseData[y, x];
                        int intensity = Math.Clamp((int)((noiseValue + 1) * 127.5f), 0, 255);

                        int index = (y * width + x) * 4; // 4 bytes per pixel (RGBA)
                        ptr[index] = (byte)intensity;      // Red
                        ptr[index + 1] = (byte)intensity;  // Green
                        ptr[index + 2] = (byte)intensity;  // Blue
                        ptr[index + 3] = 255;              // Alpha
                    }
                }
            }

            // Encode the SKBitmap to a stream
            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode(SKEncodedImageFormat.Png, 100); // Encode as PNG
            using var memoryStream = new MemoryStream();
            if (saveToFile)
            {
                sink.Write(noiseData);
            }
            else
            {
                data.SaveTo(memoryStream);    
                memoryStream.Seek(0, SeekOrigin.Begin);
                return new Bitmap(memoryStream); // Convert to Avalonia Bitmap
            }
            return null;
        }

        // Map Height
        public int WindowHeight
        {
            get => _windowHeight;
            set
            {
                if (_windowHeight != value)
                {
                    _windowHeight = value;
                    OnPropertyChanged();
                }
            }
        }
        // Map Height
        public int WindowWidth
        {
            get => _windowWidth;
            set
            {
                if (_windowWidth != value)
                {
                    _windowWidth = value;
                    OnPropertyChanged();
                }
            }
        }

        // Map Height
        public int MapHeight
        {
            get => _mapHeight;
            set
            {
                if (_mapHeight != value)
                {
                    _mapHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        // Map Width
        public int MapWidth
        {
            get => _mapWidth;
            set
            {
                if (_mapWidth != value)
                {
                    _mapWidth = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Scale
        {
            get => _scale;
            set
            {
                if (_scale != value)
                {
                    _scale = value;
                    OnPropertyChanged();
                }
            }
        }

        // Noise Type
        public NoiseType SelectedNoiseTypeIndex
        {
            get => _selectedNoiseTypeIndex;
            set
            {
                if (_selectedNoiseTypeIndex != value)
                {
                    _selectedNoiseTypeIndex = value;
                    _noise = NoiseGeneratorFactory.CreateNoiseGenerator(_selectedNoiseTypeIndex);
                    OnPropertyChanged();
                }
            }
        }

        // Rotation Type 3D
        public RotationType3D SelectedRotationTypeIndex
        {
            get => _selectedRotationTypeIndex;
            set
            {
                if (_selectedRotationTypeIndex != value)
                {
                    _selectedRotationTypeIndex = value;
                    _noise.SetRotationType3D(_selectedRotationTypeIndex);
                    OnPropertyChanged();
                }
            }
        }

        // Seed
        public int Seed
        {
            get => _seed;
            set
            {
                if (_seed != value)
                {
                    _seed = value;
                    _noise.SetSeed(_seed);
                    OnPropertyChanged();
                }
            }
        }

        // Frequency
        public double Frequency
        {
            get => _frequency;
            set
            {
                if (_frequency != value)
                {
                    _frequency = value;
                    _noise.SetFrequency((float)_frequency);
                    OnPropertyChanged();
                }
            }
        }

        // Fractal Type
        public FractalType SelectedFractalTypeIndex
        {
            get => _selectedFractalTypeIndex;
            set
            {
                if (_selectedFractalTypeIndex != value)
                {
                    _selectedFractalTypeIndex = value;
                    _noise.SetFractalType(_selectedFractalTypeIndex);
                    OnPropertyChanged();
                }
            }
        }

        // Octaves
        public int Octaves
        {
            get => _octaves;
            set
            {
                if (_octaves != value)
                {
                    _octaves = value;
                    _noise.SetFractalOctaves(_octaves);
                    OnPropertyChanged();
                }
            }
        }

        // Lacunarity
        public double Lacunarity
        {
            get => _lacunarity;
            set
            {
                if (_lacunarity != value)
                {
                    _lacunarity = value;
                    _noise.SetFractalLacunarity((float)_lacunarity);
                    OnPropertyChanged();
                }
            }
        }

        // Gain
        public double Gain
        {
            get => _gain;
            set
            {
                if (_gain != value)
                {
                    _gain = value;
                    _noise.SetFractalGain((float)_gain);
                    OnPropertyChanged();                  
                }
            }
        }

        // Weighted Strength
        public double WeightedStrength
        {
            get => _weightedStrength;
            set
            {
                if (_weightedStrength != value)
                {
                    _weightedStrength = value;
                    _noise.SetFractalWeightedStrength((float)_weightedStrength);
                    OnPropertyChanged();
                }
            }
        }

        // Ping Pong Strength
        public double PingPongStrength
        {
            get => _pingPongStrength;
            set
            {
                if (_pingPongStrength != value)
                {
                    _pingPongStrength = value;
                    _noise.SetFractalPingPongStrength((float)_pingPongStrength);
                    OnPropertyChanged();
                }
            }
        }

        // Distance Function
        public CellularDistanceFunction SelectedDistanceFunctionIndex
        {
            get => _selectedDistanceFunctionIndex;
            set
            {
                if (_selectedDistanceFunctionIndex != value)
                {
                    _selectedDistanceFunctionIndex = value;
                    _noise.SetCellularDistanceFunction(_selectedDistanceFunctionIndex);
                    OnPropertyChanged();
                }
            }
        }

        // Return Type
        public CellularReturnType SelectedReturnTypeIndex
        {
            get => _selectedReturnTypeIndex;
            set
            {
                if (_selectedReturnTypeIndex != value)
                {
                    _selectedReturnTypeIndex = value;
                    _noise.SetCellularReturnType(_selectedReturnTypeIndex);
                    OnPropertyChanged();
                }
            }
        }

        // Jitter
        public double Jitter
        {
            get => _jitter;
            set
            {
                if (_jitter != value)
                {
                    _jitter = value;
                    _noise.SetCellularJitter((float)_jitter);
                    OnPropertyChanged();
                }
            }
        }

        public class RelayCommand : ICommand
        {
            private readonly Action _execute;
            private readonly Func<bool>? _canExecute;

            public RelayCommand(Action execute, Func<bool>? canExecute = null)
            {
                _execute = execute;
                _canExecute = canExecute;
            }

            public event EventHandler? CanExecuteChanged;

            public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

            public void Execute(object? parameter) => _execute();

            public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
