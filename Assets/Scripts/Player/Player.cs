using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{


    [Header("Available States")]
    [SerializeField] private GroundMetaState groundMeta;
    [SerializeField] private AirMetaState airMeta;


    protected override void Awake() {
        base.Awake();
        Set(groundMeta);
    }

    private void FixedUpdate() {
        if (input.shouldMove)   
            if (spatial.grounded)
                Set(groundMeta);

        state.FixedDo();
    }

    public override void SetAirState()
    {
            Set(airMeta);
    }
}
