using System;
using Characters.Base;
using Easing;
using Pixel;
using UnityEngine;

namespace Components
{
    public class Life : PixelHitHandler
    {
        [SerializeField] private Character core;
        [SerializeField] private float maxHealth = 25;
        [SerializeField] private AudioClipSO hitSound;
        private float health { get; set; }

        private float _hitStopDuration;
        private float _hitStopStart;
        public bool hitStopped;


        private void Awake()
        {
            health = maxHealth;
            _hitStopStart = 0.0f;
            _hitStopDuration = 0.0f;
            hitStopped = false;
        }

        private void Update()
        {
            if (hitStopped)
            {
                hitStopped = (_hitStopStart + _hitStopDuration) > Time.time;
                if (!hitStopped)
                {
                    core.pixel.Play();
                    hitStopped = false;
                }
            }
        }

        public override void HandlePixelHit(PixelHitProps _props)
        {
            // TODO: Should use a UI to animate this difference?
            health -= _props.damage;
            ApplyHitStop(_props.hitStop);
            if (_props.initiatorType == PixelType.Hit)
            {
                ApplyHitJuice(dir:Vector2.up, mag:2.5f, squash:_props.squash);
            }

            Debug.Log(_props);
        }

        private void ApplyHitJuice(Vector2 dir, float mag, float squash)
        {
            core.squash.ApplySquashStretch(squash, 0.1f, EasingFunction.Ease.EaseOutCirc);
            core.rb.velocity = dir * mag;
            core.audio.Play(hitSound);
        }

        private void ApplyHitStop(float _propsHitStop)
        {
            if (!(_propsHitStop > 0.0f)) return;
            
            core.pixel.Pause();
            _hitStopDuration = _propsHitStop;
            _hitStopStart = Time.time;
            hitStopped = true;
            core.rb.velocity = Vector2.zero;
        }
    }
}
