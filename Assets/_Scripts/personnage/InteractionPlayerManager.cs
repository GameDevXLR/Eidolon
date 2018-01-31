using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InteractionPlayerManager : MonoBehaviour {

    #region editor
    public List<GameObject> objectInteractableList;
    public float distanceByAction;
    public GameObject playerObject;

    public GameObject prefabUIListObj;
    public GameObject UIList;

    public int PA;
    public int PAmax;

    PersonnageScriptableObj perso;

    public Text PATxt;
    public Image portrait;

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
        calculatePA();
        addObjInUIList();
        PATxt.text = PA.ToString();

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
        emptyBox();
        foreach(KeyValuePair<int, List<GameObject>> entry in objInteractableDict)
        {
            if(entry.Key != -1)
            {
                foreach(GameObject obj in entry.Value)
                {
                    GameObject uiObj = Instantiate(prefabUIListObj);
                    uiObj.transform.SetParent(UIList.transform);
                    uiObj.GetComponent<ItemUIManager>().setText(obj.GetComponent<ItemManager>().nameItem, "PA : " + entry.Key);
                    uiObj.GetComponent<ItemUIManager>().item = obj.GetComponent<ItemManager>();
                }
            }
        }
    }

    public void calculatePA()
    {
        foreach (GameObject obj in objectInteractableList)
        {
            obj.GetComponent<ItemManager>().path = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, obj.GetComponent<ItemManager>().positionArret.transform.position, NavMesh.AllAreas, obj.GetComponent<ItemManager>().path))
            {

                int PA = (int)(GetPathLength(obj.GetComponent<ItemManager>().path) / distanceByAction) + 1;
                obj.GetComponent<ItemManager>().PA = PA;
                if (objInteractableDict.ContainsKey(PA))
                {
                    objInteractableDict[PA].Add(obj);
                }
                else
                {
                    objInteractableDict.Add(PA, new List<GameObject>());
                    objInteractableDict[PA].Add(obj);
                }
                for (int i = 0; i < obj.GetComponent<ItemManager>().path.corners.Length - 1; i++)
                    Debug.DrawLine(obj.GetComponent<ItemManager>().path.corners[i], obj.GetComponent<ItemManager>().path.corners[i + 1], Color.red, 100);
            }
            else
            {
                Debug.Log("false ==> " + obj.name + " : " + obj.GetComponent<ItemManager>().positionArret.transform.position);
            }
        }
    }
    public void emptyBox()
    {
        foreach (Transform child in UIList.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void setPA(int PA)
    {
        Debug.Log(PA);
        Debug.Log(this.PA);

        this.PA += PA;
        PATxt.text = this.PA.ToString();
    }


    #endregion

}
