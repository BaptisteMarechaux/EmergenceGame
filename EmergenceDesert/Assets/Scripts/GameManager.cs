using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	GameObject cube;
	Vector3 destination;
	float t;
	float childTime;
	bool seeWater;
	bool fight;
	bool canMakeChild;
	GameObject Water;
	bool makeChild;
	List<GameObject> friends = new List<GameObject>();
	List<GameObject> listRemove = new List<GameObject> ();
	public Material fightMaterial;

	// Use this for initialization
	void Start () {
		cube = this.gameObject;
		destination = cube.transform.position;
		seeWater = false;
		fight = false;
		makeChild = false;
		canMakeChild = true;
	}

	// Update is called once per frame
	void Update () {
		if(listRemove.Count != 0)
			listRemove.Clear ();
		foreach (GameObject friend in friends) {
			if(Vector3.Distance(cube.transform.position, friend.transform.position) > 2){
				listRemove.Add(friend);
			}
		}
		foreach (GameObject friend in listRemove) {
			if(friends.Contains(friend))
				friends.Remove(friend);
		}
		childTime += 1f*Time.deltaTime;
		if (makeChild == true && Random.Range(0.0f, 1.0f)> 0.5f && childTime > 1f && canMakeChild) {
			GameObject.Instantiate(this);
			childTime = 0f;
			canMakeChild = false;
		}
		t += Random.Range(0.5f, 3f)*Time.deltaTime;
		if (t > 2) {
			destination = new Vector3 (cube.transform.position.x + Random.Range (-4.0f, 4.0f)
		                           , 1f, cube.transform.position.z + Random.Range (-4.0f, 4.0f));
			t=0.0f;
		}
		if (seeWater && Water != null) {
			if(Vector3.Distance(this.transform.position, Water.transform.position) > 5){
				destination = Water.transform.position;
			}
		}
		if (fight && friends.Count != 0) {
			int index = Random.Range(0,friends.Count-1);
			Debug.Log(index.ToString() + friends.Count.ToString());
			if(index < friends.Count){
				destination = friends[index].transform.position;
			}
		}
		cube.transform.rotation = Quaternion.Lerp (cube.transform.rotation, Quaternion.identity, 1.5f * Time.deltaTime);
		cube.transform.position = Vector3.Lerp(cube.transform.position
		        , destination, 1f*Time.deltaTime);
	}

	void OnTriggerEnter(Collider collider){
		if(collider.name == "Water"){
			seeWater = true;
			Water = collider.gameObject;
		}

	}

	void OnCollisionEnter(Collision collision){
		if (collision.collider.tag == "Character") {
			friends.Add(collision.gameObject);

		}
		if (friends.Count >= 5) {
			fight = true;
		}
		if (friends.Count == 2) {

			makeChild = true;
		}else{
			makeChild = false;
		}
	}

	void OnTriggerExit(Collider collider){
		if(collider.name == "Water"){
			seeWater = false;
			Water = null;
		}

	}

	void OnCollisionExit(Collision collision){
		if (collision.collider.tag == "Character") {
			friends.Remove(collision.gameObject);
		}
		if (friends.Count < 5) {
			fight = false;
		}
		if (friends.Count != 2) {
			makeChild = false; 
		}
	}
}
