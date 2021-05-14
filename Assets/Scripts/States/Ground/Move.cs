using Unity.Mathematics;
using UnityEngine;

using BehaviourStateTree;
using Pixel;

/*
* Summary: Move
* This State is used by all objects moving SIDE TO SIDE
* Clamps motion to the grid, can be used by other Meta-States
*/
namespace States
{
    public class Move : StateBranch
    {
        //* Contains all of the important fields modifying this behaviour
        MoveSO coreMove => ((IMoveEntity)core).moveSO;
        [SerializeField] private PixelSheet sheet;


        private Vector2 _stateVelocity;
        public override void Enter()
        {
            base.Enter();
            core.pixel.Play(sheet);
        
            float derivedStart = math.abs((core.rb.velocity.x) / coreMove.maxSpeed);
            startTime = derivedStart;
        }
        public override void FixedDo()
        {
            _stateVelocity = new Vector2(
                core.input.horizontalInput * coreMove.maxSpeed * coreMove.accelCurve.Evaluate(Time.time - startTime), 
                0.0f
            );
        
            Vector2 alignWithGround = new Vector2(
                core.spatial.groundNormal.y,
                -core.spatial.groundNormal.x
            );
            
            if (Mathf.Abs(core.spatial.groundNormal.x) > coreMove.maxSlope)
                alignWithGround *= -1;
        
            // Not a hard set to smooth out the transition
            core.rb.velocity = math.lerp(core.rb.velocity, _stateVelocity.x * alignWithGround, coreMove.accelCurve.Evaluate(Time.time - startTime));

            // Completion Check
            complete = !(core.input.shouldMove && core.spatial.grounded);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}