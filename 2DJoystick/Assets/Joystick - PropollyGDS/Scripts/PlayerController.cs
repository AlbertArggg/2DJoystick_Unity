using UnityEngine;

namespace JoystickInput
{
    public class PlayerController : MonoBehaviour
    {
        // Required Objects
        [Header("Required Objects")]
        public MobileJoyStick joystickInput;    // reference to the joystick input object
        public Transform playerObject;          // reference to the player object transform
        public GameObject playerSprite;         // reference to the player sprite object

        // Player Data
        [Header("Player Data")]
        public float speed = 9;                 // player movement speed
        public float maxRotationAngle = 45;     // maximum angle of rotation for the player sprite
        public float returnRotationSpeed = 3;   // speed at which the player sprite returns to its original rotation

        // Customize Player Behaviour
        [Header("Customize Player Behaviour")]
        public bool ReturnToOriginalRotation;   // whether the player sprite should return to its original rotation when there is no input
        public bool freezePosX;                 // whether the player object's x-axis movement is frozen
        public bool freezePosY;                 // whether the player object's y-axis movement is frozen
        public bool DirectionalRotationX;       // whether the player sprite should rotate based on the x-axis input
        public bool DirectionalRotationY;       // whether the player sprite should rotate based on the y-axis input

        private bool reset = false;             // flag indicating whether the player sprite needs to be reset to its original rotation


        public void Update()
        {
            // If ReturnToOriginalRotation is true and there is no input, rotate the player sprite back to its original orientation
            if (reset && ReturnToOriginalRotation)
            {
                Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
                playerSprite.transform.rotation = Quaternion.Lerp(playerSprite.transform.rotation, targetRotation, Time.deltaTime * returnRotationSpeed);
            }
        }

        public void MovePlayerFromJoyStickInput(Vector2 offset)
        {
            // Set the reset flag based on whether there is any input
            reset = offset.magnitude > 0.1f ? false : true;

            // Freeze movement on the x-axis or y-axis if freezePosX or freezePosY is true
            if (freezePosY || Mathf.Abs(offset.y) < 0.1f) { offset.y *= 0; }
            if (freezePosX || Mathf.Abs(offset.x) < 0.1f) { offset.x *= 0; }

            // Rotate the player sprite based on the x-axis and/or y-axis input
            if (DirectionalRotationX || DirectionalRotationY)
            {
                if (offset.magnitude > 0.1f)
                {
                    float angle = (Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg) - 90;
                    if (!DirectionalRotationY) { angle = Mathf.Clamp(angle, -maxRotationAngle, maxRotationAngle); }
                    playerSprite.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
            }

            // Move the player object based on the joystick input and speed
            playerObject.Translate(offset * speed * Time.deltaTime);
        }
    }
}