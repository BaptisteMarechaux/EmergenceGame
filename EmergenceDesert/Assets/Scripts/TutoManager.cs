using UnityEngine;
using System.Collections;

public class TutoManager : MonoBehaviour {
    [SerializeField]
    Camera mainCam;

    [SerializeField]
    SunManager sunManager;

    [SerializeField]
    Transform[] tutorialPositions;

    int tutorialIndex = 0;

    bool waterEnabled;

	// Use this for initialization
	void Start () {
        sunManager.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (tutorialIndex != 1 && tutorialIndex != 2 && tutorialIndex!=4)
            sunManager.enabled = false;
        else
            sunManager.enabled = true;

	    if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(tutorialIndex < tutorialPositions.Length-1)
            tutorialIndex += 1;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(tutorialIndex > 0)
                tutorialIndex -= 1;
        }
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, new Vector3(tutorialPositions[tutorialIndex].position.x, mainCam.transform.position.y, tutorialPositions[tutorialIndex].position.z-5), 10*Time.deltaTime);
        
	}

    public void nextPart()
    {

    }
}
