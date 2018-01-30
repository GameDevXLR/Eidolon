using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewPerso", menuName = "ScriptableObject/Personnage", order = 1)]
public class PersonnageScriptableObj : ScriptableObject
{
    public Color couleurDialogue;
    public string namePerso;
    public Image portrait;
}
