using Dissector.Helpers;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Dissector.Helpers.PInvoke;
using D3D11 = SharpDX.Direct3D11;
using DXGI = SharpDX.DXGI;
using FactoryD2D = SharpDX.Direct2D1.Factory;
using FontFactory = SharpDX.DirectWrite.Factory;

namespace Dissector.Overlay
{
    /// <summary>
    /// Most everything here that touches the overlay directly
    /// </summary>
    public partial class Canvas
    {
        private static RenderForm _renderForm;

        private static D3D11.Device _newDevice;

        private static RenderTarget _renderTarget;

        private static SwapChain _newSwapChain;

        private static TextFormat _font;

        private static TextFormat _pFont;

        private static FontFactory _fontFactory;

        #region SharpDx implementation

        /// <summary>
        /// Creates an overlay form and draws to it
        /// </summary>
        public static void RunDxForm()
        {
            _doStopRenderLoop = false;

            InitializeNewOverlayWindow();

            /* Create separate threads/tasks to update our entitys that get used while drawing to the screen
             * These tasks will calculate w2s coordinates from concurrent dictionaries that are constantly updated by our interception */
            RunEntityUpdateTasks();      

            /* Run separate threads that constantly iterate lists in memory and populate the dictionaries that we iterate over while drawing */
            GameObjectManager.DumpNetworkableObjects();          

            RenderLoop.Run(_renderForm, () =>
            {
                try
                {
                    if (_doStopRenderLoop)
                    {
                        EndEntityUpdateTasks();
                        DisposeResources();
                    }

                    /* Prep our render target */
                    BeginDrawing();
                    ClearDrawing();

                    /* Execute draw commands for our entities */
                    DrawEntities();

                    /* End and present our drawing session with swapchain.Present(4, PresentFlags.DoNotWait) for best performance */
                    EndDrawing();
                }
                catch (Exception ex)
                {
                    /* Suppress exceptions, if necessary, write to console to prevent code execution stops */
                    /* Performance++ */
                    Thread.Sleep(22);
                }

                /* Performance++ ... The higher the sleep, the better the in-game fps but slower drawings to the screen
                 * I also think there's a correlation to the amount of RAM you have, the more RAM you have, the lesser this sleep can be */
                Thread.Sleep(22);
            });
        }

        /// <summary>
        /// Beginning of our draw calls
        /// </summary>
        private static void BeginDrawing()
        {
            _renderTarget.BeginDraw();
            _renderTarget.AntialiasMode = AntialiasMode.Aliased;
            _renderTarget.TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode.Aliased;
            _renderTarget.Transform = Matrix3x2.Identity;
        }

        /// <summary>
        /// Clears the screen for updated drawings
        /// </summary>
        private static void ClearDrawing()
        {
            _renderTarget.Clear(SharpDX.Color.White);
        }

        /// <summary>
        /// Renders the drawings to the screen
        /// </summary>
        private static void EndDrawing()
        {
            _renderTarget.EndDraw();
            _newSwapChain.Present(4, PresentFlags.DoNotWait);
        }

        /// <summary>
        /// Draw entities if selected 
        /// </summary>
        private static void DrawEntities()
        {
            DrawActivePlayers();
            DrawSleepingPlayers();
            DrawResources();
            DrawLootContainers();
        }

        /// <summary>
        /// Start up our tasks that will constantly update our list with entities that have w2s x,y coordinates
        /// </summary>
        private static void RunEntityUpdateTasks()
        {
            Task.Run(() => UpdateEntities());
        }

        /// <summary>
        /// Initializes a transparent overlay window used for drawing on
        /// </summary>
        private static void InitializeNewOverlayWindow()
        {
            _renderForm = CreateRenderForm();

            // Create Device and SwapChain
            var desc = CreateSwapChainDescription();

            D3D11.Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, desc, out _newDevice, out _newSwapChain);

            IgnoreWindowsEvents();

            CreateRenderTarget();

            /* Sets the overlay on top of some window of our choosing
               Optionally run this separately in it's own thread to maintain having the overlay over your windowed game whenever the game window moves*/
            SetWindow();

            /* Things that the overlay window might consume (brushes, etc.) */
            InitializeBrushes();

            SetFont();
        }

        /// <summary>
        /// 
        /// </summary>
        private static void IgnoreWindowsEvents()
        {
            DXGI.Factory factory;
            // Ignore all windows events
            factory = _newSwapChain.GetParent<DXGI.Factory>();
            factory.MakeWindowAssociation(_renderForm.Handle, WindowAssociationFlags.IgnoreAll);
        }

        /// <summary>
        /// 
        /// </summary>
        private static void CreateRenderTarget()
        {
            var d2dFactory = new SharpDX.Direct2D1.Factory();

            // New RenderTargetView from the backbuffer
            Texture2D backBuffer = Texture2D.FromSwapChain<Texture2D>(_newSwapChain, 0);

            Surface surface = backBuffer.QueryInterface<Surface>();

            _renderTarget = new RenderTarget(d2dFactory, surface, new RenderTargetProperties(new PixelFormat(Format.R8G8B8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Ignore)));
        }

        /// <summary>
        /// 
        /// </summary>
        private static void SetFont()
        {
            _fontFactory = new SharpDX.DirectWrite.Factory();
            _font = new TextFormat(_fontFactory, "Verdana", FontWeight.ExtraBlack, FontStyle.Normal, FontStretch.Normal, 9);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static SwapChainDescription CreateSwapChainDescription()
        {
            var desc = new SwapChainDescription()
            {
                BufferCount = 2,
                ModeDescription = new ModeDescription(_renderForm.ClientSize.Width, _renderForm.ClientSize.Height, new Rational(144, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = _renderForm.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput | Usage.BackBuffer
            };

            return desc;
        }

        /// <summary>
        /// Dispose of any unused resources when we turn our overlay off
        /// </summary>
        private static void DisposeResources()
        {
            DisposeBrushes();
            DisposeOverlayComponents();
        }

        /// <summary>
        /// Dispose anything to do with the overlay form
        /// </summary>
        private static void DisposeOverlayComponents()
        {
            _renderTarget.Dispose();
            _renderForm.Dispose();
            _newDevice.ImmediateContext.ClearState();
            _newDevice.ImmediateContext.Flush();
            _newDevice.Dispose();
            _newSwapChain.Dispose();
            _fontFactory.Dispose();
            _font.Dispose();
        }

        /// <summary>
        /// End any unecessary long-running tasks when we turn off our overlay
        /// </summary>
        private static void EndEntityUpdateTasks()
        {
            _isUpdatingEntities = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameWindowName"></param>
        /// <returns></returns>
        private static bool GameIsOpen(string gameWindowName, out IntPtr gameHandle)
        {
            gameHandle = IntPtr.Zero;
            gameHandle = PInvoke.FindWindow(null, gameWindowName);
            return gameHandle != IntPtr.Zero;
        }

        /// <summary>
        /// Sets our render form over our render target (usually a game window)
        /// </summary>
        /// <param name="sender"></param>
        private static void SetWindow()
        {
            IntPtr gameHandle;
            if (GameIsOpen("Rust", out gameHandle))
            {
                RECT gameWindowRect = default(RECT);
                PInvoke.GetWindowRect(gameHandle, out gameWindowRect);

                RECT gameWindowClient = default(RECT);
                PInvoke.GetClientRect(gameHandle, out gameWindowClient);

                SharpDX.Rectangle rectToChangeTo = new Rectangle();

                /* Retreives information about the window styles */
                int windowLong = PInvoke.GetWindowLong(gameHandle, (int)PInvoke.GetWindowLongConst.GWL_STYLE);

                rectToChangeTo.Width = gameWindowRect.Right - gameWindowRect.Left;
                rectToChangeTo.Height = gameWindowRect.Bottom - gameWindowRect.Top;

                /* If we got the window styles and the border is thicker than 0 */
                bool hasThickBorders = ((long)windowLong & (long)((ulong)PInvoke.WindowStyles.WS_BORDER)) != 0L;

                if (hasThickBorders)
                {
                    UpdateWindow(gameWindowRect, rectToChangeTo, gameWindowClient);
                }

                MoveWindow(gameWindowRect, rectToChangeTo);
            }
        }

        /// <summary>
        /// Decoupled move window from SetWindow
        /// </summary>
        /// <param name="gameWindowRect"></param>
        /// <param name="rectToChangeTo"></param>
        private static void MoveWindow(RECT gameWindowRect, Rectangle rectToChangeTo)
        {
            if (_renderForm.IsDisposed == false)
                _renderForm.Invoke((MethodInvoker)(() => PInvoke.MoveWindow(_renderForm.Handle, gameWindowRect.Left, gameWindowRect.Top, rectToChangeTo.Width, rectToChangeTo.Height, true)));
        }

        /// <summary>
        /// Some stuff we do for windows with thicker borders
        /// </summary>
        /// <param name="gameWindowRect"></param>
        /// <param name="rectToChangeTo"></param>
        /// <param name="gameWindowClient"></param>
        private static void UpdateWindow(RECT gameWindowRect, Rectangle rectToChangeTo, RECT gameWindowClient)
        {
            int gameWindowHeight = gameWindowRect.Bottom - gameWindowRect.Top;
            int gameWindowWidth = gameWindowRect.Right - gameWindowRect.Left;

            rectToChangeTo.Height = gameWindowClient.Bottom - gameWindowClient.Top;
            rectToChangeTo.Width = gameWindowClient.Right - gameWindowClient.Left;

            int newHeight = gameWindowHeight - gameWindowClient.Bottom;
            int newWidth = (gameWindowWidth - gameWindowClient.Right) / 2;

            newHeight -= newWidth;

            gameWindowRect.Left += newWidth;
            gameWindowRect.Top += newHeight;

            rectToChangeTo.Left = gameWindowRect.Left;
            rectToChangeTo.Top = gameWindowRect.Top;
        }

        /// <summary>
        /// The render target for our drawings
        /// </summary>
        /// <param name="backBuffer"></param>
        /// <returns></returns>
        private static RenderTarget InitializeRenderTarget(Surface backBuffer)
        {
            RenderTarget renderTarget;

            using (var factory = new FactoryD2D())
            {
                // Get desktop DPI
                var dpi = factory.DesktopDpi;

                // Create bitmap render target from DXGI surface
                renderTarget = new RenderTarget(factory, backBuffer, new RenderTargetProperties()
                {
                    DpiX = dpi.Width,
                    DpiY = dpi.Height,
                    MinLevel = SharpDX.Direct2D1.FeatureLevel.Level_DEFAULT,
                    PixelFormat = new PixelFormat(Format.R8G8B8A8_UNorm /* Use same as what is in the swap chain */, SharpDX.Direct2D1.AlphaMode.Ignore),
                    Type = RenderTargetType.Hardware, /* Use our GPU for rendering */
                    Usage = RenderTargetUsage.None
                });
            }

            return renderTarget;
        }

        /// <summary>
        /// Sharpdx form that we'll be overlaying our window with
        /// </summary>
        /// <returns></returns>
        private static RenderForm CreateRenderForm()
        {
            RECT clientRect;

            /* Set our render forms client size the same as the rust window client size */
            var rustClientRect = PInvoke.GetClientRect(PInvoke.FindWindow(null, "Rust"), out clientRect);

            var screenWidth = clientRect.Right - clientRect.Left;
            var screenHeight = clientRect.Bottom - clientRect.Top;

            Coordinates.ScreenWidth = screenWidth;
            Coordinates.ScreenHeight = screenHeight;
            Coordinates.ScreenCenter = new SharpDX.Mathematics.Interop.RawVector2(screenWidth / 2, screenHeight / 2);

            // Create render target window
            var form = new RenderFormEx
            {
                BackColor = System.Drawing.Color.White,
                ClientSize = new System.Drawing.Size(screenWidth, screenHeight),
                Name = "Get Back To Work! - Alarm Form",
                AllowUserResizing = false,
                StartPosition = System.Windows.Forms.FormStartPosition.Manual,
                Text = "",
                TopMost = true,
                FormBorderStyle = FormBorderStyle.None,
                TransparencyKey = System.Drawing.Color.White,
                ShowInTaskbar = false,
                ShowIcon = false
            };

            form.FormClosing += Form_FormClosing;

            return form;
        }

        /// <summary>
        /// Dispose resources before our form closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeResources();            
        }

        #endregion
    }
}