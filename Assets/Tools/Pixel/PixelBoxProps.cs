using System;
using UnityEngine;

namespace Pixel
{
    [Serializable]
    public struct PixelBoxProps
    {
        public Vector2 center;
        public Vector2 size;
        public float value;
        public bool active;
        public float squash;
        public float hitStop;

        public PixelBoxProps(PixelType type)
        {
            center = Vector2.zero;
            size = Vector2.zero;
            value = 0;
            squash = 0;
            hitStop = 0;
            active = false;
        }

        public override string ToString()
        {
            var ret = "";
            
            if (center != Vector2.zero) ret += $"| center : {center} |";
            if (size != Vector2.zero) ret += $"| size : {size} |";
            if (value != 0.0f) ret += $"| value : {value} |";
            if (active != false) ret += $"| active : {active} |";
            if (squash != 0.0f) ret += $"| squash : {squash} |";
            
            return ret;
        }
    }
}