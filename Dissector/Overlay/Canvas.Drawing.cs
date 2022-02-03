using System;
using Dissector.Helpers;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;


namespace Dissector.Overlay
{
    /// <summary>
    /// All the overlay drawing methods are stored here
    /// </summary>
    public partial class Canvas
    {
        /// <summary>
        /// Draws players that are actively logged in
        /// </summary>
        /// <param name="renderTarget"></param>
        private static void DrawActivePlayers()
        {
            /* Draws active, networkable players using an aquamarinemedium color */
            foreach (var entity in _playerEntities)
            {
                /* All we really need is the distance to the player */
                _renderTarget.DrawEllipse(new Ellipse(new RawVector2(entity.Value.ScreenX, entity.Value.ScreenY), 1, 1), _playerBrush, 1);

                DrawText(String.Format("{0:n}", entity.Value.Distance), _playerBrush, entity.Value.ScreenX, entity.Value.ScreenY + 15);
                DrawText(entity.Value.PlayerActiveItem, _playerBrush, entity.Value.ScreenX, entity.Value.ScreenY + 25);

                /* I prefer to know which players are scientists.  I could care less about actual player names, that will only get me in trouble */
                if (entity.Value.PlayerName.StartsWith("Scientist"))
                {
                    DrawText(entity.Value.PlayerName, _playerBrush, entity.Value.ScreenX, entity.Value.ScreenY + 35);
                }
            }
        }

        /// <summary>
        /// Draws players that are sleeping and currently logged out
        /// </summary>
        /// <param name="renderTarget"></param>
        /// <param name="brush"></param>
        private static void DrawSleepingPlayers()
        {
            /* Draw sleepers as a red dot  */
            foreach (var sleeper in _sleeperEntities)
            {
                _renderTarget.DrawEllipse(new Ellipse(new RawVector2(sleeper.Value.ScreenX, sleeper.Value.ScreenY), 1, 1), _sleeperBrush, 1);
                DrawText(sleeper.Value.PlayerName, _playerBrush, sleeper.Value.ScreenX, sleeper.Value.ScreenY + 25);
            }
        }

        /// <summary>
        /// Draws resources
        /// </summary>
        /// <param name="renderTarget"></param>
        /// <param name="brush"></param>
        private static void DrawResources()
        {
            DrawOverlaySulfurOre();
            DrawOverlayMetalOre();
            DrawOverlayStoneOre();
            DrawOverlayAnimals();
            DrawOverlayHempNodes();
            DrawOverlayStorageContainers();
        }

        /// <summary>
        /// 
        /// </summary>
        private static void DrawLootContainers()
        {
            try
            {
                DrawOverlayMilitaryCrates();
                DrawOverlayNormalCrates();
            }
            catch (Exception ex)
            {
                _logActivity(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        /// <summary>
        /// Draw hemp nodes to the overlay
        /// </summary>
        private static void DrawOverlayHempNodes()
        {
            foreach (var resource in _hempNodes)
            {
                _renderTarget.DrawEllipse(new Ellipse(new RawVector2(resource.Value.ScreenX, resource.Value.ScreenY), 1, 1), _hempBrush, 1);
                DrawText(String.Format("{0:n}", resource.Value.Distance), _hempBrush, resource.Value.ScreenX, resource.Value.ScreenY + 15);
            }
        }

        /// <summary>
        /// Draw storage containers to the overlay
        /// </summary>
        private static void DrawOverlayStorageContainers()
        {
            foreach (var resource in _storageContainers)
            {
                _renderTarget.DrawEllipse(new Ellipse(new RawVector2(resource.Value.ScreenX, resource.Value.ScreenY), 1, 1), _woodenCrateBrush, 1);
                DrawText(String.Format("{0:n}", resource.Value.Distance), _woodenCrateBrush, resource.Value.ScreenX, resource.Value.ScreenY + 15);
            }
        }

        /// <summary>
        /// Draw storage containers to the overlay
        /// </summary>
        private static void DrawOverlayToolCupboards()
        {
            foreach (var resource in _toolCupboards)    
            {
                _renderTarget.DrawEllipse(new Ellipse(new RawVector2(resource.Value.ScreenX, resource.Value.ScreenY), 1, 1), _toolCupboardBrush, 1);
                DrawText(String.Format("{0:n}", resource.Value.Distance), _toolCupboardBrush, resource.Value.ScreenX, resource.Value.ScreenY + 15);
            }
        }

        /// <summary>
        /// Draw sulfur to the overlay
        /// </summary>
        private static void DrawOverlaySulfurOre()
        {
            foreach (var resource in _sulfurOreEntities)
            {
                _renderTarget.DrawEllipse(new Ellipse(new RawVector2(resource.Value.ScreenX, resource.Value.ScreenY), 1, 1), _sulfurBrush, 1);
                DrawText(String.Format("{0:n}", resource.Value.Distance), _sulfurBrush, resource.Value.ScreenX, resource.Value.ScreenY + 15);
            }
        }

        /// <summary>
        /// Draw metal to the overlay
        /// </summary>
        private static void DrawOverlayMetalOre()
        {
            foreach (var resource in _metalOreEntities)
            {
                _renderTarget.DrawEllipse(new Ellipse(new RawVector2(resource.Value.ScreenX, resource.Value.ScreenY), 1, 1), _metalBrush, 1);
                DrawText(String.Format("{0:n}", resource.Value.Distance), _metalBrush, resource.Value.ScreenX, resource.Value.ScreenY + 15);
            }
        }

        /// <summary>
        /// Draw stone to the overlay
        /// </summary>
        private static void DrawOverlayStoneOre()
        {
            foreach (var resource in _stoneOreEntities)
            {
                _renderTarget.DrawEllipse(new Ellipse(new RawVector2(resource.Value.ScreenX, resource.Value.ScreenY), 1, 1), _stoneBrush, 1);
                DrawText(String.Format("{0:n}", resource.Value.Distance), _stoneBrush, resource.Value.ScreenX, resource.Value.ScreenY + 15);
            }
        }

        /// <summary>
        /// Draw animals to the overlay
        /// </summary>
        private static void DrawOverlayAnimals()
        {
            foreach (var animal in _animalEntities)
            {
                _renderTarget.DrawEllipse(new Ellipse(new RawVector2(animal.Value.ScreenX, animal.Value.ScreenY), 1, 1), _animalBrush, 1);
                DrawText(String.Format("{0:n}", animal.Value.Distance), _animalBrush, animal.Value.ScreenX, animal.Value.ScreenY + 15);
                DrawText(String.Format("{0:n}", animal.Value.EntityName), _animalBrush, animal.Value.ScreenX, animal.Value.ScreenY + 25);
            }
        }

        /// <summary>
        /// Draw military crates to the overlay
        /// </summary>
        private static void DrawOverlayMilitaryCrates()
        {
            foreach (var resource in _militaryCrates)
            {
                _renderTarget.DrawEllipse(new Ellipse(new RawVector2(resource.Value.ScreenX, resource.Value.ScreenY), 1, 1), _militaryCrateBrush, 1);
                DrawText(String.Format("{0:n}", resource.Value.Distance), _militaryCrateBrush, resource.Value.ScreenX, resource.Value.ScreenY + 15);
            }
        }

        /// <summary>
        /// Draw normal crates to the overlay
        /// </summary>
        private static void DrawOverlayNormalCrates()
        {
            foreach (var resource in _woodenLootCrates)
            {
                _renderTarget.DrawEllipse(new Ellipse(new RawVector2(resource.Value.ScreenX, resource.Value.ScreenY), 1, 1), _lootBrush, 1);
                DrawText(String.Format("{0:n}", resource.Value.Distance), _lootBrush, resource.Value.ScreenX, resource.Value.ScreenY + 15);
            }
        }

        /// <summary>
        /// Draws a crosshair on the overlay
        /// </summary>
        /// <param name="renderTarget"></param>
        /// <param name="brush"></param>
        public static void DrawCrossHair(RenderTarget renderTarget, SolidColorBrush brush)
        {
            renderTarget.DrawEllipse(new Ellipse(Coordinates.ScreenCenter, 3, 3), brush, 2);
        }

        /// <summary>
        /// Draws text to the screen
        /// </summary>
        /// <param name="text"></param>
        /// <param name="renderTarget"></param>
        /// <param name="brush"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private static void DrawText(string text, SolidColorBrush brush, float x, float y)
        {
            try
            {
                TextLayout layout = new TextLayout(_fontFactory, text ?? "", _font, float.MaxValue, float.MaxValue);

                _renderTarget.DrawTextLayout(new RawVector2(x, y), layout, brush, DrawTextOptions.EnableColorFont);

                layout.Dispose();
            }
            catch (Exception ex)
            {
            }
        }
    }
}