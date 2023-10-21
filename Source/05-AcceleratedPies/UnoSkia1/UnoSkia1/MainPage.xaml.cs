using Microsoft.UI.Dispatching;

using SkiaSharp;

namespace UnoSkia1
{
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer _repeatingTimer;
        private SKColor[] _colors = new SKColor[]
        {
            SKColors.Red, SKColors.Green, SKColors.Blue, SKColors.Orange,
            SKColors.OrangeRed, SKColors.Yellow, SKColors.Purple, SKColors.Brown,
            SKColors.MintCream
        };

        private Random _random = new Random();

        private List<PieChartView> _pieChartViews = new List<PieChartView>();

        public MainPage()
        {
            this.InitializeComponent();

            for (int i = 0; i < 50; i++)
            {
                _pieChartViews.Add(CreatePieChart());
            }

            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
        }

        private PieChartView CreatePieChart()
        {
            
            var pieChart = new PieChartView();
            for (int i = 0; i < _random.Next(2, 10); i++)
            {
                SKColor randomColor = _colors[_random.Next(_colors.Length)];
                pieChart.Slices.Add(new PieSlice(randomColor.ToString(), _random.Next(10, 100), randomColor));
            }

            pieChart.Duration = TimeSpan.FromMilliseconds(_random.Next(500, 1500));
            return pieChart;
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            _repeatingTimer?.Stop();
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            _repeatingTimer = new DispatcherTimer();
            _repeatingTimer.Interval = TimeSpan.FromMilliseconds(1000/25);

            _repeatingTimer.Tick += (s, e) =>
            {
                SkiaCanvas?.Invalidate();
            };

            // Start the Timer
            _repeatingTimer.Start();
            _pieChartViews.ForEach(view => view.StartAnimation());
        }

#if WINDOWS
        private void SkiaCanvas_PaintSurface(object sender, SkiaSharp.Views.Windows.SKPaintSurfaceEventArgs e)
        {
            const int spacing = 200;
            var canvas = e.Surface.Canvas;
            canvas.Clear();

            for (int i = 0; i < _pieChartViews.Count; i++)
            {
                _pieChartViews[i].Draw(canvas, new SKPoint(spacing + ((i % 5) * spacing * 2), (spacing *2 * (i / 5)) + spacing), spacing - (spacing / 10));
            }
        }
#else
        private void SkiaCanvas_PaintSurface(object sender, SkiaSharp.Views.Windows.SKPaintGLSurfaceEventArgs e)
        {
            const int spacing = 200;
            var canvas = e.Surface.Canvas;
            canvas.Clear();

            for (int i = 0; i < _pieChartViews.Count; i++)
            {
                _pieChartViews[i].Draw(canvas, new SKPoint(spacing + ((i % 5) * spacing * 2), (spacing * 2 * (i / 5)) + spacing), spacing - (spacing / 10));
            }
        }
#endif
    }
}