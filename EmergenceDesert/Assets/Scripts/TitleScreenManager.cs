using UnityEngine;
using System.Collections;

public class TitleScreenManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    public void StartGame()
    {
        Application.LoadLevel(2);
    }
}
