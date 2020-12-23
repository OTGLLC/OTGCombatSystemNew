


using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.InfiniteRunner
{
    public class HasSwipeUpInput : OTGTransitionDecision
    {
        public override bool Decide(OTGCombatSMC _controller)
        {
            //AnimationHandler animHandler = _controller.Handler_Animation;
            InfiniteRunnerInput infInput = _controller.Handler_Input.RunnerInput;

            return ( infInput.HasSwitchLanesUpInput);
        }
    }

}
