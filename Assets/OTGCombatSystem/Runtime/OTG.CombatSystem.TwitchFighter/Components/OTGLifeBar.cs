
using UnityEngine.UI;
using UnityEngine;
using OTG.CombatSM.Core;

namespace OTG.TwitchFighter
{
    public class OTGLifeBar : MonoBehaviour
    {
        #region Inspector Vars
        private Slider m_healthSlider;
        private TwitchFighterCombatParams m_twitchCombat;
        #endregion

        #region Unity API
        private void OnEnable()
        {
            Initialize();
        }
        private void Start()
        {
            m_twitchCombat = GetComponentInParent<OTGCombatSMC>().Handler_Combat.TwitchCombat;
            m_twitchCombat.HealthUpdateEvent += OnHealthUpdate;
        }
        private void OnDisable()
        {
            Cleanup();
        }
        #endregion

        #region Public API
        public void OnHealthUpdate(int _currentHelth, int _maxHealth)
        {
            float percentRemaining = ((float)_currentHelth / (float)_maxHealth);
            m_healthSlider.value = percentRemaining;
        }
        #endregion

        #region Utility
        private void Initialize()
        {
            m_healthSlider = GetComponent<Slider>();
        }
        private void Cleanup()
        {
            m_twitchCombat.HealthUpdateEvent -= OnHealthUpdate;
            m_twitchCombat = null;
            m_healthSlider = null;
        }
        #endregion
    }

}
