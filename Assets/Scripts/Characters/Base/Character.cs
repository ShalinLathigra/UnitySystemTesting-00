using Components;
using UnityEngine;

using BehaviourStateTree;
using Pixel;

namespace Characters.Base
{
    [RequireComponent(typeof(SpatialAwareness))]
    public abstract class Character : StateTree
    {

        [SerializeField] protected PixelBoxAnimator pixelAnimator;
        [SerializeField] protected SquashAnimator squashAnimator;
        [SerializeField] protected Life life;
        public SpatialAwareness spatial { get; private set; }
        public new AudioClipPlayer audio { get; private set; }

        public PixelBoxAnimator pixel => pixelAnimator;
        public SquashAnimator squash => squashAnimator;
        public BoxCollider2D box => spatial.box;
        public Rigidbody2D rb => spatial.rb;
        public virtual bool blocked { get; set; }

        protected virtual void Awake() {
            // Cache Core components

            // Update Core for child states
            StateBranch[] childstates = GetComponentsInChildren<StateBranch>();
            foreach (StateBranch c in childstates) {
                c.SetCharacter(this);
            }
            
            spatial = GetComponent<SpatialAwareness>();
            audio = GetComponentInChildren<AudioClipPlayer>();
        }

        protected virtual void FixedUpdate() { state?.FixedDo(); }
        protected virtual void Update() { state?.Do(); }
        protected virtual void LateUpdate() { state?.LateDo(); }
    

        public virtual void SetAirState(){ Debug.Log(name + " SetAirState");}
        public virtual void SetGroundState(){ Debug.Log(name + " SetGroundState"); }
        public virtual void SetCombatState(){ Debug.Log(name + " SetCombatState"); }
    }
}