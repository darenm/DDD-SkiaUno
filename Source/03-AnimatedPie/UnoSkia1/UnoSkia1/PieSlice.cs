using SkiaSharp;

namespace UnoSkia1
{
    public class PieSlice
    {
        public PieSlice(string category, float value, SKColor sliceColor)
        {
            Category = category ?? throw new ArgumentNullException(nameof(category));
            Value = value;
            SliceColor = sliceColor;
        }

        public string Category { get; set; }
        public float Value { get; set; }
        public SKColor SliceColor { get; set; }
    }
}
