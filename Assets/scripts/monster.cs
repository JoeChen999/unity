using UnityEngine;
using System.Collections;

public class monster : MonoBehaviour {
	int speed = 5;
	public GameObject newobject;
	public GameObject gun;
	public float force = 100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis("Horizontal")*Time.deltaTime*speed;
		float z = Input.GetAxis("Vertical")*Time.deltaTime*speed;
		transform.Translate(x,0,z);
		if (z != 0 || x != 0) {
			animation.enabled = true;
		} else {
			animation.enabled = false;
		}
		if (Input.GetButtonDown ("Fire1")) {
			//Vector3 bulletPos= transform.position;
			GameObject n = Instantiate(newobject,gun.transform.position,transform.rotation) as GameObject;
			Vector3 fwd = transform.TransformDirection(Vector3.forward);
			n.SetActive(true);
			n.rigidbody.AddForce(fwd*force);
			Destroy(n,(float)3.0);
		}
		if (Input.GetKey (KeyCode.Q)) {
			transform.Rotate (0, -100 * Time.deltaTime, 0, Space.Self);
		} else if (Input.GetKey (KeyCode.E)) {
			transform.Rotate (0, 100 * Time.deltaTime, 0, Space.Self);
		}
	}
}
