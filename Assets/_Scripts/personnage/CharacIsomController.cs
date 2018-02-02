using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacIsomController : MonoBehaviour {

	public float moveSpeed;
	Vector3 newForward;
	Vector3 newRight;
	public bool mouseClicControlled;
	public LayerMask layer_mask;
	NavMeshAgent agent;
    NavMeshPath path;

    int PA = 0;
	public Animator anim;
	bool isMoving;

    bool coroutine = false;

    public GameObject sourisPointer;


    bool first = true;

    // Use this for initialization
    void Start () 
	{
		agent = GetComponent<NavMeshAgent> ();
		newForward = Camera.main.transform.forward;
		newRight = Camera.main.transform.right;
		newForward.y = 0;
		newForward = Vector3.Normalize (newForward);
		newRight = Quaternion.Euler(new Vector3(0,90,0)) *newForward;
	}
	
	//// Update is called once per frame
	//void Update () 
	//{
 //       if (!GameManager.instance.isInDialogue && !isMoving)
 //       {
 //           if (!mouseClicControlled && Input.anyKey)
 //           {
 //               ExecuteMovement();
 //           }
 //           if (mouseClicControlled)
 //           {
 //               if (Input.GetMouseButtonDown(0) && !coroutine)
 //               {

 //                   StartCoroutine(setMousePointer());
                    

 //               }
 //           }
 //       }
	//}
	void LateUpdate()
	{
		if (isMoving) 
		{
			if (Vector3.Distance(transform.position, agent.destination)<2f) 
			{
                stopMoving();
			}
		}
	}


    public void stopMovingPointer()
    {
        coroutine = false;
        first = true;
    }


    public void move()
    {
        if (!isMoving)
        {
            StartCoroutine(setMousePointer());
        }
    }


    IEnumerator setMousePointer()
    {
        coroutine = true;
        first = true; 
        while (Input.GetMouseButton(0) && coroutine)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, 2000f, layer_mask);
            if (hits.Length > 0)
            {
                if(hits[0].collider.gameObject.tag == "pointeur" && first)
                {
                    if (GameManager.instance.playerCurrent.PA > 0 && PA <= GameManager.instance.playerCurrent.PA)
                    {
                        setPath(path);
                        GameManager.instance.playerCurrent.setPA(-PA);
                        sourisPointer.GetComponent<LineRenderer>().positionCount = 0;
                        sourisPointer.SetActive(false);
                    }
                }
                else if (hits[0].collider.gameObject.tag != "pointeur" && hits[0].collider.gameObject.tag != "object")
                {
                    path = new NavMeshPath();
                    if (NavMesh.CalculatePath(transform.position, hits[0].point, NavMesh.AllAreas, path))
                    {

                        PA = (int)(InteractionPlayerManager.GetPathLength(path) / gameObject.GetComponent<InteractionPlayerManager>().distanceByAction) + 1;

                        setPointeurMouse(hits[0].point, path);
                    }
                    first = false;
                }
                
            }
            
            yield return new WaitForSeconds(0.10f);
        }
    }

    public void ExecuteMovement ()
	{
		//movements
		Vector3 sideMove = newRight * moveSpeed * Time.deltaTime * Input.GetAxis ("HorizontalKey");
		Vector3 frontMove = newForward *moveSpeed*Time.deltaTime* Input.GetAxis ("VerticalKey");
		transform.position += sideMove;
		transform.position += frontMove;
		//rotation

		Vector3 faceDir = Vector3.Normalize (sideMove + frontMove);
		transform.forward = faceDir;

	}

    public void setPointeurMouse(Vector3 pos, NavMeshPath path)
    {
        sourisPointer.GetComponent<LineRenderer>().positionCount = path.corners.Length;
        sourisPointer.GetComponent<LineRenderer>().SetPositions(path.corners);

        sourisPointer.SetActive(true);
        sourisPointer.transform.position = transform.position;
        sourisPointer.transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 100);
        sourisPointer.GetComponentInChildren<TextMesh>().text = PA.ToString();
    }

    public void setPath(Vector3 destination)
    {
        agent.SetDestination(destination);
        beginMoving();
    }

    public void setPath(NavMeshPath path)
    {
        agent.SetPath(path);
        beginMoving();
    }

    public void beginMoving()
    {
        anim.SetBool("Walk", true);
        isMoving = true;
    }

    public void stopMoving()
    {
        anim.SetBool("Walk", false);
        isMoving = false;
    }

}
