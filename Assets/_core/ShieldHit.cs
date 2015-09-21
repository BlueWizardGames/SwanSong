using UnityEngine;
using System.Collections;

public class ShieldHit : MonoBehaviour {

	private float scale = 1;
	private Vector3 sSm;
	private float sRcc; //
	private CapsuleCollider refT;
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
			scale -= 0.05f;
		}

	}
	// Update is called once per frame
	void Update () {
		scale += (0.06f * Time.deltaTime);
		scale = Mathf.Min (scale, 1.1f);
		this.gameObject.transform.localScale = sSm * scale;
		refT.radius = sRcc * scale;
	}
}
