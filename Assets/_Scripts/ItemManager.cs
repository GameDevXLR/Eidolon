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
        Debug.Log(GameManager.instance.player != null);
        Debug.Log(GameManager.instance.player.GetComponent<CharacIsomController>() != null);
        Debug.Log(path != null);
        GameManager.instance.player.GetComponent<CharacIsomController>().setPath(path);
    }
}
