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
        if (!isInDialogue && Input.GetMouseButtonDown(0))
        {
            playerCurrent.gameObject.GetComponent<CharacIsomController>().move();
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
        playerCurrent = perso;
        perso.init();
        cam.thePlayer = perso.gameObject;
    }

    public void addObjInPerso(GameObject obj)
    {
           
    }

    public void nexPerso()
    {
        int i = 0;
        bool find = false;
        while(!find && i < personnagesList.Count)
        {
            find = personnagesList[i].PA > 0;
            i = (find)? i:i++;
        }
        if (find)
        {
            playerCurrent = personnagesList[i];
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
        playerCurrent = personnagesList[0];
    }
    
    #endregion
}
