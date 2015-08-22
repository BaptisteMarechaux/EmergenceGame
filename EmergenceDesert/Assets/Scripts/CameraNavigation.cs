using UnityEngine;
using System.Collections;

public class CameraNavigation : MonoBehaviour {

    [SerializeField]
    float walkSpeed = 5;
    [SerializeField]
    float runSpeed = 10;

    float v, h;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
            transform.Translate(h * runSpeed * Time.deltaTime, 0, v * runSpeed * Time.deltaTime);
        else
            transform.Translate(h * walkSpeed * Time.deltaTime, 0, v * walkSpeed * Time.deltaTime);
    }
}
