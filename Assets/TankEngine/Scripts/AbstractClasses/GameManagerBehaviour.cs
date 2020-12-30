using UnityEngine;

namespace TankEngine.Scripts.AbstractClasses
{
    public abstract class GameManagerBehavior : MonoBehaviour
    {

        protected GameManager GameManager { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            GameManager = GameObject.FindWithTag(Tags.GAME_MANAGER).GetComponent<GameManager>();
        }
    }
}
