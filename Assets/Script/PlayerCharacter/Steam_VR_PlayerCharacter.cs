using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Steam_VR_PlayerCharacter : MonoBehaviour {

    //玩家生命值
    private float maxHP = 100;
    public float Health;
    public GameObject Text;

    public GameObject PlayerCharacter;

    //是否存活
    public bool Life;
    
    public float ReHealth;

    private float damageBloodAmount = 3;

    private float maxBloodIndication = 0.5f;

    // Use this for initialization
    void Start ()
    {
        //初始化变量
        Health = maxHP;
        Life = true;
        Text.SetActive(false);
        ReHealth = 5.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //血量上限
        if (Health > maxHP)
        {
            Health = maxHP;
        }
        //溅血效果控制
        BleedBehavior.minBloodAmount = maxBloodIndication * (maxHP - Health) / maxHP;
        
        //当玩家血量小于或等于0并且在存活状态时，玩家死亡
        if (Health <= 0 && Life == true)
        {
            //Time.timeScale = 0;
            Life = false;
            Text.SetActive(true);
            //ReStart();
            PlayerCharacter.GetComponent<BleedBehavior>().enabled = false;
            //SceneManager.LoadScene("End");
            StartCoroutine(GameOver());
        }

        //当玩具血量少于一百并且还存活时，每秒回复五滴血
	    if (Health < 100 && Life == true)
	    {
	        Health += ReHealth * Time.deltaTime;
	    }

        //print(Health);
    }

    //允许外部修改玩家血量
    public void SetHealth(float Health)
    {
        this.Health = Health;
    }
    //游戏伤害
    public void Damage(int amount)
    {
        BleedBehavior.BloodAmount += Mathf.Clamp01(damageBloodAmount * amount / Health);
        Health -= amount;
        BleedBehavior.minBloodAmount = maxBloodIndication * (maxHP - Health) / maxHP;
    }

    //跳转结束界面
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("End");
    }
}

