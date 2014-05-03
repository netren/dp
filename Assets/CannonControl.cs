using UnityEngine;
using System.Collections;

public class CannonControl : MonoBehaviour {
	public GameObject smoke;
	// Use this for initialization
	void Start () {
        DataCenter.Instance.RefreshLive();
        DataCenter.Instance.RefreshScore();
        DataCenter.Instance.RefreshCombo();		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CheckFire()
    {
		if (mNeedFire == false )
		{
			return;
		}
        Fire();
    }

    void Fire()
    {
        Debug.Log("Fire");
        mCurrCoolDown = CoolDownTime;
        mNeedFire = false;
        CreateBullet();
        DataCenter.Instance.IncreseScore(-1);
		GameObject sk = (GameObject)Instantiate(smoke, transform.position, transform.rotation);
		sk.transform.eulerAngles =new Vector3(0,0,sk.transform.eulerAngles.z - 90);
    }

    void CreateBullet()
    {
        if (BulltetType == 1)
        {
            GameObject bullet = (GameObject)Instantiate(Bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            Rigidbody2D rigidBody = bullet.GetComponent<Rigidbody2D>();
            if (rigidBody)
            {
                float ang = transform.eulerAngles.z * Mathf.Deg2Rad;
                rigidBody.AddForce(new Vector2(Mathf.Cos(ang) * Force, Mathf.Sin(ang) * Force));

                bullet.transform.position.Set(Mathf.Cos(ang) * 1, Mathf.Sin(ang) * 1, 0);
                audio.Play();
            }
        }
        else if (BulltetType == 2)
        {
            int[] v = {0,-10,10,-20, 20};
            for (int i =0; i < v.Length; ++i)
            {
                //Quaternion q = transform.rotation;
                //q.eulerAngles.Set(0, 0, q.eulerAngles.z + v[i]);
                float CannonLength = 1F;
                GameObject bullet = (GameObject)Instantiate(Bullet, new Vector3(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad) * CannonLength, Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad) * CannonLength, transform.position.z), transform.rotation);
                Rigidbody2D rigidBody = bullet.GetComponent<Rigidbody2D>();
                if (rigidBody)
                {
                    float ang = (transform.eulerAngles.z + v[i])* Mathf.Deg2Rad ;
                    rigidBody.AddForce(new Vector2(Mathf.Cos(ang) * Force, Mathf.Sin(ang) * Force));

                    //bullet.transform.position.Set(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad), 0);
                    audio.Play();
                }
            }
            
        }
    }

	void FixedUpdate() 
	{
        if (!DataCenter.Instance.isAlive())
        {
            if (Input.GetButton("Fire1") && Time.fixedTime - DataCenter.Instance.getGameOverTime() > 1)
            {
                DataCenter.Instance.Reset();
                   Application.LoadLevel(0);
 
            }
            return;
        }
		//Debug.Log (Time.fixedDeltaTime);
		//Debug.Log (transform.eulerAngles.z);
		if (Input.GetButton("Fire1"))
		{
			if (isLastKeyDown == false)
			{
				mNeedFire = true;
				isLastKeyDown = true;
			}
			//Debug.Log("fire1");
			//Debug.Log(Input.mousePosition);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float dist;


			if (screenPlane.Raycast(ray, out dist))
			{
				Vector3 point = ray.GetPoint(dist);
				targetRotateDir = point - transform.position;
				targetRotateDir.Normalize();
				//	Debug.Log(point);
				
				targetAngle = Mathf.Atan2(targetRotateDir.y, targetRotateDir.x);
				targetAngle = targetAngle * Mathf.Rad2Deg;
				if (targetAngle < 0)
				{
					if (targetAngle > -90)
					{
						targetAngle = RotateMin;
					}
					else
					{
						targetAngle = RotateMax;
					}
				}
				if (targetAngle > RotateMax)
				{
					targetAngle = RotateMax;
				}
				if (targetAngle < RotateMin)
				{
					targetAngle = RotateMin;
				}

				Debug.Log(targetAngle);
				//Debug.Log(string.Format("{0} {1} {2}", point, targetRotateDir, targetAngle));
				//transform.eulerAngles = new Vector3(0,0,angle);
			}

		}
		else{
			isLastKeyDown = false;
		}
        
        if (mCurrCoolDown > 0F)
        {
            mCurrCoolDown -= Time.fixedDeltaTime;
            if (mCurrCoolDown <= 0F)
            {
                mCurrCoolDown = 0F;
                if (isLastKeyDown)
                {
                    mNeedFire = true;
                }
                CheckFire();
				return ;
            }
        }

		float delta = targetAngle - transform.eulerAngles.z;
		if (Mathf.Abs(delta) < 0.5)
		{
			transform.eulerAngles = new Vector3(0,0,targetAngle);
            if (mCurrCoolDown <= 0)
            {
				if (isLastKeyDown)
				{
					mNeedFire = true;
				}
                CheckFire();
            }
            
		}
		else
		{
			float rotateSpeed = 360 / CoolDownTime;
			if (mCurrCoolDown > 0)
			{
				rotateSpeed = Mathf.Abs(delta / mCurrCoolDown );
			}
			rotateSpeed = RotateSpeed;
			Debug.Log(rotateSpeed);
			float inc = Mathf.Sign(delta) * rotateSpeed * Time.fixedDeltaTime;

			if (Mathf.Abs(inc) > Mathf.Abs(delta))
			{
				transform.eulerAngles = new Vector3(0,0,targetAngle);;
			}
			else
			{
				transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z + inc);
			}
            
		}
        
	}



    public GameObject Bullet;
	public float RotateMax;
	public float RotateMin;
	public float RotateSpeed;
	public float CoolDownTime;
    public float FireTime;
	public float Force;
    public int BulltetType;

	bool isLastKeyDown = false;
	float mCurrCoolDown = 0;
	bool mNeedFire = false;
	Plane screenPlane = new Plane(new Vector3(0,0,-1), new Vector3(0,0,0));
	Vector3 targetRotateDir = new Vector3(0,0,0);
	float targetAngle = 90;
}
