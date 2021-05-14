using System;
using UnityEngine.InputSystem.Utilities;
// ReSharper disable MemberCanBePrivate.Global

namespace Pixel
{
    public readonly struct PixelHitProps
    {
        public string message { get; }
        public int damage { get; }
        public float hitStop { get; }
        public float squash { get; }

        public readonly PixelType initiatorType;
        
        // Store data type representing DOT effects here.

        public PixelHitProps
        (
            PixelType _initiatorType = PixelType.Default, 
            int _damage=0, 
            float _hitStop=0.0f,
            float _squash=0.0f,
            string _message = ""
        )
        {
            initiatorType = _initiatorType;
            damage = _damage;
            hitStop = _hitStop;
            squash = _squash;
            message = _message;
        }
        
        public override string ToString()
        {
            var ret = "";
            
            if (initiatorType != PixelType.Default) ret += $"| type : {Enum.GetName(typeof(PixelType), initiatorType)} |";
            if (damage != 0) ret += $"| damage : {damage} |";
            if (hitStop != 0.0f) ret += $"| hitStop : {hitStop} |";
            if (squash != 0.0f) ret += $"| squash : {squash} |";
            if (message != "") ret += $"| message : {message} |";
            
            return ret;
        }

        // Should track:
        /* 
         * Knockback Amount
         * Knockback Vector (Based on PixelBoxProps, just passed in here
         * should the character be moving? (is there a vel at this point
         */
    }
}