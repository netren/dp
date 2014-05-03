using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {
	public GameObject bomb;
	bool isRendering = false;
	float lastTime = 0;
	float currTime = 0;
	public static int score = 0; 
	public bool isOver = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		isRendering=(currTime == 0 || currTime!=lastTime)?true:false;
		lastTime=currTime;
		
		if (!isRendering && transform.position.y < 3)
		{
			Destroy(gameObject);
		}
	}

	void OnWillRenderObject()
	{
		currTime=Time.time;
	}

    public void UpdateHitPoint()
    {
        

        DataCenter.Instance.IncreseScore(2);	
		audio.Play();
		GameObject b = (GameObject)Instantiate(bomb, transform.position, transform.rotation);
		
		Destroy(gameObject, 0.1F);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log(gameObject.gameObject.tag);
		//Destroy(other.gameObject);
	}

	void FixedUpdate() 
    {
        if (transform.position.y < 0 && isOver == false) 
        {
            isOver = true;
			DataCenter.Instance.DecreaseLive(1);
        }
    }
}
