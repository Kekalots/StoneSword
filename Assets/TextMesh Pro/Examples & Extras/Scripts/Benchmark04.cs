using UnityEngine;
using System.Collections;

namespace TMPro.Examples
{
    public class Benchmark04 : MonoBehaviour
    {
        public int SpawnType = 0;

        public int MinPointSize = 12;
        public int MaxPointSize = 64;
        public int Steps = 4;

        private Transform m_Transform;

        void Start()
        {
            m_Transform = transform;

            float lineHeight = 0;
            float orthoSize = Camera.main.orthographicSize = Screen.height / 2;
            float ratio = (float)Screen.width / Screen.height;

            for (int i = MinPointSize; i <= MaxPointSize; i += Steps)
            {
                if (SpawnType == 0)
                {
                    GameObject go = new GameObject("Text - " + i + " Pts");

                    if (lineHeight > orthoSize * 2) return;

                    go.transform.position = m_Transform.position + new Vector3(ratio * -orthoSize * 0.975f, orthoSize * 0.975f - lineHeight, 0);

                    TextMeshPro textMeshPro = go.AddComponent<TextMeshPro>();

                    textMeshPro.rectTransform.pivot = new Vector2(0, 0.5f);

                    textMeshPro.enableWordWrapping = false;
                    textMeshPro.extraPadding = true;
                    textMeshPro.isOrthographic = true;
                    textMeshPro.fontSize = i;

                    textMeshPro.text = i + " pts - Lorem ipsum dolor sit...";
                    textMeshPro.color = new Color32(255, 255, 255, 255);

                    lineHeight += i;
                }
                else
                {
                }
            }
        }
    }
}