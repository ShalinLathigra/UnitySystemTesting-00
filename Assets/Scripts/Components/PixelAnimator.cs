using UnityEngine;
using UnityEditor;

using Pixel;


namespace Components
{
    /* Responsibilities:
     * Iterating through a PixelAnimation
     * Setting the active PixelBoxes for the current PixelFrame as returned by PixelAnimation
     */
    public class PixelAnimator : MonoBehaviour
    {
        private bool _fakeAnim;
        private bool fakeAnim => _fakeAnim || anim == null;
        public PixelSheet anim;
        [SerializeField] private SpriteRenderer spriteRenderer;
        public HitBox2D hitBox;
        public HurtBox2D hurtBox;

        public bool playing;

        private float _timeOfLastFrame;
        public int index;
        
        private void Awake()
        {
            _timeOfLastFrame = Time.time;

            if (fakeAnim)
            {
                _fakeAnim = true; 
                anim = ScriptableObject.CreateInstance<PixelSheet>();
                anim.Init("FAKE");
            }
            
            index = anim.startFrame;
            anim.RestartAnimation();
            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Update()
        {
            Debug.Log(anim.name + " " + anim.frames.Count);
            if (!playing) return;
            if (Time.time < _timeOfLastFrame + anim.frameDuration) return;
            if (anim.frames.Count <= 0) return;
            _timeOfLastFrame = Time.time;
            
            anim.NextFrame(out index);
            
            spriteRenderer.sprite = anim.currentFrame.sprite;
            
            HitFrameProperties hit = anim.currentFrame.hitProps;
            HurtFrameProperties hurt = anim.currentFrame.hurtProps;
            
            hitBox.SetCollider(hit.active, hit.shape);
            hurtBox.SetCollider(hurt.active, hurt.shape);
        }

        public void GetBoxInfo(out HitFrameProperties hit, out HurtFrameProperties hurt)
        {
            if (fakeAnim)
            {
                hit = new HitFrameProperties();
                hurt = new HurtFrameProperties();
                return;
            }
            hit = anim.frames[index % anim.frames.Count].hitProps;
            hurt = anim.frames[index % anim.frames.Count].hurtProps;
        }

        public void Play(PixelSheet _sheet)
        {
            anim = _sheet;
            playing = true;
            _fakeAnim = false;
            
            Debug.Log("Play Called! " + anim.name + " " + anim.frames.Count);
        }
    }
    
    //TODO: In Edit mode, work on a RECT, have the box collider updated to use RECT every frame.
    //TODO: Change DrawBox to use rect + colour rather than this
}