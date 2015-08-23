using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CountScript : MonoBehaviour {

	public List<GameObject> persoStock;
	public Text countText;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		countText.text = "Walkers : " + persoStock.Count.ToString();
	}
}
