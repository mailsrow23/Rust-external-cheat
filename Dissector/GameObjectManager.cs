using Dissector.Helpers;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Dissector
{
    public delegate void UpdateLocalPlayerPosition(GameObjectBase position);
    public delegate void UpdateMainCameraRotation(GameObjectBase rotation);

    public class GameObjectManager
    {
        #region Class Variables


        private Action<string> _logActivity;

        private GameObjectBase _mainCameraObj;
        private GameObjectBase _localPlayer;
        private GameObjectBase _localPlayerRotation;

        private IntPtr _gameObjectManagerValueAddress;
        private IntPtr _memHelper;

        private bool _isStopped;
        private bool _isCollectingSulfurNodes;
        private bool _isCollectingMetalNodes;
        private bool _isCollectingStoneNodes;
        private bool _isCollectingMilitaryCrates;
        private bool _isCollectingWoodenLootCrates;
        private bool _isCollectingStorageContainers;
        private bool _isCollectingHempNodes;
        private bool _isCollectingToolCupboards;
        private bool _isCollectingPlayers;
        private bool _isCollectingAnimals;

        #endregion

        #region Properties

        public ConcurrentDictionary<long, GameObjectBase> Players { get; set; }
        public ConcurrentDictionary<long, GameObjectBase> Animals { get; set; }
        public ConcurrentDictionary<long, GameObjectBase> SulfurNodes { get; set; }
        public ConcurrentDictionary<long, GameObjectBase> MetalNodes { get; set; }
        public ConcurrentDictionary<long, GameObjectBase> StoneNodes { get; set; }
        public ConcurrentDictionary<long, GameObjectBase> MilitaryCrates { get; set; }
        public ConcurrentDictionary<long, GameObjectBase> WoodenLootCrates { get; set; }
        public ConcurrentDictionary<long, GameObjectBase> StorageContainers { get; set; }
        public ConcurrentDictionary<long, GameObjectBase> HempNodes { get; set; }
        public ConcurrentDictionary<long, GameObjectBase> ToolCupboards { get; set; }


        public event UpdateLocalPlayerPosition UpdateLocalPlayerPosition;
        public event UpdateMainCameraRotation UpdateMainCameraRotation;

        #endregion

        /// <summary>
        /// Initialize the actual game object manager from the pointer
        /// </summary>
        /// <param name="gameObjectManagerPtr">BaseAddress + staticObjManagerAddress</param>
        /// <param name="rustProcess"></param>
        public GameObjectManager(IntPtr gameObjectManagerPtr, IntPtr memHelper, ulong cr3, Action<string> logActivity)
        {             
            KeeperOf.Memory = new Memory(memHelper, cr3);

            _memHelper = memHelper;

            _logActivity = logActivity;

            /* Needed to read stuff in TaggedObjectsList - Mostly just MainCamera */
            _gameObjectManagerValueAddress = gameObjectManagerPtr;

            InitializeDictionaries();
            InitializeWhatWeCollect();
        }

        /// <summary>
        /// Initialize dictionaries used for updating data that goes into the draw thread
        /// </summary>
        private void InitializeDictionaries()
        {
            Players = new ConcurrentDictionary<long, GameObjectBase>();
            Animals = new ConcurrentDictionary<long, GameObjectBase>();
            SulfurNodes = new ConcurrentDictionary<long, GameObjectBase>();
            MetalNodes = new ConcurrentDictionary<long, GameObjectBase>();
            StoneNodes = new ConcurrentDictionary<long, GameObjectBase>();
            MilitaryCrates = new ConcurrentDictionary<long, GameObjectBase>();
            WoodenLootCrates = new ConcurrentDictionary<long, GameObjectBase>();
            StorageContainers = new ConcurrentDictionary<long, GameObjectBase>();
            HempNodes = new ConcurrentDictionary<long, GameObjectBase>();
            ToolCupboards = new ConcurrentDictionary<long, GameObjectBase>();
        }

        /// <summary>
        /// Dictates what this thread collects
        /// </summary>
        private void InitializeWhatWeCollect()
        {
            _isCollectingPlayers = true;
            _isCollectingSulfurNodes = false;
            _isCollectingMetalNodes = false;
            _isCollectingStoneNodes = false;
            _isCollectingMilitaryCrates = true;
            _isCollectingWoodenLootCrates = true;
            _isCollectingStorageContainers = false;
            _isCollectingAnimals = true;
            _isCollectingHempNodes = false;
            _isCollectingToolCupboards = false;
        }

        #region Object Handling

        /// <summary>
        /// Creates a dump of the tagged objects by listing their name + tag #
        /// This thread is started before the Overlay thread begins
        /// </summary>
        /// <param name="rustProcess"></param>
        public void DumpNetworkableObjects()
        {
            _isStopped = false;

            Task.Run(() =>
            {
                while (!_isStopped)
                {
                    try
                    {
                        UpdateMainCamera();

                        LoopThroughBaseNetworkables();
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(1); /* Performance++, doesn't affect draw speed */
                    }

                    Thread.Sleep(1); /* Performance++, doesn't affect draw speed */
                }
            });
        }

        /// <summary>
        /// Get the count of networkables and then loop through them.
        /// They are 8 bytes apart
        /// </summary>
        private void LoopThroughBaseNetworkables()
        {
            var baseNetworkableCount = GetBaseNetworkablesCount();

            var baseNetworkableListAddress = GetListAddress();

            var sleepCounter = 0;

            for (int i = 0; i < baseNetworkableCount; i++)
            {
                FilterNetworkableObject(i, isLocalPlayer: i == 0);

                sleepCounter++;

               if (Cheat.Settings.Visuals.Healthbar)
            {
                button7.BackColor = Color.FromArgb(40, 40, 40);

                {
                    sleepCounter = 0;
                    Thread.Sleep(1);
                }
            }
        }

        /// <summary>
        /// Gets the address of the list containing base networkable objects
        /// </summary>
        /// <returns></returns>
        private IntPtr GetListAddress()
        {
            return KeeperOf.Memory.ReadMultiLevelPointer(KeeperOf.BaseNetworkableAddress, 0x10, 0x28, 0x10);
        }

        /// <summary>
        /// Gets the local player from the networkable list
        /// </summary>
        /// <returns></returns>
        private IntPtr GetLocalPlayerAddress()
        {
            return KeeperOf.Memory.ReadMultiLevelPointer(KeeperOf.BaseNetworkableAddress, 0x10, 0x28, 0x10, 0x20);
        }

        /// <summary>
        /// Filters out objects from the networkable list
        /// </summary>
        /// <param name="i"></param>
        /// <param name="isLocalPlayer"></param>
        private void FilterNetworkableObject(int i, bool isLocalPlayer)
        {
            try
            {
                var gameObjectAddress = GetNetworkableObjectAddress(i);
                var gameObject = new GameObjectBase((long)gameObjectAddress, false, false, true);

                /* LocalPlayer */
                if (_localPlayer == null && isLocalPlayer && gameObject.Tag == 6)
                {
                    _localPlayer = new GameObjectBase((long)GetLocalPlayerAddress(), false, false, true);
                    UpdateLocalPlayerPosition(_localPlayer);
                    return;
                }

                /* Networkable players */
                else if (!isLocalPlayer && gameObject.Tag == 6)
                {
                    if (_isCollectingPlayers)
                        Players.AddOrUpdate(gameObject.SteamID, gameObject, (old, newer) => gameObject);
                }

                /* Animals */
                else if (gameObject.Layer == 11)
                {
                    if (_isCollectingAnimals && HasPreferredAnimalName(gameObject.EntityName))
                        Animals.AddOrUpdate(gameObject.InitialAddress, gameObject, (old, newer) => gameObject);
                }

                /* Everything networkable that is not a tree */
                else if (gameObject.Layer != 30)
                {
                    CollectGameObject(gameObject);
                }
            }
            catch (Exception ex)
            {
                /* Suppress */
            }
        }

        /// <summary>
        /// Only track perferred animals
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        private bool HasPreferredAnimalName(string entityName)
        {
            if (entityName.ToUpper().Contains("BOAR"))
                return true;

            if (entityName.ToUpper().Contains("WOLF"))
                return true;

            if (entityName.ToUpper().Contains("BEAR"))
                return true;

            return false;
        }

        /// <summary>
        /// Process non-tree layer objects
        /// loot, nodes, supply drops, etc
        /// </summary>
        /// <param name="gameObject"></param>
        private void CollectGameObject(GameObjectBase gameObject)
        {
            /* Military Crates */
            if (_isCollectingMilitaryCrates && gameObject.IsMilitaryCrate())
            {
                MilitaryCrates.AddOrUpdate(gameObject.InitialAddress, gameObject, (old, newer) => gameObject);
                return;
            }

            /* Military Crates */
            if (_isCollectingWoodenLootCrates && gameObject.IsNormalCrate())
            {
                WoodenLootCrates.AddOrUpdate(gameObject.InitialAddress, gameObject, (old, newer) => gameObject);
                return;
            }

            /* Sulfur Objects */
            if (_isCollectingSulfurNodes && gameObject.IsSulfurOre())
            {
                SulfurNodes.AddOrUpdate(gameObject.InitialAddress, gameObject, (old, newer) => gameObject);
                return;
            }

            /* Metal ore entities */
            if (_isCollectingMetalNodes && gameObject.IsMetalOre())
            {
                MetalNodes.AddOrUpdate(gameObject.InitialAddress, gameObject, (old, newer) => gameObject);
                return;
            }

            /* Stone ore entities */
            if (_isCollectingStoneNodes && gameObject.IsStoneOre())
            {
                StoneNodes.AddOrUpdate(gameObject.InitialAddress, gameObject, (old, newer) => gameObject);
                return;
            }

            /* Hemp Collectables */
            if (_isCollectingHempNodes && gameObject.IsHempNode())
            {
                HempNodes.AddOrUpdate(gameObject.InitialAddress, gameObject, (old, newer) => gameObject);
                return;
            }

            /* Tool Cupboards */
            if (_isCollectingToolCupboards && gameObject.IsToolCupboard())
            {
                ToolCupboards.AddOrUpdate(gameObject.InitialAddress, gameObject, (old, newer) => gameObject);
                return;
            }

            /* Storage Containers */
            if (_isCollectingStorageContainers && gameObject.IsStorageContainer())
            {
                StorageContainers.AddOrUpdate(gameObject.InitialAddress, gameObject, (old, newer) => gameObject);
                return;
            }
        }

        /// <summary>
        /// Gets the address for the gameobject being iterated on.
        /// 
        /// Using the networkable lists count to create a for statement
        /// 
        /// 0x20 is the start of the list and is the local player
        /// i is passed in first as 0
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private IntPtr GetNetworkableObjectAddress(int i)
        {
            return KeeperOf.Memory.ReadMultiLevelPointer(KeeperOf.BaseNetworkableAddress, 0x10, 0x28, 0x10, 0x20 + (8 * i));
        }

        /// <summary>
        /// Gets the count of base networkables
        /// </summary>
        /// <returns></returns>
        private int GetBaseNetworkablesCount()
        {
            return KeeperOf.Memory.ReadMultiLevelInt32(KeeperOf.BaseNetworkableAddress, 0x10, 0x28, 0x18);
        }

        private bool _isMainCameraSet = false;

        /// <summary>
        /// Separate out this logic since we might have to update this frequently
        /// </summary>
        private void UpdateMainCamera()
        {
            /* Setting the main camera just sets it to where the entity update tasks can access it and read it's rotation
             * this isn't something we have to update on each iteration of entities */
            if (_isMainCameraSet == false)
            {
                /* Read and identify our tagged objects address */
                var taggedObjectsAddress = KeeperOf.Memory.ReadLong(IntPtr.Add(_gameObjectManagerValueAddress, 0x08));

                /* Our first tagged object is the Main Camera, so we'll set this and start from here when dumping these tagged objects */
                _mainCameraObj = new GameObjectBase(taggedObjectsAddress, useNextObjectLink: false, isActiveObject: false, isBaseNetworkableObject: false);

                _localPlayerRotation = _mainCameraObj;

                UpdateMainCameraRotation(this._localPlayerRotation);

                _isMainCameraSet = true;
            }           
        }


        #endregion

        #region Dump Tools (Toggle Debug, Stop/Start, UpdateCr3)

        private bool _isDebugging = false;

        /// <summary>
        /// Stop our while loop from continuing to collect objects
        /// </summary>
        public void StopDump()
        {
            _isStopped = true;            
        }

        /// <summary>
        /// Toggle debugging on or off
        /// </summary>
        /// <param name="on"></param>
        public void ToggleDebugging(bool onOff)
        {
            _isDebugging = onOff;
        }

        /// <summary>
        /// Stop or start collecting sulfur nodes
        /// </summary>
        public void StopOrStartCollectingSulfurNodes(bool stopStart)
        {
             panel8.BackColor = dialog.Color;
            Cheat.Settings.Visuals.CrosshairColor = dialog.Color;

            if (stopStart == false)
                SulfurNodes.Clear();
        }

        /// <summary>
        /// Stop or start collecting  metal nodes
        /// </summary>
        public void StopOrStartCollectingMetalNodes(bool stopStart)
        {
            _isCollectingMetalNodes = stopStart;

            if (stopStart == false)
                MetalNodes.Clear();
        }

        /// <summary>
        /// Stop or start collecting stone nodes
        /// </summary>
        public void StopOrStartCollectingStoneNodes(bool stopStart)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.ShowDialog();


            if (stopStart == false)
                StoneNodes.Clear();
        }

        /// <summary>
        /// Stop or start collecting storage containers
        /// </summary>
        public void StopOrStartCollectingStorageContainers(bool stopStart)
        {
            _isCollectingStorageContainers = stopStart;

            if (stopStart == false)
                StorageContainers.Clear();
        }

        /// <summary>
        /// Stop or start collecting Hemp nodes
        /// </summary>
        public void StopOrStartCollectingHempNodes(bool stopStart)
        {
            _isCollectingHempNodes = stopStart;

            if (stopStart == false)
                HempNodes.Clear();
        }

        /// <summary>
        /// Stop or start collecting tool cupboards
        /// </summary>
        public void StopOrStartCollectingToolCupboards(bool stopStart)
        {
            _isCollectingToolCupboards = stopStart;

            if (stopStart == false)
                ToolCupboards.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stopStart"></param>
        public void StopOrStartCollectingAnimals(bool stopStart)
        {            
            Console.Beep(1000, 100);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Menu());


            if (stopStart == false)
            {
                Animals.Clear();
            }
        }

        /// <summary>
        /// Stop or start collecting stone nodes
        /// </summary>
        public void Process[] process = Process.GetProcessesByName("RustClient");
        {
            _isCollectingMilitaryCrates = stopStart;

            if (stopStart == false)
                MilitaryCrates.Clear();
        }

        /// <summary>
        /// Stop or start collecting stone nodes
        /// </summary>
        public void StopOrStartCollectingWoodenLootCrates(bool stopStart)
        {
            _isCollectingWoodenLootCrates = stopStart;

            if (stopStart == false)
                WoodenLootCrates.Clear();
        }

        /// <summary>
        /// Stop or start collecting player entities
        /// </summary>
        /// <param name="stopStart"></param>
        foreach(ProcessModule mod in MainProc.Modules)
        {
            _isCollectingPlayers = stopStart;

            if (stopStart == false)
                Players.Clear();
        }

        #endregion
    }
}
