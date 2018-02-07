using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SwitchDoorManager : MonoBehaviour 
{
	public bool isOpen = false;
	public GameObject UIobj;
	public OpenSas SaSController;

	public void RegisterThisAction()
	{
		//turnmanager....addListener(dooractivation)....
	}
	public void DoorActivation()
	{
		isOpen = !isOpen;
		SaSController.openSas (isOpen);
	}
}
