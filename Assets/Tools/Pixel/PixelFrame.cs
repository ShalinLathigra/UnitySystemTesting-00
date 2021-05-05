using UnityEngine;
using UnityEngine.Serialization;


namespace Pixel
{
    [CreateAssetMenu(fileName = "PixelFrame", menuName = "PrototypeProject/Pixel/PixelFrame")]
    public class PixelFrame : ScriptableObject
    {
        /* Responsibilities: 
         * a PixelFrame is purely a data container
         * Stores:
         *      Sprite
         *      Added squash/Stretch in this cell
         *      Colliders at this frame
         *          Each frame can have a list of colliders of each type?
         *          Makes more sense to have colliders be generic, and supplied with a type?
         */
        public Sprite sprite;
        
        [Tooltip("Positive squash, negative stretch")]
        [Range(-0.5f, 0.5f)]
        public float frameSquash = 0.0f;
    }   
}