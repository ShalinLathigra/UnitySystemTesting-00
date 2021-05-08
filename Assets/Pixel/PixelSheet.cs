using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixel
{
    [CreateAssetMenu(fileName = "PixelSheet", menuName = "PrototypeProject/Pixel/PixelSheet")]
    public class PixelSheet : ScriptableObject
    {
        public static PixelSheet CreateInstance()
        {
            return ScriptableObject.CreateInstance<PixelSheet>();
        }

        // a PixelSheet object is read by the PixelAnimator
        [Tooltip("Animation Frames")]
        public List<PixelFrame> frames;

        [Tooltip("Frames Per Second")]
        public int frameRate;

        public bool loop;

        public float frameDuration => 1.0f / (float) frameRate;
        public int frameCount => frames.Count;

        public void AddFrame(Sprite s, int index)
        {
            PixelFrame newFrame = new PixelFrame(s);
            frames.Insert(index, newFrame);
        }

        public int RemoveFrame(int _index)
        {
            if (_index >= 0 && _index < frameCount)
            {
                frames.RemoveAt(_index);
            }

            return Mathf.Min(_index, frameCount - 1);
        }
    }
}