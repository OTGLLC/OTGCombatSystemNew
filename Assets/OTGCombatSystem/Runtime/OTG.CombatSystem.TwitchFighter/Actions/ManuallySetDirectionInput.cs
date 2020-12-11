

using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.TwitchFighter
{
    public class ManuallySetDirectionInput : TwitchFighterBaseAction
    {
       
        protected override void Awake()
        {
            m_combatActionType = E_ActionType.MovementBased;
            m_processType = E_ProcessType.SetParameter;
            base.Awake();
        }
        public override void Act(OTGCombatSMC _controller)
        {
            TwitchMovementParams twitchMove = _controller.Handler_Movement.TwitchParams;

            TwitchFighterInput twitchInput = _controller.Handler_Input.TwitchInput;

            TwitchFighterInputController inputCtrl = _controller.GetComponent<TwitchFighterInputController>();

            bool facingLeft = twitchMove.Comp_Transform.rotation.eulerAngles.y == twitchMove.GlobalCombatConfig.FacingLeftRotation;
            bool facingRight = twitchMove.Comp_Transform.rotation.eulerAngles.y == twitchMove.GlobalCombatConfig.FacingRightRotation;

           
        }
    }
}
