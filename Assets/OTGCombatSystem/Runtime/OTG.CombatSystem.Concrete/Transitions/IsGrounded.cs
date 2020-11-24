
using UnityEngine;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.Concrete
{
   
    public class IsGrounded : OTGTransitionDecision
    {
        private void Awake()
        {
            m_transitionDecisionType = E_TransitionDecisionType.Status;
        }
        public override bool Decide(OTGCombatSMC _controller)
        {
            return true;
        }

    }
}