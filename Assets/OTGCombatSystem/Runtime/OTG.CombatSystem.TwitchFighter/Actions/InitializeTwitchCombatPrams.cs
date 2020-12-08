﻿
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM.TwitchFighter
{
    public class InitializeTwitchCombatPrams : TwitchFighterBaseAction
    {
        protected override void Awake()
        {
            m_combatActionType = E_ActionType.MovementBased;
            m_processType = E_ProcessType.SetParameter;
            base.Awake();
        }
        public override void Act(OTGCombatSMC _controller)
        {
            
            TwitchFighterCombatParams twitchCombat = _controller.Handler_Combat.TwitchCombat;
            
            

            InitializeLevel(twitchCombat);
            InitializeHealth(twitchCombat);
            InitializeEnergy(twitchCombat);
            InitializePhysicalAttack(twitchCombat);
            InitializeEnergyAttack(twitchCombat);
            InitializeComboBreakPoint(twitchCombat);
        }
        private void InitializeLevel(TwitchFighterCombatParams _twitchCombat)
        {
            _twitchCombat.CombatLevel = 1;
        }
        private void InitializeHealth(TwitchFighterCombatParams _twitchCombat)
        {
            _twitchCombat.CurrentHealth = _twitchCombat.Data.MaxHealth;
        }
        private void InitializeEnergy(TwitchFighterCombatParams _twitchCombat)
        {
            _twitchCombat.CurrentEnergy = _twitchCombat.Data.MaxEnergy;
        }
        private void InitializePhysicalAttack(TwitchFighterCombatParams _twitchCombat)
        {
            _twitchCombat.CurrentPhysicalAttack = _twitchCombat.Data.BasePhysicalAttack;
        }
        private void InitializeEnergyAttack(TwitchFighterCombatParams _twitchCombat)
        {
            _twitchCombat.CurrentEnergyAttack = _twitchCombat.Data.BaseEnergyAttack;
        }
        private void InitializeComboBreakPoint(TwitchFighterCombatParams _twitchCombat)
        {
            _twitchCombat.CurrentComboBreakPoint = _twitchCombat.Data.ComboBreakPoint;
        }
       
    }
}
