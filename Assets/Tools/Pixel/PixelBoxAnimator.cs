using UnityEngine;

namespace Pixel
{
    public class PixelBoxAnimator : PixelAnimator
    {

        public PixelHitHandler pixelHandler;

        public PixelBox hitBox;
        public PixelBox hurtBox;
        public bool hittable = true;
        protected override void Awake()
        {
            base.Awake();
            if (hittable)
                PopulateBoxes();
        }

        private void PopulateBoxes()
        {
            hitBox.id = gameObject.name + "_hitBox";
            hitBox.type = PixelType.Hit;
            hitBox.owner = this;
            
            hurtBox.id = gameObject.name + "_hurtBox";
            hurtBox.type = PixelType.Hurt;
            hurtBox.owner = this;
        }

        protected override void ApplyFrame(PixelFrame f)
        {
            base.ApplyFrame(f);
            hitBox.ApplyProperties(f.hitProps, _sprite.sprite.pixelsPerUnit, _sprite.flipX);
            hurtBox.ApplyProperties(f.hurtProps, _sprite.sprite.pixelsPerUnit, _sprite.flipX);
        }
    }
}
