using SkiaSharp;

namespace UnoSkia1
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void SkiaCanvas_PaintSurface(object sender, SkiaSharp.Views.Windows.SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear();

            var fillPaint = new SKPaint()
            {
                Color = SKColors.Blue,
                Style = SKPaintStyle.Fill,
            };

            var strokePaint = new SKPaint()
            {
                Color = SKColors.Red,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 5f,
                IsAntialias = true
            };

            canvas.DrawCircle(new SKPoint(500, 500), 400f, fillPaint);
            canvas.DrawCircle(new SKPoint(500, 500), 400f, strokePaint);
        }
    }
}