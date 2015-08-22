using UnityEngine;
using System.Collections;

public class PlacementGrowth : MonoBehaviour {
    [SerializeField]
    Vector3 minimummScale;

    [SerializeField]
    Vector3 maximumScale;

    void OnEnable()
    {
        transform.localScale = minimummScale;
    }

	// Update is called once per frame
	void Update () {
        if(transform.localScale.x >= maximumScale.x*0.96f)
        {
            
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, maximumScale, Time.deltaTime);
        }
        
	}
}
