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
	public Animator anim;
	bool isMoving;
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
	
	// Update is called once per frame
	void Update () 
	{
		if (!mouseClicControlled && Input.anyKey) 
		{
			ExecuteMovement ();
		}
        if (mouseClicControlled) 
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit[] hits = Physics.RaycastAll(ray, 2000f, layer_mask);
                if (hits.Length > 0) {
                    //setPath(hits[0].point);
				}

            }
		}
		
	}

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
        anim.SetBool("Run", true);
        isMoving = true;
    }

    public void stopMoving()
    {
        anim.SetBool("Run", false);
        isMoving = false;
    }

}
