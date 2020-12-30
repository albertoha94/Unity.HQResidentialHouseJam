using System;
using TankEngine.Scripts.Interactions;
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (interactionSequence == null)
                {
                    ConsoleLogger.Error("An Interaction must be set!", this);
                }
                else
                {
                    if (isInteractionHappening)
                    {
                        if (currentInteraction.IsCompleted())
                        {
                            currentInteraction.OnComplete();

                            // Check if there are interactions pending to do, if not, end the sequence.
                            if (interactionIndex < interactionSequence.Length - 1)
                            {
                                interactionIndex++;
                                currentInteraction = interactionSequence[interactionIndex];
                                StartCoroutine(currentInteraction.PerformInteraction());
                            }
                            else
                            {
                                isInteractionHappening = false;
                                currentInteraction = null;
                                interactionSequence = null;
                            }
                        }
                        else
                        {
                            StartCoroutine(currentInteraction.ForceComplete());
                        }
                    }
                    else
                    {
                        isInteractionHappening = true;
                        interactionIndex = 0;
                        currentInteraction = interactionSequence[interactionIndex];
                        StartCoroutine(currentInteraction.PerformInteraction());
                    }
                }
            }
        }

        internal void PlaySound(AudioClip soundIn)
        {
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
    }
}
