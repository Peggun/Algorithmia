using Avalonia;
using Avalonia.Controls;

using Algorithmia.Noise;
using Algorithmia.Factories;
using Algorithmia.Enums;
using Algorithmia.Interfaces;
using System;
using System.Diagnostics;
using Avalonia.Diagnostics;

namespace Algorithmia.Visualisation.Views
{
    public partial class MainWindow : Window
    {
        INoiseGenerator noise;
        public MainWindow()
        {
            InitializeComponent();
        }

        // Noise Type
        public void NoiseTypeDropdown_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var noiseType = ((ComboBoxItem)NoiseTypeDropdown.SelectedItem)?.Content.ToString();
        }

        // Rotation Type 3D
        public void Rotation3DDropdown_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var rotationType = ((ComboBoxItem)Rotation3DDropdown.SelectedItem)?.Content.ToString();
        }

        // Seed
        public void SeedInput_TextChanged(object? sender, TextChangedEventArgs e)
        {
            var seed = SeedInput.Text;
        }

        // Frequency
        public void FrequencySlider_ValueChanged(object? sender, AvaloniaPropertyChangedEventArgs<double> e)
        {
            var frequency = FrequencySlider.Value;
            FrequencyValue.Text = $"Value: {frequency:F1}";
        }

        // Fractal Type
        public void FractalTypeDropdown_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var fractalType = ((ComboBoxItem)FractalTypeDropdown.SelectedItem)?.Content.ToString();
        }

        // Octaves
        public void OctavesSlider_ValueChanged(object? sender, AvaloniaPropertyChangedEventArgs<double> e)
        {
            var octaves = OctavesSlider.Value;
            OctavesValue.Text = $"Value: {octaves:F0}";
        }

        // Lacunarity
        public void LacunaritySlider_ValueChanged(object? sender, AvaloniaPropertyChangedEventArgs<double> e)
        {
            var lacunarity = LacunaritySlider.Value;
            LacunarityValue.Text = $"Value: {lacunarity:F1}";
        }

        // Gain
        public void GainSlider_ValueChanged(object? sender, AvaloniaPropertyChangedEventArgs<double> e)
        {
            var gain = GainSlider.Value;
            GainValue.Text = $"Value: {gain:F1}";
        }

        // Weighted Strength
        public void WeightedStrengthSlider_ValueChanged(object? sender, AvaloniaPropertyChangedEventArgs<double> e)
        {
            var weightedStrength = WeightedStrengthSlider.Value;
            WeightedStrengthValue.Text = $"Value: {weightedStrength:F1}";
        }

        // Ping Pong Strength
        public void PingPongStrengthSlider_ValueChanged(object? sender, AvaloniaPropertyChangedEventArgs<double> e)
        {
            var pingPongStrength = PingPongStrengthSlider.Value;
            PingPongStrengthValue.Text = $"Value: {pingPongStrength:F1}";
        }

        // Distance Function
        public void DistanceFunctionDropdown_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var distanceFunction = ((ComboBoxItem)DistanceFunctionDropdown.SelectedItem)?.Content.ToString();
        }

        // Return Type
        public void ReturnTypeDropdown_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var returnType = ((ComboBoxItem)ReturnTypeDropdown.SelectedItem)?.Content.ToString();
        }

        // Jitter
        public void JitterSlider_ValueChanged(object? sender, AvaloniaPropertyChangedEventArgs<double> e)
        {
            var jitter = JitterSlider.Value;
            JitterValue.Text = $"Value: {jitter:F1}";
        }
    }
}