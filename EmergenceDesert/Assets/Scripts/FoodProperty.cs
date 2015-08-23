using UnityEngine;
using System.Collections;

public class FoodProperty : MonoBehaviour {

	public int life = 500;
	int definitedLife;

	// Use this for initialization
	void Start () {
		definitedLife = life;
	}
	
	// Update is called once per frame
	void Update() {
		if(life < 0)
		{
			this.gameObject.transform.position = this.gameObject.transform.parent.transform.position;
			life = definitedLife;
			this.gameObject.SetActive(false);
		}
	}
}
