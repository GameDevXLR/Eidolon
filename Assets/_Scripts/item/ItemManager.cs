using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemManager : MonoBehaviour {

    public GameObject positionArret;
    public string nameItem;
    public NavMeshPath path;
    public int PA;

    private void Start()
    {
        gameObject.GetComponent<cakeslice.Outline>().enabled = false;
    }

    private void OnMouseDown()
    {
        if (!GameManager.instance.isInDialogue)
        {
            action();
        }
    }

    private void OnMouseEnter()
    {
        if(!GameManager.instance.isInDialogue)
            enterHover();
    }

    private void OnMouseExit()
    {
        exitHover();
    }

    public void action()
    {
        if(GameManager.instance.playerCurrent.PA >= PA)
        {
            GameManager.instance.setPlayerPath(path);
            GameManager.instance.playerCurrent.setPA(-PA);
        }
    }

    public void enterHover()
    {
        gameObject.GetComponent<cakeslice.Outline>().enabled =true;
    }
    public void exitHover()
    {
        gameObject.GetComponent<cakeslice.Outline>().enabled = false;
    }
}
