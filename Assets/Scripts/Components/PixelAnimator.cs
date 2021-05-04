using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PixelAnimator : MonoBehaviour
    {
        public Pixel.PixelAnimation anim;

        private SpriteRenderer _renderer;
        /* Responsibilities: 
     * Iterating through a PixelAnimation
     * Setting the active PixelBoxes for the current PixelFrame as returned by PixelAnimation
     */

        private float _timeOfLastFrame;

        private void Awake()
        {
            _timeOfLastFrame = Time.time;
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void Update()
        {
            if (Time.time < _timeOfLastFrame + anim.frameDuration) return;
            anim.nextFrame();
            _renderer.sprite = anim.currentFrame.sprite;
        }
    }
}    