using UnityEngine;
using System.Collections;

public class BodyHit : MonoBehaviour {

	private float scale = 1;
	private Vector3 sSm;
	private float sRcc; //
	private CapsuleCollider refT;
	public GameObject explosion;
	private int hitCounter = 0;

	void Awake()
	{
		sSm = this.gameObject.transform.localScale;
		sSm = new Vector3 (sSm.x, sSm.y, sSm.z);
		refT = this.GetComponent<CapsuleCollider> ();
		sRcc = refT.radius;
	}

	void OnTriggerEnter ( Collider other)
	{
		if (other.tag == "shot") {
			Destroy (other.gameObject);
			if (explosion != null && hitCounter > 3) {
				Instantiate (explosion, transform.position, transform.rotation);
				Destroy(this.gameObject);
			}
			hitCounter++;
		}
	}


	void OnCollisionEnter(Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay (contact.point, contact.normal, Color.white);
		}
	}

	void OnCollisionStay(Collision collisionInfo) {
		foreach (ContactPoint contact in collisionInfo.contacts) {
			Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
		}
	}


	// Update is called once per frame
	void Update () {

	}
}
