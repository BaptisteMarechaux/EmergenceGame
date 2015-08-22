using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SunManager : MonoBehaviour {

    [SerializeField]
    ParticleSystem rainParticle;

    [SerializeField]
    Transform rainParticleTransform;

    [SerializeField]
    Transform SelectorPosition;

    [SerializeField]
    GameObject[] waterSources;

    [SerializeField]
    int disponibleWaterSources;

    [SerializeField]
    Text waterSourcesDispoText;

    bool startedPlacingWater;
    Vector3 selectedPosition;
    int selectedWaterSourceToPlace;

    float t;
    int layerMask = 1 << 9;

    //Disparition
    [SerializeField]
    ParticleSystem smokeParticle;

    [SerializeField]
    bool canMakeDisappear;

    [SerializeField]
    SelectorScript selectorScript;


    // Use this for initialization
    void Start () {
        waterSourcesDispoText.text = "Sources Dispo : " + disponibleWaterSources;
        t = 0;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.R))
        {
            //Make it rain
            rainParticle.Play();
        }

        if(!selectorScript.touchingWater)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!startedPlacingWater)
                {
                    if (disponibleWaterSources > 0)
                    {
                        for (int i = 0; i < waterSources.Length; i++)
                        {
                            if (!waterSources[i].activeSelf)
                            {
                                startedPlacingWater = true;
                                selectedPosition = SelectorPosition.position;
                                selectedWaterSourceToPlace = i;
                                Debug.Log(selectedWaterSourceToPlace);
                                rainParticleTransform.position.Set(selectedPosition.x, rainParticle.transform.position.y, selectedPosition.z);
                                rainParticle.Play();
                                break;
                            }
                        }
                        disponibleWaterSources--;
                        waterSourcesDispoText.text = "Sources Dispo : " + disponibleWaterSources.ToString();
                    }
                    else
                    {

                    }
                }


            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                selectorScript.Process();
                disponibleWaterSources++;
                waterSourcesDispoText.text = "Sources Dispo : " + disponibleWaterSources.ToString();
            }
                
        }
        

        if (startedPlacingWater)
        {
            if(PlacingWater())
            {
                waterSources[selectedWaterSourceToPlace].SetActive(true);
                waterSources[selectedWaterSourceToPlace].transform.position = selectedPosition;
            }
        }

    }

    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, layerMask))
        {
            Debug.DrawLine(ray.origin, hit.point);
            //if (hit.transform == truc) ;
            SelectorPosition.position = new Vector3(hit.point.x, SelectorPosition.position.y, hit.point.z);
            
        }
    }

    bool PlacingWater()
    {
        t += Time.deltaTime;
        if(t>=rainParticle.duration*0.5f)
        {
            rainParticle.Stop();
            startedPlacingWater = false;
            t = 0;
            return true;
        }

        return false;
    }
}
