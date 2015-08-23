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
	GameObject[] foodSources;

    [SerializeField]
    int disponibleWaterSources;

	[SerializeField]
	int disponibleFoodSources;

    [SerializeField]
    Text waterSourcesDispoText;

	[SerializeField]
	Text foodSourcesDispoText;

    bool startedPlacingWater;
	bool canPlacingWater;
    Vector3 selectedPosition;
    int selectedWaterSourceToPlace;
	int selectedFoodSourceToPlace;

    float t;
    int layerMask = 1 << 9;

    //Disparition
    [SerializeField]
    ParticleSystem smokeParticle;

    [SerializeField]
    bool canMakeDisappear;

    [SerializeField]
    SelectorScript selectorScript;

    [SerializeField]
    GameObject ExitGameCanvas;


    // Use this for initialization
    void Start () {
        waterSourcesDispoText.text = "Sources Available : " + disponibleWaterSources;
		foodSourcesDispoText.text = "Food Available : " + foodSources.Length.ToString();
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
                                //Debug.Log(selectedWaterSourceToPlace);
								//Debug.Log(selectedPosition.x.ToString());
								rainParticleTransform.position = new Vector3(selectedPosition.x, rainParticleTransform.position.y, selectedPosition.z);
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
			else
			{
				if(Input.GetMouseButton(1)){
					if (disponibleFoodSources > 0 && canPlacingWater)
					{
						for (int i = 0; i < foodSources.Length; i++)
						{
							if (!foodSources[i].activeSelf)
							{
								//startedPlacingFood = true;
								selectedPosition = SelectorPosition.position;
								selectedFoodSourceToPlace = i;
								foodSources[selectedFoodSourceToPlace].SetActive(true);
								//foodSources[selectedFoodSourceToPlace].transform.position = selectedPosition;
								foodSources[selectedFoodSourceToPlace].transform.position = Camera.main.transform.position;
								foodSources[selectedFoodSourceToPlace].GetComponent<Rigidbody>().AddForce(
									new Vector3(selectedPosition.x-Camera.main.transform.position.x,
								            selectedPosition.y-Camera.main.transform.position.y,
								            selectedPosition.z-Camera.main.transform.position.z)*5, ForceMode.Impulse);
								canPlacingWater = false;
								break;
							}
						}
						disponibleFoodSources--;
					}
				}else
					canPlacingWater = true;
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
        
		int foods=0;
		for (int i = 0; i < foodSources.Length; i++)
		{
			if (!foodSources[i].activeSelf)
				foods++;
		}
		foodSourcesDispoText.text = "Sources Dispo : " + foods.ToString();

        if (startedPlacingWater)
        {
            if(PlacingWater())
            {
                waterSources[selectedWaterSourceToPlace].SetActive(true);
                waterSources[selectedWaterSourceToPlace].transform.position = selectedPosition;
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGameCanvas.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, layerMask))
        {
            //Debug.DrawLine(ray.origin, hit.point);
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
