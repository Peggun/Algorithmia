using System.Timers;
using Avalonia.Controls;
using Algorithmia.Visualisation.ViewModels;
using Avalonia.Threading;

namespace Algorithmia.Visualisation.Views
{
    public partial class MainWindow : Window
    {
        private Timer? _resizeTimer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnMapDisplaySizeChanged(object? sender, SizeChangedEventArgs e)
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                _resizeTimer?.Stop(); // Stop any existing timer

                _resizeTimer = new System.Timers.Timer(200); // Delay 200ms
                _resizeTimer.Elapsed += (s, args) =>
                {
                    _resizeTimer?.Stop();
                    _resizeTimer = null;

                    Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        viewModel.MapWidth = (int)e.NewSize.Width;
                        viewModel.MapHeight = (int)e.NewSize.Height;
                    });
                };
                _resizeTimer.Start();
            }
        }
    }
}