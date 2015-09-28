using UnityEngine;
using System.Collections;

public class simpleEnemyMover : MonoBehaviour {
	
	private Vector3 homeTo;
	private NavMeshAgent agent;
	private float lastPathUpdate = 1;
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		homeTo = GameObject.FindGameObjectWithTag ("Player").transform.position;
		agent.destination = homeTo;
	}
	
	void Update(){
		if(Time.fixedTime - lastPathUpdate > 2){
			homeTo = GameObject.FindGameObjectWithTag ("Player").transform.position;
			if(Vector3.Distance(homeTo, agent.destination) > 5) 
			{
				agent.destination = homeTo;
			}
			lastPathUpdate = Time.fixedTime;
		}
	}
}