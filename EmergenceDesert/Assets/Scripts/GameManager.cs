using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	GameObject cube;
	Vector3 destination;
	float t;
	bool seeWater;
	GameObject Water;

	// Use this for initialization
	void Start () {
		cube = this.gameObject;
		destination = cube.transform.position;
		seeWater = false;
	}
	
	// Update is called once per frame
	void Update () {
		t += Random.Range(0.5f, 3f)*Time.deltaTime;
		if (t > 2) {
			destination = new Vector3 (cube.transform.position.x + Random.Range (-4.0f, 4.0f)
		                           , 1f, cube.transform.position.z + Random.Range (-4.0f, 4.0f));
			t=0.0f;
		}
		if (Water) {
			if(Vector3.Distance(this.transform.position, Water.transform.position) > 5){
				destination = Water.transform.position;
			}
		}
		cube.transform.position = Vector3.Lerp(cube.transform.position
		        , destination, 1f*Time.deltaTime);
	}

	void OnTriggerEnter(Collider collider){
		Debug.Log (this.name);
		if(collider.name == "Water"){
			seeWater = true;
			Water = collider.gameObject;
		}
	}
}
