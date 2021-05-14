using System;
using BehaviourStateTree;
using Pixel;
using UnityEngine;

namespace States
{
    public class Idle : StateBranch
    {
        [SerializeField] private PixelSheet sheet;
        [Range(0, 1)] [SerializeField] private float friction;

        public override void Enter()
        {
            core.pixel.Play(sheet);
        }

        public override void FixedDo()
        {
            base.FixedDo();
            core.rb.velocity *= new Vector2(friction, 1);
        }
    }
}