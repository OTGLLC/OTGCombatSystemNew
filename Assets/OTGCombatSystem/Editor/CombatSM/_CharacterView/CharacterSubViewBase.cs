
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.EditorTools
{
    public abstract class CharacterSubViewBase : CombatSMBaseView
    {
        protected CharacterViewData m_charViewData;
        public CharacterSubViewBase(CharacterViewData _charViewData) : base() { m_charViewData = _charViewData; }
        public void OnCharacterSelected()
        {
            HandleCharacterSelection();
        }

        protected abstract void HandleCharacterSelection();
    }

}
