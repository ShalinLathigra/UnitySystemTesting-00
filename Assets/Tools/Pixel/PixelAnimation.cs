using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixel
{
    [CreateAssetMenu(fileName = "PixelAnimation", menuName = "PrototypeProject/PixelAnimator/PixelAnimation")]
    public class PixelAnimation : ScriptableObject
    {
        public static PixelAnimation CreateInstance()
        {
            return ScriptableObject.CreateInstance<PixelAnimation>();
        }

        public List<PixelFrame> frames;
        public int frameCount => frames.Count;
        public float frameRate;
        public float frameDuration => frameCount / frameRate;

        public int frameIndex = 0;

        public PixelFrame currentFrame => frames[frameIndex];
        public bool looping;

        public PixelFrame nextFrame()
        {
            frameIndex += 1;
            frameIndex = (looping) ? frameIndex % frameCount : Mathf.Clamp(frameIndex, 0, frameCount - 1);
            return currentFrame;
        }
        /*
        TODO: as far as the Animator goes, this is where I'll need to start considering the different collider types
        TODO: Animator should be a whole different Part that can be attached, and reads from a scriptable object animations passed in by states. 
        TODO: AnimObjects contain: maybe a dict mapping each frame (key = i {0, anim length}) to {sprite, hit/hurt/etc boxes}
        */

        /* Responsibilities: 
         * Primarily a data storage object
         * Knows what Frames exist, provides a way to access an arbitrary frame, or get the current frame
         */
    }    
}

