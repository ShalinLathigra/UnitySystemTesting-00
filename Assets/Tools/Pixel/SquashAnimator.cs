using System;
using Easing;
using Unity.Mathematics;
using UnityEngine;

namespace Pixel
{
    /* Responsibilities:
     * Application of Squash and Stretch to a transform
     * Only affects X and Y values
     * Currently assumes default scale of 1,1,1. Can change later if desired
     */
    public class SquashAnimator : MonoBehaviour
    {
        private SimpleAnimation _simple;
        private Action<float> _callBack;
        
        private const float Tolerance = 0.005f;
        
        private float _squashStretch;

        private readonly float BaseSquashStretch = 1.0f;

        private Transform _target;
        //TODO: Set Base Squash Stretch based on initial transform values. This allows scaling to happen
        void Awake ()
        {
            _squashStretch = 0.0f;
            _callBack = value => this._squashStretch = value;
            _simple = new SimpleAnimation(_callBack);

            _target = GetComponent<Transform>();
        }

        public void Update()
        {
            _target.localScale = new Vector3(
                BaseSquashStretch + _squashStretch,
                1.0f / (BaseSquashStretch + math.max(_squashStretch, -0.9f)),
                1.0f
            );
            if (_simple.complete)
                if (Math.Abs(_squashStretch - 0.0f) > Tolerance)
                {
                    AnimateReset();
                }
                else
                {
                    _squashStretch = 0.0f;
                    return;
                }
            _simple.Step();
        }

        public void AnimateReset(float duration = 1.0f, EasingFunction.Ease ease = EasingFunction.Ease.EaseOutElastic)
        {
            _simple = new SimpleAnimation(
                _callBack, 
                _squashStretch, 
                0.0f, 
                duration,
                ease
            );
        }
        public void ApplySquashStretch(float to = 0.25f, float duration = 0.125f, EasingFunction.Ease ease = EasingFunction.Ease.EaseInOutElastic)
        {
            _simple = new SimpleAnimation(
                _callBack, 
                _squashStretch, 
                to, 
                duration, 
                ease
            );
        }
    }
}