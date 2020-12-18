
using System.Collections;
using OTG.Input.Touch;
using OTG.CombatSM.Core;
using UnityEngine;
using System.Text;

namespace OTG.InfiniteRunner
{
    [RequireComponent(typeof(GestureController))]
    public class OTGTouchInputHandler : MonoBehaviour
    {
        #region enum
        
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
        private InfiniteRunnerInput m_infiniteRunnerInput;
        private GestureController m_gestureController;
        private float m_screenMiddleX;

        private StringBuilder m_stringBuild;
        #endregion


        #region Unity API
        private void Start()
        {
            m_stringBuild = new StringBuilder();

            DetermineScreenMiddle();
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
            m_infiniteRunnerInput = GetComponent<OTGCombatSMC>().Handler_Input.RunnerInput;
            m_inputDecay = new WaitForSeconds(m_inputDelay);

        }
        private void CleanupRefs()
        {
            StopAllCoroutines();
            m_gestureController = null;
            m_inputDecay = null;
            m_infiniteRunnerInput = null;
        }
        private void DetermineScreenMiddle()
        {
            float screenWidth = Screen.width;
            m_screenMiddleX = screenWidth / 2;
        }
        #endregion

        #region Input Callbacks
        private void OnSwiped(SwipeInput _input)
        {
            DetermineSwipeDirection(_input);
            //PrintInputInfo();
        }
        private void OnTapped(TapInput _input)
        {
            DetermineTapInput(_input);
            //PrintInputInfo();
        }
        #endregion

        #region Utility
        private void DetermineSwipeDirection(SwipeInput _input)
        {

            if (_input.SwipeDirection.y > 0 && Mathf.Abs(_input.SwipeDirection.x) <= 0.5f)
                StartInputCoroutine(ref m_swipeUpCoroutine,SwipeUp_CO());
            if (_input.SwipeDirection.y < 0 && Mathf.Abs(_input.SwipeDirection.x) <= 0.5f)
                StartInputCoroutine(ref m_swipeDownCoroutine, SwipeDown_CO());
        }
        private void DetermineTapInput(TapInput _input)
        {
            if (_input.ReleasePosition.x > m_screenMiddleX)
                StartInputCoroutine(ref m_tapRightCoroutine, RightTap_CO());
            if (_input.ReleasePosition.x < m_screenMiddleX)
                StartInputCoroutine(ref m_tapLeftCoroutine, LeftTap_CO());
        }
        private void StartInputCoroutine(ref Coroutine _target, IEnumerator _coroutine)
        {
            if (_target != null)
                StopCoroutine(_target);

            _target = StartCoroutine(_coroutine);
            
        }
        private void PrintInputInfo()
        {
            m_stringBuild.Clear();

            m_stringBuild.AppendFormat("SwitchLaneUp: {0}", m_infiniteRunnerInput.HasSwitchLanesUpInput);
            m_stringBuild.AppendLine();
            m_stringBuild.AppendFormat("SwitchLaneDown: {0}", m_infiniteRunnerInput.HasSwitchLanesDownInpu);
            m_stringBuild.AppendLine();
            m_stringBuild.AppendFormat("RightInput: {0}", m_infiniteRunnerInput.HasRightInput);
            m_stringBuild.AppendLine();
            m_stringBuild.AppendFormat("LeftInput: {0}", m_infiniteRunnerInput.HasLeftInput);
            m_stringBuild.AppendLine();

            m_debugText.text = m_stringBuild.ToString();
        }
        #endregion


        #region Coroutines
        private IEnumerator SwipeUp_CO()
        {
            m_infiniteRunnerInput.HasSwitchLanesUpInput = true;
            yield return m_inputDecay;
            m_infiniteRunnerInput.HasSwitchLanesUpInput = false;
        }
        private IEnumerator SwipeDown_CO()
        {
            m_infiniteRunnerInput.HasSwitchLanesDownInpu = true;
            yield return m_inputDecay;
            m_infiniteRunnerInput.HasSwitchLanesDownInpu = false;
        }
        private IEnumerator RightTap_CO()
        {
            m_infiniteRunnerInput.HasRightInput = true;
            yield return m_inputDecay;
            m_infiniteRunnerInput.HasRightInput = false;
        }
        private IEnumerator LeftTap_CO()
        {
            m_infiniteRunnerInput.HasLeftInput = true;
            yield return m_inputDecay;
            m_infiniteRunnerInput.HasLeftInput = false;
        }
        #endregion
    }

}