using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//adapted from https://www.youtube.com/watch?v=ExRQAEm4jPg
public class Bird : MonoBehaviour {

	[SerializeField]
	Transform[] waypoints;

	[SerializeField]
	float moveSpeed = 2f;

	int waypointIndex = 0;
	private bool flyback = false;

	void Start () {
		transform.position = waypoints [waypointIndex].transform.position;
	}
	

	void FixedUpdate()
	{
		if (DialogueManager.instance.IsDisplayingDialogue() || IngameMenuBehaviour.instance.IsMenuActive())
		{
			return;
		}

		transform.position = Vector2.MoveTowards (transform.position,
												waypoints[waypointIndex].transform.position,
												moveSpeed * Time.deltaTime);
		

		if (transform.position == waypoints [waypointIndex].transform.position) {
			if(!flyback)
			    waypointIndex += 1;
			else 
				waypointIndex -= 1;
		}
		
		if (waypointIndex == waypoints.Length)
		{
			Flip();
			flyback = true;
			waypointIndex -= 1;
		}
		
		if (waypointIndex == -1)
		{
			Flip();
			flyback = false;
			waypointIndex += 1;
		}
			
	}
	
	private void Flip()
	{
		var scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

}
