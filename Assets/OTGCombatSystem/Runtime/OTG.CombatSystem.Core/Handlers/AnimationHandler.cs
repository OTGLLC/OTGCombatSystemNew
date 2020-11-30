using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

namespace OTG.CombatSM.Core
{
    public class AnimationHandler
    {
        #region Fields
        private AnimationHandlerData m_handlerData;
        private Animator m_animComponent;
        private AnimationClip m_currentClip;
        private PlayableGraph m_playableGraph;
        private AnimationPlayableOutput m_playableOutput;
        private AnimationClipPlayable m_clipPlayable;
        #endregion

        #region Properties
        public OTGAnimationEvent CurrentAnimationEvent { get; private set; }
        #endregion

        #region Public API
        public AnimationHandler(HandlerDataGroup _dataGroup, Animator _animComponent)
        {
            InitializeData(_dataGroup);
            InitializeAnimator(_animComponent);
            InitializePlayableGraph();
        }
        public void PlayAnimation(AnimationClip _clip)
        {
            SetAnimationClip(_clip);
            PlayCurrentAnimation();
        }
        public void PlayAnimationByTime(float _percentCompletion)
        {
            PlayCurrentAnimationByTime(_percentCompletion);
        }
        public void CleanupHandler()
        {
            Cleanup();
        }
        public void UpdateAnimationEvent(OTGAnimationEvent _event)
        {
            CurrentAnimationEvent = _event;
        }
        #endregion


        #region Utility

        private void InitializeData(HandlerDataGroup _data)
        {
            m_handlerData = _data.AnimHandlerData;
        }
        private void InitializeAnimator(Animator _animComponent)
        {
            m_animComponent = _animComponent;
        }
        private void InitializePlayableGraph()
        {
            m_playableGraph = PlayableGraph.Create();
            m_playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            m_playableOutput = AnimationPlayableOutput.Create(m_playableGraph, "CombatAnimation", m_animComponent);
        }
        private void SetAnimationClip(AnimationClip _clip)
        {
            m_currentClip = _clip;
            m_clipPlayable = AnimationClipPlayable.Create(m_playableGraph, _clip);
            m_playableOutput.SetSourcePlayable(m_clipPlayable);

        }
        void PlayCurrentAnimation()
        {
            m_playableGraph.Play();
        }
        private void PlayCurrentAnimationByTime(float _percComplete)
        {
            //m_playableGraph.Stop();
            m_clipPlayable.SetTime((double)_percComplete);
        }
        private void Cleanup()
        {
            m_handlerData = null;
            m_playableGraph.Destroy();
            m_animComponent = null;
        }
        #endregion
    }
}
