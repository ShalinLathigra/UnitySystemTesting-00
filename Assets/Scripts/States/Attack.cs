using BehaviourStateTree;
using Pixel;
using UnityEngine;

namespace States
{
    public class Attack : StateBranch
    {
        /* By default, an attack is just a single sheet that plays out and returns control when the sheet completes
         * 
         */

        [SerializeField] private PixelSheet sheet;
        [SerializeField] private int minSkipIndex;
        [SerializeField] private int maxSkipIndex;

        public bool canSkip => (core.pixel.PlayingSheet(sheet) && core.pixel.currentIndex >= minSkipIndex && core.pixel.currentIndex < maxSkipIndex);

        private void Awake()
        {
            minSkipIndex = Mathf.Min(minSkipIndex, sheet.frameCount - 1);   
            maxSkipIndex = Mathf.Min(maxSkipIndex, sheet.frameCount - 1);   
        }

        public override void Enter()
        {
            base.Enter();
            core.pixel.Play(sheet, true);
            core.blocked = true;
            core.pixel.pixelComplete += Exit;
        }

        public override void Exit()
        {
            core.pixel.pixelComplete -= Exit;
            core.blocked = false;
            base.Exit();
        }
    }
}
