using UnityEngine;
using System.Collections;

public class ExitScript : MonoBehaviour {
    [SerializeField]
    SunManager sunManager;



    void OnEnable()
    {
        sunManager.enabled = false;
    }

    void OnDisable()
    {
        sunManager.enabled = true;
    }

    public void ExitGame()
    {
        Application.LoadLevel(0);
    }

    public void NotExitGame()
    {
        gameObject.SetActive(false);
    }

}
