using UnityEngine;

namespace Pixel
{
    public class PixelAnimator : MonoBehaviour
    {
        public delegate void PixelCompleteDelegate();
        public PixelCompleteDelegate pixelComplete;

        public SquashAnimator squash;
        [SerializeField] protected PixelSheet sheet;
        [SerializeField] protected bool playing = true;

        protected SpriteRenderer _sprite;
        private float _timeOfLastFrame;
        [SerializeField] protected int _sheetIndex;
        private bool _invoked;
        private bool sheetActive => sheet != null;
        public int currentIndex => _sheetIndex;

        public void Pause() => playing = false;
        public void Play() => playing = true;
        protected virtual void Awake()
        {
            _invoked = false;
            _sheetIndex = 0;
            _timeOfLastFrame = Time.time;
            _sprite = GetComponent<SpriteRenderer>();
        }

        protected void Update()
        {
            if (!playing || !sheetActive) return;  // If sheet null
            if (Time.time < _timeOfLastFrame + sheet.frameDuration || sheet.frames.Count <= 0) return; // If sheet invalid
            AdvanceFrame();
            ApplyCurrentFrame();
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
        
            if (_sheetIndex >= sheet.frameCount && !_invoked)
            {
                pixelComplete?.Invoke();
                _invoked = true;
            }
            
            _sheetIndex =  Mathf.Min(_sheetIndex, sheet.frameCount);
        }

        private void ApplyCurrentFrame()
        {
            ApplyFrame(sheet.frames[_sheetIndex % sheet.frameCount]);
        }

        protected virtual void ApplyFrame(PixelFrame f)
        {
            _sprite.sprite = f.sprite;
            if (f.appliedSquash != 0.0f)
                squash.ApplySquashStretch(f.appliedSquash);
        }

        public void SetFrameUnsafe(int index)
        {
            _sheetIndex = index;
            ApplyCurrentFrame();
        }
        public void StopAndSwap(PixelSheet s, bool overRide = false)
        {
            playing = false;
            if (sheet == s && !overRide) return;
            sheet = s;
            ResetParams();
            ApplyCurrentFrame();
        }
        public void Play(PixelSheet s, bool overRide = false)
        {
            if (sheet == s && !overRide) return;

            sheet = s;
            ResetParams();
            ApplyCurrentFrame();
        }

        private void ResetParams()
        {
            _sheetIndex = 0;
            _invoked = false;
            playing = true;
            _timeOfLastFrame = Time.time;
        }

        public void SetDir(bool dir)
        {
            if (dir != _sprite.flipX)
                _sprite.flipX = dir;
        }

        public bool PlayingSheet(PixelSheet _sheet)
        {
            return sheet == _sheet;
        }
    }
}