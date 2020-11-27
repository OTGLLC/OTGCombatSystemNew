
using UnityEngine;
using OTG.CombatSM.Core;


namespace OTG.CombatSM.Concrete
{
    public class PassThrough : OTGTransitionDecision
    {
        private void Awake()
        {
            m_transitionDecisionType = E_TransitionDecisionType.Misc;
        }
        public override bool Decide(OTGCombatSMC _controller)
        {
            return true;
        }
    }
}