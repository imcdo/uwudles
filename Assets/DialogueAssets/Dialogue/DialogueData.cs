using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "New Dialogue Data", menuName = "Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public List<DialogueBlock> conversation;

    [System.Serializable]
    public struct DialogueBlock
    {
        public CharacterData character;
        [TextArea(4, 4)]
        public string dialogue;
    }
}
