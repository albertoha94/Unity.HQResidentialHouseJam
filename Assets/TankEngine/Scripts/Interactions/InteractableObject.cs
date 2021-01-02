using TankEngine.Scripts.Interactions.Base;
using UnityEngine;

namespace TankEngine.Scripts.Interactions
{

    public class InteractableObject : MonoBehaviour
    {

        [SerializeField] bool inZone = false;

        #region Glow elements

        [Header("Glow")]
        [SerializeField] Transform glowPrefab;
        [SerializeField] Transform glowLocation;

        #endregion

        [SerializeField] Interactable[] interactionSequence;

        GameManager gameManager;

        private void Start()
        {
            gameManager = GameObject.FindWithTag(Tags.GAME_MANAGER).GetComponent<GameManager>();

            InstantiateGlow();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag != Tags.PLAYER)
                return;

            inZone = true;
        }

        void OnTriggerExit(Collider other)
        {
            if (other.tag != Tags.PLAYER)
                return;

            inZone = false;
            gameManager.interactionSequence = null;
        }

        private void OnTriggerStay(Collider other)
        {
            if (inZone && other.tag == Tags.PLAYER_INTERACTOR)
            {
                gameManager.interactionSequence = interactionSequence;
            }
        }

        private void InstantiateGlow()
        {
            if (glowPrefab == null || glowLocation == null)
                return;

            Instantiate(glowPrefab, glowLocation.position, Quaternion.identity);
        }
    }
}
