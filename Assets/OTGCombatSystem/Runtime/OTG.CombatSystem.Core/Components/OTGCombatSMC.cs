#pragma warning disable CS0649


using UnityEngine;

namespace OTG.CombatSM.Core
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    public class OTGCombatSMC : MonoBehaviour
    {
        #region Inspector Vars
        [SerializeField] private HandlerDataGroup m_handlerDataGroup;
        [SerializeField] private OTGCombatState m_startingState;
        [SerializeField] private e_CombatantType m_combatantType;
        #endregion

        #region Fields
        [HideInInspector]
        [SerializeField]
        private OTGCombatState m_currentState;

        private OTGCombatState m_previousState;
        
        #endregion

        #region Handlers
        public AnimationHandler Handler_Animation { get; private set; }
        public MovementHandler Handler_Movement { get; private set; }
        public InputHandler Handler_Input { get; private set; }
        public CollisionHandler Handler_Collision { get; private set; }
        public CombatHandler Handler_Combat { get; private set; }
        public VFXHandler Handler_VFX { get; private set; }
        #endregion

        #region Unity API
        private void OnEnable()
        {
            InitializeHandlers();
            //OTGCombatantManager.SubscribeToCombatantUpdateLoop(this);
        }
        private void Start()
        {
            ChangeState(m_startingState);
        }
        private void Update()
        {
            m_currentState.OnStateUpdate(this);
      
        }
        private void OnAnimatorMove()
        {
            m_currentState.OnStateAnimUpdate(this);
        }
        private void OnDisable()
        {
            CleanupHandlers();
            //OTGCombatantManager.UnsubscribeFromCombatantUpdateLoop(this);
        }
        #endregion

        #region Public API
        public void OnChangeStateRequested(OTGCombatState _newState, bool _usePrevious = false)
        {
            ChangeState(_newState, _usePrevious);
        }
        public void UpdateCombatant()
        {
            m_currentState.OnStateUpdate(this);
        }
        public void OnAnimationEvent(OTGAnimationEvent _event)
        {
            Handler_Animation.UpdateAnimationEvent(_event);
            Handler_VFX.OnAnimationEvent(_event);
        }
        #endregion

        #region Utility
 
        private void ChangeState(OTGCombatState _newState, bool _usePrevious = false)
        {
            if(_usePrevious && m_previousState != null && m_previousState.ID != m_currentState.ID)
            {

                Debug.Log("Changing to previous state");

                m_currentState.OnStateExit(this);
                m_currentState = m_previousState;
                m_currentState.OnStateEnter(this);
                m_previousState = null;

                return;
            }


            if (m_currentState != null)
                m_currentState.OnStateExit(this);

            m_previousState = m_currentState;
            m_currentState = _newState;
            m_currentState.OnStateEnter(this);
            
        }
        private void InitializeHandlers()
        {
            Handler_Animation = new AnimationHandler(m_handlerDataGroup, GetComponent<Animator>());
            Handler_Movement = new MovementHandler(m_handlerDataGroup, GetComponent<CharacterController>(), GetComponent<Transform>());
            Handler_Input = new InputHandler(m_handlerDataGroup);
            Handler_Collision = new CollisionHandler(m_handlerDataGroup);
            Handler_Combat = new CombatHandler(m_handlerDataGroup);
            Handler_VFX = new VFXHandler(GetComponentsInChildren<OTGVFXController>());
        }
        private void CleanupHandlers()
        {
            Handler_Animation.CleanupHandler();
            Handler_Animation = null;

            Handler_Movement.CleanupHandler();
            Handler_Movement = null;

            Handler_Input.CleanupHandler();
            Handler_Input = null;

            Handler_Collision.CleanupHandler();
            Handler_Collision = null;

            Handler_Combat.CleanupHandler();
            Handler_Combat = null;

            Handler_VFX.CleanupHandler();
            Handler_VFX = null;
        }
        #endregion
    }
    public enum e_CombatantType
    {
        Player,
        Enemy,
        Prop,
        None
    }
}
