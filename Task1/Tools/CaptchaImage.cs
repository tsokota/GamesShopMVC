using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Yevhenii_KoliesnikTask1.Tools
{
    /// <summary>
    /// Generation capcha 
    /// </summary>
    public class CaptchaImage
    {
        public const string CaptchaValueKey = "CaptchaImageText";

        public string Text
        {
            get { return _text; }
        }
        public Bitmap Image
        {
            get { return _image; }
        }
        public int Width
        {
            get { return _width; }
        }
        public int Height
        {
            get { return _height; }
        }

        // Internal properties.
        private readonly string _text;
        private int _width;
        private int _height;
        private string _familyName;
        private Bitmap _image;

        // For generating random numbers.
        private readonly Random _random = new Random();

        public CaptchaImage(string s, int width, int height)
        {
            _text = s;
            SetDimensions(width, height);
            GenerateImage();
        }

        public CaptchaImage(string s, int width, int height, string familyName)
        {
            _text = s;
            SetDimensions(width, height);
            SetFamilyName(familyName);
            GenerateImage();
        }

        // ====================================================================
        // This member overrides Object.Finalize.
        // ====================================================================
        ~CaptchaImage()
        {
            Dispose(false);
        }

        // ====================================================================
        // Releases all resources used by this object.
        // ====================================================================
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        // ====================================================================
        // Custom Dispose method to clean up unmanaged resources.
        // ====================================================================
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                // Dispose of the bitmap.
                _image.Dispose();
        }

        // ====================================================================
        // Sets the image aWidth and aHeight.
        // ====================================================================
        private void SetDimensions(int aWidth, int aHeight)
        {
            // Check the aWidth and aHeight.
            if (aWidth <= 0)
                throw new ArgumentOutOfRangeException("aWidth", aWidth, "Argument out of range, must be greater than zero.");
            if (aHeight <= 0)
                throw new ArgumentOutOfRangeException("aHeight", aHeight, "Argument out of range, must be greater than zero.");
            _width = aWidth;
            _height = aHeight;
        }

        // ====================================================================
        // Sets the font used for the image text.
        // ====================================================================
        private void SetFamilyName(string aFamilyName)
        {
            // If the named font is not installed, default to a system font.
            try
            {
                Font font = new Font(aFamilyName, 12F);
                _familyName = aFamilyName;
                font.Dispose();
            }
            catch (Exception)
            {
                _familyName = FontFamily.GenericSerif.Name;
            }
        }

        // ====================================================================
        // Creates the bitmap image.
        // ====================================================================
        private void GenerateImage()
        {
            // Create a new 32-bit bitmap image.
            Bitmap bitmap = new Bitmap(_width, _height, PixelFormat.Format32bppArgb);

            // Create a graphics object for drawing.
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, _width, _height);

            // Fill in the background.
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, Color.White);
            g.FillRectangle(hatchBrush, rect);

            // Set up the text font.
            SizeF size;
            float fontSize = rect.Height + 1;
            Font font;
            // Adjust the font size until the text fits within the image.
            do
            {
                fontSize--;
                font = new Font(_familyName, fontSize, FontStyle.Bold);
                size = g.MeasureString(_text, font);
            } while (size.Width > rect.Width);

            // Set up the text format.
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            // Create a path using the text and warp it randomly.
            GraphicsPath path = new GraphicsPath();
            path.AddString(_text, font.FontFamily, (int)font.Style, font.Size, rect, format);
            float v = 4F;
            PointF[] points =
			{
				new PointF(_random.Next(rect.Width) / v, _random.Next(rect.Height) / v),
				new PointF(rect.Width - _random.Next(rect.Width) / v, _random.Next(rect.Height) / v),
				new PointF(_random.Next(rect.Width) / v, rect.Height - _random.Next(rect.Height) / v),
				new PointF(rect.Width - _random.Next(rect.Width) / v, rect.Height - _random.Next(rect.Height) / v)
			};
            Matrix matrix = new Matrix();
            matrix.Translate(0F, 0F);
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

            // Draw the text.
            hatchBrush = new HatchBrush(HatchStyle.LargeConfetti, Color.LightGray, Color.DarkGray);
            g.FillPath(hatchBrush, path);

            // Add some random noise.
            int m = Math.Max(rect.Width, rect.Height);
            for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
            {
                int x = _random.Next(rect.Width);
                int y = _random.Next(rect.Height);
                int w = _random.Next(m / 50);
                int h = _random.Next(m / 50);
                g.FillEllipse(hatchBrush, x, y, w, h);
            }

            // Clean up.
            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();

            // Set the image.
            _image = bitmap;
        }
    }
}