using UnityEngine;
using System.Collections;

public class BlockLife : MonoBehaviour {

	float lifeTime;
	int blockFriends;
	float timeWIthFriends;

	// Use this for initialization
	void Start () {
		lifeTime = Random.Range (20.0f, 35.0f);
	}
	
	// Update is called once per frame
	void Update () {
		lifeTime -= 1.0f * Time.deltaTime;
		if (lifeTime <= 0.0f) {
			Destroy(this.gameObject);
		}
		if (blockFriends >= 3) {
			if(Random.Range(0.0f,1.0f) > 0.25f){
				GameObject littleCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				littleCube.AddComponent(typeof(Rigidbody));
				littleCube.transform.localScale = new Vector3(Random.Range (1.5f, 3.0f), Random.Range (1.5f, 3.0f), Random.Range (1.5f, 3.0f));
				littleCube.GetComponent<Rigidbody>().mass = 2 * (littleCube.transform.localScale.x + littleCube.transform.localScale.y +littleCube.transform.localScale.z);
				littleCube.AddComponent(typeof(BlockLife));
				littleCube.tag = "Block";
				littleCube.transform.position = this.transform.position;
				Destroy(this.gameObject);
			}
		}
	}

	void OnTrigerEnter(Collider collider){
		if (collider.tag == "Block") {
			blockFriends += 1;
		}
	}

	void OnTrigerExit(Collider collider){
		if (collider.tag == "Block") {
			blockFriends -= 1;
		}
	}
}
