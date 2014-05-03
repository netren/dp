using UnityEngine;
using System.Collections;

public class NewBehaviourScript1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject g = GameObject.Find("UI Root (2D)/Camera/Anchor/Panel/Widget/Button");
        
        if (g)
        {
        //    if (g == gameObject)
            {
                Debug.Log("same");
            }
            UIEventListener.Get(g).onClick = Btncick;
        }
        else
        {
            Debug.LogError(("wrong "));
        }	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Btncick(GameObject btn)
	{
		Debug.Log("licd");
	}
}
