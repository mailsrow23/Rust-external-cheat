namespace Dissector
{
    /// <summary>
    /// Extension methods for gameobjectbase
    /// </summary>
    public static class GameObjectBaseEx
    {
        /// <summary>
        /// Determine if game object is sulfur ore
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool IsSulfurOre(this GameObjectBase gameObject)
        {
            var entityName = gameObject.EntityName.ToUpper();

            if ((entityName.Contains("ORE_SULFUR") ||
                 entityName.Contains("SULFUR-ORE")) &&
                !entityName.Contains("COLLECTABLE") &&
                !entityName.Contains("WORLD"))
            {
                return true;
            }

            return false;            
        }

        /// <summary>
        /// Determine if game object is metal ore
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool IsMetalOre(this GameObjectBase gameObject)
        {
            var entityName = gameObject.EntityName.ToUpper();

            if ((entityName.Contains("ORE_METAL") ||
                entityName.Contains("METAL-ORE")) &&
                !entityName.Contains("COLLECTABLE") &&
                !entityName.Contains("WORLD"))
            {
                return true;
            }

            return false;            
        }

        /// <summary>
        /// Determine if game object is a stone node
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool IsStoneOre(this GameObjectBase gameObject)
        {
            var entityName = gameObject.EntityName.ToUpper();

            if ((entityName.Contains("ORE_STONE") ||
                 entityName.Contains("STONE-ORE")) &&
                !entityName.Contains("COLLECTABLE") &&
                !entityName.Contains("WORLD"))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determine if game object is a military crate
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool IsMilitaryCrate(this GameObjectBase gameObject)
        {
            var entityName = gameObject.EntityName.ToUpper();

            if (entityName.Contains("CRATE_NORMAL_2") ||
                entityName.Contains("CRATE_ELITE")) /* Military crates */
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determine if gameobject is normal crate or tool box
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool IsNormalCrate(this GameObjectBase gameObject)
        {
            var entityName = gameObject.EntityName.ToUpper();

            if (entityName.Contains("CRATE_MINE") || /* Obvious */
                entityName.Contains("CRATE_NORMAL") || /* Wooden box you see on side of roads an in monuments.  Has the black stripes on it. */
                entityName.Contains("CRATE_TOOLS") || /* Tool box */
                entityName.Contains("CRATE_BASIC"))  /* Small box at T1 monuments */
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool IsStorageContainer(this GameObjectBase gameObject)
        {
            var entityName = gameObject.EntityName.ToUpper();

            if ((entityName.Contains("LARGE") && entityName.Contains("BOX")) ||
                (entityName.Contains("SMALL") && entityName.Contains("STASH")))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool IsToolCupboard(this GameObjectBase gameObject)
        {
            var entityName = gameObject.EntityName.ToUpper();

            if ((entityName.Contains("TOOL") && entityName.Contains("CUPBOARD")))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool IsHempNode(this GameObjectBase gameObject)
        {
            var entityName = gameObject.EntityName.ToUpper();
            if ((entityName.Contains("HEMP-COLLECTABLE")))
            {
                return true;
            }

            return false;
        }

        /// <summary>        
        /// Determine if crate is elite crate
        /// </summary>
        public static bool IsEliteCrate(this GameObjectBase gameObject)
        {
            var entityName = gameObject.EntityName.ToUpper();

            if (entityName.Contains("CRATE_ELITE")) /* Elite military crates */
            {
                return true;
            }

            return false;
        }
    }
}