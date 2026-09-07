using UnityEngine;
using System.Collections;

namespace TMPro.Examples
{
    public class VertexShakeA : MonoBehaviour
    {
        public float AngleMultiplier = 1.0f;
        public float SpeedMultiplier = 1.0f;
        public float ScaleMultiplier = 1.0f;
        public float RotationMultiplier = 1.0f;

        private TMP_Text m_TextComponent;
        private bool hasTextChanged;

        void Awake()
        {
            m_TextComponent = GetComponent<TMP_Text>();
        }

        void OnEnable()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
        }

        void OnDisable()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
        }

        void Start()
        {
            StartCoroutine(AnimateVertexColors());
        }

        void ON_TEXT_CHANGED(Object obj)
        {
            if (obj = m_TextComponent)
                hasTextChanged = true;
        }

        IEnumerator AnimateVertexColors()
        {
            m_TextComponent.ForceMeshUpdate();

            TMP_TextInfo textInfo = m_TextComponent.textInfo;

            Matrix4x4 matrix;
            Vector3[][] copyOfVertices = new Vector3[0][];

            hasTextChanged = true;

            while (true)
            {
                if (hasTextChanged)
                {
                    if (copyOfVertices.Length < textInfo.meshInfo.Length)
                        copyOfVertices = new Vector3[textInfo.meshInfo.Length][];

                    for (int i = 0; i < textInfo.meshInfo.Length; i++)
                    {
                        int length = textInfo.meshInfo[i].vertices.Length;
                        copyOfVertices[i] = new Vector3[length];
                    }

                    hasTextChanged = false;
                }

                int characterCount = textInfo.characterCount;

                if (characterCount == 0)
                {
                    yield return new WaitForSeconds(0.25f);
                    continue;
                }

                int lineCount = textInfo.lineCount;

                for (int i = 0; i < lineCount; i++)
                {
                    int first = textInfo.lineInfo[i].firstCharacterIndex;
                    int last = textInfo.lineInfo[i].lastCharacterIndex;

                    Vector3 centerOfLine = (textInfo.characterInfo[first].bottomLeft + textInfo.characterInfo[last].topRight) / 2;
                    Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(-0.25f, 0.25f) * RotationMultiplier);

                    for (int j = first; j <= last; j++)
                    {
                        if (!textInfo.characterInfo[j].isVisible)
                            continue;

                        int materialIndex = textInfo.characterInfo[j].materialReferenceIndex;

                        int vertexIndex = textInfo.characterInfo[j].vertexIndex;

                        Vector3[] sourceVertices = textInfo.meshInfo[materialIndex].vertices;

                        copyOfVertices[materialIndex][vertexIndex + 0] = sourceVertices[vertexIndex + 0] - centerOfLine;
                        copyOfVertices[materialIndex][vertexIndex + 1] = sourceVertices[vertexIndex + 1] - centerOfLine;
                        copyOfVertices[materialIndex][vertexIndex + 2] = sourceVertices[vertexIndex + 2] - centerOfLine;
                        copyOfVertices[materialIndex][vertexIndex + 3] = sourceVertices[vertexIndex + 3] - centerOfLine;

                        float randomScale = Random.Range(0.995f - 0.001f * ScaleMultiplier, 1.005f + 0.001f * ScaleMultiplier);

                        matrix = Matrix4x4.TRS(Vector3.one, rotation, Vector3.one * randomScale);

                        copyOfVertices[materialIndex][vertexIndex + 0] = matrix.MultiplyPoint3x4(copyOfVertices[materialIndex][vertexIndex + 0]);
                        copyOfVertices[materialIndex][vertexIndex + 1] = matrix.MultiplyPoint3x4(copyOfVertices[materialIndex][vertexIndex + 1]);
                        copyOfVertices[materialIndex][vertexIndex + 2] = matrix.MultiplyPoint3x4(copyOfVertices[materialIndex][vertexIndex + 2]);
                        copyOfVertices[materialIndex][vertexIndex + 3] = matrix.MultiplyPoint3x4(copyOfVertices[materialIndex][vertexIndex + 3]);

                        copyOfVertices[materialIndex][vertexIndex + 0] += centerOfLine;
                        copyOfVertices[materialIndex][vertexIndex + 1] += centerOfLine;
                        copyOfVertices[materialIndex][vertexIndex + 2] += centerOfLine;
                        copyOfVertices[materialIndex][vertexIndex + 3] += centerOfLine;
                    }
                }

                for (int i = 0; i < textInfo.meshInfo.Length; i++)
                {
                    textInfo.meshInfo[i].mesh.vertices = copyOfVertices[i];
                    m_TextComponent.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}