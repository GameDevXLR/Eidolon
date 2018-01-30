using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

#region editor variable

    public static GameManager instance;

    public GameObject player;

    public bool isInDialogue = true;

    public int PAMax;
    public int PACurrent;

    public Text PATxt;
    public GameObject PAPanel;

    #endregion

    #region Monobehavior Methods

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            PATxt.text = PACurrent.ToString();
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
        player.GetComponent<CharacIsomController>().setPath(path);
    }
    public void setPlayerPath(Vector3 path)
    {
        player.GetComponent<CharacIsomController>().setPath(path);
    }


    public void minusPA()
    {
        PACurrent--;
        setPAText();
    }


    public void addPA()
    {
        PACurrent++;
        setPAText();
    }


    public void setPAText()
    {
        PATxt.text = PACurrent.ToString();
    }
    #endregion
}
