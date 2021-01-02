using System;
using TankEngine.Scripts.Interactions;
using TankEngine.Scripts.Interactions.Base;
using TankEngine.Scripts.UI;
using UnityEngine;

namespace TankEngine.Scripts
{

    [RequireComponent(typeof(AudioSource))]
    public class GameManager : MonoBehaviour
    {

        public UIAssistant uIAssistant;
        AudioSource audioSource;

        #region Interaction variables.

        [SerializeField] bool isInteractionHappening = false;
        public Interactable[] interactionSequence;
        int interactionIndex;
        Interactable currentInteraction;

        #endregion

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            HandleInteractionOptionMovement();
            HandleInteractions();
        }


        internal void AddInteractionToArray(Interactable interaction)
        {
            Interactable[] finalArray = new Interactable[interactionSequence.Length + 1];
            for (int i = 0; i < interactionSequence.Length; i++)
            {
                finalArray[i] = interactionSequence[i];
            }
            finalArray[finalArray.Length - 1] = interaction;
            interactionSequence = finalArray;
        }

        internal void PlaySound(AudioClip soundIn)
        {
            if (soundIn == null)
                return;

            audioSource.clip = soundIn;
            audioSource.Play();
        }

        internal void ResumeTime()
        {
            Time.timeScale = 1;
        }

        internal void StopTime()
        {
            Time.timeScale = 0;
        }

        private void HandleInteractions()
        {
            var wasKeyPressed = Input.GetKeyDown(KeyCode.E);

            if (!wasKeyPressed)
                return;
            if (interactionSequence == null || interactionSequence.Length == 0)
                return;

            if (isInteractionHappening)
            {
                if (currentInteraction == null)
                {
                    ConsoleLogger.Error("Interaction happening when there's no current interaction set!", this);
                    isInteractionHappening = false;
                    return;
                }

                if (currentInteraction.IsCompleted())
                {
                    CompleteInteractionAndCheckForSequenceEnding(currentInteraction);
                }
                else
                {
                    var isInteractionWithOptions = currentInteraction.GetInteractionType() == InteractionTypes.Options;

                    if (!isInteractionWithOptions)
                    {
                        StartCoroutine(currentInteraction.ForceComplete());
                    }
                    else
                    {
                        var interWithOptions = (currentInteraction as InteractionWithOptions);
                        if (!interWithOptions.AreOptionsSet)
                        {
                            StartCoroutine(currentInteraction.ForceComplete());
                        }
                        else
                        {
                            StartCoroutine(interWithOptions.PerformSelectedOption());
                        }
                    }
                }
            }
            else
            {
                isInteractionHappening = true;
                interactionIndex = 0;
                currentInteraction = interactionSequence[interactionIndex];
                StopTime();
                PerformInteraction(currentInteraction);
            }
        }

        private void HandleInteractionOptionMovement()
        {
            if (interactionSequence == null)
                return;
            if (currentInteraction == null)
                return;
            if (currentInteraction.GetInteractionType() != InteractionTypes.Options)
                return;

            var interactionWithOptions = (currentInteraction as InteractionWithOptions);

            #region Side movement.

            var movementLeft = Input.GetKeyDown(KeyCode.A);
            var movementRight = Input.GetKeyDown(KeyCode.D);
            if (movementLeft || movementRight)
            {
                interactionWithOptions.MoveOption(movementLeft, movementRight);
            }

            #endregion

            #region Check if its completed.

            if (interactionWithOptions.IsCompleted())
            {
                CompleteInteractionAndCheckForSequenceEnding(interactionWithOptions);
            }

            #endregion

        }

        /// <summary>
        /// Completes the intended interaction and checks if there's another pending to do.
        /// </summary>
        /// <param name="interactable">Interactable to complete.</param>
        private void CompleteInteractionAndCheckForSequenceEnding(Interactable interactable)
        {
            interactable.OnComplete();

            // Check if there are interactions pending to do, if not, end the sequence.
            if (HasSequenceEnded())
            {
                isInteractionHappening = false;
                currentInteraction = null;
                interactionSequence = null;
                ResumeTime();
            }
            else
            {
                interactionIndex++;
                currentInteraction = interactionSequence[interactionIndex];
                PerformInteraction(currentInteraction);
            }
        }

        private void PerformInteraction(Interactable interactable)
        {
            PlaySound(interactable.SoundIn);
            StartCoroutine(interactable.PerformInteraction());
        }

        private bool HasSequenceEnded() => interactionSequence != null && interactionIndex >= interactionSequence.Length - 1;
    }
}
