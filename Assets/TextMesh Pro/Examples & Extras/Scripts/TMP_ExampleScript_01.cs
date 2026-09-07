using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace TMPro.Examples
{
    public class TMP_ExampleScript_01 : MonoBehaviour
    {
        public enum objectType { TextMeshPro = 0, TextMeshProUGUI = 1 };

        public objectType ObjectType;
        public bool isStatic;

        private TMP_Text m_text;

        private const string k_label = "The count is <#0080ff>{0}</color>";
        private int count;

        void Awake()
        {
            if (ObjectType == 0)
                m_text = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
            else
                m_text = GetComponent<TextMeshProUGUI>() ?? gameObject.AddComponent<TextMeshProUGUI>();

            m_text.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/Anton SDF");

            m_text.fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/Anton SDF - Drop Shadow");

            m_text.fontSize = 120;

            m_text.text = "A <#0080ff>simple</color> line of text.";

            Vector2 size = m_text.GetPreferredValues(Mathf.Infinity, Mathf.Infinity);

            m_text.rectTransform.sizeDelta = new Vector2(size.x, size.y);
        }

        void Update()
        {
            if (!isStatic)
            {
                m_text.SetText(k_label, count % 1000);
                count += 1;
            }
        }
    }
}