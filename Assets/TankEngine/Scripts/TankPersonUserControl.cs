using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(TankPersonCharacter))]
    public class TankPersonUserControl : MonoBehaviour
    {

        private TankPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        [SerializeField] private Vector3 Forward;
        [SerializeField] private Vector3 Right;

        private void Start()
        {
            m_Character = GetComponent<TankPersonCharacter>();
        }

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // Read inputs
            float horizontalMove = CrossPlatformInputManager.GetAxis("Horizontal");
            float verticalMove = CrossPlatformInputManager.GetAxis("Vertical");
            m_Character.Move(horizontalMove, verticalMove);
        }
    }
}
