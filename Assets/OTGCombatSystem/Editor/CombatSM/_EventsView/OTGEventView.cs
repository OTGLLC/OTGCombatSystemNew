
using UnityEditor;
using UnityEngine;


namespace OTG.CombatSM.EditorTools
{
    public class OTGEventView : CombatSMBaseView
    {


        #region abstract class implemenetations

      
        protected override void SetStrings()
        {
            m_stylePath = CombatSMStrings.Styles.EventView;
            m_templatePath = CombatSMStrings.Templates.EventView;
            ContainerStyleName = CombatSMStrings.StyleContainerName.EventView;
        }
        protected override void Refresh()
        {

        }
        protected override void HandleOnProjectUpdate()
        {
           
        }
        protected override void HandleOnViewFocused()
        {
            Debug.Log("Event view focused");
        }
        protected override void HandleViewLostFocus()
        {
            
        }
        protected override void HandleOnHierarchyChanged()
        {

        }
        #endregion
    }

}
