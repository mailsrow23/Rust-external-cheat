using SharpDX.Direct2D1;

namespace Dissector.Overlay
{
    /// <summary>
    /// Brushes for our canvas
    /// </summary>
    public partial class Canvas
    {
        private static SolidColorBrush _metalBrush;
        private static SolidColorBrush _stoneBrush;
        private static SolidColorBrush _sulfurBrush;
        private static SolidColorBrush _hempBrush;
        private static SolidColorBrush _sleeperBrush;
        private static SolidColorBrush _woodenCrateBrush;
        private static SolidColorBrush _militaryCrateBrush;
        private static SolidColorBrush _lootBrush;
        private static SolidColorBrush _playerBrush;
        private static SolidColorBrush _barrelBrush;
        private static SolidColorBrush _sheetMetalDoorBrush;
        private static SolidColorBrush _doubleMetalDoorBrush;
        private static SolidColorBrush _garageDoorBrush;
        private static SolidColorBrush _armoredDoorBrush;
        private static SolidColorBrush _toolCupboardBrush;
        private static SolidColorBrush _crosshairBrush;
        private static SolidColorBrush _animalBrush;

        /// <summary>
        /// Initializes our drawing brushes/other resources
        /// </summary>
        private static void InitializeBrushes()
        {
            _metalBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.DarkSlateBlue);
            _stoneBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Silver);
            _sulfurBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Yellow);
            _hempBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Green);
            _animalBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Purple);
            _sleeperBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Red);
            _woodenCrateBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Blue);
            _militaryCrateBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.LimeGreen);
            _lootBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.DarkOrange);
            _playerBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.MediumAquamarine);
            _barrelBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Blue);
            _sheetMetalDoorBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.OrangeRed);
            _doubleMetalDoorBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Pink);
            _garageDoorBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Blue);
            _armoredDoorBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Blue);
            _toolCupboardBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Blue);
            _crosshairBrush = new SolidColorBrush(_renderTarget, new SharpDX.Mathematics.Interop.RawColor4(255,0,0,0));
        }

        /// <summary>
        /// Dispose brushes when the overlay form closes
        /// </summary>
        private static void DisposeBrushes()
        {
            _metalBrush.Dispose();
            _stoneBrush.Dispose();
            _sulfurBrush.Dispose();
            _hempBrush.Dispose();
            _sleeperBrush.Dispose();
            _woodenCrateBrush.Dispose();
            _militaryCrateBrush.Dispose();
            _lootBrush.Dispose();
            _playerBrush.Dispose();
            _barrelBrush.Dispose();
            _sheetMetalDoorBrush.Dispose();
            _doubleMetalDoorBrush.Dispose();
            _garageDoorBrush.Dispose();
            _armoredDoorBrush.Dispose();
            _toolCupboardBrush.Dispose();
            _crosshairBrush.Dispose();
        }

        /// <summary>
        /// Allow user to modify brush color for sulfur
        /// </summary>
        /// <param name="color"></param>
        public static void SetSulfurBrush(System.Drawing.Color color)
        {
            _sulfurBrush.Color = new SharpDX.Color(color.R, color.G, color.B, color.A);
        }
    }
}