using Microsoft.UI.Dispatching;

using SkiaSharp;

namespace UnoSkia1
{
    public sealed partial class MainPage : Page
    {
        private PieChartView _pieChart;
        private DispatcherTimer _repeatingTimer;

        public MainPage()
        {
            this.InitializeComponent();

            _pieChart = new PieChartView();
            _pieChart.Slices.Add(new PieSlice("Red", 25, SKColors.Red));
            _pieChart.Slices.Add(new PieSlice("Green", 50, SKColors.Green));
            _pieChart.Slices.Add(new PieSlice("Blue", 40, SKColors.Blue));
            _pieChart.Slices.Add(new PieSlice("Purple", 30, SKColors.Purple));
            _pieChart.Slices.Add(new PieSlice("Yellow", 60, SKColors.Yellow));
            _pieChart.Slices.Add(new PieSlice("Orange", 10, SKColors.Orange));

            _pieChart.Duration = TimeSpan.FromSeconds(1);

            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            _repeatingTimer?.Stop();
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            _repeatingTimer = new DispatcherTimer();
            _repeatingTimer.Interval = TimeSpan.FromMilliseconds(1000/25);

            // The tick handler will be invoked repeatedly after every 5
            // seconds on the dedicated thread.
            _repeatingTimer.Tick += (s, e) =>
            {
                SkiaCanvas?.Invalidate();
            };

            // Start the Timer
            _repeatingTimer.Start();
            _pieChart.StartAnimation();
        }

        private void SkiaCanvas_PaintSurface(object sender, SkiaSharp.Views.Windows.SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear();

            _pieChart.Draw(canvas, new SKPoint(500, 500), 400);
        }
    }
}