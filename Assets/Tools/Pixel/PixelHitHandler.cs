using System;
using UnityEngine;

namespace Pixel
{
    public class PixelHitHandler : MonoBehaviour
    {
        public virtual void HandlePixelHit(PixelHitProps _props) => Debug.Log(_props);
    }
}