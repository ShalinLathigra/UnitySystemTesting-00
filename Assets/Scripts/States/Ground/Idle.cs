using BStateMachine;
using Pixel;

namespace States
{
    public class Idle : BehaviourState
    {
        public PixelSheet sheet;

        public override void Enter()
        {
            core.pixel.Play(sheet);
        }
    }
}