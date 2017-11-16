using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class VR_Contorller_Menu : MonoBehaviour
{
    //使用的图片变量
    public Image PlaneImage;
    //图片显示开关
    private bool isImageShow = false;

	// Use this for initialization
	void Start ()
    {
        //图片位置归0与隐藏
        PlaneImage.enabled=false;
        PlaneImage.rectTransform.localScale = Vector3.zero;
        //按键按下检测和按键方法实现
        VRTK_ControllerEvents events = GetComponent<VRTK_ControllerEvents>();
        events.ButtonTwoPressed += new ControllerInteractionEventHandler(OnApplicationMenuPressed);
	}

    //按钮按下显示检测
    private void OnApplicationMenuPressed(object sender, ControllerInteractionEventArgs events)
    {
        //判断显示
        if (isImageShow)
        {
            PlaneImage.rectTransform.DOScale(Vector3.zero, 0.3f);
            isImageShow = false;
        }
        else
        {
            PlaneImage.enabled = true;
            PlaneImage.rectTransform.DOScale(Vector3.one, 0.3f);
            isImageShow = true;
        }
    }
}
