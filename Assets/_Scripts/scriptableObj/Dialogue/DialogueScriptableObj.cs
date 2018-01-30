using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "ScriptableObject/Dialogue", order = 1)]
public class DialogueScriptableObj : ScriptableObject
{
    public List<Sentence> sentences;
    public List<PersonnageScriptableObj> personnage;
}
