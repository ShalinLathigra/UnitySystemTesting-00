using BStateMachine;
using Easing;
using UnityEngine;

namespace States
{
    public class Fall : BehaviourState
    {
        AirSO coreAir => ((IAirEntity)core).airSO;

        private AnimationCurve _fallCurve;

        bool _longFall = true;


        public override void Enter()
        {
            base.Enter();
            if (_fallCurve == null)
                SetFall(true);
            core.pixel.ApplySquashStretch(-0.3f, 0.75f, EasingFunction.Ease.Linear);
        }

        public void SetFall(bool longFall)
        {
            this._longFall = longFall;
            _fallCurve = (this._longFall) ? coreAir.longFallCurve : coreAir.shortFallCurve;
        }
    
        public override void FixedDo()
        {
            core.rb.velocity = new Vector2(
                core.rb.velocity.x, 
                Engine.e.maxFallSpeed * _fallCurve.Evaluate(Time.time - startTime)
            );

            if (core.spatial.grounded)
                complete = true;
        }
        public override void Exit()
        {
            core.rb.velocity = new Vector2(core.rb.velocity.x, 0.0f);
            float squash = _fallCurve.Evaluate(Time.time - startTime) * 0.25f;
            core.pixel.ApplySquashStretch(squash, 0.125f, EasingFunction.Ease.Linear);
        }
    }
}
