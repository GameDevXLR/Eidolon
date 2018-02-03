using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSas : MonoBehaviour {

	public Animator DoorAnim;
	public ParticleSystem DoorParticle;
	public AudioClip openSASSnd;
	public AudioSource audioS;

	public void openSas()
    {
        Debug.Log("openSas");
		DoorAnim.SetBool("DoorOpen", true);
		DoorParticle.Play ();
		audioS.PlayOneShot (openSASSnd);

    }
}
