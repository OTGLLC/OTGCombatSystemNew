using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.CombatSM
{
    [CreateAssetMenu(menuName = OTGCombatUtilities.CombatActionPath+"TestActionOne", fileName ="TestActionOne")]
   public class TestActionOne : OTGCombatAction
    {
        public override void Act(OTGCombatSMC _controller)
        {
            Debug.Log("This is test Action one");
        }
    }
}
