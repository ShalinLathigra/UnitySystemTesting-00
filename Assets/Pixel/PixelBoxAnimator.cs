using System;
using UnityEngine;

namespace Pixel
{
    public class PixelBoxAnimator : MonoBehaviour
    {
        public delegate void PixelCompleteDelegate();
        public PixelCompleteDelegate pixelComplete;
        
        public PixelBox.PixelCollisionDelegate hitCollision;
        
        public PixelSheet sheet;
        public bool playing;

        private SpriteRenderer _sprite;
        private float _timeOfLastFrame;
        private int _sheetIndex;
        private bool _invoked;
        private bool sheetActive => sheet != null;

        private PixelBox _hitBox;
        private PixelBox _hurtBox;

        private void Awake()
        {
            _invoked = false;
            _sheetIndex = 0;
            _timeOfLastFrame = Time.time;
            _sprite = GetComponent<SpriteRenderer>();

            _hitBox = gameObject.AddComponent<PixelBox>();
            _hurtBox = gameObject.AddComponent<PixelBox>();

            hitCollision = _hitBox.pixelCollision;

            if (sheetActive)
                if (sheet.frames.Count > 0)
                    ApplyFrame(sheet.frames[_sheetIndex]);

            Engine.e.RegisterPixelAnimator(this);
        }

        private void Update()
        {
            if (!playing || !sheetActive) return;  // If sheet null
            if (!(Time.time > _timeOfLastFrame + sheet.frameDuration) || sheet.frames.Count <= 0) return; // If sheet invalid
            ApplyCurrentFrame();
            AdvanceFrame();
            _timeOfLastFrame = Time.time;
        }

        private void AdvanceFrame()
        {
            _sheetIndex += 1;
            if (sheet.loop)
            {
                _sheetIndex %= sheet.frameCount;
                return;
            }
        
            // in this case, advance frame, emit a signal
            if (_sheetIndex >= sheet.frameCount && !_invoked)
            {
                pixelComplete?.Invoke();
                hitCollision?.Invoke(_hitBox, _hurtBox);
                _invoked = true;
            }
            
            _sheetIndex =  Mathf.Min(_sheetIndex+1, sheet.frameCount - 1);
        }

        private void ApplyCurrentFrame()
        {
            ApplyFrame(sheet.frames[_sheetIndex]);
        }
        private void ApplyFrame(PixelFrame f)
        {
            _sprite.sprite = f.sprite;

            _hitBox.ApplyProperties(f.hitProps, _sprite.sprite.pixelsPerUnit);
            _hurtBox.ApplyProperties(f.hurtProps, _sprite.sprite.pixelsPerUnit); 
        }

        public void Play(PixelSheet s)
        {
            sheet = s;
            _sheetIndex = 0;
            _invoked = false;
            playing = true;
            ApplyCurrentFrame();
        }

        private void OnDestroy()
        {
            Engine.e.DeRegisterPixelAnimator(this);
        }
    }
}
