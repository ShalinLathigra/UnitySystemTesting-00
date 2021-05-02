using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : BehaviourState
{
    AirSO coreAir => ((IAirEntity)core).airSO;

    private AnimationCurve fallCurve;

    bool longFall = true;

    public override void Enter()
    {
        base.Enter();
        
        if (fallCurve == null)
            SetFall(true);
    }

    public void SetFall(bool _longFall)
    {
        longFall = _longFall;
        fallCurve = (longFall) ? coreAir.longFallCurve : coreAir.shortFallCurve;
    }
    
    public override void FixedDo()
    {
        core.rb.velocity = new Vector2(
            core.rb.velocity.x, 
            Engine.e.maxFallSpeed * fallCurve.Evaluate(Time.time - startTime)
            );

        if (core.spatial.grounded)
            complete = true;
    }
    public override void Exit()
    {
        core.rb.velocity = new Vector2(core.rb.velocity.x, 0.0f);
    }
}
