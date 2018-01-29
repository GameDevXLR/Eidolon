using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour {

#region editor variable

    public static GameManager instance;

    public GameObject player;

    #endregion

    #region Monobehavior Methods

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region other Methods
    public void setPlayerPath(NavMeshPath path)
    {
        
    }
#endregion
}
