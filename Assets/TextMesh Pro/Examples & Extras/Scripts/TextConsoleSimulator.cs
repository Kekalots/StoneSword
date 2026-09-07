using UnityEngine;
using System.Collections;

namespace TMPro.Examples
{
    public class TextConsoleSimulator : MonoBehaviour
    {
        private TMP_Text m_TextComponent;
        private bool hasTextChanged;

        void Awake()
        {
            m_TextComponent = gameObject.GetComponent<TMP_Text>();
        }

        void Start()
        {
            StartCoroutine(RevealCharacters(m_TextComponent));
        }

        void OnEnable()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
        }

        void OnDisable()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
        }

        void ON_TEXT_CHANGED(Object obj)
        {
            hasTextChanged = true;
        }

        IEnumerator RevealCharacters(TMP_Text textComponent)
        {
            textComponent.ForceMeshUpdate();

            TMP_TextInfo textInfo = textComponent.textInfo;

            int totalVisibleCharacters = textInfo.characterCount;
            int visibleCount = 0;

            while (true)
            {
                if (hasTextChanged)
                {
                    totalVisibleCharacters = textInfo.characterCount;
                    hasTextChanged = false;
                }

                if (visibleCount > totalVisibleCharacters)
                {
                    yield return new WaitForSeconds(1.0f);
                    visibleCount = 0;
                }

                textComponent.maxVisibleCharacters = visibleCount;

                visibleCount += 1;

                yield return null;
            }
        }

        IEnumerator RevealWords(TMP_Text textComponent)
        {
            textComponent.ForceMeshUpdate();

            int totalWordCount = textComponent.textInfo.wordCount;
            int totalVisibleCharacters = textComponent.textInfo.characterCount;
            int counter = 0;
            int currentWord = 0;
            int visibleCount = 0;

            while (true)
            {
                currentWord = counter % (totalWordCount + 1);

                if (currentWord == 0)
                    visibleCount = 0;
                else if (currentWord < totalWordCount)
                    visibleCount = textComponent.textInfo.wordInfo[currentWord - 1].lastCharacterIndex + 1;
                else if (currentWord == totalWordCount)
                    visibleCount = totalVisibleCharacters;

                textComponent.maxVisibleCharacters = visibleCount;

                if (visibleCount >= totalVisibleCharacters)
                {
                    yield return new WaitForSeconds(1.0f);
                }

                counter += 1;

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}