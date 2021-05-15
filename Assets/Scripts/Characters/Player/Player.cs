using System;
using System.Collections.Generic;
using AttackLibrary;
using UnityEngine;
using Characters.Base;
using States;

namespace Characters.Player
{
    public class Player : HybridCharacter
    {
        [Header("Available States")]
        [SerializeField] private GroundMetaBranch groundMeta;
        [SerializeField] private AirMetaBranch airMeta;
        [SerializeField] private List<Attack> attacks;

        [SerializeField] private PlayerAttackLibrary _library;

        // Change up how inputs work?
            // Input is a struct?
            // core.input just tells the thing Should I do this stuff?
            // The playerInputWrapper
            
            // Player's states are determined based on 

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
                    Debug.Log(input.lastAttackKey);
                    var strikeValid = _library.RequestAttack(input.lastAttackKey, out var toAttack);
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
                        //var toAttack = attacks[input.lastAttackKey % attacks.Count];
                        //TODO: Implement Attack Library Object
                        // Create an AttackLibrary that just stores all the different possible attacks. Set(QueryAttackLibrary(state, index), true)
                        //Set(toAttack, true);
                        var strikeValid = _library.RequestAttack(input.lastAttackKey, out var toAttack);
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
