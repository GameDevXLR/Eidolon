using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplacementClick : MonoBehaviour {

    float time;

    private void OnMouseDown()
    {
        time = Time.fixedTime;
    }

    private void OnMouseUp()
    {
        if(Time.fixedTime - time < 0.15)
        {
            GameManager.instance.setPlayerPath(GetComponentInParent<Transform>().position);
        }
    }
    
}
