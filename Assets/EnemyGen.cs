using UnityEngine;
using System.Collections;

public class EnemyGen : MonoBehaviour {
    static float GameSpeed = 1;
    static float LastSpeedUpdateTime = 0;
    static float IncreaseSpeedInteval = 10;
	// Use this for initialization
	void Start () {
        GenNextTime();
        GameSpeed = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void GenNextTime()
    {
		mNextGenerateTime = Random.Range(NextGenRange.x * 1000, NextGenRange.y * 1000) / 1000F;
    }

	void FixedUpdate()
	{
        if (Time.fixedTime - LastSpeedUpdateTime >= IncreaseSpeedInteval)
        {
            GameSpeed += 0.3F;
            if (GameSpeed >= 2.5F)
            {
                GameSpeed = 2.5F;
            }
            Debug.Log(GameSpeed);
            LastSpeedUpdateTime = Time.fixedTime;
        }
        if (!DataCenter.Instance.isAlive())
        {
            return;
        }
        mNextGenerateTime -= Time.fixedDeltaTime;
        if (mNextGenerateTime <= 0)
        {

            Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			pos.x = Random.Range(pos.x * 100, -pos.x * 100) / 100;
            GameObject enemy = (GameObject)Instantiate(Enemy, pos, transform.rotation);
			if (Random.Range(0,10) >1) 
			{
            	enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -Random.Range(ForceRange.x, ForceRange.y) * GameSpeed));
			}
			else
           	{
                enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -Random.Range(ForceRange.x, ForceRange.y) * 1.5F * GameSpeed));
			}
			

            GenNextTime();
        }
		
	}
	public GameObject Enemy;
	public Vector2 NextGenRange;

	public Vector2 ForceRange;
    float mNextGenerateTime;
}
