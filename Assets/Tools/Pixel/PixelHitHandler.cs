using System;
using UnityEngine;

namespace Pixel
{
    public abstract class PixelHitHandler : MonoBehaviour
    {
        public abstract void HandlePixelHit(PixelHitProps _props);
    }
}