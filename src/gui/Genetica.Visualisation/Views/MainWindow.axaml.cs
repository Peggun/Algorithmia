using System.Timers;
using Avalonia.Controls;
using Genetica.Visualisation.ViewModels;
using Avalonia.Threading;

namespace Genetica.Visualisation.Views
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

                _resizeTimer = new Timer(200); // Delay 200ms
                _resizeTimer.Elapsed += (s, args) =>
                {
                    _resizeTimer?.Stop();
                    _resizeTimer = null;

                    Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        viewModel.MapWidth = (int)e.NewSize.Width;
                        viewModel.MapHeight = (int)e.NewSize.Height;
                        viewModel.WindowWidth = (int)e.NewSize.Width;
                        viewModel.WindowHeight = (int)e.NewSize.Height;
                    });
                };
                _resizeTimer.Start();
            }
        }
    }
}