
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.InfiniteRunner
{
    public class PassThrough : OTGTransitionDecision
    {
        public override bool Decide(OTGCombatSMC _controller)
        {
            return true;
        }
    }

}
