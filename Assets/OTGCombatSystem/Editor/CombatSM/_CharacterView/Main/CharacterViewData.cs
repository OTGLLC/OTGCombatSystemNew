
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using OTG.CombatSM.Core;

namespace OTG.CombatSM.EditorTools
{
    public class CharacterViewData
    {
        #region Properties
        public List<OTGCombatSMC> CharactersInScene { get; private set; }

        #endregion

        #region Public API
        public CharacterViewData()
        {
            CharactersInScene = new List<OTGCombatSMC>();
        }
        public void GetAllCharactersInScene()
        {
            OTGCombatSMC[] chars = Object.FindObjectsOfType<OTGCombatSMC>();

            for(int i = 0; i < chars.Length; i++)
            {
                if (!CharactersInScene.Contains(chars[i]))
                    CharactersInScene.Add(chars[i]);
            }

            
        }
        #endregion
    }

}
