using BehaviourStateTree;
using UnityEngine;

namespace States
{
    public class GroundMetaBranch : StateBranch
    {
        [SerializeField] private Move move;
        [SerializeField] private Idle idle;
        // Also needs reference to the input mechanism
        public override void Enter()
        {
            base.Enter();
            Set(idle);
        }

        public override void FixedDo()
        {
            if (core.input.shouldMove)
                Set(move);
            else
                Set(idle);

            state.FixedDo();
        }

        public override void LateDo()
        {
            if (core.spatial.distanceToGround > core.spatial.skinWidth) 
            {
                float distance = core.spatial.distanceToGround - core.spatial.skinWidth;
                core.rb.MovePosition(core.rb.position - new Vector2(0.0f, distance));
            }
            base.LateDo();
        }
    }
}
