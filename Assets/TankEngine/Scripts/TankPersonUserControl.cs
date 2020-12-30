using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace TankEngine.Scripts
{
    [RequireComponent(typeof(TankPersonCharacter))]
    public class TankPersonUserControl : MonoBehaviour
    {

        [SerializeField] public BoxCollider InteractionCollider;
        private TankPersonCharacter m_Character; // A reference to the TankPersonCharacter on the object.

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
