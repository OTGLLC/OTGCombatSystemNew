using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace OTG.CombatSM.EditorTools
{
    public class CombatantView : CombatantBaseView
    {
        
        protected override void HandleOnProjectUpdate()
        {
            
        }

        protected override void HandleSelection(CombatantViewData _data)
        {
           
        }

        protected override void SetStrings()
        {
            m_templatePath = CombatSMStrings.Templates.CharacterView;
            m_stylePath = CombatSMStrings.Styles.CharacterView;
            ContainerStyleName = CombatSMStrings.StyleContainerName.CharacterView;
        }
    }

}
