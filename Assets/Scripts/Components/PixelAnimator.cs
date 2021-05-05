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
        private Transform _spriteTransform;
        private SpriteRenderer _renderer;

        private SimpleAnimation _simple;
        private Action<float> _simpleCallback;

        private const float TOLERANCE = 0.005f;
        /* Responsibilities:
         * Iterating through a PixelAnimation
         * Setting the active PixelBoxes for the current PixelFrame as returned by PixelAnimation
         * Applying Squash and Stretch to sprite (Visual only effect)
         */

        private float _timeOfLastFrame;
        
        private float _squashStretch;

        private const float BaseSquashStretch = 1.0f;
        private void Awake()
        {
            _timeOfLastFrame = Time.time;
            _squashStretch = 0.0f;

            anim.RestartAnimation();
            _spriteTransform = GetComponent<Transform>();
            _renderer = GetComponent<SpriteRenderer>();

            _simpleCallback = value => this._squashStretch = value;
            _simple = new SimpleAnimation(_simpleCallback);
            
        }

        public void Update()
        {
            HandleSquashStretch();
            if (Time.time < _timeOfLastFrame + anim.frameDuration) return;
            anim.nextFrame();
            _renderer.sprite = anim.currentFrame.sprite;
            _timeOfLastFrame = Time.time;
        }

        private void HandleSquashStretch()
        {
            _spriteTransform.localScale = new Vector3(
                BaseSquashStretch + _squashStretch,
                1.0f / (BaseSquashStretch + math.max(_squashStretch, -0.9f)),
                1.0f
            );

            if (_simple.complete)
                if (Math.Abs(_squashStretch - 0.0f) > TOLERANCE)
                {
                    AnimateResetSquash();
                }
                else
                {
                    _squashStretch = 0.0f;
                    return;
                }
            _simple.Step();
        }

        public void AnimateResetSquash()
        {
            _simple = new SimpleAnimation(
                _simpleCallback, 
                _squashStretch, 
                0.0f, 
                1.0f,
                EasingFunction.Ease.EaseOutElastic
            );
        }

        public void ApplySquashStretch(float to = 0.25f, float duration = 0.125f, EasingFunction.Ease _ease = EasingFunction.Ease.EaseInOutElastic)
        {
            _simple = new SimpleAnimation(
                _simpleCallback, 
                _squashStretch, 
                to, 
                duration, 
                _ease
                );
        }
    }
}    