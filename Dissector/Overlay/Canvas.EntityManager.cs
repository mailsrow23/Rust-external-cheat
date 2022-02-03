using Dissector.Helpers;
using System;


namespace Dissector.Overlay
{
    public partial class Canvas
    {
        /* Objects used in the entity update tasks for getting coord/distance */
        private static GameObjectBase _localPlayerPosition;
        private static GameObjectBase _localPlayerRotation;

        private static GameObjectManager _gameObjectManager;
        private static IntPtr _memHelper;
        private static Action<string> _logActivity;

        /* Manages the SharpDx render loop */
        private static bool _doStopRenderLoop;

        public static GameObjectManager GameObjectManager { get { return _gameObjectManager; } }

        /// <summary>
        /// Dumps objects/entities of our choosing from the game object manager
        /// </summary>
        /// <param name="enableDebugging"></param>
        public static void StartManagingGameObjects(bool enableDebugging, IntPtr memHelper, Action<string> logActivity)
        {
            try
            {
                _memHelper = memHelper;

                _logActivity = logActivity;

                StartGameObjectManager(enableDebugging);
            }
            catch (Exception ex)
            {
                var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logActivity(message);
            }
        }

        /// <summary>
        ///  Method used to tie to the event UpdateLocalPlayerPosition, in GameObjectManager
        /// </summary>
        /// <param name="position"></param>
        private static void _gameObjectManager_UpdateLocalPlayerPosition(GameObjectBase position)
        {
            _localPlayerPosition = position;
        }

        /// <summary>
        /// Method used to tie to the event UpdateMainCameraRotation, in GameObjectManager
        /// </summary>
        /// <param name="position"></param>
        private static void _gameObjectManager_UpdateLocalPlayerRotation(GameObjectBase rotation)
        {
            _localPlayerRotation = rotation;
        }

        /// <summary>
        /// Prepare our manager to start reading game objects
        /// </summary>
        private static void StartGameObjectManager(bool enableDebugging)
        {
            try
            {
                CreateGameObjectManager();
                SubscribeToLocalPlayerUpdates();
                InitializeOffsets();

                _gameObjectManager.ToggleDebugging(enableDebugging);
            }
            catch (Exception ex)
            {
                var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logActivity(message);
            }
        }

        /// <summary>
        /// Gets the base address and initializes the game object manager
        /// </summary>
        private static void CreateGameObjectManager()
        {
            ulong cr3;

            ProcessHelper.SetActivityLog(_logActivity);

            var gameObjectManagerAddress = ProcessHelper.GetGameManager(_memHelper, out cr3);

            _gameObjectManager = new GameObjectManager(gameObjectManagerAddress, _memHelper, cr3, _logActivity);
        }

        /// <summary>
        /// New up our paths to the offset once instead of everytime we need to access it
        /// </summary>
        private static void InitializeOffsets()
        {
            OffsetStructs.Load();
        }

        /// <summary>
        /// Subscribe to local player updates
        /// </summary>
        public static void SubscribeToLocalPlayerUpdates()
        {
            _gameObjectManager.UpdateLocalPlayerPosition += _gameObjectManager_UpdateLocalPlayerPosition;
            _gameObjectManager.UpdateMainCameraRotation += _gameObjectManager_UpdateLocalPlayerRotation;
        }

        /// <summary>
        /// Unsubscribe to local player updates
        /// </summary>
        public static void UnsubscribeToLocalPlayerUpdates()
        {
            _gameObjectManager.UpdateLocalPlayerPosition -= _gameObjectManager_UpdateLocalPlayerPosition;
            _gameObjectManager.UpdateMainCameraRotation -= _gameObjectManager_UpdateLocalPlayerRotation;
        }

        /// <summary>
        /// Set the the EntityUpdate tasks to stop running and clear out the dictionaries they read/write to
        /// </summary>
        public static void Stop()
        {
            try
            {
                if (GameObjectManager != null && GameObjectManager.Players != null)
                {
                    GameObjectManager.Players.Clear();

                    if (GameObjectManager.Animals != null)
                        GameObjectManager.Animals.Clear();

                    if (GameObjectManager.SulfurNodes != null)
                        GameObjectManager.SulfurNodes.Clear();

                    if (GameObjectManager.StoneNodes != null)
                        GameObjectManager.StoneNodes.Clear();

                    if (GameObjectManager.MetalNodes != null)
                        GameObjectManager.MetalNodes.Clear();

                    if (GameObjectManager.MilitaryCrates != null)
                        GameObjectManager.MilitaryCrates.Clear();

                    if (GameObjectManager.WoodenLootCrates != null)
                        GameObjectManager.WoodenLootCrates.Clear();

                    if (GameObjectManager.HempNodes != null)
                        GameObjectManager.HempNodes.Clear();

                    if (GameObjectManager.ToolCupboards != null)
                        GameObjectManager.ToolCupboards.Clear();

                    if (GameObjectManager.StorageContainers != null)
                        GameObjectManager.StorageContainers.Clear();

                }

                /* Stop the updating tasks and then clear the lists */
                _isUpdatingEntities = false;
                _isUpdatingAnimals = false;
                _isUpdatingMetalNodes = false;
                _isUpdatingStoneNodes = false;
                _isUpdatingPlayers = false;
                _isUpdatingSulfurNodes = false;
                _isUpdatingWoodenLootCrates = false;
                _isUpdatingMilitaryCrates = false;
                _isUpdatingHempNodes = false;
                _isUpdatingToolCupboards = false;
                _isUpdatingStorageContainers = false;

                _militaryCrates.Clear();
                _woodenLootCrates.Clear();
                _playerEntities.Clear();
                _sleeperEntities.Clear();
                _sulfurOreEntities.Clear();
                _metalOreEntities.Clear();
                _stoneOreEntities.Clear();
                _animalEntities.Clear();
                _storageContainers.Clear();
                _toolCupboards.Clear();
                _hempNodes.Clear();

                _doStopRenderLoop = true;
            }
            catch (Exception ex)
            {
                _logActivity(ex.Message);
            }
        }

        public static void StopDrawingMilitaryCrates()
        {
            _isUpdatingMilitaryCrates = false;
            _militaryCrates.Clear();
        }

        public static void DrawMilitaryCrates()
        {
            _isUpdatingMilitaryCrates = true;
        }

        public static void StopDrawingWoodenLootCrates()
        {
            _isUpdatingWoodenLootCrates = false;
            _woodenLootCrates.Clear();
        }

        public static void DrawWoodenLootCrates()
        {
            _isUpdatingWoodenLootCrates = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void StopDrawingSulfurNodes()
        {
            _isUpdatingSulfurNodes = false;
            _sulfurOreEntities.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void DrawSulfurNodes()
        {
            _isUpdatingSulfurNodes = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void StopDrawingMetalNodes()
        {
            _isUpdatingMetalNodes = false;
            _metalOreEntities.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void DrawMetalNodes()
        {
            _isUpdatingMetalNodes = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void StopDrawingStoneNodes()
        {
            _isUpdatingStoneNodes = false;
            _stoneOreEntities.Clear();
        }

        public static void StopDrawingStorageContainers()
        {
            _isUpdatingStorageContainers = false;
            _storageContainers.Clear();
        }

        public static void StopDrawingHempNodes()
        {
            _isUpdatingHempNodes = false;
            _hempNodes.Clear();
        }

        public static void StopDrawingAnimals()
        {
            _isUpdatingAnimals = false;
            _animalEntities.Clear();
        }

        public static void StopDrawingToolCupboards()
        {
            _isUpdatingToolCupboards = false;
            _toolCupboards.Clear();
        }

        public static void DrawAnimals()
        {
            _isUpdatingAnimals = true;
        }

        public static void DrawStoneNodes()
        {
            _isUpdatingStoneNodes = true;
        }

        public static void DrawStorageContainers()
        {
            _isUpdatingStorageContainers = true;
        }

        public static void DrawHempNodes()
        {
            _isUpdatingHempNodes = true;
        }

        public static void DrawToolCupboards()
        {
            _isUpdatingToolCupboards = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void DrawPlayers()
        {
            _isUpdatingPlayers = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stopStart"></param>
        public static void StopDrawingPlayers()
        {
            _isUpdatingPlayers = false;

            if (_gameObjectManager != null)
                _gameObjectManager.Players.Clear();

            _sleeperEntities.Clear();
            _playerEntities.Clear();
        }
    }
}