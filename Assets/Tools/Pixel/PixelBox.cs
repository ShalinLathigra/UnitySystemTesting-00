using System;
using SuperTiled2Unity.Editor.LibTessDotNet;
using UnityEngine;

namespace Pixel
{
    /* Provides PixelAnimator with way to edit collider details
     * Colliders implement an Interface, that interface basically acts like a tag.
     * IHit or IHurt
     * Interfaces should provide a way to set properties?
     */
    public abstract class PixelBox : MonoBehaviour
    {
        private BoxCollider2D _boxCollider;

        protected virtual void Awake()
        {
            _boxCollider = gameObject.AddComponent<BoxCollider2D>();;
            _boxCollider.isTrigger = true;
        }

        public void SetCollider(bool active, Rect rect)
        {
            _boxCollider.enabled = active;
            _boxCollider.size = rect.size;
            _boxCollider.offset = rect.center;
        }
        

        private void OnDrawGizmos()
        {
            
        }
    }
}