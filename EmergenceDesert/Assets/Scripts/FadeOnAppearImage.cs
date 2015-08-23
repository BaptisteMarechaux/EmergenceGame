using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOnAppearImage : MonoBehaviour {
    [SerializeField]
    Text text;

    [SerializeField]
    Image image;

    float t = 0;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        if(t > 5.0f)
        {
            text.color = Color.Lerp(text.color, new Color(text.color.r, text.color.g, text.color.b, 0), Time.deltaTime);
            image.color = Color.Lerp(image.color, new Color(image.color.r, image.color.g, image.color.b, 0), Time.deltaTime);

            if(image.color.a < 0.1f)
            {
                text.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
	}
}
