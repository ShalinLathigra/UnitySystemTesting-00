using System;
using UnityEngine;

namespace Pixel
{
    [Serializable]
    public class PixelFrame
    {
        public Sprite sprite;

        public PixelBoxProps hitProps;
        public PixelBoxProps hurtProps;

        public PixelFrame(Sprite s)
        {
            sprite = s;
            hitProps = new PixelBoxProps(PixelBoxType.Hit);
            hurtProps = new PixelBoxProps(PixelBoxType.Hurt);
        }
    }

    [Serializable]
    public struct PixelBoxProps
    {
        public Vector2 center;
        public Vector2 size;
        public float value;
        public bool active;

        public PixelBoxProps(PixelBoxType type)
        {
            center = Vector2.zero;
            size = Vector2.zero;
            value = 0.0f;
            active = false;
        }
    }
}