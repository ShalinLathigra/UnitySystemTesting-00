using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace  Easing
{
    public class SimpleAnimation
    {
        private readonly Action<float> _setOutput;
        private readonly float _from;
        private readonly float _to;
        public float current { get; private set; }
        private readonly float _duration;
        private float _progress;
        private readonly EasingFunction.Function _function;

        public bool complete => (_progress >= _duration) || _duration == 0.0f;


        public SimpleAnimation
        (
            Action<float> setOutput, 
            float from = 0.0f, 
            float to = 0.0f, 
            float duration = 0.0f, 
            EasingFunction.Ease _ease = EasingFunction.Ease.Linear,
            float progress = 0.0f
        )
        {
            _setOutput = setOutput;
            _from = from;
            _to = to;
            _duration = duration;
            _progress = progress;

            _function = EasingFunction.GetEasingFunction(_ease);

            CalcCurrent();
        }

        public float Step()
        {
            return Step(Time.deltaTime);
        }
        
        public float Step(float delta)
        {
            _progress = math.clamp(_progress + delta, 0.0f, _duration);
            CalcCurrent();
            _setOutput.Invoke(current);
            return current;
        }
        

        private float CalcCurrent()
        {
            current = _function.Invoke(_from, _to, _progress / _duration);
            return current;
        }
    }
}