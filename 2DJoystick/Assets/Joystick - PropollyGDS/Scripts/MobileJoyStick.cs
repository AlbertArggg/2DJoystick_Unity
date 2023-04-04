using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JoystickInput
{
    public class MobileJoyStick : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
    {
        private RectTransform joyStickTransform;
        [SerializeField] private float dragThreshold = 0.6f;        // minimum drag distance required to register joystick input
        [SerializeField] private int dragMovementDistance = 30;     // maximum distance that the joystick can be moved from its center
        [SerializeField] private int dragOffsetDistance = 100;      // maximum distance that the joystick can be dragged from its initial position
        [SerializeField] private GameObject player;                 // reference to the player object
        public event Action<Vector2> OnMove;                        // event that is triggered when the joystick is moved
        [HideInInspector] public Vector2 offsetActual;              // current joystick offset

        private void Awake() { joyStickTransform = (RectTransform)transform; }

        // If player is not assigned in the inspector, try to find it based on the "Player" tag
        private void Start() { if (player == null) { player = GameObject.FindGameObjectWithTag("Player"); } }

        // Call MovePlayerFromJoyStickInput in the PlayerController script with the current joystick offset as input
        private void Update() { player.GetComponent<PlayerController>().MovePlayerFromJoyStickInput(offsetActual); }

        public void OnDrag(PointerEventData eventData)
        {
            // Calculate the offset of the joystick based on the touch/mouse position
            Vector2 offset;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(joyStickTransform, eventData.position, null, out offset);

            // Clamp the offset to the maximum drag offset distance and scale it to the maximum drag movement distance
            offset = Vector2.ClampMagnitude(offset, dragOffsetDistance) / dragOffsetDistance;
            joyStickTransform.anchoredPosition = offset * dragMovementDistance;

            // Calculate the movement input vector based on the joystick offset and invoke the OnMove event
            Vector2 inputVector = CalculateMovementInput(offset);
            OnMove?.Invoke(inputVector);
            offsetActual = offset;
        }

        // Determine the x and y components of the movement input vector based on the joystick offset
        private Vector2 CalculateMovementInput(Vector2 offset)
        {
            float x = Mathf.Abs(offset.x) > dragThreshold ? offset.x : 0;
            float y = Mathf.Abs(offset.y) > dragThreshold ? offset.y : 0;
            offsetActual = new Vector2(x, y);
            return offsetActual;
        }

        // Reset the joystick offset when the pointer is pressed down
        public void OnPointerDown(PointerEventData eventData) { offsetActual = Vector2.zero; }

        // Reset the joystick position and invoke the OnMove event with a zero vector when the pointer is released
        public void OnPointerUp(PointerEventData eventData)
        {
            joyStickTransform.anchoredPosition = Vector2.zero;
            OnMove?.Invoke(Vector2.zero);
            offsetActual = Vector2.zero;
        }
    }
}