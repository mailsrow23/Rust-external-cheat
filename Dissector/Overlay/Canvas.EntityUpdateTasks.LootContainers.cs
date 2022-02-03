using Dissector.Helpers;
using SharpDX;

namespace Dissector.Overlay
{
    public partial class Canvas
    {
		private static void ProcessLootEntities()
        {
            ProcessMilitaryCrates();
            ProcessWoodenLootCrates();
            ProcessStorageContainers();
            ProcessToolCupboards();
        }

        #region Military Crates

        /// <summary>
        /// Process military crates
        /// </summary>
        private static void ProcessMilitaryCrates()
        {
            if (_isUpdatingMilitaryCrates == false)
            {
                _militaryCrates.Clear();
                _gameObjectManager.MilitaryCrates.Clear();
            }
            else
            {
                foreach (var node in _gameObjectManager.MilitaryCrates)
                {
                    if (!ValidateResource(node.Value, ResourceType.MilitaryCrate))
                        continue;

                    AddOrUpdateMilitaryCrate(node.Value);
                }
            }
        }

        /// <summary>
        /// If necessary, Add/Update/Remove the lootEntity
        /// </summary>
        /// <param name="player"></param>
        private static void AddOrUpdateMilitaryCrate(GameObjectBase lootEntity)
        {
            Vector3 screenPoint;
            var resourcePostion = lootEntity.Position;
            float distance = GetDistance(_localPlayerPosition.Position, resourcePostion);
            Coordinates.WorldToScreen(_localPlayerRotation.Rotation, resourcePostion, out screenPoint);

            /* We only want to draw it on the screen if it's within a certain distance and a valid point */
            if (distance <= 250 && screenPoint.Z > 0)
            {
                AddMilitaryCrateToDrawingList(lootEntity, distance, screenPoint);
            }
            else
            {
                RemoveMilitaryCrateFromDrawingList(lootEntity);
            }
        }

        /// <summary>
        /// Add loot entity to drawing list
        /// </summary>
        /// <param name="metalOre"></param>
        /// <param name="distance"></param>
        /// <param name="screenPoint"></param>
        private static void AddMilitaryCrateToDrawingList(GameObjectBase lootEntity, float distance, Vector3 screenPoint)
        {
            float screenX = screenPoint.X;
            float screenY = screenPoint.Y;
            EntityInfo entityInfo = new EntityInfo() { Entity = lootEntity, Distance = distance, ScreenX = screenX, ScreenY = screenY };
            long key = lootEntity.InitialAddress;
            _militaryCrates[key] = entityInfo;
        }

        /// <summary>
        /// Remove loot entity from drawing list
        /// </summary>
        /// <param name="metalOre"></param>
        private static void RemoveMilitaryCrateFromDrawingList(GameObjectBase lootEntity)
        {
            EntityInfo entityInfo = new EntityInfo();
            long key = lootEntity.InitialAddress;
            _militaryCrates.TryRemove(key, out entityInfo);
        }

        #endregion

        #region Wooden Loot Crates

        /// <summary>
        /// Process military crates
        /// </summary>
        private static void ProcessWoodenLootCrates()
        {
            if (_isUpdatingWoodenLootCrates == false)
            {
                _woodenLootCrates.Clear();
                _gameObjectManager.WoodenLootCrates.Clear();
            }
            else
            {
                foreach (var node in _gameObjectManager.WoodenLootCrates)
                {
                    if (!ValidateResource(node.Value, ResourceType.WoodenLootCrate))
                        continue;

                    AddOrUpdateWoodenLootCrate(node.Value);
                }
            }
        }

        /// <summary>
        /// If necessary, Add/Update/Remove the lootEntity
        /// </summary>
        /// <param name="player"></param>
        private static void AddOrUpdateWoodenLootCrate(GameObjectBase lootEntity)
        {
            Vector3 screenPoint;
            var resourcePostion = lootEntity.Position;
            float distance = GetDistance(_localPlayerPosition.Position, resourcePostion);
            Coordinates.WorldToScreen(_localPlayerRotation.Rotation, resourcePostion, out screenPoint);

            /* We only want to draw it on the screen if it's within a certain distance and a valid point */
            if (distance <= 250 && screenPoint.Z > 0)
            {
                AddWoodLootCrateToDrawingList(lootEntity, distance, screenPoint);
            }
            else
            {
                RemoveWoodLootCrateFromDrawingList(lootEntity);
            }
        }

        /// <summary>
        /// Add loot entity to drawing list
        /// </summary>
        /// <param name="metalOre"></param>
        /// <param name="distance"></param>
        /// <param name="screenPoint"></param>
        private static void AddWoodLootCrateToDrawingList(GameObjectBase lootEntity, float distance, Vector3 screenPoint)
        {
            float screenX = screenPoint.X;
            float screenY = screenPoint.Y;
            EntityInfo entityInfo = new EntityInfo() { Entity = lootEntity, Distance = distance, ScreenX = screenX, ScreenY = screenY };
            long key = lootEntity.InitialAddress;
            _woodenLootCrates[key] = entityInfo;
        }

        /// <summary>
        /// Remove loot entity from drawing list
        /// </summary>
        /// <param name="metalOre"></param>
        private static void RemoveWoodLootCrateFromDrawingList(GameObjectBase lootEntity)
        {
            EntityInfo entityInfo = new EntityInfo();
            long key = lootEntity.InitialAddress;
            _woodenLootCrates.TryRemove(key, out entityInfo);
        }

        #endregion

        #region StorageBoxes

        /// <summary>
        /// Process storage containers
        /// </summary>
        private static void ProcessStorageContainers()
        {
            if (_isUpdatingStorageContainers == false)
            {
                _storageContainers.Clear();
                _gameObjectManager.StorageContainers.Clear();
            }
            else
            {
                foreach (var node in _gameObjectManager.StorageContainers)
                {
                    if (!ValidateResource(node.Value, ResourceType.StorageContainer))
                        continue;

                    AddOrUpdateStorageContainer(node.Value);
                }
            }
        }

        /// <summary>
        /// If necessary, Add/Update/Remove the lootEntity
        /// </summary>
        /// <param name="player"></param>
        private static void AddOrUpdateStorageContainer(GameObjectBase storageEntity)
        {
            Vector3 screenPoint;
            var resourcePostion = storageEntity.Position;
            float distance = GetDistance(_localPlayerPosition.Position, resourcePostion);
            Coordinates.WorldToScreen(_localPlayerRotation.Rotation, resourcePostion, out screenPoint);

            /* We only want to draw it on the screen if it's within a certain distance and a valid point */
            if (distance <= 250 && screenPoint.Z > 0)
            {
                AddStorageContainerToDrawingList(storageEntity, distance, screenPoint);
            }
            else
            {
                RemoveStorageContainerFromDrawingList(storageEntity);
            }
        }

        /// <summary>
        /// Add loot entity to drawing list
        /// </summary>
        /// <param name="metalOre"></param>
        /// <param name="distance"></param>
        /// <param name="screenPoint"></param>
        private static void AddStorageContainerToDrawingList(GameObjectBase storageEntity, float distance, Vector3 screenPoint)
        {
            float screenX = screenPoint.X;
            float screenY = screenPoint.Y;
            EntityInfo entityInfo = new EntityInfo() { Entity = storageEntity, Distance = distance, ScreenX = screenX, ScreenY = screenY };
            long key = storageEntity.InitialAddress;
            _storageContainers[key] = entityInfo;
        }

        /// <summary>
        /// Remove loot entity from drawing list
        /// </summary>
        /// <param name="metalOre"></param>
        private static void RemoveStorageContainerFromDrawingList(GameObjectBase storageEntity)
        {
            EntityInfo entityInfo = new EntityInfo();
            long key = storageEntity.InitialAddress;
            _storageContainers.TryRemove(key, out entityInfo);
        }

        #endregion

        #region Tool Cupboards

        /// <summary>
        /// Process tool cupboards
        /// </summary>
        private static void ProcessToolCupboards()
        {
            if (_isUpdatingToolCupboards == false)
            {
                _toolCupboards.Clear();
                _gameObjectManager.ToolCupboards.Clear();
            }
            else
            {
                foreach (var node in _gameObjectManager.ToolCupboards)
                {
                    if (!ValidateResource(node.Value, ResourceType.ToolCupboard))
                        continue;

                    AddOrUpdateToolCupboard(node.Value);
                }
            }
        }

        /// <summary>
        /// If necessary, Add/Update/Remove the lootEntity
        /// </summary>
        /// <param name="player"></param>
        private static void AddOrUpdateToolCupboard(GameObjectBase toolCupboard)
        {
            Vector3 screenPoint;
            var resourcePostion = toolCupboard.Position;
            float distance = GetDistance(_localPlayerPosition.Position, resourcePostion);
            Coordinates.WorldToScreen(_localPlayerRotation.Rotation, resourcePostion, out screenPoint);

            /* We only want to draw it on the screen if it's within a certain distance and a valid point */
            if (distance <= 250 && screenPoint.Z > 0)
            {
                AddToolCupboardToDrawingList(toolCupboard, distance, screenPoint);
            }
            else
            {
                RemoveToolCupboardFromDrawingList(toolCupboard);
            }
        }

        /// <summary>
        /// Add loot entity to drawing list
        /// </summary>
        /// <param name="metalOre"></param>
        /// <param name="distance"></param>
        /// <param name="screenPoint"></param>
        private static void AddToolCupboardToDrawingList(GameObjectBase toolCupboard, float distance, Vector3 screenPoint)
        {
            float screenX = screenPoint.X;
            float screenY = screenPoint.Y;
            EntityInfo entityInfo = new EntityInfo() { Entity = toolCupboard, Distance = distance, ScreenX = screenX, ScreenY = screenY };
            long key = toolCupboard.InitialAddress;
            _toolCupboards[key] = entityInfo;
        }

        /// <summary>
        /// Remove loot entity from drawing list
        /// </summary>
        /// <param name="metalOre"></param>
        private static void RemoveToolCupboardFromDrawingList(GameObjectBase toolCupboard)
        {
            EntityInfo entityInfo = new EntityInfo();
            long key = toolCupboard.InitialAddress;
            _toolCupboards.TryRemove(key, out entityInfo);
        }

        #endregion
    }
}
