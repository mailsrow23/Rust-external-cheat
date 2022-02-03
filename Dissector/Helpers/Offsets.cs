namespace Dissector.Helpers
{
    public static class OffsetStructs
    {
        public static EntityPosition PositionOffsetPath;
        public static EntityRotation RotationOffsetPath;
        public static EntityNetworkable NetworkableOffsetPath;
        public static EntityPlayerFlags PlayerFlagsPath;
        public static EntityTag EntityTagPath;
        public static EntityName EntityNamePath;
        public static PlayerName PlayerNamePath;
        public static EntityLayerPath EntityLayerPath;
        public static EntitySteamID EntitySteamID;
        public static EntityLifeState EntityLifeState;
        public static EntityPlayerModelFlags EntityPlayerModelFlags;
        public static PlayerActiveItem ActiveItem;
        public static PlayerBeltItemList BeltList;
        public static EntityHealth EntityHealth;

        public static void Load()
        {
            PositionOffsetPath = new EntityPosition(0);
            RotationOffsetPath = new EntityRotation(0);
            NetworkableOffsetPath = new EntityNetworkable(0);
            PlayerFlagsPath = new EntityPlayerFlags(0);
            EntityTagPath = new EntityTag(0);
            EntityNamePath = new EntityName(0);
            PlayerNamePath = new PlayerName(0);
            EntityLayerPath = new EntityLayerPath(0);
            EntitySteamID = new EntitySteamID(0);
            EntityLifeState = new EntityLifeState(0);
            EntityPlayerModelFlags = new EntityPlayerModelFlags(0);
            ActiveItem = new PlayerActiveItem(0);
            BeltList = new PlayerBeltItemList(0);
            EntityHealth = new EntityHealth(0);
        }
    }

    public struct EntityHealth
    {
        public int[] FromBaseNetworkablePath { get; set; }

        public EntityHealth(int key = 0)
        {
            FromBaseNetworkablePath = new int[] { GameObject.CorrespondingObject, GameObject.CorrespondingObject, EntityRef.Value, Base.Entity, BasePlayer.Health };
        }
    }

    public struct EntityLifeState
    {
        public int[] FromBaseNetworkablePath { get; set; }

        public EntityLifeState(int key = 0)
        {
            FromBaseNetworkablePath = new int[] { GameObject.CorrespondingObject, GameObject.CorrespondingObject, EntityRef.Value, Base.Entity, BasePlayer.LifeState};
        }
    }

    public struct EntityPosition
    {
        public int[] FromInitialObject { get; set; }
        public int[] FromNextObjectLink { get; set; }
        public int[] FromBaseNetworkablePath { get; set; }

        /* Can't have parameterless constructor within a struct */
        public EntityPosition(int key = 0) : this()
        {            
            FromInitialObject = new int[] { GameObject.CorrespondingObject, LocalEntity.Transform, Transform.VisualState, VisualState.Position };
            FromNextObjectLink = new int[] { LastObjectBase.GameObject, GameObject.CorrespondingObject, LocalEntity.Transform, Transform.VisualState, VisualState.Position };
            FromBaseNetworkablePath = new int[] { /*LastObjectBase.GameObject,*/ GameObject.CorrespondingObject, GameObject.CorrespondingObject, LocalEntity.Transform, Transform.VisualState, VisualState.Position };
        }
    }

    public struct EntitySteamID
    {
        public int[] FromBaseNetworkablePath { get; set; }

        public EntitySteamID(int key = 0): this()
        {
            FromBaseNetworkablePath = new int[] { GameObject.CorrespondingObject, GameObject.CorrespondingObject, EntityRef.Value, Base.Entity, BasePlayer.SteamID };
        }
    }

    public struct EntityRotation
    {
        public int[] FromInitialObject { get; set; }
        public int[] FromNextObjectLink { get; set; }

        public EntityRotation(int key = 0)
        {
            FromInitialObject = new int[] { GameObject.CorrespondingObject, LocalEntity.MainCamera, MainCamera.CameraViewMatrix };
            FromNextObjectLink = new int[] { LastObjectBase.GameObject, GameObject.CorrespondingObject, LocalEntity.MainCamera, MainCamera.CameraViewMatrix };
        }
    }

    public struct EntityNetworkable
    {
        public int[] FromInitialObject { get; set; }
        public int[] FromNextObjectLink { get; set; }
        public int[] FromBaseNetworkablePath { get; set; }

        public EntityNetworkable(int key = 0)
        {
            /* 0x10] + 0x30] + 0x18] + 0x28] + 0x20 - CurrentVersion */
            FromNextObjectLink = new int[] { LastObjectBase.GameObject, GameObject.CorrespondingObject, EntityRef.Value, Base.Entity, BaseNetworkable.IsNetworkable };

            /* 0x30] + 0x18] + 0x28] + 0x20 */
            FromInitialObject = new int[] { GameObject.CorrespondingObject, EntityRef.Value, Base.Entity, BaseNetworkable.IsNetworkable };

            /* 0x30] + 0x30] + 0x18] + 0x28] + 0x20 */
            FromBaseNetworkablePath = new int[] { GameObject.CorrespondingObject, GameObject.CorrespondingObject, EntityRef.Value, Base.Entity, BaseNetworkable.IsNetworkable };
        }
    }

    public struct EntityPlayerFlags
    {
        public int[] FromInitialObject { get; set; }
        public int[] FromNextObjectLink { get; set; }
        public int[] FromBaseNetworkablePath { get; set; }

        public EntityPlayerFlags(int key = 0)
        {
            FromNextObjectLink = new int[] { LastObjectBase.GameObject, GameObject.CorrespondingObject, EntityRef.Value, Base.Entity, BasePlayer.PlayerFlags };
            FromInitialObject = new int[] { GameObject.CorrespondingObject, EntityRef.Value, Base.Entity, BasePlayer.PlayerFlags };
            FromBaseNetworkablePath = new int[] { GameObject.CorrespondingObject, GameObject.CorrespondingObject, EntityRef.Value, Base.Entity, BasePlayer.PlayerFlags };
        }
    }

    public struct EntityPlayerModelFlags
    {
        public int[] FromInitialObject { get; set; }
        public int[] FromNextObjectLink { get; set; }
        public int[] FromBaseNetworkablePath { get; set; }

        public EntityPlayerModelFlags(int key = 0)
        {
            FromNextObjectLink = new int[] { LastObjectBase.GameObject, GameObject.CorrespondingObject, EntityRef.Value, Base.Entity, BasePlayer.PlayerFlags };
            FromInitialObject = new int[] { GameObject.CorrespondingObject, EntityRef.Value, Base.Entity, BasePlayer.PlayerFlags };
            FromBaseNetworkablePath = new int[] { GameObject.CorrespondingObject, GameObject.CorrespondingObject, EntityRef.Value, Base.Entity, BasePlayer.PlayerFlags };
        }
    }

    public struct EntityTag
    {
        public int[] FromInitialObject { get; set; }
        public int[] FromNextObjectLink { get; set; }
        public int[] FromBaseNetworkablePath { get; set; }

        public EntityTag(int key = 0)
        {
            FromNextObjectLink = new int[] { LastObjectBase.GameObject, GameObject.ObjectTag };
            FromInitialObject = new int[] { GameObject.ObjectTag };
            FromBaseNetworkablePath = new int[] { /*LastObjectBase.GameObject,*/ GameObject.CorrespondingObject, GameObject.ObjectTag };
        }
    }

    public struct EntityName
    {
        public int[] FromInitialObject { get; set; }
        public int[] FromNextObjectLink { get; set; }
        public int[] FromBaseNetworkablePath { get; set; }

        public EntityName(int key = 0)
        {
            FromNextObjectLink = new int[] { LastObjectBase.GameObject, GameObject.ObjectName };
            FromInitialObject = new int[] { GameObject.ObjectName };
            FromBaseNetworkablePath = new int[] {/* LastObjectBase.GameObject, */ GameObject.CorrespondingObject, GameObject.ObjectName };
        }
    }

    public struct EntityLayerPath
    {
        public int[] FromInitialObject { get; set; }
        public int[] FromNextObjectLink { get; set; }
        public int[] FromBaseNetworkablePath { get; set; }

        public EntityLayerPath(int key = 0)
        {
            FromNextObjectLink = new int[] { LastObjectBase.GameObject, GameObject.ObjectLayer };
            FromInitialObject = new int[] { GameObject.ObjectLayer };
            FromBaseNetworkablePath = new int[] {/* LastObjectBase.GameObject,*/ GameObject.CorrespondingObject, GameObject.ObjectLayer };
        }
    }

    public struct PlayerName
    {
        public int[] FromInitialObject { get; set; }
        public int[] FromNextObjectLink { get; set; }
        public int[] FromBaseNetworkablePath { get; set; }

        public PlayerName(int key = 0)
        {
            FromNextObjectLink = new int[] { LastObjectBase.GameObject, GameObject.CorrespondingObject, EntityRef.Value, Base.Entity, BasePlayer.DisplayName };
            FromInitialObject = new int[] { GameObject.CorrespondingObject, EntityRef.Value, Base.Entity, BasePlayer.DisplayName };
            FromBaseNetworkablePath = new int[] { GameObject.CorrespondingObject, GameObject.CorrespondingObject, EntityRef.Value, Base.Entity, BasePlayer.DisplayName, 0x14 };
        }
    }

    public struct PlayerActiveItem
    {
        public int[] FromBaseNetworkablePath { get; set; }

        public PlayerActiveItem(int key = 0)
        {
            FromBaseNetworkablePath = new int[] { GameObject.CorrespondingObject, GameObject.CorrespondingObject, EntityRef.Value, Base.Entity, BasePlayer.ActiveItem };
        }
    }

    /// <summary>
    /// 0x30 > 0x30 > 0x18 > 0x28 > 0x350 > 0x28 > 0x20 > 0x10
    /// </summary>
    public struct PlayerBeltItemList
    {
        public int[] FromBaseNetworkablePath { get; set; }

        public PlayerBeltItemList(int key = 0)
        {
            FromBaseNetworkablePath = new int[] { GameObject.CorrespondingObject, GameObject.CorrespondingObject, EntityRef.Value, Base.Entity, BasePlayer.PlayerInventory,
                                                  PlayerInventory.BeltContainer, ItemContainer.ItemList, ItemContainer.ItemListOffset };
        }
    }

    #region Actual Offsets

    /// <summary>
    /// Contains necessary offsets within our game object manager
    /// </summary>
    public static class GameManager
    {
        public static readonly ulong Address = 0x0144BD60;
        public static readonly int LastTaggedObject = 0x0;
        public static readonly int LastTaggedObjects = 0x08;
        public static readonly int LastActiveObject = 0x10;
        public static readonly int LastActiveObjects = 0x18;
    }

    /// <summary>
    /// Contains the necessary offsets within the LastObjectBase class
    /// </summary>
    public static class LastObjectBase
    {
        public static readonly int NextObjectLink = 0x08;
        public static readonly int GameObject = 0x10;
    }

    /// <summary>
    /// 
    /// Contains the necessary offsets within a game object
    /// </summary>
    public static class GameObject
    {
        public static readonly int ObjectTag = 0x5C;
        public static readonly int ObjectLayer = 0x58;
        public static readonly int ObjectName = 0x68;

        /// <summary>
        /// Could be a PlayerObject or CameraObject, etc.
        /// </summary>
        public static readonly int CorrespondingObject = 0x30;
    }        

    /// <summary>
    /// Provides base offsets once we get to the base object
    /// BasePlayer, BaseNetworkable, etc.
    /// </summary>
    public static class Base
    {
        public static readonly int Entity = 0x28;
    }

    /// <summary>
    /// 
    /// </summary>
    public static class BaseNetworkable
    {
        public static readonly int IsNetworkable = 0x20;
        public static readonly int PrefabID = 0x2C;
    }

    /// <summary>
    /// 
    /// </summary>
    public static class BasePlayer
    {
        public static readonly int PlayerFlags = 0x4c8;// 4/5 update 0x410; // Prior to 3/1/2018 update - 0x440;
        public static readonly int ModelState = 0x4a0;
        public static readonly int PlayerModel = 0x460;
        public static readonly int DisplayName = 0x450;
        public static readonly int SteamID = 0x4d0;
        public static readonly int LifeState = 0x1c0;
        public static readonly int ActiveItem = 0x538;
        public static readonly int PlayerInventory = 0x400;
        public static readonly int Health = 0x1c8;
    }

    /// <summary>
    /// 
    /// </summary>
    public static class PlayerInventory
    {
        public static readonly int BeltContainer = 0x28;
    }

    /// <summary>
    /// 
    /// </summary>
    public static class ItemContainer
    {
        public static readonly int ItemList = 0x20;
        public static readonly int ItemListOffset = 0x10;
    }

    /// <summary>
    /// 
    /// </summary>
    public static class Item
    {
        public static readonly int ItemUID = 0x78;
    }

    /// <summary>
    /// 
    /// </summary>
    public static class ItemDefinition
    {
        public static readonly int Name = 0x20;
    }

    /// <summary>
    /// Base player entity
    /// </summary>
    public static class EntityRef
    {
        public static readonly int Value = 0x18;
    }

    /// <summary>
    /// Contains the necessary offsets within an entity class
    /// </summary>
    public static class LocalEntity
    {
        public static readonly int Transform = 0x08;
        public static readonly int MainCamera = 0x18;
    }

    /// <summary>
    /// Contains the necessary offsets within the main camera class
    /// </summary>
    public static class MainCamera
    {
        public static readonly int CameraViewMatrix = 0xC0;
    }

    /// <summary>
    /// Contains the necessary offsets within the Transform class
    /// </summary>
    public static class Transform
    {
        public static readonly int VisualState = 0x38;
    }

    /// <summary>
    /// Contains the necessary offsets within the Visual State of an object
    /// </summary>
    public static class VisualState
    {
        public static readonly int Position = 0xB0;
    }

    #endregion
}