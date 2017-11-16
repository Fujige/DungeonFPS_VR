using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //粒子效果
    public GameObject Blood;
    public GameObject Rock;

    void OnCollisionEnter(Collision other)
    {
        //如果撞击到的物体标签为Enemy，则销毁子弹同时在碰撞到的位置上实例化血液效果
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            GameObject _blood = Instantiate(Blood,this.transform.position,this.transform.rotation) as GameObject;
            //效果在0.1秒后销毁
            Destroy(_blood, 0.5f);
        }
        //如果撞击到的物体标签为Floor或Wall，则销毁子弹同事在碰撞到的位置上实例化击中效果
        else if(other.gameObject.tag == "Floor" || other.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
            GameObject _environment = Instantiate(Rock) as GameObject;
            _environment.transform.position = this.transform.position;
            //效果在五秒后销毁
            Destroy(_environment,5.0f);
        }
    }
}
