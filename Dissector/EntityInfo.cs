using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using System.Collections.Generic;

namespace Dissector
{
    /// <summary>
    /// Struct for storing in our dictionaries that get iterated on for drawing.
    /// </summary>
    public struct EntityInfo
    {
        public string PlayerName { get; set; }

        public string PlayerActiveItem { get; set; }

        public float ScreenX { get; set; }

        public float ScreenY { get; set; }

        public float Z { get; set; }

        public float Distance { get; set; }

        public float RadiusX { get; set; }

        public float RadiusY { get; set; }

        public bool DoDraw { get; set; }

        public bool IsSleeping { get; set; }

        public GameObjectBase Entity { get; set; }     
        
        public string EntityName { get; set; }
        
        public float Render { get; set; }
        {
            
        public float Injector { get; set; }       
    }
}
