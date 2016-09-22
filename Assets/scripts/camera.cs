using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {
	// Use this for initialization
	public int killCount=0;
	public GameObject overTxt;
	public GameObject light;
	public GameObject monster;
	private Rect r =new Rect(180,300,120,40);
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		if (killCount == 3 ) {
			light.SetActive(false);
			overTxt.SetActive(true);
			monster.GetComponent<monster>().enabled=false;
			if(GUI.Button(r,"Next")){
				land.width+=2;
				land.height+=2;
				Application.LoadLevel("test");
			}
		}
	}
}
