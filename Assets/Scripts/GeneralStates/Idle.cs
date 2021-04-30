using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BehaviourState
{
    public override void Enter()
    {
        complete = false;
    }
}
