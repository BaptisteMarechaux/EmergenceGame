using UnityEngine;
using System.Collections;

public class WaterPoint : MonoBehaviour {
    [SerializeField]
    int countryNumber;
    public int CountryNumber{
        get { return countryNumber; }
        set {countryNumber = value; }
    }

    [SerializeField]
    Color waterColor;
    [SerializeField]
    Renderer waterRenderer;

    public bool activated = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
