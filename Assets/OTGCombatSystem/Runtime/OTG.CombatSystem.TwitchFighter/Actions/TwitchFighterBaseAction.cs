
using OTG.CombatSM.Core;


namespace OTG.CombatSM.TwitchFighter
{
    public abstract class TwitchFighterBaseAction : OTGCombatAction
    {
        protected virtual void Awake()
        {
            m_SystemTemplate = E_Template.TwitchFighter;
        }

    }
}