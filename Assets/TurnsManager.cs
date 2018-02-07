using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnsManager : MonoBehaviour 
{

	public static TurnsManager instance;
	public UnityEvent character1EndTurnEvent;
	public UnityEvent character2EndTurnEvent;
	public UnityEvent enemy1EndTurnEvent;

	public GameObject EndTurnBtn;

	public int turnNbr;
	public int roundNbr = 1;

	public ActualEntityPlaying currentEntity;
	public enum ActualEntityPlaying
	{
		charac1,
		charac2,
		enemy,
	}

	public void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		} else 
		{
			Destroy (gameObject);
		}
	}
	public void Start()
	{
		if (character1EndTurnEvent == null) 
		{
			character1EndTurnEvent = new UnityEvent ();
			character2EndTurnEvent = new UnityEvent ();
			enemy1EndTurnEvent = new UnityEvent ();

		}
	}
	
	public void EndARound()
	{
		switch (currentEntity) 
		{
		case ActualEntityPlaying.charac1:
			break;
		case ActualEntityPlaying.charac2:
			break;
		case ActualEntityPlaying.enemy:
			break;
		default:
			break;
		}

		roundNbr++;
		if (roundNbr > 3) 
		{
			roundNbr = 1;
			EndATurn ();
		}
	}

	public void EndATurn()
	{
		turnNbr++;
	}

}
