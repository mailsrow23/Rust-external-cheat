using Dissector.Helpers;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Dissector
{
    /// <summary>
    /// Wrapper for Tagged Objects in the Game Object Manager
    /// </summary>
    public class GameObjectBase 
    {
        #region Class members that hold memory addresses for this game object

        private long _nextObjectAddress;
        private IntPtr _objectAddressPtr;
        private IntPtr _nextObjectAddressPtr;


        #endregion

        #region Encapsulated class members that shouldn't change

        /// <summary>
        /// These values shouldn't need to be read each time a call to the public property is needed
        /// so we'll store local values and read these instead of reading the memory each time expecting the value to change
        /// </summary> 
        private Int16? _tag;
        private Int16 _layer;
        private Int16 _lifeState;
        private string _name;
        private Vector3 _positionVector;
        private SharpDX.Mathematics.Interop.RawMatrix _rotationMatrix;
        private string _playerNameUnicode;
        private IntPtr _position;
        private IntPtr _rotation;
        private IntPtr _networkablePtr;
        private IntPtr _playerFlagsPtr;
        private bool _isActiveObject;
        private bool _isNetworkableObject;
        private unsafe long _steamId;

        #endregion

        /// <summary>
        /// Helps us determine where we are in the object structure when 'getting' properties
        /// </summary>
        private bool _useNextAddress;

        /// <summary>
        /// Address passed in from the constructor (initial meaning before 0x10 or 0x08 added to it)
        /// </summary>
        public long InitialAddress { get; set; }

        /// <summary>
        /// The next object address (this objects address + 0x08)
        /// We use this in our loop to compare the last tagged object address to this address 
        /// </summary>        
        public long NextObjectAddress
        {
            get { return _nextObjectAddress; }
            set { _nextObjectAddress = value; }
        }

        /// <summary>
        /// Gets the position for this game object
        /// 
        /// address ] + 0x10 ] + 0x30 ] + 0x08 ] + 0x38 ] + 0x90
        /// 
        /// This object should constantly change for pretty much everything except for stationary objects (rocks, collectibles, etc.)
        /// </summary>
        public unsafe Vector3 Position
        {
            get
            {
                /* The position of an 'active object' should not change so we can store it here 
                 * i.e. don't re-read the memory if we don't need to.  Hopefully position for active objects isn't reused by other objects */
                if (_isActiveObject && _positionVector.Z != 0 && _positionVector.Y != 0 && _positionVector.X != 0)
                    return _positionVector;

                if (_isNetworkableObject)
                {
                    _positionVector = KeeperOf.Memory.ReadMultiLevelManaged<Vector3>(_objectAddressPtr, OffsetStructs.PositionOffsetPath.FromBaseNetworkablePath);
                    return _positionVector;
                }

                if (_useNextAddress) /* Indicates we're at the next object link so we have to read 0x10 first */
                {
                    _positionVector = KeeperOf.Memory.ReadMultiLevelManaged<Vector3>(_nextObjectAddressPtr, OffsetStructs.PositionOffsetPath.FromNextObjectLink);
                    return _positionVector;
                }
                else
                {
                    _positionVector = KeeperOf.Memory.ReadMultiLevelManaged<Vector3>(_objectAddressPtr, OffsetStructs.PositionOffsetPath.FromInitialObject);
                    return _positionVector;
                }
            }

            private set { _positionVector = value; }
        }

        /// <summary>
        /// This is used as the key for player dictionary
        /// </summary>
        public unsafe long SteamID
        {
            get
            {
                if (_isNetworkableObject)
                {
                    if (_steamId == 0)
                    {
                        _steamId = KeeperOf.Memory.ReadMultiLevelLong(_objectAddressPtr, OffsetStructs.EntitySteamID.FromBaseNetworkablePath);
                        return _steamId;
                    }
                    else
                    {
                        return _steamId;
                    }
                }
                else
                {
                    return 0;
                }
            }
            set { _steamId = value; }
        }

        /// <summary>
        /// Gets the Rotation from the Main Camera viewmatrix
        /// </summary>
        public unsafe SharpDX.Matrix Rotation
        {
            get
            {
                if (_useNextAddress)
                {
                    return KeeperOf.Memory.ReadMultiLevelManaged<Matrix>(_nextObjectAddressPtr, OffsetStructs.RotationOffsetPath.FromNextObjectLink);
                }
                else
                {
                    return KeeperOf.Memory.ReadMultiLevelManaged<Matrix>(_objectAddressPtr, OffsetStructs.RotationOffsetPath.FromInitialObject);
                }
            }

            private set { _rotationMatrix = value; }
        }

        /// <summary>
        /// Reads the layer integer value for this object (basically a category that this object would fall into.)
        /// </summary>
        public unsafe int Networkable
        {
            get
            {
                if (_isNetworkableObject)
                {
                    return KeeperOf.Memory.ReadMultiLevelInt32(_objectAddressPtr, OffsetStructs.NetworkableOffsetPath.FromBaseNetworkablePath);
                }
                if (_useNextAddress)
                {
                    return KeeperOf.Memory.ReadMultiLevelInt32(_nextObjectAddressPtr, OffsetStructs.NetworkableOffsetPath.FromNextObjectLink);
                }
                else
                {
                    return KeeperOf.Memory.ReadMultiLevelInt32(_objectAddressPtr, OffsetStructs.NetworkableOffsetPath.FromInitialObject);
                }
            }

            private set { _networkablePtr = IntPtr.Zero; }
        }

        /// <summary>
        /// Reads a players active item uid
        /// we use this to compare it to the itemuid on a players belt to get the name of their active item
        /// </summary>
        public unsafe int ActiveItemUID
        {
            get
            {
                if (_isNetworkableObject)
                {
                    return KeeperOf.Memory.ReadMultiLevelInt32(_objectAddressPtr, OffsetStructs.ActiveItem.FromBaseNetworkablePath);
                }

                return 0;
            }
            private set { }
        }
        public unsafe long SteamID
        {
            get
            {
                if (_isNetworkableObject)
                {
                    if (_steamId == 0)
                    {
                        _steamId = KeeperOf.Memory.ReadMultiLevelLong(_objectAddressPtr, OffsetStructs.EntitySteamID.FromBaseNetworkablePath);
                        return _steamId;
                    }
                    else
                    {
                        return _steamId;
                    }
                }
                else
                {
                    return 0;
                }
            }
            set { _steamId = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public unsafe IntPtr PlayerBeltItemList
        {
            get
            {
                if (_isNetworkableObject)
                {
                    return KeeperOf.Memory.ReadMultiLevelPointer(_objectAddressPtr, OffsetStructs.BeltList.FromBaseNetworkablePath);
                }

                return IntPtr.Zero;
            }
        }

        /// <summary>
        /// Name of a players active item
        /// </summary>
        public unsafe string ActiveItemName
        {
            get
            {
                for (int i = 0; i < 6; i++)
                {
                    var itemAddress = PlayerBeltItemList + 0x20 + (0x8 * i);

                    var uidPtr = KeeperOf.Memory.Read<IntPtr>(itemAddress);

                    var uid = KeeperOf.Memory.Read<int>(uidPtr + 0x78);

                    if (uid == ActiveItemUID)
                    {
                        var name = KeeperOf.Memory.ReadMultiLevelUnicodeString(itemAddress, new int[] { 0x10, 0x18, 0x14 });
                        return name;
                    }
                }

                return "";
            }
        }

        /// <summary>
        /// Reads the name of this base entity
        /// </summary>
        public unsafe string PlayerNameUnicode
        {
            get
            {
                if (string.IsNullOrEmpty(_playerNameUnicode))
                {
                    if (_isNetworkableObject)
                    {
                        _playerNameUnicode = KeeperOf.Memory.ReadMultiLevelUnicodeString(_objectAddressPtr, OffsetStructs.PlayerNamePath.FromBaseNetworkablePath);
                    }
                    else if (_useNextAddress)
                    {
                        _playerNameUnicode = KeeperOf.Memory.ReadMultiLevelUnicodeString(_nextObjectAddressPtr, OffsetStructs.PlayerNamePath.FromNextObjectLink);
                    }
                    else
                    {
                        _playerNameUnicode = KeeperOf.Memory.ReadMultiLevelUnicodeString(_objectAddressPtr, OffsetStructs.PlayerNamePath.FromInitialObject);
                    }
                }
                else
                {
                    return _playerNameUnicode;
                }

                return _playerNameUnicode;
            }

            private set { _playerNameUnicode = value; }
        }

        /// <summary>
        /// Reads the name of this object
        /// </summary>
        public unsafe string EntityName
        {
            get
            {
                if (_isNetworkableObject)
                {
                    return KeeperOf.Memory.ReadMultiLevelUTF8String(_objectAddressPtr, OffsetStructs.EntityNamePath.FromBaseNetworkablePath);
                }

                if (_useNextAddress)
                {
                    return KeeperOf.Memory.ReadMultiLevelUTF8String(_nextObjectAddressPtr, OffsetStructs.EntityNamePath.FromNextObjectLink);
                }
                else
                {
                    return KeeperOf.Memory.ReadMultiLevelUTF8String(_objectAddressPtr, OffsetStructs.EntityNamePath.FromInitialObject);
                }
            }

            private set { _name = value; }
        }
        /// <summary>
        /// The tag is a more high level category that this object falls into
        /// </summary>
        public unsafe Int16 Tag
        {
            get
            {
                if (_isNetworkableObject)
                {
                    return KeeperOf.Memory.ReadMultiLevelInt16(_objectAddressPtr, OffsetStructs.EntityTagPath.FromBaseNetworkablePath);
                }
                if (_useNextAddress)
                {
                    return KeeperOf.Memory.ReadMultiLevelInt16(_nextObjectAddressPtr, OffsetStructs.EntityTagPath.FromNextObjectLink);
                }
                else
                {
                    return KeeperOf.Memory.ReadMultiLevelInt16(_objectAddressPtr, OffsetStructs.EntityTagPath.FromInitialObject);
                }
            }

            private set { _tag = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public unsafe Int16 Layer
        {
            get
            {
                if (_isNetworkableObject)
                {
                    return KeeperOf.Memory.ReadMultiLevelInt16(_objectAddressPtr, OffsetStructs.EntityLayerPath.FromBaseNetworkablePath);
                }
                if (_useNextAddress)
                {
                    _layer = KeeperOf.Memory.ReadMultiLevelInt16(_nextObjectAddressPtr,OffsetStructs.EntityLayerPath.FromNextObjectLink);
                }
                else
                {
                    _layer = KeeperOf.Memory.ReadMultiLevelInt16(_objectAddressPtr, OffsetStructs.EntityLayerPath.FromInitialObject);
                }

                return _layer;
            }

            private set { _layer = value; }
        }

        /// <summary>
        /// Determine if the player is sleeping or not
        /// </summary>
        public bool IsSleeping
        {
            get
            {
                return HasPlayerFlag((int)PFlags.Sleeping) ;
            }

            private set { _playerFlagsPtr = IntPtr.Zero; }
        }

        /// <summary>
        /// Is this even being populated anymore by FP?
        /// </summary>
        public Int16 LifeState
        {
            get
            {
                if (_isNetworkableObject)
                {
                    return KeeperOf.Memory.ReadMultiLevelInt16(_objectAddressPtr, OffsetStructs.EntityLifeState.FromBaseNetworkablePath);
                }                

                return 1;
            }

            set { _lifeState = value; }
        }

        /// <summary>
        /// Reads a players health
        /// </summary>
        public float Health
        {
            get
            {
                if (_isNetworkableObject)
                {
                    return KeeperOf.Memory.ReadMultiLevelFloat(_objectAddressPtr, OffsetStructs.EntityHealth.FromBaseNetworkablePath);
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets the player flags
        /// </summary>
        public unsafe int PlayerFlags
        {
            get
            {
                if (_isNetworkableObject)
                {
                    return KeeperOf.Memory.ReadMultiLevelInt32(_objectAddressPtr, OffsetStructs.PlayerFlagsPath.FromBaseNetworkablePath);
                }

                /* Address + 0x08 = We need to get the game object from here */
                if (_useNextAddress)
                {
                    return KeeperOf.Memory.ReadMultiLevelInt32(_nextObjectAddressPtr, OffsetStructs.PlayerFlagsPath.FromNextObjectLink);
                }
                else
                {
                    return KeeperOf.Memory.ReadMultiLevelInt32(_objectAddressPtr, OffsetStructs.PlayerFlagsPath.FromInitialObject);
                }
            }

            private set { _playerFlagsPtr = IntPtr.Zero; }
        }

        /// <summary>
        /// Gets the player flags
        /// </summary>
        public unsafe int PlayerModelFlags
        {
            get
            {
                if (_isNetworkableObject)
                {
                    return KeeperOf.Memory.ReadMultiLevelInt32(_objectAddressPtr, OffsetStructs.EntityPlayerModelFlags.FromBaseNetworkablePath);
                }

                return 0;
            }

            private set { _playerFlagsPtr = IntPtr.Zero; }
        }

        /// <summary>
        /// Ctor
        /// 
        /// From here we have to decide if this "GameObject" starts from the next object link at 0x08 or if it's an initial object to which we start at 0x10
        /// Initial objects are objects at the start of the active and tagged objects.
        /// </summary>
        /// <param name="objectAddress"></param>
        /// <param name="memory"></param>
        /// <param name="useNextObjectLink"></param>
        public GameObjectBase(long objectAddress, bool useNextObjectLink, bool isActiveObject, bool isBaseNetworkableObject)
        {
            _useNextAddress = useNextObjectLink;
            InitialAddress = objectAddress;
            var t = objectAddress.ToString("X");
            _isActiveObject = isActiveObject;
            _rotationMatrix = new SharpDX.Mathematics.Interop.RawMatrix();
            _layer = 0;
            _tag = null;

            _name = "";
            _positionVector = new Vector3();
            _playerNameUnicode = "";
            _networkablePtr = IntPtr.Zero;
            _playerFlagsPtr = IntPtr.Zero;
            _position = IntPtr.Zero;
            _rotation = IntPtr.Zero;
            _steamId = 0;
            _lifeState = 0;

            var objectPtr = new IntPtr(objectAddress);

            _isNetworkableObject = isBaseNetworkableObject;

            /* Read the memory at 0x08 and sets the "NextObjectAddress" which is used if this is an object using the next object link */
            if (_useNextAddress)
            {
                var gameObjectValue = KeeperOf.Memory.ReadLong(IntPtr.Add(objectPtr, LastObjectBase.NextObjectLink));
                _nextObjectAddress = gameObjectValue;

                /* Now initializing the pointers here, once, instead of creating a new one everytime a mem call was used */
                _nextObjectAddressPtr = new IntPtr(gameObjectValue);

                _objectAddressPtr = IntPtr.Zero;
            }
            else if (_isNetworkableObject)
            {
                var gameObjectValue = KeeperOf.Memory.ReadLong(IntPtr.Add(objectPtr, LastObjectBase.GameObject));
                _objectAddressPtr = new IntPtr(gameObjectValue);

                /* Since this is a struct, we have to do something here */
                var nextGameObjectValue = KeeperOf.Memory.ReadLong(IntPtr.Add(objectPtr, LastObjectBase.NextObjectLink));
                _nextObjectAddressPtr = new IntPtr(nextGameObjectValue);
                _nextObjectAddress = nextGameObjectValue;
            }
            else
            {
                var gameObjectValue = KeeperOf.Memory.ReadLong(IntPtr.Add(objectPtr, LastObjectBase.GameObject));
                var nextGameObjectValue = KeeperOf.Memory.ReadLong(IntPtr.Add(objectPtr, LastObjectBase.NextObjectLink));

                /* Now initializing the pointers here, once, instead of creating a new one everytime a mem call was used */
                _objectAddressPtr = new IntPtr(gameObjectValue);
                _nextObjectAddressPtr = new IntPtr(nextGameObjectValue);
                _nextObjectAddress = nextGameObjectValue;
            }
        }

        /// <summary>
        /// Check if the flags contain a specific flag
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private bool HasPlayerFlag(int f)
        {
            return (this.PlayerFlags & f) == f;
        }

        public bool HasFlag(MFlags f)
        {
            return ((MFlags)this.PlayerModelFlags & f) == f;
        }
    }
}
