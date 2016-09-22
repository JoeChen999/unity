using UnityEngine;
using System.Collections;

public class boss : MonoBehaviour {
	public camera c;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision bullet){
		if (bullet.gameObject.name.StartsWith("Sphere")) {
			c.killCount++;
			gameObject.SetActive (false);
			Destroy (bullet.gameObject);
		}
	}
}
