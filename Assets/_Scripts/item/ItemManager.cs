using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class ItemManager : MonoBehaviour {

    public GameObject positionArret;
    public string nameItem;
    public NavMeshPath path;
	public Transform targetSpot;
    public int PA;
    public UnityEvent events;
    public bool isAction = false;
    public bool isActivable = true;
	public Sprite buttonImg;

    private void Start()
    {
        gameObject.GetComponent<cakeslice.Outline>().enabled = false;
        isActivable = true;
    }

    private void OnMouseDown()
    {
        if (!GameManager.instance.isInDialogue && path != null)
        {
            goTo();
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

    public void goTo()
    {
        if(GameManager.instance.playerCurrent.PA >= PA)
        {
            GameManager.instance.setPlayerPath(positionArret.transform.position);
            GameManager.instance.playerCurrent.setPA(-PA);
            isAction = true;
        }
    }

    public void action()
    {
        if (isAction && isActivable)
        {
            events.Invoke();
            isActivable = false;
        }
    }

    public void enterHover()
    {
        gameObject.GetComponent<cakeslice.Outline>().enabled = true;
    }
    public void exitHover()
    {
        gameObject.GetComponent<cakeslice.Outline>().enabled = false;
    }
}
