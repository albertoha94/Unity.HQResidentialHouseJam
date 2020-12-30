using System.Collections;
using TankEngine.Scripts.AbstractClasses;
using UnityEngine;

namespace TankEngine.Scripts.Interactions
{

    /// <summary>
    /// Abstract class used to create interactions.
    /// </summary>
    public abstract class Interactable : GameManagerBehavior
    {

        #region Sound effects

        [Header("Interaction Sound effects")]
        public AudioClip SoundIn;

        #endregion

        /// <summary>
        /// Main method used to perform interaction.
        /// </summary>
        /// <returns>An IEnumerator.</returns>
        public abstract IEnumerator PerformInteraction();

        /// <summary>
        /// Used to define the type of interaction is setting.
        /// </summary>
        /// <returns>An InteracitonType.</returns>
        public abstract InteractionTypes GetInteractionType();

        /// <summary>
        /// Used to define if the interaction is finished.
        /// </summary>
        /// <returns>A boolean flag.</returns>
        public abstract bool IsCompleted();

        /// <summary>
        /// Action to perform once interaction is completed.
        /// </summary>
        public abstract void OnComplete();

        /// <summary>
        /// Used to complete the interaction by force.
        /// Generally used to skip a cutscene.
        /// </summary>
        /// <returns>An IEnumerator.</returns>
        public abstract IEnumerator ForceComplete();
    }
}
