using BehaviourStateTree;
using Characters.Base;
using Pixel;
using Unity.Mathematics;
using UnityEngine;

namespace States
{
    public class AirMetaBranch : StateBranch
    {
        [SerializeField] Jump jump;
        [SerializeField] Fall fall;

        [SerializeField] private PixelSheet sheet;
        
        IAirEntity coreEntity => (IAirEntity) core;
        AirSO coreAir => coreEntity.airSO;

        public bool landing => state == fall && state.complete;
        public override void Enter()
        {
            base.Enter();
            if (coreEntity.shouldJump && coreEntity.canJump && state != jump)
            {
                Set(jump, true);
            }
            else if (state != jump)
            {
                Set(fall);
                fall.SetFall(false);
            }
            core.pixel.StopAndSwap(sheet);
        }

        public override void Do()
        {
            float velY = core.rb.velocity.y;
            float min = GameEngine.e.maxFallSpeed; // -50
            float max = coreAir.maxJumpSpeed;   // 10

            int i = 0;

            if (velY < -10.0)
                i = 2;
            else if (velY < 5.0)
                i = 1;
            else
                i = 0;
            
            core.pixel.SetFrameUnsafe(i);

        }
    
        public override void FixedDo()
        {
            core.rb.velocity = math.lerp(
                core.rb.velocity, 
                new Vector2(GameEngine.e.input.horizontalInput * coreAir.maxAirSpeed, 0.0f),
                Mathf.Clamp(Time.time - startTime, 0.0f, 1.0f)
            );

            state.FixedDo();

            if (!state.complete) return;
            
            if (state == jump)
            {
                Set(fall);
                fall.SetFall(jump.extendedJump);
            }
            else if (state == fall)
            {
                complete = true;
            }
        }
        public override void Exit()
        {
            base.Exit();
            state = null;
        }
    }
}
