using Characters.Base;
using UnityEngine;

namespace BStateMachine
{
    public abstract class BehaviourState : BehaviourStateMachine
    {
        [HideInInspector] public Character core;
        [SerializeField] protected AudioClipSO audioSO;
        public virtual bool complete { get; set; }
        public virtual float startTime { get; set; }
        public virtual void Enter(){ ClearState(false); startTime = Time.time; }
        public virtual void Do(){ state?.Do(); }
        public virtual void FixedDo(){ state?.FixedDo(); }
        public virtual void LateDo(){ state?.LateDo(); }
        public virtual void Exit(){ state?.Exit(); ClearState(true); }
        public virtual void Finish(){ state?.Finish(); ClearState(true); }

        protected void ClearState(bool _complete) { complete = _complete; state = null; }

        public void SetCharacter(Character cr) => core = cr;
    }
}
