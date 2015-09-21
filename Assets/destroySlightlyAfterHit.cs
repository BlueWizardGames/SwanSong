using UnityEngine;
using System.Collections;

public class destroySlightlyAfterHit : MonoBehaviour {
	
	void OnTriggerEnter ( Collider other)
	{
		Transform v = other.gameObject.transform.parent;
		if( !other.tag.Contains("shot"))
			if( (v == null && other.tag != "Player") 
			   || (v != null && v.gameObject.tag != "Player"))
				Destroy (this.gameObject, 1.1f * Time.smoothDeltaTime);
	}
}
