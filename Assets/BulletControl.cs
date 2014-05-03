using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {
	
		
	bool isRendering = false;
	float lastTime = 0;
	float currTime = 0;

	void Start()
	{
        
	}
	void Update()
	{
		isRendering=(currTime == 0 || currTime!=lastTime)?true:false;
		lastTime=currTime;

		if (!isRendering)
		{
			Destroy(gameObject);
            DataCenter.Instance.ResetCombo();
			
		}
	}
	
	
	void OnWillRenderObject()
	{
		currTime=Time.time;
	}

	
	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.tag == "enemyentity")
        {
            
            EnemyControl control = other.gameObject.GetComponent<EnemyControl>();
            control.UpdateHitPoint();
            DataCenter.Instance.IncreseCombo(1);
			
			
            Destroy(gameObject);

        }
       Debug.Log(other.gameObject.name);
        //Destroy(other.gameObject);
    }
}
