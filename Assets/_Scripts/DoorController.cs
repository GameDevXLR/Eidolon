using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
	public Animation anim;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
	
		anim.Play ("DoorOpen");
	}
		
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") {

			anim.Play ("DoorClose");
		}

	}

}
