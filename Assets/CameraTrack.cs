using UnityEngine;
using System.Collections;

public class CameraTrack : MonoBehaviour {

	public Transform player;
	// Use this for initialization
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	private Camera thisC;

	void Start ()
	{
		thisC = GetComponent<Camera>();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		if (player)
		{
			Vector3 point = thisC.WorldToViewportPoint(player.position);
			Vector3 delta = player.position - thisC.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); 
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}

}
