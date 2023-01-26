using GameOverlay.Drawing;
using GameOverlay.Windows;
using MDriver.MEME;
using System;

namespace Impure.Overlay
{
    public class Drawing1
    {
        public static int screen_Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
        public static int screen_Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        public Requests.Vector2.Vector2f ScreenCenter = new Requests.Vector2.Vector2f(screen_Width / 2, screen_Height / 2);

        #region Overlay Vars
        private readonly GameOverlay.Windows.OverlayWindow _window;
        public readonly Graphics _graphics;

        public static Font _font;

        public static SolidBrush Clear;

        //Crayola crayon colors
        public static SolidBrush Red;
        public static SolidBrush Scarlet;
        public static SolidBrush Red_Orange;
        public static SolidBrush Orange;
        public static SolidBrush Yellow_Orange;
        public static SolidBrush Yellow;
        public static SolidBrush Green_Yellow;
        public static SolidBrush Yellow_Green;
        public static SolidBrush Green;
        public static SolidBrush Blue_Green;
        public static SolidBrush Cerulean;
        public static SolidBrush Blue;
        public static SolidBrush Bluetiful;
        public static SolidBrush Indigo;
        public static SolidBrush Blue_Violet;
        public static SolidBrush Violet;
        public static SolidBrush Red_Violet;
        public static SolidBrush Carnation_Pink;
        public static SolidBrush Violet_Red;
        public static SolidBrush Brown;
        public static SolidBrush Apricot;
        public static SolidBrush Black;
        public static SolidBrush Gray;
        public static SolidBrush White;
        public static SolidBrush White_Washed;


        //Fluorescent crayons
        public static SolidBrush Radical_Red;
        public static SolidBrush Wild_Watermelon;
        public static SolidBrush Outrageous_Orange;
        public static SolidBrush Atomic_Tangerine;
        public static SolidBrush Neon_Carrot;
        public static SolidBrush Sunglow;
        public static SolidBrush Laser_Lemon;
        public static SolidBrush Unmellow_Yellow;
        public static SolidBrush Electric_Lime;
        public static SolidBrush Screamin_Green;
        public static SolidBrush Magic_Mint;
        public static SolidBrush Blizzard_Blue;
        public static SolidBrush Shocking_Pink;
        public static SolidBrush Razzle_Dazzle_Rose;
        public static SolidBrush Hot_Magenta;
        public static SolidBrush Purple_Pizzazz;

        //Neons
        public static SolidBrush Bright_Chartreuse;
        public static SolidBrush Bright_Green;
        public static SolidBrush Bright_Magenta;
        public static SolidBrush Bright_Pink;
        public static SolidBrush Bright_Purple;
        public static SolidBrush Bright_Red;
        public static SolidBrush Bright_Saffron;
        public static SolidBrush Bright_Scarlet;
        public static SolidBrush Bright_Teal;
        public static SolidBrush Electric_Crimson;
        public static SolidBrush Electric_Cyan;
        public static SolidBrush Electric_Flamingo;
        public static SolidBrush Electric_Green;
        public static SolidBrush Electric_Indigo;
        public static SolidBrush Electric_Orange;
        public static SolidBrush Electric_Pink;
        public static SolidBrush Electric_Purple;
        public static SolidBrush Electric_Red;
        public static SolidBrush Electric_Sheep;
        public static SolidBrush Electric_Violet;
        public static SolidBrush Electric_Yellow;
        public static SolidBrush Fluorescent_Green;
        public static SolidBrush Fluorescent_Orange;
        public static SolidBrush Fluorescent_Pink;
        public static SolidBrush Fluorescent_Red;
        public static SolidBrush Fluorescent_Red_Orange;
        public static SolidBrush Fluorescent_Turquoise;
        public static SolidBrush Fluorescent_Yellow;
        public static SolidBrush Light_Neon_Pink;
        public static SolidBrush Neon_Blue;
        public static SolidBrush Neon_Fuchsia;
        public static SolidBrush Neon_Green;
        public static SolidBrush Neon_Pink;
        public static SolidBrush Neon_Purple;
        public static SolidBrush Neon_Red;
        public static SolidBrush Neon_Yellow;
        public static SolidBrush Pinkish_Red_Neon;

        #endregion

        #region Overlay setup
        public Drawing1()
        {
            // it is important to set the window to visible (and topmost) if you want to see it!
            _window = new GameOverlay.Windows.OverlayWindow(0, 0, screen_Width, screen_Height)
            {
                IsTopmost = true,
                IsVisible = true
            };

            // handle this event to resize your Graphics surface
            _window.SizeChanged += _window_SizeChanged;
            // initialize a new Graphics object
            // set everything before you call _graphics.Setup()
            _graphics = new Graphics
            {
                MeasureFPS = true,
                Height = _window.Height,
                Width = _window.Width,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true,
                UseMultiThreadedFactories = true,
                VSync = false,
                WindowHandle = IntPtr.Zero
            };
        }

        ~Drawing1()
        {
            // don't forget to free resources
            _graphics.Dispose();
            _window.Dispose();
        }

        public void Initialize()
        {
            // creates the window using the settings we applied to it in the constructor
            _window.Create();

            _graphics.WindowHandle = _window.Handle; // set the target handle before calling Setup()
            _graphics.Setup();

            // creates a simple font with no additional style
            _font = _graphics.CreateFont("Tahoma", 14); // Arial

            //colors for brushes will be automatically normalized. 0.0f - 1.0f and 0.0f - 255.0f is accepted!
            Clear = _graphics.CreateSolidBrush(0, 0, 0, 0);
            White_Washed = _graphics.CreateSolidBrush(255, 255, 255, 75);

            Red = _graphics.CreateSolidBrush(237, 10, 63);
            Scarlet = _graphics.CreateSolidBrush(253, 14, 53);
            Red_Orange = _graphics.CreateSolidBrush(255, 104, 31);
            Orange = _graphics.CreateSolidBrush(255, 136, 51);
            Yellow_Orange = _graphics.CreateSolidBrush(255, 174, 66);
            Yellow = _graphics.CreateSolidBrush(251, 232, 112);
            Green_Yellow = _graphics.CreateSolidBrush(241, 231, 136);
            Yellow_Green = _graphics.CreateSolidBrush(197, 225, 122);
            Green = _graphics.CreateSolidBrush(58, 166, 85);
            Blue_Green = _graphics.CreateSolidBrush(0, 149, 183);
            Cerulean = _graphics.CreateSolidBrush(2, 164, 211);
            Blue = _graphics.CreateSolidBrush(0, 102, 255);
            Bluetiful = _graphics.CreateSolidBrush(60, 105, 231);
            Indigo = _graphics.CreateSolidBrush(79, 105, 198);
            Blue_Violet = _graphics.CreateSolidBrush(100, 86, 183);
            Violet = _graphics.CreateSolidBrush(131, 89, 163);
            Red_Violet = _graphics.CreateSolidBrush(187, 51, 133);
            Carnation_Pink = _graphics.CreateSolidBrush(255, 166, 201);
            Violet_Red = _graphics.CreateSolidBrush(247, 70, 138);
            Brown = _graphics.CreateSolidBrush(175, 89, 62);
            Apricot = _graphics.CreateSolidBrush(253, 213, 177);
            Black = _graphics.CreateSolidBrush(0, 0, 0);
            Gray = _graphics.CreateSolidBrush(139, 134, 128);
            White = _graphics.CreateSolidBrush(255, 255, 255);

            Radical_Red = _graphics.CreateSolidBrush(255, 53, 94);
            Wild_Watermelon = _graphics.CreateSolidBrush(253, 91, 120);
            Outrageous_Orange = _graphics.CreateSolidBrush(255, 96, 55);
            Atomic_Tangerine = _graphics.CreateSolidBrush(255, 153, 102);
            Neon_Carrot = _graphics.CreateSolidBrush(255, 153, 51);
            Sunglow = _graphics.CreateSolidBrush(255, 204, 51);
            Laser_Lemon = _graphics.CreateSolidBrush(255, 255, 102);
            Unmellow_Yellow = _graphics.CreateSolidBrush(255, 255, 102);
            Electric_Lime = _graphics.CreateSolidBrush(204, 255, 0);
            Screamin_Green = _graphics.CreateSolidBrush(102, 255, 102);
            Magic_Mint = _graphics.CreateSolidBrush(170, 240, 209);
            Blizzard_Blue = _graphics.CreateSolidBrush(80, 191, 230);
            Shocking_Pink = _graphics.CreateSolidBrush(255, 110, 255);
            Razzle_Dazzle_Rose = _graphics.CreateSolidBrush(238, 52, 210);
            Hot_Magenta = _graphics.CreateSolidBrush(255, 0, 204);
            Purple_Pizzazz = _graphics.CreateSolidBrush(255, 0, 204);

            Bright_Chartreuse = _graphics.CreateSolidBrush(223, 255, 17);
            Bright_Green = _graphics.CreateSolidBrush(102, 255, 0);
            Bright_Magenta = _graphics.CreateSolidBrush(255, 8, 232);
            Bright_Pink = _graphics.CreateSolidBrush(254, 1, 177);
            Bright_Purple = _graphics.CreateSolidBrush(190, 3, 253);
            Bright_Red = _graphics.CreateSolidBrush(255, 0, 13);
            Bright_Saffron = _graphics.CreateSolidBrush(255, 207, 9);
            Bright_Scarlet = _graphics.CreateSolidBrush(252, 14, 52);
            Bright_Teal = _graphics.CreateSolidBrush(1, 249, 198);
            Electric_Crimson = _graphics.CreateSolidBrush(255, 0, 63);
            Electric_Cyan = _graphics.CreateSolidBrush(15, 240, 252);
            Electric_Flamingo = _graphics.CreateSolidBrush(252, 116, 253);
            Electric_Green = _graphics.CreateSolidBrush(33, 252, 13);
            Electric_Indigo = _graphics.CreateSolidBrush(102, 0, 255);
            Electric_Orange = _graphics.CreateSolidBrush(255, 53, 3);
            Electric_Pink = _graphics.CreateSolidBrush(255, 4, 144);
            Electric_Purple = _graphics.CreateSolidBrush(191, 0, 255);
            Electric_Red = _graphics.CreateSolidBrush(230, 0, 0);
            Electric_Sheep = _graphics.CreateSolidBrush(85, 255, 255);
            Electric_Violet = _graphics.CreateSolidBrush(143, 0, 241);
            Electric_Yellow = _graphics.CreateSolidBrush(255, 252, 0);
            Fluorescent_Green = _graphics.CreateSolidBrush(8, 255, 8);
            Fluorescent_Orange = _graphics.CreateSolidBrush(255, 207, 0);
            Fluorescent_Pink = _graphics.CreateSolidBrush(254, 20, 147);
            Fluorescent_Red = _graphics.CreateSolidBrush(255, 85, 85);
            Fluorescent_Red_Orange = _graphics.CreateSolidBrush(252, 132, 39);
            Fluorescent_Turquoise = _graphics.CreateSolidBrush(0, 253, 255);
            Fluorescent_Yellow = _graphics.CreateSolidBrush(204, 255, 2);
            Light_Neon_Pink = _graphics.CreateSolidBrush(255, 17, 255);
            Neon_Blue = _graphics.CreateSolidBrush(4, 217, 255);
            Neon_Fuchsia = _graphics.CreateSolidBrush(254, 65, 100);
            Neon_Green = _graphics.CreateSolidBrush(57, 255, 20);
            Neon_Pink = _graphics.CreateSolidBrush(254, 1, 154);
            Neon_Purple = _graphics.CreateSolidBrush(188, 19, 254);
            Neon_Red = _graphics.CreateSolidBrush(255, 7, 58);
            Neon_Yellow = _graphics.CreateSolidBrush(207, 255, 4);
            Pinkish_Red_Neon = _graphics.CreateSolidBrush(255, 0, 85);
        }

        private void _window_SizeChanged(object sender, OverlaySizeEventArgs e)
        {
            if (_graphics == null) return;

            if (_graphics.IsInitialized)
            {
                _graphics.Resize(e.Width, e.Height);
            }
            else
            {
                _graphics.Width = e.Width;
                _graphics.Height = e.Height;
            }
        }
        #endregion

    }
}
