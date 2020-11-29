
using OTG.CombatSM.Core;

namespace OTG.CombatSM.TwitchFighter
{
    public abstract class TwitchFighterBaseTransition : OTGTransitionDecision
    {
        protected virtual void Awake()
        {
            m_combatSystemType = E_Template.TwitchFighter;
        }
    }
}
