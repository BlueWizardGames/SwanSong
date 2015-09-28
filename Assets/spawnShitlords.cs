using UnityEngine;
using System.Collections;

public class spawnShitlords : MonoBehaviour {

	public GameObject Shitlord;
	private float shitlordTimestamp = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.fixedTime - shitlordTimestamp > 1f) {
			UnityEngine.Object.Instantiate(Shitlord, this.transform.position, this.transform.rotation);
			shitlordTimestamp = Time.fixedTime;
		}
	}
}
