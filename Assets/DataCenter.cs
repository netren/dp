using UnityEngine;
using System.Collections;

sealed public class DataCenter : MonoBehaviour
{
    private DataCenter() { }
    public static readonly DataCenter Instance = new DataCenter();

    private int mScore = 0;
    private int mCombo = 0;
    private int mLive = 3;
    private float mGameOverTime = 0;
    GameObject mComboText = null;
    GameObject mScoreText = null;
    GameObject mLifeText = null;

    public void Reset()
    {
        mScore = 0;
        mCombo = 0;
        mLive = 3;
        mGameOverTime = 0;
    }

    public void RefreshScore()
    {
        if (mScoreText == null)
        {
            mScoreText =  GameObject.Find("ScoreLabel");
        }
        
		mScoreText.GetComponent<UILabel>().text = string.Format("·ÖÊý:{0}", mScore);
        
        
    }
	public void IncreseScore(int value)
	{
        mScore += value;
        //if (mScore <0)
        {
          //  mScore = 0;
        }
        
		RefreshScore();
  
	}
    public void RefreshCombo()
    {
        if (mComboText == null)
        {
			mComboText = GameObject.Find("ComboLabel");
        }
        if (mCombo == 0)
        {
			mComboText.GetComponent<UILabel>().alpha = 0.2F;
            //c.a = 0.2F;
            //mComboText.guiText.color = c;
        }
        else
        {
			mComboText.GetComponent<UILabel>().alpha = 1F;
           // Color c = mComboText.guiText.color;
            //c.a = 1F;
            //mComboText.guiText.color = c;
            //mComboText.guiText.text = string.Format("{0} combo", mCombo);
        }
        
      
    }

    public void ResetCombo()
    {
        mCombo = 0;
        RefreshCombo();
        
    }

    public void IncreseCombo(int value)
    {
        mCombo += value;
        RefreshCombo();
    }
    
    public void DecreaseLive(int value)
    {
        if (mLive <=0)
        {
            return;
        }
        mLive -= value;
		RefreshLive();

        if (mLive == 0)
        {
            //Application.LoadLevel(0);
            GameObject obj = GameObject.Find("Game Over");
            if (obj != null)
            {
                obj.GetComponent<GUIText>().enabled = true;
            }
            mGameOverTime = Time.fixedTime;
            
        }
    }
    
    public bool isAlive()
    {
        return mLive > 0;
    }
   
    public float getGameOverTime()
    {
        return mGameOverTime;
    }
    public void RefreshLive()
    {
        if (mLifeText == null)
        {
            mLifeText = GameObject.Find("LifeLabel");
        }
		mLifeText.GetComponent<UILabel>().text = string.Format("ÉúÃü:{0}", mLive);
	
    }
}

