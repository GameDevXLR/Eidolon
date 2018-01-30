using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemManager : MonoBehaviour {

    public GameObject positionArret;
    public string nameItem;
    public NavMeshPath path;

    private void OnMouseDown()
    {
        if (!GameManager.instance.isInDialogue)
        {
            action();
        }
    }

    public void action()
    {
        GameManager.instance.setPlayerPath(path);
    }
}
