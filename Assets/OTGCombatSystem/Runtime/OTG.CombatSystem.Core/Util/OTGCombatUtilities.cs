


namespace OTG.CombatSM.Core
{
    public enum E_NewCombatStateTemplate
    {
        Attack,
        Idle,
        Dash,
        HitStun,
        HitStop,
        Knockdown,
        KnockBack
    }


    public static class OTGCombatUtilities
    {
       
        public const string OTGRoot = "OTG/";
        public const string CombatStateMachinePath = OTGRoot + "CombatStateMachine/";
        public const string CombatStatePath = CombatStateMachinePath + "CombatState";
        public const string DataGroupPath = CombatStateMachinePath + "DataGroups/";

        public const string CombatActionPath = CombatStateMachinePath + "Actions/";
        public const string CombatTransitionDecisionPath = CombatStateMachinePath + "TransitionDecisions/";
    }
}