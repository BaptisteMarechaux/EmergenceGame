using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	GameObject cube;
	Vector3 destination;
	float t;
	float childTime;
	public float fightTime;
	float deathTime;
	bool seeWater;
	bool fight;
	bool canMakeChild;
	GameObject Water;
	bool makeChild;
	public List<GameObject> friends = new List<GameObject>();
	List<GameObject> listRemove = new List<GameObject> ();
	public Material defaultMaterial;
	public Material fightMaterial;
	public Material deadMaterial;
	public GameObject model;

	// Use this for initialization
	void Start () {
		cube = this.gameObject;
		destination = cube.transform.position;
		seeWater = false;
		fight = false;
		makeChild = false;
		canMakeChild = true;
		fightTime = 0;
		deathTime = 0;
		childTime = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (cube.tag != "Dead")
			Action ();
		else
			deathTime += 1f * Time.deltaTime;
		if (deathTime > 6f)
			Destroy (this.gameObject);
	}

	void Action(){
		// Supprime ceux qui sont trop loin
		if(listRemove.Count != 0)
			listRemove.Clear ();
		foreach (GameObject friend in friends) {
			if(Vector3.Distance(cube.transform.position, friend.transform.position) > 3f || friend.tag == "Dead"){
				listRemove.Add(friend);
			}
		}
		foreach (GameObject friend in listRemove) {
			if(friends.Contains(friend))
				friends.Remove(friend);
		}
		if (friends.Count < 3) {
			fight = false;
		}
		if (friends.Count != 2) {
			makeChild = false; 
		}
		// Enfants
		childTime += 1f*Time.deltaTime;
		if (makeChild == true && Random.Range(0.0f, 1.0f)> 0.5f && childTime > 1.5f && canMakeChild && !fight) {
			int proximityFriends = 0;
			foreach (GameObject friend in friends) {
				if(Vector3.Distance(cube.transform.position, friend.transform.position) < 2f){
					proximityFriends += 1;
				}
			}
			if(proximityFriends >= 2){
				GameObject.Instantiate(this);
				childTime = 0f;
				canMakeChild = false;
			}
		}
		// Changement de destination
		t += Random.Range(0.5f, 3f)*Time.deltaTime;
		if (t > 2) {
			destination = new Vector3 (cube.transform.position.x + Random.Range (-4.0f, 4.0f)
			                           , 1f, cube.transform.position.z + Random.Range (-4.0f, 4.0f));
			t=0.0f;
		}
		// Eau
		if (seeWater && Water != null) {
			if(Vector3.Distance(this.transform.position, Water.transform.position) > 5){
				destination = Water.transform.position;
			}
		}
		// Combat
		if (fight && friends.Count != 0) {
			fightTime += 0.5f * Time.deltaTime;
			int index = Random.Range (0, friends.Count - 1);
			//Debug.Log (index.ToString () + friends.Count.ToString ());
			if (index < friends.Count) {
				destination = friends [index].transform.position;
			}
			model.GetComponent<Renderer> ().material = fightMaterial;
			if (Random.Range (0.0f, 1.0f) > 0.5f && fightTime > 2.5f) {
				cube.tag = "Dead";
				model.tag = "Dead";
				model.GetComponent<Renderer> ().material = deadMaterial;
				/*Destroy (this);*/
			}
		} else {
			model.GetComponent<Renderer> ().material = defaultMaterial;
			fightTime = 0;
		}
		// Déplacements
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
			if(!friends.Contains(collision.gameObject))
				friends.Add(collision.gameObject);

		}
		if (friends.Count >= 4) {
			//Debug.Log(friends.Count.ToString());
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
		/*if (collision.collider.tag == "Character") {
			friends.Remove(collision.gameObject);
		}*/
		/*if (friends.Count < 3) {
			fight = false;
		}*/
		/*if (friends.Count != 2) {
			makeChild = false; 
		}*/
	}
}
