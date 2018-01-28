using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractionPlayerManager : MonoBehaviour {

    #region editor
    public List<GameObject> objectInteractableList;
    public float distanceByAction;
    public GameObject playerObject;

    public GameObject prefabUIListObj;

    #endregion

    #region other variables

    NavMeshAgent _navMeshAgent;

    Dictionary<int, List<GameObject>> objInteractableDict;

    #endregion

    #region MonoBehavior Methods 

    private void Start()
    {
        _navMeshAgent = playerObject.GetComponent<NavMeshAgent>();
        objInteractableDict = new Dictionary<int, List<GameObject>>();
        NavMeshPath path = new NavMeshPath();
        foreach (GameObject obj in objectInteractableList)
        {
            path.ClearCorners();
            if(NavMesh.CalculatePath(transform.position, obj.GetComponent<ItemManager>().positionArret.transform.position, NavMesh.AllAreas, path))
            {
                int PA = (int)(GetPathLength(path) / distanceByAction);
                if (objInteractableDict.ContainsKey(PA))
                {
                    objInteractableDict[PA].Add(obj);
                }
                else
                {
                    objInteractableDict.Add(PA, new List<GameObject>());
                    objInteractableDict[PA].Add(obj);
                }
                for (int i = 0; i < path.corners.Length - 1; i++)
                    Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 100);
            }
            else
            {
                Debug.Log("false ==> " + obj.name + " : " + obj.GetComponent<ItemManager>().positionArret.transform.position);
            }
        }

    }
    #endregion

    #region other Methods

    public static float GetPathLength(NavMeshPath path)
    {
        float lng = 0.0f;

        if ((path.status != NavMeshPathStatus.PathInvalid) && (path.corners.Length > 1))
        {
            for (int i = 1; i < path.corners.Length; i++)
            {
                lng += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
        }

        return lng;
    }

    public void addObjInUIList()
    {
        foreach(KeyValuePair<int, List<GameObject>> entry in objInteractableDict)
        {
            if(entry.Key != -1)
            {
                foreach(GameObject obj in entry.Value)
                {

                }
            }
        }
    }

    #endregion

}
