using UnityEngine;
using Characters.Base;
using BStateMachine;


namespace Characters.Player
{
    public class Player : HybridCharacter
    {
        [Header("Available States")]
        [SerializeField] private GroundMetaState groundMeta;
        [SerializeField] private AirMetaState airMeta;

        //TODO: Connect Animations

        public override bool canJump => Time.time - spatial.timeLastGrounded <= 0.1f;
        public override bool airComplete => (spatial.grounded && airMeta.complete);
        public override bool shouldJump => input.shouldJump;

        protected override void Awake() {
            base.Awake();
            Set(groundMeta);
        }

        protected override void FixedUpdate() {
            //TODO: Debug why coyote time is not working
            if ((!airComplete) || (canJump && input.shouldJump))
            {
                Set(airMeta);
            }   
            else
            {
                Set(groundMeta);
            }

            base.FixedUpdate();
        }

        public override void SetAirState()
        {
            Set(airMeta);
        }
    }
}
