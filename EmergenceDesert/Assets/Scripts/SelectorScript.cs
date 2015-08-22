using UnityEngine;
using System.Collections;

public class SelectorScript : MonoBehaviour {
    public bool touchingWater;

    GameObject touchedWaterObject;

	// Use this for initialization
	void Start () {
	    
	}

    void OnCollisionStay(Collision col)
    {
        Debug.Log(col.gameObject.name);
        if(col.gameObject.layer == 8) //On touche l'eau
        {
            touchingWater = true;
            touchedWaterObject = col.gameObject;
        }

        if(col.gameObject.layer == 11) //On touche un personnage
        {

        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.layer == 8) //On touche l'eau
        {
            touchingWater = false;
            touchedWaterObject = null;
        }

        if (col.gameObject.layer == 11) //On touche un personnage
        {

        }
    }

    public void Process()
    {
        if (touchingWater)
        {
            if (touchedWaterObject != null)
            {
                touchedWaterObject.transform.parent.gameObject.SetActive(false);
            }
            touchingWater = false;
                    
        }
    }
}
