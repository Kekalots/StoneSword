using UnityEngine;
using System.Collections;

namespace TMPro.Examples
{
    public class TextMeshSpawner : MonoBehaviour
    {
        public int SpawnType = 0;
        public int NumberOfNPC = 12;

        public Font TheFont;

        private TextMeshProFloatingText floatingText_Script;

        void Awake()
        {
        }

        void Start()
        {
            for (int i = 0; i < NumberOfNPC; i++)
            {
                if (SpawnType == 0)
                {
                    GameObject go = new GameObject();
                    go.transform.position = new Vector3(Random.Range(-95f, 95f), 0.5f, Random.Range(-95f, 95f));

                    TextMeshPro textMeshPro = go.AddComponent<TextMeshPro>();

                    textMeshPro.fontSize = 96;

                    textMeshPro.text = "!";
                    textMeshPro.color = new Color32(255, 255, 0, 255);

                    floatingText_Script = go.AddComponent<TextMeshProFloatingText>();
                    floatingText_Script.SpawnType = 0;
                }
                else
                {
                    GameObject go = new GameObject();
                    go.transform.position = new Vector3(Random.Range(-95f, 95f), 0.5f, Random.Range(-95f, 95f));

                    TextMesh textMesh = go.AddComponent<TextMesh>();
                    textMesh.GetComponent<Renderer>().sharedMaterial = TheFont.material;
                    textMesh.font = TheFont;
                    textMesh.anchor = TextAnchor.LowerCenter;
                    textMesh.fontSize = 96;

                    textMesh.color = new Color32(255, 255, 0, 255);
                    textMesh.text = "!";

                    floatingText_Script = go.AddComponent<TextMeshProFloatingText>();
                    floatingText_Script.SpawnType = 1;
                }
            }
        }
    }
}