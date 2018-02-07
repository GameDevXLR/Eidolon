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
    public LayerMask layer_mask_Second;
    NavMeshAgent agent;
    NavMeshPath path;

    float time;

    int PA = 0;
	public Animator anim;
	bool isMoving;

    bool coroutine = false;

    public GameObject sourisPointer;
    public GameObject posPing;


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
//			if (Vector3.Distance(transform.position, agent.destination)<2f) 
//			{
//                stopMoving();
//			}
			if( !agent.hasPath)
			{
				stopMoving ();
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
        if(!isMoving)
            setMousePointer();
       
    }


    public void setMousePointer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,100, layer_mask_Second))
        {
            path = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, hit.point, NavMesh.AllAreas, path))
            {

                PA = (int)(InteractionPlayerManager.GetPathLength(path) / gameObject.GetComponent<InteractionPlayerManager>().distanceByAction) + 1;

                setPointeurMouse(hit.point, path);
            }
            first = false;
        }
    }

    public void effectOnMouseUp()
    {
        if (Time.fixedTime - time < 0.15)
        {
            if (PA <= GameManager.instance.playerCurrent.PA)
            {
                GameManager.instance.playerCurrent.setPA(-PA);
                setPath(path);
            }
                
        }
    }
    public void effectOnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, layer_mask))
        {
            if (hit.collider.gameObject.tag != "pointeur")
            {
                path = new NavMeshPath();
                if (NavMesh.CalculatePath(transform.position, hit.point, NavMesh.AllAreas, path))
                {

                    PA = (int)(InteractionPlayerManager.GetPathLength(path) / gameObject.GetComponent<InteractionPlayerManager>().distanceByAction) + 1;

                    setPointeurMouse(hit.point, path);
                }
            }
            else
            {
                time = Time.fixedTime;
            }
        }

    }

    public void effectOnMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, layer_mask))
        {
            if (hit.collider.gameObject.tag != "pointeur")
            {
                path = new NavMeshPath();
                if (NavMesh.CalculatePath(transform.position, hit.point, NavMesh.AllAreas, path))
                {

                    PA = (int)(InteractionPlayerManager.GetPathLength(path) / gameObject.GetComponent<InteractionPlayerManager>().distanceByAction) + 1;

                    setPointeurMouse(hit.point, path);
                }
            }
        }
    }


    public void deactvatePointerMouse()
    {
        sourisPointer.GetComponent<LineRenderer>().positionCount = 0;
        sourisPointer.SetActive(false);
        posPing.SetActive(false);
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
        posPing.SetActive(true);
        sourisPointer.SetActive(true);
        sourisPointer.transform.position = transform.position;
        sourisPointer.transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 100);
        sourisPointer.GetComponentInChildren<TextMesh>().text = PA.ToString();
    }

    public void setPath(Vector3 destination)
    {
        
        if(PA <= GameManager.instance.playerCurrent.PA)
        {

            agent.SetDestination(destination);
            GameManager.instance.playerCurrent.setPA(-PA);
            beginMoving();
            deactvatePointerMouse();
        }
    }

    public void setPath(NavMeshPath path)
    {
        agent.SetPath(path);
        beginMoving();
        deactvatePointerMouse();
    }

    public void beginMoving()
    {
        anim.SetBool("Walk", true);
        isMoving = true;
		anim.transform.localPosition = Vector3.zero;
		anim.transform.localRotation = Quaternion.identity;

    }

    public void stopMoving()
    {
        anim.SetBool("Walk", false);
        isMoving = false;
		anim.transform.localPosition = Vector3.zero;
		anim.transform.localRotation = Quaternion.identity;
    }

}
