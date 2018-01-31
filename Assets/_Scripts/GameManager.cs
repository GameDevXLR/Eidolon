using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

#region editor variable

    public static GameManager instance;

    public InteractionPlayerManager playerCurrent;

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

    
    
    #endregion
}
