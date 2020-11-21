

using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM
{
    [CreateAssetMenu(menuName =OTGCombatUtilities.CombatActionPath+"TestActionTwo",fileName ="TestActionTwo")]
    public class TestActionTwo : OTGCombatAction
    {
        public override void Act(OTGCombatSMC _controller)
        {
            Debug.Log("This is Action Two");
        }
    }
}
