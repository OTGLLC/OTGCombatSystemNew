
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.EditorTools
{
    public abstract class CharacterSubViewBase : CombatSMBaseView
    {

        public CharacterSubViewBase() : base() { }
        public void OnCharacterSelected(OTGCombatSMC _selectedCharacter)
        {
            HandleCharacterSelection(_selectedCharacter);
        }

        protected abstract void HandleCharacterSelection(OTGCombatSMC _selectedCharacter);
    }

}
