using Dissector.Helpers;
using SharpDX;
using System;

namespace Dissector.Overlay
{
    /// <summary>
    /// Players update tasks
    /// </summary>
    public partial class Canvas
    {
        #region Player Entities

        /// <summary>
        /// Processes the players (including sleepers)
        /// </summary>
        private static void ProcessPlayerEntities()
        {
            if (!_isUpdatingPlayers)
            {
                _playerEntities.Clear();
                _gameObjectManager.Players.Clear();
                return;
            }

            foreach (var player in _gameObjectManager.Players)
            {
                /* If we're not validated, we're removing from the list and moving on. */
                if (ValidatePlayer(player.Value) == false)
                    continue;

                try
                {
                    if (player.Value.IsSleeping)
                    {
                        /* Because they can go from sleeping to alive and if we don't remove it's a static dot */
                        RemoveActivePlayerEntityFromDrawingList(player.Value);
                        AddOrUpdateSleeperEntity(player.Value);
                    }
                    else
                    {
                        /* Because they can go from sleeping to alive and if we don't remove it's a static dot */
                        RemoveFromSleeperDrawingList(player.Value);
                        AddOrUpdatePlayerEntity(player.Value);
                    }
                }
                catch (Exception)
                {
                    /* Suppress */
                }
            }
        }

        /// <summary>
        /// Player address get reused so we have to validate a player before we continue updating their information that gets drawn.
        /// Invalid players are removed from all lists until added again
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private static bool ValidatePlayer(GameObjectBase player)
        {
            /* Address no longer used to store player struct */
            if (player.Tag != 6)
            {
                RemoveActivePlayerEntityFromDrawingList(player);
                RemoveFromSleeperDrawingList(player);
                _gameObjectManager.Players.TryRemove(player.SteamID, out GameObjectBase p);
                return false;
            }
            /* Dead player */
            if (player.Health == 0)
            {
                try
                {
                    RemoveActivePlayerEntityFromDrawingList(player);
                    RemoveFromSleeperDrawingList(player);
                    _gameObjectManager.Players.TryRemove(player.SteamID, out GameObjectBase p);
                }
                catch (Exception)
                {
                    return false;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// If necessary, Add/Update/Remove the player entity from the player list that is used to draw from.
        /// </summary>
        /// <param name="player"></param>
        private static void AddOrUpdatePlayerEntity(GameObjectBase player)
        {
            Vector3 screenPoint;
            Vector3 postion = player.Position;
            float distance = GetDistance(_localPlayerPosition.Position, postion);
            Coordinates.WorldToScreen(_localPlayerRotation.Rotation, postion, out screenPoint);

            /* We only want to draw it on the screen if it's within a certain distance and a valid point */
            if (distance <= 350 && screenPoint.Z > 0)
            {
                AddToActivePlayerDrawingList(player, screenPoint, distance);
            }
            else
            {
                RemoveActivePlayerEntityFromDrawingList(player);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="screenPoint"></param>
        /// <param name="distance"></param>
        private static void AddToActivePlayerDrawingList(GameObjectBase player, Vector3 screenPoint, float distance)
        {
            float screenX = screenPoint.X;
            float screenY = screenPoint.Y;
            string activeItemName = "";

            if (player.ActiveItemUID > 0)
            {
                activeItemName = player.ActiveItemName;
            }

            EntityInfo entityInfo = new EntityInfo() { PlayerActiveItem = activeItemName,  Entity = player, Distance = distance, ScreenX = screenX, ScreenY = screenY , PlayerName = player.PlayerNameUnicode };
            long key = player.SteamID;
            _playerEntities[key] = entityInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        private static void RemoveActivePlayerEntityFromDrawingList(GameObjectBase player)
        {
            try
            {
                EntityInfo entityInfo = new EntityInfo();
                long key = player.SteamID;
                _playerEntities.TryRemove(key, out entityInfo);
            }
            catch (Exception)
            {
                /* Suppress */
            }
        }

        /// <summary>
        /// If necessary, Add/Update/Remove the sleeper entity from the player list that is used to draw from.
        /// </summary>
        /// <param name="player"></param>
        private static void AddOrUpdateSleeperEntity(GameObjectBase player)
        {
            Vector3 screenPoint;
            Vector3 position = player.Position;
            float distance = GetDistance(_localPlayerPosition.Position, position);
            Coordinates.WorldToScreen(_localPlayerRotation.Rotation, position, out screenPoint);

            /* We only want to draw it on the screen if it's within a certain distance and a valid point */
            if (distance <= 250 && screenPoint.Z > 0)
            {
                AddToSleeperDrawingList(player, screenPoint, distance);
            }
            else
            {
                RemoveFromSleeperDrawingList(player);
            }
        }

        /// <summary>
        /// Making it more clear what's going on in those if/else within add or update method
        /// </summary>
        /// <param name="sleeper"></param>
        /// <param name="screenPoint"></param>
        /// <param name="distance"></param>
        private static void AddToSleeperDrawingList(GameObjectBase sleeper, Vector3 screenPoint, float distance)
        {
            float screenX = screenPoint.X;
            float screenY = screenPoint.Y;

            var entityInfo = new EntityInfo() { Entity = sleeper, Distance = distance, ScreenX = screenX, ScreenY = screenY, PlayerName = sleeper.PlayerNameUnicode };
            var key = sleeper.SteamID;
            _sleeperEntities[key] = entityInfo;
        }

        /// <summary>
        /// Making it more clear what's going on in those if/else within add or update method
        /// </summary>
        /// <param name="player"></param>
        private static void RemoveFromSleeperDrawingList(GameObjectBase player)
        {
            try
            {
                var entityInfo = new EntityInfo();
                var key = player.SteamID;
                _sleeperEntities.TryRemove(key, out entityInfo);
            }
            catch (Exception)
            {
                /* Suppress */
            }
        }

        #endregion
    }
}