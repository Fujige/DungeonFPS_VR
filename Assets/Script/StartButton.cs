using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {
    //开始按钮
    public void BtnDown()
    {
        SceneManager.LoadScene("Level1");
    }
}
