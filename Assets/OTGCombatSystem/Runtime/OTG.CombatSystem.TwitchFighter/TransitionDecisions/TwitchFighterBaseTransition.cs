
using OTG.CombatSM.Core;

namespace OTG.TwitchFighter
{
    public abstract class TwitchFighterBaseTransition : OTGTransitionDecision
    {
        protected virtual void Awake()
        {
            m_combatSystemType = E_Template.TwitchFighter;
        }
    }
}
