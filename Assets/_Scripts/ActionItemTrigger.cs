using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionItemTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<ItemManager>().action();
    }
}
