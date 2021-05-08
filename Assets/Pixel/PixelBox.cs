using System;
using UnityEngine;

namespace Pixel
{
    public class PixelBox : MonoBehaviour
    {
        private BoxCollider2D _box;
        
        public delegate void PixelCollisionDelegate(PixelBox origin, PixelBox target);
        public PixelCollisionDelegate pixelCollision;

        private void Awake()
        {
            _box = gameObject.AddComponent<BoxCollider2D>();
            _box.enabled = false;
            _box.size = Vector2.zero;
            _box.offset = Vector2.zero;
            _box.isTrigger = true;
        }

        public void ApplyProperties(PixelBoxProps _props, float ppu)
        {
            _box.enabled = _props.active; 
            _box.size = _props.size / ppu;
            _box.offset = _props.center / ppu;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            PixelBox otherBox = other.gameObject.GetComponent<PixelBox>();
            if (otherBox != null)
            {
                pixelCollision.Invoke(this, otherBox);
            }
        }
    }
}
