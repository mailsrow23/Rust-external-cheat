using Dissector.Helpers;
using SharpDX;

namespace Dissector.Overlay
{
    public partial class Canvas
    {
        /// <summary>
        /// Process resource type stuff, basically everything that is not a layer 30 (tree)
        /// </summary>
        private static void ProcessResourceEntities()
        {
            ProcessSulfur();
            ProcessMetalOre();
            ProcessStoneOre();
            ProcessAnimals();
            ProcessHemp();
        }

        #region Sulfur

        /// <summary>
        /// Process collected sulfur objects
        /// </summary>
        private static void ProcessSulfur()
        {
            if (_isUpdatingSulfurNodes == false)
            {
                _sulfurOreEntities.Clear();
                _gameObjectManager.SulfurNodes.Clear();
            }
            else
            {
                foreach (var node in _gameObjectManager.SulfurNodes)
                {
                    if (!ValidateResource(node.Value, ResourceType.Sulfur))
                        continue;

                    AddOrUpdateSulfurEntity(node.Value);
                }
            }
        }

        /// <summary>
        /// If necessary, Add/Update/Remove the sulfur entity from the sulfur list that is used to draw from.
        /// </summary>
        /// <param name="player"></param>
        private static void AddOrUpdateSulfurEntity(GameObjectBase sulfur)
        {
            Vector3 screenPoint;
            var resourcePostion = sulfur.Position;
            float distance = GetDistance(_localPlayerPosition.Position, resourcePostion);
            Coordinates.WorldToScreen(_localPlayerRotation.Rotation, resourcePostion, out screenPoint);

            /* We only want to draw it on the screen if it's within a certain distance and a valid point */
            if (distance <= 250 && screenPoint.Z > 0)
            {
                AddSulfurEntityToDrawingList(sulfur, distance, screenPoint);
            }
            else
            {
                RemoveSulfurEntityFromDrawingList(sulfur);
            }
        }

        /// <summary>
        /// Add sulfur to drawing list
        /// </summary>
        /// <param name="sulfur"></param>
        /// <param name="distance"></param>
        /// <param name="screenPoint"></param>
        private static void AddSulfurEntityToDrawingList(GameObjectBase sulfur, float distance, Vector3 screenPoint)
        {
            float screenX = screenPoint.X;
            float screenY = screenPoint.Y;
            EntityInfo entityInfo = new EntityInfo() { Entity = sulfur, Distance = distance, ScreenX = screenX, ScreenY = screenY};
            long key = sulfur.InitialAddress;
            _sulfurOreEntities[key] = entityInfo;
        }

        /// <summary>
        /// Remove sulfur from drawing list
        /// </summary>
        /// <param name="sulfur"></param>
        private static void RemoveSulfurEntityFromDrawingList(GameObjectBase sulfur)
        {
            EntityInfo entityInfo = new EntityInfo();
            long key = sulfur.InitialAddress;
            _sulfurOreEntities.TryRemove(key, out entityInfo);
        }

        #endregion

        #region Metal

        /// <summary>
        /// Process collected metal ore objects
        /// </summary>
        private static void ProcessMetalOre()
        {
            if (_isUpdatingMetalNodes == false)
            {
                _metalOreEntities.Clear();
                _gameObjectManager.MetalNodes.Clear();
            }
            else
            {
                foreach (var node in _gameObjectManager.MetalNodes)
                {
                    if (!ValidateResource(node.Value, ResourceType.Metal))
                        continue;

                    AddOrUpdateMetalOreEntity(node.Value);
                }
            }
        }

        /// <summary>
        /// If necessary, Add/Update/Remove the metalOre entity
        /// </summary>
        /// <param name="player"></param>
        private static void AddOrUpdateMetalOreEntity(GameObjectBase metalOre)
        {
            Vector3 screenPoint;
            var resourcePostion = metalOre.Position;
            float distance = GetDistance(_localPlayerPosition.Position, resourcePostion);
            Coordinates.WorldToScreen(_localPlayerRotation.Rotation, resourcePostion, out screenPoint);

            /* We only want to draw it on the screen if it's within a certain distance and a valid point */
            if (distance <= 250 && screenPoint.Z > 0)
            {
                AddMetalOreEntityToDrawingList(metalOre, distance, screenPoint);
            }
            else
            {
                RemoveMetalOreEntityFromDrawingList(metalOre);
            }
        }

        /// <summary>
        /// Add metalOre to drawing list
        /// </summary>
        /// <param name="metalOre"></param>
        /// <param name="distance"></param>
        /// <param name="screenPoint"></param>
        private static void AddMetalOreEntityToDrawingList(GameObjectBase metalOre, float distance, Vector3 screenPoint)
        {
            float screenX = screenPoint.X;
            float screenY = screenPoint.Y;
            EntityInfo entityInfo = new EntityInfo() { Entity = metalOre, Distance = distance, ScreenX = screenX, ScreenY = screenY };
            long key = metalOre.InitialAddress;
            _metalOreEntities[key] = entityInfo;
        }

        /// <summary>
        /// Remove metalOre from drawing list
        /// </summary>
        /// <param name="metalOre"></param>
        private static void RemoveMetalOreEntityFromDrawingList(GameObjectBase metalOre)
        {
            EntityInfo entityInfo = new EntityInfo();
            long key = metalOre.InitialAddress;
            _metalOreEntities.TryRemove(key, out entityInfo);
        }

        #endregion

        #region Stone

        /// <summary>
        /// Process collected StoneOre objects
        /// </summary>
        private static void ProcessStoneOre()
        {
            if (_isUpdatingStoneNodes == false)
            {
                _stoneOreEntities.Clear();
                _gameObjectManager.StoneNodes.Clear();
            }
            else
            {
                foreach (var node in _gameObjectManager.StoneNodes)
                {
                    if (!ValidateResource(node.Value, ResourceType.Stone))
                        continue;

                    AddOrUpdateStoneOreEntity(node.Value);
                }
            }
        }

        /// <summary>
        /// If necessary, Add/Update/Remove the StoneOre entity
        /// </summary>
        /// <param name="player"></param>
        private static void AddOrUpdateStoneOreEntity(GameObjectBase stoneOre)
        {
            Vector3 screenPoint;
            var resourcePostion = stoneOre.Position;
            float distance = GetDistance(_localPlayerPosition.Position, resourcePostion);
            Coordinates.WorldToScreen(_localPlayerRotation.Rotation, resourcePostion, out screenPoint);

            /* We only want to draw it on the screen if it's within a certain distance and a valid point */
            if (distance <= 250 && screenPoint.Z > 0)
            {
                AddStoneOreEntityToDrawingList(stoneOre, distance, screenPoint);
            }
            else
            {
                RemoveStoneOreEntityFromDrawingList(stoneOre);
            }
        }

        /// <summary>
        /// Add stoneOre to drawing list
        /// </summary>
        /// <param name="metalOre"></param>
        /// <param name="distance"></param>
        /// <param name="screenPoint"></param>
        private static void AddStoneOreEntityToDrawingList(GameObjectBase stoneOre, float distance, Vector3 screenPoint)
        {
            float screenX = screenPoint.X;
            float screenY = screenPoint.Y;
            EntityInfo entityInfo = new EntityInfo() { Entity = stoneOre, Distance = distance, ScreenX = screenX, ScreenY = screenY };
            long key = stoneOre.InitialAddress;
            _stoneOreEntities[key] = entityInfo;
        }

        /// <summary>
        /// Remove stoneOre from drawing list
        /// </summary>
        /// <param name="metalOre"></param>
        private static void RemoveStoneOreEntityFromDrawingList(GameObjectBase stoneOre)
        {
            EntityInfo entityInfo = new EntityInfo();
            long key = stoneOre.InitialAddress;
            _stoneOreEntities.TryRemove(key, out entityInfo);
        }

        #endregion

        #region Animals

        /// <summary>
        /// Process collected animal entities
        /// </summary>
        private static void ProcessAnimals()
        {
            if (_isUpdatingAnimals == false)
            {
                _animalEntities.Clear();
                _gameObjectManager.Animals.Clear();
            }
            else
            {
                foreach (var animal in _gameObjectManager.Animals)
                {
                    if (!ValidateResource(animal.Value, ResourceType.Animal))
                        continue;

                    AddOrUpdateAnimalEntity(animal.Value);
                }
            }
        }

        /// <summary>
        /// If necessary, Add/Update/Remove the animal entity
        /// </summary>
        /// <param name="player"></param>
        private static void AddOrUpdateAnimalEntity(GameObjectBase animal)
        {
            Vector3 screenPoint;
            var resourcePostion = animal.Position;
            float distance = GetDistance(_localPlayerPosition.Position, resourcePostion);
            Coordinates.WorldToScreen(_localPlayerRotation.Rotation, resourcePostion, out screenPoint);

            /* We only want to draw it on the screen if it's within a certain distance and a valid point */
            if (distance <= 250 && screenPoint.Z > 0)
            {
                AddAnimalEntityToDrawingList(animal, distance, screenPoint);
            }
            else
            {
                RemoveAnimalEntityFromDrawingList(animal);
            }
        }

        /// <summary>
        /// Add animal entity to drawing list
        /// </summary>
        /// <param name="metalOre"></param>
        /// <param name="distance"></param>
        /// <param name="screenPoint"></param>
        private static void AddAnimalEntityToDrawingList(GameObjectBase animal, float distance, Vector3 screenPoint)
        {
            var name = GetName(animal);
            float screenX = screenPoint.X;
            float screenY = screenPoint.Y;
            EntityInfo entityInfo = new EntityInfo() { Entity = animal, Distance = distance, ScreenX = screenX, ScreenY = screenY, EntityName = name };
            long key = animal.InitialAddress;
            _animalEntities[key] = entityInfo;
        }

        /// <summary>
        /// Remove animal entity from drawing list
        /// </summary>
        /// <param name="metalOre"></param>
        private static void RemoveAnimalEntityFromDrawingList(GameObjectBase animal)
        {
            EntityInfo entityInfo = new EntityInfo();
            long key = animal.InitialAddress;
            _animalEntities.TryRemove(key, out entityInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        private static string GetName(GameObjectBase animal)
        {
            if (animal.EntityName.ToString().ToUpper().Contains("BOAR"))
                return "Boar";

            if (animal.EntityName.ToString().ToUpper().Contains("WOLF"))
                return "Wolf";

            if (animal.EntityName.ToString().ToUpper().Contains("BEAR"))
                return "Bear";

            return "";
        }

        #endregion

        #region Hemp

        /// <summary>
        /// Process hemp objects
        /// </summary>
        private static void ProcessHemp()
        {
            if (_isUpdatingHempNodes == false)
            {
                _hempNodes.Clear();
                _gameObjectManager.HempNodes.Clear();
            }
            else
            {
                foreach (var node in _gameObjectManager.HempNodes)
                {
                    if (!ValidateResource(node.Value, ResourceType.Hemp))
                        continue;

                    AddOrUpdateHempNode(node.Value);
                }
            }
        }

        /// <summary>
        /// If necessary, Add/Update/Remove the hemp entity
        /// </summary>
        /// <param name="player"></param>
        private static void AddOrUpdateHempNode(GameObjectBase hempNode)
        {
            Vector3 screenPoint;
            var resourcePostion = hempNode.Position;
            float distance = GetDistance(_localPlayerPosition.Position, resourcePostion);
            Coordinates.WorldToScreen(_localPlayerRotation.Rotation, resourcePostion, out screenPoint);

            /* We only want to draw it on the screen if it's within a certain distance and a valid point */
            if (distance <= 250 && screenPoint.Z > 0)
            {
                AddHempNodeToDrawingList(hempNode, distance, screenPoint);
            }
            else
            {
                RemoveHempNodeFromDrawingList(hempNode);
            }
        }

        /// <summary>
        /// Add hemp to drawing list
        /// </summary>
        /// <param name="metalOre"></param>
        /// <param name="distance"></param>
        /// <param name="screenPoint"></param>
        private static void AddHempNodeToDrawingList(GameObjectBase hemp, float distance, Vector3 screenPoint)
        {
            float screenX = screenPoint.X;
            float screenY = screenPoint.Y;
            EntityInfo entityInfo = new EntityInfo() { Entity = hemp, Distance = distance, ScreenX = screenX, ScreenY = screenY };
            long key = hemp.InitialAddress;
            _hempNodes[key] = entityInfo;
        }

        /// <summary>
        /// Remove hemp from drawing list
        /// </summary>
        /// <param name="metalOre"></param>
        private static void RemoveHempNodeFromDrawingList(GameObjectBase hemp)
        {
            EntityInfo entityInfo = new EntityInfo();
            long key = hemp.InitialAddress;
            _hempNodes.TryRemove(key, out entityInfo);
        }

        #endregion


/// <summary>
/// Validates a resource and removes it from the appropriate resource list if it has been used or destroyed.
/// </summary>
/// <param name="resource">The resource to validate.</param>
/// <param name="type">The type of resource.</param>
/// <returns>True if the resource is still valid; false otherwise.</returns>
private static bool ValidateResource(GameObjectBase resource, ResourceType type)
{
    if (resource.Networkable == 0)
    {
        switch (type)
        {
            case ResourceType.StorageContainer:
                RemoveStorageContainerFromDrawingList(resource);
                _gameObjectManager.StorageContainers.TryRemove(resource.InitialAddress, out _);
                break;
            case ResourceType.Hemp:
                RemoveHempNodeFromDrawingList(resource);
                _gameObjectManager.HempNodes.TryRemove(resource.InitialAddress, out _);
                break;
            case ResourceType.ToolCupboard:
                RemoveToolCupboardFromDrawingList(resource);
                _gameObjectManager.ToolCupboards.TryRemove(resource.InitialAddress, out _);
                break;
            case ResourceType.MilitaryCrate:
                RemoveMilitaryCrateFromDrawingList(resource);
                _gameObjectManager.MilitaryCrates.TryRemove(resource.InitialAddress, out _);
                break;
            case ResourceType.WoodenLootCrate:
                RemoveWoodLootCrateFromDrawingList(resource);
                _gameObjectManager.WoodenLootCrates.TryRemove(resource.InitialAddress, out _);
                break;
            case ResourceType.Animal:
                RemoveAnimalEntityFromDrawingList(resource);
                _gameObjectManager.Animals.TryRemove(resource.InitialAddress, out _);
                break;
            case ResourceType.Sulfur:
                RemoveSulfurEntityFromDrawingList(resource);
                _gameObjectManager.SulfurNodes.TryRemove(resource.InitialAddress, out _);
                break;
            case ResourceType.Metal:
                RemoveMetalOreEntityFromDrawingList(resource);
                _gameObjectManager.MetalNodes.TryRemove(resource.InitialAddress, out _);
                break;
            case ResourceType.Stone:
                RemoveStoneOreEntityFromDrawingList(resource);
                _gameObjectManager.StoneNodes.TryRemove(resource.InitialAddress, out _);
                break;
            default:
                throw new ArgumentException($"Invalid resource type: {type}", nameof(type));
        }

        return false;
    }

    return true;
}
