using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourState : BehaviourStateMachine, IBehaviourState
{
    [HideInInspector] public Character core;
    public virtual bool complete { get; set; }
    public virtual float startTime { get; set; }
    public virtual void Enter(){}
    public virtual void Do(){}
    public virtual void FixedDo(){}
    public virtual void Exit(){}
    public virtual void Finish(){}

    public void SetCharacter(Character cr) => core = cr;
}

public interface IBehaviourState
{
    bool complete { get; set; }
    float startTime { get; set; }

    void Enter();
    void Do();
    void FixedDo();
    void Exit();
    void Finish();
}
