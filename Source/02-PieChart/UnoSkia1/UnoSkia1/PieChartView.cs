using SkiaSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoSkia1
{
    public class PieChartView
    {
        public List<PieSlice> Slices { get; set; } = new List<PieSlice>();

        public void Draw(SKCanvas canvas, SKPoint origin, float radius)
        {
            var totalValue = Slices.Sum(x => x.Value);
            float explodeOffset = 50;

            var chartRect = new SKRect(origin.X - radius, origin.Y - radius, origin.X + radius, origin.Y + radius);

            float startAngle = 0;

            using var strokePaint = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Black,
                StrokeWidth = 5,
                IsAntialias = true,
            };
            foreach (var slice in Slices)
            {
                float sweepAngle = (slice.Value / totalValue) * 360f;

                using var fillPaint = new SKPaint()
                {
                    Style = SKPaintStyle.Fill,
                    Color = slice.SliceColor,
                    IsAntialias = true,
                };
                using var path = new SKPath();

                path.MoveTo(origin);
                path.ArcTo(chartRect, startAngle, sweepAngle, false);
                path.Close();

                // Calculate "explode" transform
                float angle = startAngle + 0.5f * sweepAngle;
                float x = explodeOffset * (float)Math.Cos(Math.PI * angle / 180);
                float y = explodeOffset * (float)Math.Sin(Math.PI * angle / 180);

                canvas.Save();
                //canvas.Translate(x, y);

                canvas.DrawPath(path, fillPaint);
                canvas.DrawPath(path, strokePaint);

                canvas.Restore();

                startAngle += sweepAngle;
            }
        }

        private float RadiansFromDegrees(float degrees) => degrees * (float)Math.PI / 180f;
    }
}
