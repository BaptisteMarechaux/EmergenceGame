using UnityEngine;
using System.Collections;

public class CameraNavigation : MonoBehaviour {

    [SerializeField]
    float walkSpeed = 5;
    [SerializeField]
    float runSpeed = 10;
	[SerializeField]
	float scrollSpeed = 6;

    float v, h;

	public bool scrollingDown = false;
	public bool scrollingUp = false;
	Vector3 destination = new Vector3 ();

	// Use this for initialization
	void Start () {
		destination = new Vector3  (transform.position.x, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
            transform.Translate(h * runSpeed * Time.deltaTime, 0, v * runSpeed * Time.deltaTime);
        else
            transform.Translate(h * walkSpeed * Time.deltaTime, 0, v * walkSpeed * Time.deltaTime);

		if (v != 0 || h != 0) {
			destination = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			//speedMove = scrollSpeed/2;
		}

        var d = Input.GetAxis("Mouse ScrollWheel");

		if(scrollingDown == true)
			destination = new Vector3(transform.position.x, transform.position.y-scrollSpeed, transform.position.z);
		if(scrollingUp == true)
			destination = new Vector3(transform.position.x, transform.position.y+scrollSpeed, transform.position.z);
		if (Vector3.Distance (transform.position, destination) < 2)
			destination = new Vector3  (transform.position.x, transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp(transform.position, destination, 2f*Time.deltaTime);
        if (d > 0f) {
			scrollingDown = true;
			scrollingUp = false;
			//transform.Translate(Vector3.down * Time.deltaTime * 30, Space.Self);
		} else {
			if (d < 0f) {
				scrollingUp = true;
				scrollingDown = false;
				//transform.Translate (Vector3.up * Time.deltaTime * 30, Space.Self);
			}else{
				scrollingUp = false;
				scrollingDown = false;
			}
		}
			
    }
}
