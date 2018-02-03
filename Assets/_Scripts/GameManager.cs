using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

#region editor variable

    public static GameManager instance;

    public CameraController cam;

    public InteractionPlayerManager playerCurrent;

    public List<InteractionPlayerManager> personnagesList;

    public bool isInDialogue = true;
    
    public GameObject PAPanel;

	public bool isPlayingIntroVideo = true;

	public GameObject introVideoObj;
	public GameObject blackScreenObj;
	public Canvas mainCanvas;
	public float videoDuration;

	public AudioSource SoundEffectAudioS;

	public AudioClip actionConfirmedSnd;

    #endregion
	float videoTimer = 0;

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
	void FixedUpdate()
	{
		if (isPlayingIntroVideo) 
		{
			if (videoTimer > 1.5f && blackScreenObj.activeInHierarchy) 
			{
				blackScreenObj.SetActive (false);
			}
			videoTimer += Time.fixedDeltaTime;
			if (videoTimer > videoDuration) 
			{
				StopVideo ();
			}
		}
	}
    void Update()
    {
		if (isPlayingIntroVideo) 
		{
			if(Input.GetMouseButtonDown(0))
			{
				StopVideo ();
			}
		}
        if (!isInDialogue)
        {
            if (Input.GetMouseButtonDown(0))
            {
                playerCurrent.gameObject.GetComponent<CharacIsomController>().effectOnMouseDown();
            }
            else if (Input.GetMouseButton(0))
            {
                playerCurrent.gameObject.GetComponent<CharacIsomController>().effectOnMouse();
            }
            else if(Input.GetMouseButtonUp(0))
            {
                playerCurrent.gameObject.GetComponent<CharacIsomController>().effectOnMouseUp();
            }
        }
    }
    #endregion

    #region other Methods

	public void StopVideo()
	{
		blackScreenObj.SetActive (false);
		GetComponent<AudioSource> ().enabled = true;
		mainCanvas.enabled = true;
		introVideoObj.SetActive (false);
		InitializeDialog ();
		isPlayingIntroVideo = false;
	}

	public void InitializeDialog()
	{
		isInDialogue = true;
		DialogueController.instance.Initialize(0);
		PAPanel.SetActive(false);
	}
	
    public void activePA()
    {
        PAPanel.SetActive(true);
        isInDialogue = false;
    }

    public void setPlayerPath(NavMeshPath path)
    {
        playerCurrent.GetComponent<CharacIsomController>().setPath(path);

    }
    public void setPlayerPath(Vector3 path)
    {
        playerCurrent.GetComponent<CharacIsomController>().setPath(path);
    }

    public void changePerso(InteractionPlayerManager perso)
    {
        if (perso.PA > 0)
        {
            playerCurrent.gameObject.GetComponent<CharacIsomController>().sourisPointer.SetActive(false);
            playerCurrent.gameObject.GetComponent<CharacIsomController>().posPing.SetActive(false);
            playerCurrent = perso;
            perso.init();
            cam.thePlayer = perso.gameObject;
        }
    }

    public void removeObjInPersoList(GameObject obj)
    {
        foreach (InteractionPlayerManager perso in personnagesList)
        {
            perso.objectInteractableList.Remove(obj);
        }
    }

    public void addObjInPerso(GameObject obj)
    {
        foreach (InteractionPlayerManager perso in personnagesList)
        {
			SoundEffectAudioS.PlayOneShot (actionConfirmedSnd);
            perso.objectInteractableList.Add(obj);
        }
    }

    public void createObjList()
    {
        foreach (InteractionPlayerManager perso in personnagesList)
        {
            perso.addObjInUIList();
        }
    }

    public void initPerso()
    {
        foreach (InteractionPlayerManager perso in personnagesList)
        {
            perso.init();
        }
    }

    public void nextPerso()
    {
        int i = 0;
        bool find = false;
        while (!find && i < personnagesList.Count)
        {
            find = personnagesList[i].PA != 0;
            if (find == false)
                i++;
        } 
        if (find)
        {
            changePerso( personnagesList[i]);
        }
        else
        {
            LaunchEndTurn();
        }
        
    }

    public void LaunchEndTurn()
    {
        foreach (InteractionPlayerManager perso in personnagesList)
        {
            perso.setPA(2);
        }
        changePerso(personnagesList[0]);
    }

    public void test()
    {
        Debug.Log("load");
    }
    
    #endregion
}
