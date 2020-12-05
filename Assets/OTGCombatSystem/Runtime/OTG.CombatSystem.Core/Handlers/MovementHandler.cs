
using UnityEngine;

namespace OTG.CombatSM.Core
{
    public class MovementHandler
    {
        
        #region Properties
        public TwitchMovementParams TwitchParams { get; private set; }
        #endregion

        #region Public API
        public MovementHandler(HandlerDataGroup _dataGroup, CharacterController _charControl, Transform _trans)
        {
            TwitchParams = new TwitchMovementParams(_trans,_charControl, _dataGroup.MoveHandlerData);
        }
        public void CleanupHandler()
        {
            Cleanup();
        }
        

        #endregion

        #region Utility
        private void Cleanup()
        {
            TwitchParams.Cleanup();
            TwitchParams = null;
        }

        #endregion
    }
    public class TwitchMovementParams
    {
        public Transform Comp_Transform { get; private set; }
        public CharacterController Comp_CharacterControl { get; private set; }
        public TwitchMovementData Data { get; private set; }

        public Vector3 DashStartPosition;
        public float DesiredDashDistance;
        public float CurrentDashDistance;
        public float DesiredDashSpeed;
        public float PercentageDistanceTraveled;
      
        public float HorizontalSpeed;
        public float VerticalSpeed;
        public float DepthSpeed;
        
        public TwitchMovementParams(Transform _trans, CharacterController _control, MovementHandlerData _data)
        {
            Comp_Transform = _trans;
            Comp_CharacterControl = _control;
            Data = _data.TwitchData;
        }
        public void Cleanup()
        {
            Comp_Transform = null;
            Comp_CharacterControl = null;
            Data = null;

            ResetParams();
        }
        public void ResetParams()
        {
            DashStartPosition = Vector3.zero;
            DesiredDashSpeed = 0;
            DesiredDashDistance = 0;
            CurrentDashDistance = 0;
            HorizontalSpeed = 0;
            VerticalSpeed = 0;
            DepthSpeed = 0;
        }
        public void RecieveDamagePayload(IDamagePayload _payload)
        {
            Vector3 impactForce = _payload.GetImpactForce();
            HorizontalSpeed = impactForce.x;
            VerticalSpeed = impactForce.y;
            DepthSpeed = impactForce.z;
            Debug.Log("Impact force " + impactForce);
        }
    }
}