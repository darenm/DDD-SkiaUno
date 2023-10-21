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
        private bool _isAnimationRunning;
        private bool _isAnimationComplete;
        private long _ticksStarted;

        public TimeSpan Duration { get; set; } = TimeSpan.Zero;

        public List<PieSlice> Slices { get; set; } = new List<PieSlice>();

        public void StartAnimation()
        {
            _ticksStarted = DateTime.Now.Ticks;
            _isAnimationRunning = true;
        }

        public void StopAnimation()
        {
            _isAnimationRunning = false;
        }

        public void Draw(SKCanvas canvas, SKPoint origin, float radius)
        {
            float animationFactor = 1.0f;

            if (Duration > TimeSpan.Zero && _isAnimationRunning == true && _isAnimationComplete == false)
            {
                animationFactor = (float)(DateTime.Now.Ticks - _ticksStarted) / (float)Duration.Ticks;
            }
            else if (Duration > TimeSpan.Zero && _isAnimationRunning == false && _isAnimationComplete == false)
            {
                animationFactor = 0f;
            }

            if (animationFactor >= 1.0f)
            {
                _isAnimationRunning = false;
                _isAnimationComplete = true;
                animationFactor = 1.0f;
            }

            var totalValue = Slices.Sum(x => x.Value);
            var translateThreshhold = 0.2f;
            float explodeOffset = animationFactor < 1f - translateThreshhold ? 0 : (radius/10) * (1 - (1f - animationFactor) / translateThreshhold);

            radius *= animationFactor;

            var chartRect = new SKRect(origin.X - radius, origin.Y - radius, origin.X + radius, origin.Y + radius);

            float startAngle = -180f * ((1 - animationFactor) / 2f);

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
                canvas.Translate(x, y);

                canvas.DrawPath(path, fillPaint);
                canvas.DrawPath(path, strokePaint);

                canvas.Restore();

                startAngle += sweepAngle;
            }
        }

        private float RadiansFromDegrees(float degrees) => degrees * (float)Math.PI / 180f;
    }
}
