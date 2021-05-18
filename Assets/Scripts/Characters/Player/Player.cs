using System;
using System.Collections.Generic;
using AttackLibrary;
using UnityEngine;
using Characters.Base;
using Components;
using States;

namespace Characters.Player
{
    public class Player : HybridCharacter
    {
        [Header("Available States")]
        [SerializeField] private GroundMetaBranch groundMeta;
        [SerializeField] private AirMetaBranch airMeta;

        [SerializeField] private PlayerAttackLibrary library;

        
        private static InputWrapper input => GameEngine.e.input;
        public override bool canJump => Time.time - spatial.timeLastGrounded <= 0.25f;
        public override bool airComplete => (spatial.grounded && airMeta.complete);
        public override bool shouldJump => input.shouldJump;
        public override bool blocked => inAttack && !state.complete;
        private bool inAttack => state.GetType() == typeof(Attack);


        protected override void Awake() {
            base.Awake();
            Set(groundMeta);

            blocked = false;
        }

        protected override void Update()
        {
            base.Update();
            if (Mathf.Abs(input.horizontalInput) > 0.0f && !(life.hitStopped || blocked)) pixel.SetDir(input.horizontalInput < 0);
        }

        protected override void FixedUpdate()
        {
            //TODO: Debug why coyote time is not working
            if (life.hitStopped) return;
            if (!blocked)
            {
                if (input.shouldAttack) // First priority
                {
                    var strikeValid = library.RequestAttack(input.lastAttackKey, out var toAttack);
                    if (strikeValid) Set(toAttack, inAttack);
                }
                else if ((!airComplete) || (canJump && input.shouldJump)) // Second priority
                {
                    Set(airMeta);
                }
                else  // Default
                {
                    Set(groundMeta);
                }
            }
            else 
            {
                if (input.shouldAttack)    // In this case, you are definitely blocked somehow
                {
                    if (((Attack) state).canSkip)
                    {
                        var strikeValid = library.RequestAttack(input.lastAttackKey, out var toAttack);
                        if (strikeValid) Set(toAttack, inAttack);
                    }
                }
            }
            base.FixedUpdate();
        }

        public override void SetAirState()
        {
            if (!life.hitStopped)
                Set(airMeta);
        }
    }
}
