using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName="ConversationData",menuName = "Conversation/ConversationData",order =2)]
public class ConversationSO : ScriptableObject
{
    public List<Sentence> sentences = new List<Sentence>();
}

[System.Serializable]
public class Sentence
{
    public string content;
    public string speakerName;
    public Sprite speakerAvatar;
}