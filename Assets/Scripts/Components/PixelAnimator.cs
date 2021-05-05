using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;

using Easing;
using Unity.Mathematics;

namespace Components
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PixelAnimator : MonoBehaviour
    {
        public Pixel.PixelSheet anim;
        private SpriteRenderer _renderer;

        /* Responsibilities:
         * Iterating through a PixelAnimation
         * Setting the active PixelBoxes for the current PixelFrame as returned by PixelAnimation
         * Applying Squash and Stretch to sprite (Visual only effect)
         */

        private float _timeOfLastFrame;
        private void Awake()
        {
            _timeOfLastFrame = Time.time;

            anim.RestartAnimation();
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void Update()
        {
            if (Time.time < _timeOfLastFrame + anim.frameDuration) return;
            anim.NextFrame();
            _renderer.sprite = anim.currentFrame.sprite;
            _timeOfLastFrame = Time.time;
        }
    }
}    