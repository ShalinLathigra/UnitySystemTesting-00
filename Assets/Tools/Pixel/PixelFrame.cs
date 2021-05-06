using System;
using UnityEngine;
using UnityEngine.Serialization;


namespace Pixel
{
    [CreateAssetMenu(fileName = "PixelFrame", menuName = "PrototypeProject/Pixel/PixelFrame")]
    [Serializable]
    public class PixelFrame : ScriptableObject
    {
        /* Responsibilities: 
         * a PixelFrame is purely a data container
         * Stores:
         *      Sprite
         *      Added squash/Stretch in this cell
         *      Lists of PixelBox types  
         */

        public void Init()
        {
            hitProps = new HitFrameProperties(Color.red);
            hurtProps = new HurtFrameProperties(Color.green);
            
        }

        [SerializeField]
        public Sprite sprite;

        [SerializeField]
        public HitFrameProperties hitProps;
        [SerializeField]
        public HurtFrameProperties hurtProps;

        // Has some sort of timeline of events
        // Updates it's box values based on these input timelines?
    }
    [System.Serializable]
    public struct HitFrameProperties
    {
        public Rect shape;
        public bool active;
        public Color color;
        public HitFrameProperties(Color c)
        {
            color = c;
            active = false;
            shape = Rect.zero;
        }
    }
    [System.Serializable]
    public struct HurtFrameProperties
    {
        public Rect shape;
        public bool active;
        public Color color;
        public HurtFrameProperties(Color c)
        {
            color = c;
            active = false;
            shape = Rect.zero;
        }
    }
}