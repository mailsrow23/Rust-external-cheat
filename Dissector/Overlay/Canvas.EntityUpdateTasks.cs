using SharpDX;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Dissector.Overlay
{
    public partial class Canvas
    {
        private static bool _isUpdatingEntities;
        private static bool _isUpdatingPlayers;
        private static bool _isUpdatingSulfurNodes;
        private static bool _isUpdatingMetalNodes;
        private static bool _isUpdatingStoneNodes;
        private static bool _isUpdatingLootNodes;
        private static bool _isUpdatingMilitaryCrates;
        private static bool _isUpdatingWoodenLootCrates;
        private static bool _isUpdatingStorageContainers;
        private static bool _isUpdatingAnimals;
        private static bool _isUpdatingToolCupboards;
        private static bool _isUpdatingHempNodes;

        /* These dictionaries are iterated on and used for drawing, so we keep them updated in our UpdateEntities task */
        private static ConcurrentDictionary<long, EntityInfo> _playerEntities;
        private static ConcurrentDictionary<long, EntityInfo> _sleeperEntities;
        private static ConcurrentDictionary<long, EntityInfo> _resourceEntities;
        private static ConcurrentDictionary<long, EntityInfo> _sulfurOreEntities;
        private static ConcurrentDictionary<long, EntityInfo> _metalOreEntities;
        private static ConcurrentDictionary<long, EntityInfo> _stoneOreEntities;
        private static ConcurrentDictionary<long, EntityInfo> _animalEntities;
        private static ConcurrentDictionary<long, EntityInfo> _militaryCrates;
        private static ConcurrentDictionary<long, EntityInfo> _woodenLootCrates;
        private static ConcurrentDictionary<long, EntityInfo> _storageContainers;
        private static ConcurrentDictionary<long, EntityInfo> _hempNodes;
        private static ConcurrentDictionary<long, EntityInfo> _toolCupboards;


        /// <summary>
        /// Finds the distance between local player and some object in the game
        /// </summary>
        /// <param name="localPlayer"></param>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static float GetDistance(Vector3 localPlayer, Vector3 gameObject)
        {
            float deltaX = localPlayer.X - gameObject.X;
            float deltaY = localPlayer.Y - gameObject.Y;
            float deltaZ = localPlayer.Z - gameObject.Z;

            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        }

        /// <summary>
        /// Separate this out since we'll have several 
        /// </summary>
        private static void InitializeDrawingDictionaries()
        {
            _playerEntities = new ConcurrentDictionary<long, EntityInfo>();
            _sleeperEntities = new ConcurrentDictionary<long, EntityInfo>();
            _resourceEntities = new ConcurrentDictionary<long, EntityInfo>();
            _sulfurOreEntities = new ConcurrentDictionary<long, EntityInfo>();
            _metalOreEntities = new ConcurrentDictionary<long, EntityInfo>();
            _stoneOreEntities = new ConcurrentDictionary<long, EntityInfo>();
            _animalEntities = new ConcurrentDictionary<long, EntityInfo>();
            _woodenLootCrates = new ConcurrentDictionary<long, EntityInfo>();
            _storageContainers = new ConcurrentDictionary<long, EntityInfo>();
            _hempNodes = new ConcurrentDictionary<long, EntityInfo>();
            _toolCupboards = new ConcurrentDictionary<long, EntityInfo>();
            _militaryCrates = new ConcurrentDictionary<long, EntityInfo>();
        }

        /// <summary>
        /// Initializes the types of entities we'll kee updated
        /// </summary>
        private static void InitializeWhatWeUpdate()
        {
            _isUpdatingEntities = true;
            _isUpdatingPlayers = true;
            _isUpdatingSulfurNodes = false;
            _isUpdatingMetalNodes = false;
            _isUpdatingStoneNodes = false;
            _isUpdatingMilitaryCrates = true;
            _isUpdatingWoodenLootCrates = true;
            _isUpdatingAnimals = true;
            _isUpdatingHempNodes = false;
            _isUpdatingToolCupboards = false;
            _isUpdatingStorageContainers = false;
        }

        /// <summary>
        /// Manages what players are tracked for drawing
        /// 
        /// Currently Active & Sleeping players are tracked
        /// </summary>
        private static void UpdateEntities()
        {
            InitializeDrawingDictionaries();

            InitializeWhatWeUpdate();

            while (_isUpdatingEntities)
            {
                ProcessPlayerEntities();
                ProcessResourceEntities();
                ProcessLootEntities();
                Thread.Sleep(1);
            }
        }
    }
}