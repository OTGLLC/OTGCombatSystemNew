


using OTG.CombatSM.Core;
using System.Net.Http.Headers;
using UnityEngine;

namespace OTG.SidescrollCombatSystem
{
    [CreateAssetMenu(menuName =SideScrollStrings.TransitionsPath+"PassThrough",fileName ="PassThrough")]
    public class PassThroughTransition: OTGTransitionDecision
    {
        public override bool Decide(OTGCombatSMC _controller)
        {
            return true;
        }
    }
}