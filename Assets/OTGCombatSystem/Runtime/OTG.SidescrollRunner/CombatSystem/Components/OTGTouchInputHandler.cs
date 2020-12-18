
using OTG.Input.Touch;
using OTG.CombatSM.Core;
using UnityEngine;

namespace OTG.InfiniteRunner
{
    [RequireComponent(typeof(GestureController))]
    public class OTGTouchInputHandler : MonoBehaviour
    {
        #region enum
        private enum SwipeDirection
        {
            SwipeUp,
            SwipeDown,
            none
        }
        #endregion

        #region Inspector Vars

        [SerializeField] private float m_inputDelay;
        [SerializeField] private UnityEngine.UI.Text m_debugText;
        #endregion

        #region Coroutines
        private Coroutine m_tapLeftCoroutine;
        private Coroutine m_tapRightCoroutine;
        private Coroutine m_swipeUpCoroutine;
        private Coroutine m_swipeDownCoroutine;
        private WaitForSeconds m_inputDecay;
        private InputHandler m_inputHandler;
        private GestureController m_gestureController;
        #endregion


        #region Unity API
        private void Start()
        {
            InitializeRefs();
            SubscribeToGestureController();
        }
        private void OnDisable()
        {
            UnsubscribeFromGestureController();
            CleanupRefs();
        }
        #endregion

        #region Utility
        private void SubscribeToGestureController()
        {
            m_gestureController.Tapped += OnTapped;
            m_gestureController.Swiped += OnSwiped;
        }
        private void UnsubscribeFromGestureController()
        {
            m_gestureController.Tapped -= OnTapped;
            m_gestureController.Swiped -= OnSwiped;
        }
        private void InitializeRefs()
        {
            m_gestureController = GetComponent<GestureController>();
            m_inputHandler = GetComponent<OTGCombatSMC>().Handler_Input;
            m_inputDecay = new WaitForSeconds(m_inputDelay);

        }
        private void CleanupRefs()
        {
            StopAllCoroutines();
            m_gestureController = null;
            m_inputDecay = null;
            m_inputHandler = null;
        }
        #endregion

        #region Input Callbacks
        private void OnSwiped(SwipeInput _input)
        {
            m_debugText.text = DetermineSwipeDirection(_input).ToString();
        }
        private void OnTapped(TapInput _input)
        {
           m_debugText.text = "Tapped";
        }
        #endregion

        #region Utility
        private SwipeDirection DetermineSwipeDirection(SwipeInput _input)
        {
            SwipeDirection dir = SwipeDirection.none;
            if (_input.SwipeDirection.y > 0)
                dir = SwipeDirection.SwipeUp;
            if (_input.SwipeDirection.y < 0)
                dir = SwipeDirection.SwipeDown;
           
            return dir;
        }
        #endregion


        #region Debuging

        #endregion
    }

}