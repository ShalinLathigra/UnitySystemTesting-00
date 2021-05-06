using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pixel
{
    [CreateAssetMenu(fileName = "PixelSheet", menuName = "PrototypeProject/Pixel/PixelSheet")]
    public class PixelSheet : ScriptableObject
    {
        public static PixelSheet CreateInstance()
        {
            return ScriptableObject.CreateInstance<PixelSheet>();
        }

        public void Init(string _owner)
        {
            ownerDirectory = _owner;
            frames = new List<PixelFrame>();
            frameRate = 12;
            startFrame = 0;
            looping = false;
        }

        public string ownerDirectory;
        
        public List<PixelFrame> frames;
        public float frameDuration => frameCount / frameRate;
        public float frameRate;
        public int startFrame;
        public PixelFrame currentFrame => frames[_frameIndex];
        public bool looping;

        private int frameCount => frames.Count;
        private int _frameIndex = 0;

        public void NextFrame(out int _i)
        {
            _frameIndex += 1;
            _frameIndex = (looping) ? _frameIndex % frameCount : Mathf.Clamp(_frameIndex, 0, frameCount - 1);
            _i = _frameIndex;
        }

        public void RestartAnimation()
        {
            _frameIndex = (looping) ? startFrame % frameCount : Mathf.Clamp(startFrame, 0, frameCount - 1);;
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

