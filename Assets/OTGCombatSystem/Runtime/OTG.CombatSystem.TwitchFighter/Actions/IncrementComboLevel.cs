


using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{
    public class IncrementComboLevel : TwitchFighterBaseAction
    {
        protected override void Awake()
        {
            m_combatActionType = E_ActionType.MovementBased;
            m_processType = E_ProcessType.SetParameter;
            base.Awake();
        }
        public override void Act(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitch = _controller.Handler_Movement.TwitchParams;
            TwitchFighterCombatParams twitchCombat = _controller.Handler_Combat.TwitchCombat;
            TwitchFighterInput twitchInput = _controller.Handler_Input.TwitchInput;

            
            twitchCombat.ComboLevel++;
            twitchCombat.ComboLevel = Mathf.Clamp(twitchCombat.ComboLevel, 0, twitchCombat.Data.MaxComboLevel);
        }
    }
}