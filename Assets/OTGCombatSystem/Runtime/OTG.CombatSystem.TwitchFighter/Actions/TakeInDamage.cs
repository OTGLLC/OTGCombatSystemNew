
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM.TwitchFighter
{
    public class TakeInDamage : TwitchFighterBaseAction
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
            CollisionHandler collHandler = _controller.Handler_Collision;
            AnimationHandler animHandler = _controller.Handler_Animation;

            IDamagePayload incDamage = collHandler.HitCollider.CurrendPayload;
            collHandler.HitCollider.HasRecievedDamage = false;
            twitchCombat.RecieveDamagePayload(incDamage);
            animHandler.RecieveDamagePayload(incDamage);
            twitch.RecieveDamagePayload(incDamage);
        }
    }
}