using BehaviourStateTree;
using Characters.Base;
using Easing;
using UnityEngine;

namespace States
{
    public class Jump : StateBranch
    {
        AirSO coreAir => ((IAirEntity)core).airSO;
        [SerializeField] protected AudioClipSO audioSO;
        private AnimationCurve jumpCurve => coreAir.jumpCurve;
        private float duration => jumpCurve.keys[jumpCurve.keys.Length - 1].time;

        private float _completeTime = 0.0f;
        public bool extendedJump => (_completeTime >= startTime + duration);
        private bool canEndJump => Time.time - startTime > duration * 0.4f;

        public override void Enter()
        {
            base.Enter();
            core.audio.Play(audioSO);
            core.squash.ApplySquashStretch( 0.125f, 0.125f, EasingFunction.Ease.Spring);
        }
    
        public override void FixedDo()
        {
            core.rb.velocity = new Vector2(
                core.rb.velocity.x, 
                jumpCurve.Evaluate(Time.time - startTime) * coreAir.maxJumpSpeed
            );

            complete = (Time.time > (startTime + duration)) || (Engine.e.input.jumpCancelled && canEndJump);
        }

        public override void Exit()
        {
            base.Exit();
            _completeTime = Time.time;
        }
    }
}
