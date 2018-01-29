using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIManager : MonoBehaviour {

    public Text textName;
    public Text textPA;

    public ItemManager item;

    public void setText(string name, string PA)
    {
        textName.text = name;
        textPA.text = PA;
    }

    public void action()
    {
        item.action();
    }
}
