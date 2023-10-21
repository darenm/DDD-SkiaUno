using SkiaSharp;

namespace UnoSkia1
{
    public sealed partial class MainPage : Page
    {
        PieChartView _pieChart;

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
        }


        private void SkiaCanvas_PaintSurface(object sender, SkiaSharp.Views.Windows.SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear();

            _pieChart.Draw(canvas, new SKPoint(500, 500), 400);
        }
    }
}