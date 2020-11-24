
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.EditorTools
{
    public abstract class CharacterSubViewBase : CombatSMBaseView
    {
        protected CharacterViewData m_charViewData;
        protected EditorConfig m_editorConfig;
        public CharacterSubViewBase(CharacterViewData _charViewData, EditorConfig _editorConfig) : base() { m_charViewData = _charViewData; m_editorConfig = _editorConfig; }
        public void OnCharacterSelected()
        {
            HandleCharacterSelection();
        }

        protected abstract void HandleCharacterSelection();
    }

}
