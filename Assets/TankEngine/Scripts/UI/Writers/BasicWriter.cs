using System;
using TMPro;
using UnityEngine;

public class BasicWriter : MonoBehaviour
{

    public float timePerCharacter;
    public bool addBlankSpacing;

    private float mTimer;
    private int characterIndex = 0;
    private TMP_Text mainText;
    private string messageToWrite;
    private Action onComplete;
    private bool allowWriting;

    private void Update()
    {
        if (allowWriting && !string.IsNullOrEmpty(messageToWrite))
        {
            mTimer -= Time.unscaledDeltaTime;
            while (mTimer <= 0f)
            {
                mTimer += timePerCharacter;
                characterIndex++;
                string text = messageToWrite.Substring(0, characterIndex);
                if (addBlankSpacing)
                {
                    text += $"<color=#00000000>{messageToWrite.Substring(characterIndex)}</color>";
                }
                mainText.text = text;

                if (!allowWriting) break;
                if (characterIndex >= messageToWrite.Length)
                {
                    messageToWrite = string.Empty;
                    onComplete?.Invoke();
                }
            }
        }
    }

    internal void Write(TMP_Text mainMessageText, string message, Action onComplete)
    {
        allowWriting = true;
        characterIndex = 0;
        mainText = mainMessageText;
        messageToWrite = message;
        this.onComplete = onComplete;
    }

    internal bool IsActive()
    {
        return characterIndex < messageToWrite.Length;
    }

    internal void Disable()
    {
        allowWriting = false;
    }
}
