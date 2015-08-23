using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Rotate : MonoBehaviour {
    [SerializeField]
    Vector3 axis;
    [SerializeField]
    Transform trans;
    [SerializeField]
    bool needNight;
    [SerializeField]
    GlobalFog gbFog;

    [SerializeField]
    float fogMax = 2.0f;
    [SerializeField]
    float gofMin = 0.0f;

    float fogTarget;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        trans.Rotate(axis, Time.deltaTime * 7.5f);
        if(trans.rotation.eulerAngles.x < 180)
        {
            if (needNight)
            {
                fogTarget = gofMin;
                
                
            }
        }
	    if(trans.rotation.eulerAngles.x > 180)
        {
            if(needNight)
            {
                fogTarget = fogMax;
            }
        }
        if(trans.rotation.eulerAngles.x >= 360)
        {
            
        }

        gbFog.heightDensity = Mathf.Lerp(gbFog.heightDensity, fogTarget, Time.deltaTime);
    }
}
