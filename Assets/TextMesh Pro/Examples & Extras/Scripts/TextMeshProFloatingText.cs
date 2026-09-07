using UnityEngine;
using System.Collections;

namespace TMPro.Examples
{
    public class TextMeshProFloatingText : MonoBehaviour
    {
        public Font TheFont;

        private GameObject m_floatingText;
        private TextMeshPro m_textMeshPro;
        private TextMesh m_textMesh;

        private Transform m_transform;
        private Transform m_floatingText_Transform;
        private Transform m_cameraTransform;

        Vector3 lastPOS = Vector3.zero;
        Quaternion lastRotation = Quaternion.identity;

        public int SpawnType;
        public bool IsTextObjectScaleStatic;

        static WaitForEndOfFrame k_WaitForEndOfFrame = new WaitForEndOfFrame();
        static WaitForSeconds[] k_WaitForSecondsRandom = new WaitForSeconds[]
        {
            new WaitForSeconds(0.05f), new WaitForSeconds(0.1f), new WaitForSeconds(0.15f), new WaitForSeconds(0.2f), new WaitForSeconds(0.25f),
            new WaitForSeconds(0.3f), new WaitForSeconds(0.35f), new WaitForSeconds(0.4f), new WaitForSeconds(0.45f), new WaitForSeconds(0.5f),
            new WaitForSeconds(0.55f), new WaitForSeconds(0.6f), new WaitForSeconds(0.65f), new WaitForSeconds(0.7f), new WaitForSeconds(0.75f),
            new WaitForSeconds(0.8f), new WaitForSeconds(0.85f), new WaitForSeconds(0.9f), new WaitForSeconds(0.95f), new WaitForSeconds(1.0f),
        };

        void Awake()
        {
            m_transform = transform;
            m_floatingText = new GameObject(this.name + " floating text");

            m_cameraTransform = Camera.main.transform;
        }

        void Start()
        {
            if (SpawnType == 0)
            {
                m_textMeshPro = m_floatingText.AddComponent<TextMeshPro>();
                m_textMeshPro.rectTransform.sizeDelta = new Vector2(3, 3);

                m_floatingText_Transform = m_floatingText.transform;
                m_floatingText_Transform.position = m_transform.position + new Vector3(0, 15f, 0);

                m_textMeshPro.alignment = TextAlignmentOptions.Center;
                m_textMeshPro.color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
                m_textMeshPro.fontSize = 24;

                m_textMeshPro.enableKerning = false;
                m_textMeshPro.text = string.Empty;
                m_textMeshPro.isTextObjectScaleStatic = IsTextObjectScaleStatic;

                StartCoroutine(DisplayTextMeshProFloatingText());
            }
            else if (SpawnType == 1)
            {
                m_floatingText_Transform = m_floatingText.transform;
                m_floatingText_Transform.position = m_transform.position + new Vector3(0, 15f, 0);

                m_textMesh = m_floatingText.AddComponent<TextMesh>();
                m_textMesh.font = Resources.Load<Font>("Fonts/ARIAL");
                m_textMesh.GetComponent<Renderer>().sharedMaterial = m_textMesh.font.material;
                m_textMesh.color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
                m_textMesh.anchor = TextAnchor.LowerCenter;
                m_textMesh.fontSize = 24;

                StartCoroutine(DisplayTextMeshFloatingText());
            }
            else if (SpawnType == 2)
            {
            }
        }

        public IEnumerator DisplayTextMeshProFloatingText()
        {
            float CountDuration = 2.0f;
            float starting_Count = Random.Range(5f, 20f);
            float current_Count = starting_Count;

            Vector3 start_pos = m_floatingText_Transform.position;
            Color32 start_color = m_textMeshPro.color;
            float alpha = 255;
            int int_counter = 0;

            float fadeDuration = 3 / starting_Count * CountDuration;

            while (current_Count > 0)
            {
                current_Count -= (Time.deltaTime / CountDuration) * starting_Count;

                if (current_Count <= 3)
                {
                    alpha = Mathf.Clamp(alpha - (Time.deltaTime / fadeDuration) * 255, 0, 255);
                }

                int_counter = (int)current_Count;
                m_textMeshPro.text = int_counter.ToString();

                m_textMeshPro.color = new Color32(start_color.r, start_color.g, start_color.b, (byte)alpha);

                m_floatingText_Transform.position += new Vector3(0, starting_Count * Time.deltaTime, 0);

                if (!lastPOS.Compare(m_cameraTransform.position, 1000) || !lastRotation.Compare(m_cameraTransform.rotation, 1000))
                {
                    lastPOS = m_cameraTransform.position;
                    lastRotation = m_cameraTransform.rotation;
                    m_floatingText_Transform.rotation = lastRotation;
                    Vector3 dir = m_transform.position - lastPOS;
                    m_transform.forward = new Vector3(dir.x, 0, dir.z);
                }

                yield return k_WaitForEndOfFrame;
            }

            yield return k_WaitForSecondsRandom[Random.Range(0, 19)];

            m_floatingText_Transform.position = start_pos;

            StartCoroutine(DisplayTextMeshProFloatingText());
        }

        public IEnumerator DisplayTextMeshFloatingText()
        {
            float CountDuration = 2.0f;
            float starting_Count = Random.Range(5f, 20f);
            float current_Count = starting_Count;

            Vector3 start_pos = m_floatingText_Transform.position;
            Color32 start_color = m_textMesh.color;
            float alpha = 255;
            int int_counter = 0;

            float fadeDuration = 3 / starting_Count * CountDuration;

            while (current_Count > 0)
            {
                current_Count -= (Time.deltaTime / CountDuration) * starting_Count;

                if (current_Count <= 3)
                {
                    alpha = Mathf.Clamp(alpha - (Time.deltaTime / fadeDuration) * 255, 0, 255);
                }

                int_counter = (int)current_Count;
                m_textMesh.text = int_counter.ToString();

                m_textMesh.color = new Color32(start_color.r, start_color.g, start_color.b, (byte)alpha);

                m_floatingText_Transform.position += new Vector3(0, starting_Count * Time.deltaTime, 0);

                if (!lastPOS.Compare(m_cameraTransform.position, 1000) || !lastRotation.Compare(m_cameraTransform.rotation, 1000))
                {
                    lastPOS = m_cameraTransform.position;
                    lastRotation = m_cameraTransform.rotation;
                    m_floatingText_Transform.rotation = lastRotation;
                    Vector3 dir = m_transform.position - lastPOS;
                    m_transform.forward = new Vector3(dir.x, 0, dir.z);
                }

                yield return k_WaitForEndOfFrame;
            }

            yield return k_WaitForSecondsRandom[Random.Range(0, 20)];

            m_floatingText_Transform.position = start_pos;

            StartCoroutine(DisplayTextMeshFloatingText());
        }
    }
}