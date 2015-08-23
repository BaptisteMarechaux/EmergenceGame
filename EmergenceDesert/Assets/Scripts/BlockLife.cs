using UnityEngine;
using System.Collections;

public class BlockLife : MonoBehaviour {

	float lifeTime;

	// Use this for initialization
	void Start () {
		lifeTime = Random.Range (15.0f, 30.0f);
	}
	
	// Update is called once per frame
	void Update () {
		lifeTime -= 1.0f * Time.deltaTime;
		if (lifeTime <= 0.0f) {
			Destroy(this.gameObject);
		}
	}
}
