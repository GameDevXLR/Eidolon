using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

#region editor variable

    public static GameManager instance;

    public CameraController cam;

    public InteractionPlayerManager playerCurrent;

    public List<InteractionPlayerManager> personnagesList;

    public bool isInDialogue = true;
    
    public GameObject PAPanel;

    #endregion

    #region Monobehavior Methods

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            isInDialogue = true;
            DialogueController.instance.Initialize(0);
            PAPanel.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!isInDialogue)
        {
            if (Input.GetMouseButton(0))
            {
                playerCurrent.gameObject.GetComponent<CharacIsomController>().move();
            }
            else if(Input.GetMouseButtonUp(0))
            {
                //playerCurrent.gameObject.GetComponent<CharacIsomController>().stopMovingPointer();
            }
        }
    }
    #endregion

    #region other Methods
    public void activePA()
    {
        PAPanel.SetActive(true);
        isInDialogue = false;
    }

    public void setPlayerPath(NavMeshPath path)
    {
        playerCurrent.GetComponent<CharacIsomController>().setPath(path);

    }
    public void setPlayerPath(Vector3 path)
    {
        playerCurrent.GetComponent<CharacIsomController>().setPath(path);
    }

    public void changePerso(InteractionPlayerManager perso)
    {
        if (perso.PA > 0)
        {
            playerCurrent = perso;
            perso.init();
            cam.thePlayer = perso.gameObject;
        }
    }

    public void addObjInPerso(GameObject obj)
    {
           
    }

    public void nextPerso()
    {
        int i = 0;
        bool find = false;
        while (!find && i < personnagesList.Count)
        {
            find = personnagesList[i].PA != 0;
            if (find == false)
                i++;
        } 
        if (find)
        {
            changePerso( personnagesList[i]);
        }
        else
        {
            LaunchEndTurn();
        }
        
    }

    public void LaunchEndTurn()
    {
        foreach (InteractionPlayerManager perso in personnagesList)
        {
            perso.setPA(2);
        }
        changePerso(personnagesList[0]);
    }

    public void test()
    {
        Debug.Log("load");
    }
    
    #endregion
}
