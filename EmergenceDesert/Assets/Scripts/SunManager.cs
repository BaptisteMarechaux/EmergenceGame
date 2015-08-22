using UnityEngine;
using System.Collections;

public class SunManager : MonoBehaviour {

    [SerializeField]
    ParticleSystem rainParticle;

    [SerializeField]
    Transform SelectorPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.R))
        {
            //Make it rain
            rainParticle.Play();
        }
            

    }

    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500))
        {
            Debug.DrawLine(ray.origin, hit.point);
            //if (hit.transform == truc) ;
            SelectorPosition.position = new Vector3(hit.point.x, SelectorPosition.position.y, hit.point.z);
        }
    }
}
