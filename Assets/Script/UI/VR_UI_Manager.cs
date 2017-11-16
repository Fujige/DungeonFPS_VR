using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class VR_UI_Manager : MonoBehaviour {
    //游戏菜单
    public GameObject PanelList;
    //开始按钮
    public GameObject StartButton;

    [Header("左手控制器")]
    public GameObject LeftGun;
    public GameObject LeftModel;
    public GameObject LeftDragonblade;
    public GameObject LeftMP5_Gun;

    [Header("右手控制器")]
    public GameObject RightGun;
    public GameObject RightModel;
    public GameObject RightDragonblade;
    public GameObject RightMP5_Gun;

    //游戏暂停控制
    private bool IsGamePaused;

    /// <summary>
    /// 显示选择列表
    /// </summary>
    public void ShowPanelList()
    {
        PanelList.SetActive(true);
        StartButton.SetActive(false);

        int count = PanelList.transform.childCount;//childCount子节点的个数
        for(int i =0; i<count; i++)
        {
            Transform Btn = PanelList.transform.GetChild(i);//遍历子节点，拿到每一个子节点的引用
            //缩放
            Btn.transform.localScale = Vector3.zero;//0
            Btn.DOScale(Vector3.one, 0.3f).SetDelay(i*0.1f);//终点数 , 延迟时间  SetDelay 专用于渐变
        }
    }

    /// <summary>
    /// 隐藏选择列表
    /// </summary>
    public void HidePanelList()
    {
        PanelList.SetActive(false);
        StartButton.SetActive(true);
        int count = PanelList.transform.childCount;//childCount子节点的个数
        for (int i = 0; i < count; i++)
        {
            Transform Btn = PanelList.transform.GetChild(i);//遍历子节点，拿到每一个子节点的引用
            //缩放
            Btn.DOScale(Vector3.zero, 0.3f).SetDelay(i * 0.1f);//终点数 , 延迟时间  SetDelay 专用于渐变
        }
    }

    /// <summary>
    /// 菜单点击切换关卡
    /// </summary>
    /// <param name="name"></param>
    public void OnBtnClick(string name)
    {
        if (name=="1")
        {
            LeftModel.SetActive(false);
            LeftGun.SetActive(false);
            LeftDragonblade.SetActive(false);
            LeftMP5_Gun.SetActive(true);
        }
        else if(name=="2")
        {
            LeftModel.SetActive(false);
            LeftGun.SetActive(true);
            LeftDragonblade.SetActive(false);
            LeftMP5_Gun.SetActive(false);
        }
        else if (name == "3")
        {
            LeftModel.SetActive(false);
            LeftGun.SetActive(false);
            LeftMP5_Gun.SetActive(false);
            LeftDragonblade.SetActive(true);
        }
        else if (name == "4")
        {
            LeftModel.SetActive(true);
            LeftGun.SetActive(false);
            LeftMP5_Gun.SetActive(false);
            LeftDragonblade.SetActive(false);
        }
        else if (name == "5")
        {
            RightModel.SetActive(false);
            RightGun.SetActive(false);
            RightDragonblade.SetActive(false);
            RightMP5_Gun.SetActive(true);
        }
        else if (name == "6")
        {
            RightModel.SetActive(false);
            RightGun.SetActive(true);
            RightDragonblade.SetActive(false);
            RightMP5_Gun.SetActive(false);
        }
        else if (name == "7")
        {
            RightModel.SetActive(false);
            RightGun.SetActive(false);
            RightMP5_Gun.SetActive(false);
            RightDragonblade.SetActive(true);
        }
        else if (name == "8")
        {
            RightModel.SetActive(true);
            RightGun.SetActive(false);
            RightMP5_Gun.SetActive(false);
            RightDragonblade.SetActive(false);
        }
    }


    //开始游戏控制
    public void StartGame()
    {
        Application.LoadLevel("Level1");
        IsGamePaused = false;
        Time.timeScale = 1;
        //Debug.Log("Start Game" + Time.fixedTime);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    //暂停游戏控制
    public void PauseGame()
    {
        IsGamePaused = true;
        Time.timeScale = 0;
        //Debug.Log("Pause Game");
    }
}
