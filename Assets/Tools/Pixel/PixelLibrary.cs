using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace  Pixel
{
    [CreateAssetMenu(fileName = "PixelLibrary", menuName = "PrototypeProject/Pixel/PixelLibrary")]
    public class PixelLibrary : ScriptableObject
    {
        public static PixelLibrary CreateInstance()
        {
            return ScriptableObject.CreateInstance<PixelLibrary>();
        }

        // This is for personal organization. Not used in game, but gives easy way to track animations + Owners
        public List<PixelSheet> pixelSheets;
        public string ownerName;
        
        /* Pixel Sheets are edited individually
         * 
         */
    }
}