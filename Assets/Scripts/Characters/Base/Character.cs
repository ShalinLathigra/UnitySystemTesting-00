using Components;
using UnityEngine;

using BStateMachine;

namespace Characters.Base
{
    [RequireComponent(typeof(SpatialAwareness))]
    public abstract class Character : BehaviourStateMachine
    {

        [SerializeField] protected PixelAnimator pixelAnimator;
        [SerializeField] protected InputWrapper inputWrapper;
        public SpatialAwareness spatial { get; private set; }
        public new AudioClipPlayer audio { get; private set; }

        public InputWrapper input => inputWrapper;
        public PixelAnimator pixel => pixelAnimator;
        public BoxCollider2D box => spatial.box;
        public Rigidbody2D rb => spatial.rb;

        protected virtual void Awake() {
            // Cache Core components
            spatial = GetComponent<SpatialAwareness>();
            audio = GetComponentInChildren<AudioClipPlayer>();

            // Update Core for child states
            BehaviourState[] childstates = GetComponentsInChildren<BehaviourState>();
            foreach (BehaviourState c in childstates) {
                c.SetCharacter(this);
            }    
        }

        protected virtual void FixedUpdate() { state?.FixedDo(); }
        protected virtual void Update() { state?.Do(); }
        protected virtual void LateUpdate() { state?.LateDo(); }
    

        public virtual void SetAirState(){ Debug.Log(name + " SetAirState");}
        public virtual void SetGroundState(){ Debug.Log(name + " SetGroundState"); }
        public virtual void SetCombatState(){ Debug.Log(name + " SetCombatState"); }
    }
}