using System.Collections;
using UnityEngine;

namespace OTG.CombatSM.Core
{
    public class KeyboardInputController : MonoBehaviour
    {
        private InputHandler m_inputHandler;


        private void Start()
        {
            m_inputHandler = GetComponent<OTGCombatSMC>().Handler_Input;
        }

        private void Update()
        {
            Vector2 inputVector = new Vector2();
            float xInput = 0;
            float yinput = 0;
            if (Input.GetKey(KeyCode.D))
                xInput = 1;
            if (Input.GetKey(KeyCode.A))
                xInput = -1;
            if (Input.GetKeyDown(KeyCode.W))
                yinput = 1;
            if (Input.GetKeyDown(KeyCode.S))
                yinput = -1;

            inputVector = new Vector2(xInput, yinput);
            m_inputHandler.MovementVector = inputVector;
                

        }

    }
}