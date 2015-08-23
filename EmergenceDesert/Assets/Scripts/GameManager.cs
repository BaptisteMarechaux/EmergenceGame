using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	GameObject cube;
	Vector3 destination;
	public GameObject Water;
	public GameObject Food;
	float t;
	float childTime;
	public float fightTime;
	float deathTime;
	bool seeWater;
	bool fight;
	bool canMakeChild;
	bool makeChild;
	bool eatFood;
	public List<GameObject> friends = new List<GameObject>();
	List<GameObject> listRemove = new List<GameObject> ();
	public Material defaultMaterial;
	public Material fightMaterial;
	public Material deadMaterial;
	public Material blockMaterial;
	public GameObject model;
	public ParticleSystem blood;
	public ParticleSystem waterParticle;
	public float lifeTime;
	public CountScript countScript;
	List<GameObject> nearBlocks = new List<GameObject>();
	float buildTime;

	float blockTime;

    [SerializeField]
    Animator animator;

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
		buildTime = 0;
		blood.enableEmission = false; 
		waterParticle.enableEmission = false; 
		lifeTime = 100f;
	}

	// Update is called once per frame
	void FixedUpdate () {
		// Mort
		if (cube.tag != "Dead")
			Action ();
		else
			deathTime += 1f * Time.deltaTime;
		if (deathTime > 8f)
			Destroy (this.gameObject);
		// Création des blocks
		blockTime += 1f * Time.deltaTime;
		if (blockTime > Random.Range (10.0f, 20.0f)) {
			if(Random.Range(0.0f,1.0f) > 0.4f){
				GameObject littleCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				littleCube.AddComponent(typeof(Rigidbody));
				littleCube.transform.localScale = new Vector3(Random.Range (0.3f, 2.0f), Random.Range (0.3f, 2.0f), Random.Range (0.3f, 2.0f));
				littleCube.GetComponent<Rigidbody>().mass = 2 * (littleCube.transform.localScale.x + littleCube.transform.localScale.y +littleCube.transform.localScale.z);
				littleCube.GetComponent<Renderer>().material = blockMaterial;
				littleCube.AddComponent(typeof(BlockLife));
				littleCube.tag = "Block";
				littleCube.transform.position = cube.transform.position;
			}
			blockTime = 0;
		}
	}

	void Action(){
		// Construction
		if(listRemove.Count != 0)
			listRemove.Clear ();
		foreach(GameObject block in nearBlocks){
			if(block != null && Vector3.Distance(cube.transform.position, block.transform.position) > 5f){
				listRemove.Add(block);
			}
		}
		foreach(GameObject block in listRemove){
			if(nearBlocks.Contains(block))
				nearBlocks.Remove(block);
		}
		if (nearBlocks.Count >= 4) {
			buildTime += 1f*Time.deltaTime;
			if(buildTime >= 2f){
				if(listRemove.Count != 0)
					listRemove.Clear ();
				foreach(GameObject block in nearBlocks){
					Destroy(block);
					//listRemove.Add(block);
				}/*
				foreach(GameObject block in listRemove){
					if(nearBlocks.Contains(block))
						nearBlocks.Remove(block);
					Destroy(block);
				}*/
				nearBlocks= new List<GameObject>();

				/*GameObject littleCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				littleCube.AddComponent(typeof(Rigidbody));
				littleCube.transform.localScale = new Vector3(Random.Range (1.5f, 3.0f), Random.Range (1.5f, 3.0f), Random.Range (1.5f, 3.0f));
				littleCube.GetComponent<Rigidbody>().mass = 2 * (littleCube.transform.localScale.x + littleCube.transform.localScale.y +littleCube.transform.localScale.z);
				littleCube.GetComponent<Renderer>().material = blockMaterial;
				littleCube.transform.position = cube.transform.position;*/
				GameObject littleCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				littleCube.transform.localScale = new Vector3(Random.Range (1f, 3f), Random.Range (0.01f, 0.1f), Random.Range (1f, 3f));
				littleCube.transform.rotation = new Quaternion(0f,  1f, 0f, Mathf.PI * (Random.Range (0f, 360f))/180);
				littleCube.GetComponent<Renderer>().material = new Material(blockMaterial);
				littleCube.GetComponent<Renderer>().material.color = new Color(Random.Range (0.0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
				littleCube.AddComponent(typeof(BlockLife));
				littleCube.transform.position = new Vector3(cube.transform.position.x, 0.1f, cube.transform.position.z);
				buildTime = 0;
				//Debug.Log(nearBlocks.Count.ToString() + "  " + buildTime);
			}
		} else
			buildTime = 0;
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
		if (makeChild == true && Random.Range(0.0f, 1.0f)> 0.5f && childTime > 1.5f && canMakeChild && !fight && !eatFood) {
			int proximityFriends = 0;
			foreach (GameObject friend in friends) {
				if(Vector3.Distance(cube.transform.position, friend.transform.position) < 2f){
					proximityFriends += 1;
				}
			}
			if(proximityFriends >= 2){
				GameManager newPerso = GameObject.Instantiate(this);
				countScript.persoStock.Add(newPerso.gameObject);
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
            animator.SetFloat("hazard", Random.Range(0.0f, 1f));
            //Debug.Log(animator.GetFloat("hazard"));
		}
		// Eau
		if (seeWater && Water != null) {
			if(Vector3.Distance(this.transform.position, Water.transform.position) > 20){
				Water = null;
				seeWater = false;
				waterParticle.enableEmission = false;
			}else{
				if(Vector3.Distance(this.transform.position, Water.transform.position) > 12){
					destination = Water.transform.position;
				}
				if(Vector3.Distance(this.transform.position, Water.transform.position) < 5)
				{
					waterParticle.enableEmission = true; 
				}else
					waterParticle.enableEmission = false;
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
			blood.enableEmission = true;
			// Mort
			if (Random.Range (0.0f, 1.0f) > 0.4f && fightTime > 2f) {
				cube.tag = "Dead";
				model.tag = "Dead";
				model.GetComponent<Renderer> ().material = deadMaterial;
				blood.enableEmission = false;
				waterParticle.enableEmission = false;
				/*Destroy (this);*/
			}
		} else {
			model.GetComponent<Renderer> ().material = defaultMaterial;
			blood.enableEmission = false;
			fightTime = 0;
		}
		// Nourriture
		if (eatFood && Food != null) {
			if(Vector3.Distance(this.transform.position, Food.transform.position) > 10f){
				Food = null;
				eatFood = false;
			}else{
				if(Vector3.Distance(this.transform.position, Food.transform.position) < 2f){
					Food.GetComponent<FoodProperty>().life -= 1;
				}
				destination = Food.transform.position;
			}
		}
		// Déplacements
		cube.transform.rotation = Quaternion.Lerp (cube.transform.rotation, Quaternion.identity, Quaternion.Angle(cube.transform.rotation, Quaternion.identity)/1.5f * Time.deltaTime);
		if (Quaternion.Angle (cube.transform.rotation, Quaternion.identity) > 20f) {
			//cube.transform.rotation = Quaternion.Lerp (cube.transform.rotation, Quaternion.identity, Quaternion.Angle(cube.transform.rotation, Quaternion.identity)/5 * Time.deltaTime);
			Debug.Log(this.gameObject.name);
		}
		cube.transform.position = Vector3.Lerp(cube.transform.position
		                                       , destination, 0.5f*Time.deltaTime);
	}

	void OnTriggerEnter(Collider collider){
		if(collider.tag == "Water"){
			seeWater = true;
			Water = collider.gameObject;
		}
		if(collider.tag == "Food"){
			eatFood = true;
			Food = collider.gameObject;
		}
		if(collider.tag == "Block"){
			if(!nearBlocks.Contains(collider.gameObject))
				nearBlocks.Add(collider.gameObject);
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
		if(collider.name == "Food"){
			eatFood = false;
			Food = null;
		}
		if(collider.tag == "Block"){
			if(nearBlocks.Contains(collider.gameObject))
				nearBlocks.Remove(collider.gameObject);
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
