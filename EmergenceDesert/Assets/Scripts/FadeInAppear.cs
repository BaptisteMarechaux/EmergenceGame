using UnityEngine;
using UnityEngine.UI;

public class FadeInAppear : MonoBehaviour {
    [SerializeField]
    Text text;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        text.color = Color.Lerp(text.color, new Color(255, 255, 255, 1), Time.deltaTime);
        if (text.color.a > 0.96)
            enabled = false;
            text.transform.localScale *= 1.0001f;
	}
}
