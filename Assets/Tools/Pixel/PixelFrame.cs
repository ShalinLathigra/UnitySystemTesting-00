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

        public float appliedSquash;
        public PixelFrame(Sprite s)
        {
            sprite = s;
            hitProps = new PixelBoxProps(PixelType.Hit);
            hurtProps = new PixelBoxProps(PixelType.Hurt);
        }
    }
}