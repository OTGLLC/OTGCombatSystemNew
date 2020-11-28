
using UnityEngine;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.Concrete
{
    public class HasLeftOrRightInput : OTGTransitionDecision
    {
        private void Awake()
        {
            m_transitionDecisionType = E_TransitionDecisionType.Input;
        }
        public override bool Decide(OTGCombatSMC _controller)
        {
            TwitchFighterInput twitchInput = _controller.Handler_Input.TwitchInput;

            if (twitchInput.HasRightInput || twitchInput.HasLeftInput)
                return true;
            else
                return false;
        }
    }
}
