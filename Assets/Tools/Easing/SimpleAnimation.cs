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

        private bool _timeScaled;

        // lastTime = (if scaled time) : Time.time
        // lastTime = (if unscaled time) Time.unscaledTime
        // progress += () - lastTime

        public SimpleAnimation
        (
            Action<float> setOutput, 
            float from = 0.0f, 
            float to = 0.0f, 
            float duration = 0.0f, 
            EasingFunction.Ease _ease = EasingFunction.Ease.Linear,
            float progress = 0.0f,
            bool timeScaled = true
        )
        {
            _setOutput = setOutput;
            _timeScaled = timeScaled;
            _from = from;
            _to = to;
            _duration = duration;
            _progress = progress;

            _function = EasingFunction.GetEasingFunction(_ease);

            _timeScaled = timeScaled;

            CalcCurrent();
        }

        public void Step()
        {
            Step((_timeScaled) ? Time.deltaTime : Time.unscaledDeltaTime);
        }

        private void Step(float delta)
        {
            _progress = math.clamp(_progress + delta, 0.0f, _duration);
            CalcCurrent();
            _setOutput.Invoke(current);
        }
        

        private void CalcCurrent()
        {
            current = _function.Invoke(_from, _to, _progress / _duration);
        }
    }
}