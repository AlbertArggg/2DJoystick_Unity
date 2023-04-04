using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JoystickInput
{
    public class PlayerMobileInput : MonoBehaviour, IAgentInput
    {
        public Vector2 MovementVector { get; private set; }         // the current movement vector of the player

        public event Action<Vector2> OnMovement;                    // event that is triggered when the player moves

        [SerializeField] private MobileJoyStick joyStick;           // reference to the MobileJoyStick component

        private void FixedUpdate() { joyStick.OnMove += Move; }     // Subscribe to the OnMove event of the MobileJoyStick component

        private void Move(Vector2 input)                            // Update the MovementVector property and invoke the OnMovement event
        {
            MovementVector = input;
            OnMovement?.Invoke(MovementVector);
        }
    }
}


