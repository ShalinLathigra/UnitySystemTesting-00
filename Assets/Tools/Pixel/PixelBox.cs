using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pixel
{
    public class PixelBox : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D box;
        private PixelHitHandler handle => owner.pixelHandler;
        public PixelBoxAnimator owner { get; set; }

        public PixelType type;
        public string id;
        public bool collisionActive;

        public PixelBoxProps props;

        private void Awake()
        {
            collisionActive = false;
            box.size = Vector2.zero;
            box.offset = Vector2.zero;
            box.isTrigger = true;
            type = PixelType.Default;
        }
        
        public void ApplyProperties(PixelBoxProps _props, float ppu, bool flipX)
        {
            props = _props;
            collisionActive = props.active;
            box.size = props.size / ppu;
            
            box.offset = props.center / ppu;
            if (flipX) box.offset *= new Vector2(-1, 1);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (VerifyCollision(other, out var otherBox))
                ResolveCollisionStart(this, otherBox);
        }

        private bool VerifyCollision(Collider2D other, out PixelBox otherBox)
        {
            otherBox = other.gameObject.GetComponent<PixelBox>();
            if (otherBox == null) return false;
            if (!collisionActive || !otherBox.collisionActive) return false;
            if (otherBox.type == type) return false;

            return true;
        }

        private static void ResolveCollisionStart(PixelBox hurt, PixelBox hit)
        {   
            var hurtProps = new PixelHitProps(
                _initiatorType: PixelType.Hit,
                _squash: hit.props.squash, 
                _hitStop: hit.props.hitStop,
                _message: $"Getting Hurt By: {hit.id}"
                );
            var hitProps = new PixelHitProps( 
                _initiatorType: PixelType.Hurt,
                _message: $"Hurting: {hurt.id}"
                );
            
            hurt.handle?.HandlePixelHit(hurtProps);
            hit.handle?.HandlePixelHit(hitProps);
        }
    }
}
