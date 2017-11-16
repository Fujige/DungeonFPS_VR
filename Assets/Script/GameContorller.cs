using System.Collections;
using UnityEngine;

public class GameContorller : MonoBehaviour
{
    [SerializeField] private GameObject AxePrefab;
    [SerializeField] private GameObject SpiderPrefab;
    [SerializeField] private GameObject ZombiePrefab;

    //敌人刷新间隔
    public float CloneEnemySpeed;

    //敌人数量
    public int EnemyNumber;
    public int MaxEnemyNumber;

    [Header("第一波敌人")]
    //第一波敌人数量
    public int FirstwWaveNum;
    //第一波敌人最大数量
    public int MaxFirstWaveNum;

    [Header("第二波敌人")]
    //第二波敌人数量
    public int SecondWaveNum;
    //第二波敌人最大数量
    public int MaxSecondWaveNum;

    [Header("第三波敌人")]
    //第三波敌人数量
    public int ThirdWaveNum;
    //第三波敌人最大数量
    public int MaxThirdWaveNum;

    [Header("")]
    public GameObject Target;

    //每一波的间隔时间
    public float WaitWaveTime;
    public float ScenePassTime;
    public float SceneTime;
    public GameObject ScenePassEffect;

    // Use this for initialization
    void Start()
    {
        //变量初始化
        MaxEnemyNumber = 9;
        MaxFirstWaveNum = 3;
        MaxSecondWaveNum = 3;
        MaxThirdWaveNum = 3;
        CloneEnemySpeed = 2.5f;
        WaitWaveTime = 8.0f;
        ScenePassTime = 90.0f;
        //调用协程
        StartCoroutine(CloneEnemy());
        StartCoroutine(ScenePass());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //协程控制敌人刷新速度和数量
    IEnumerator CloneEnemy()
    {
        //开始游戏时不会立即刷新,会给玩家一定的等待时间
        yield return new WaitForSeconds(3f);

        while (EnemyNumber <= MaxEnemyNumber)
        {
            float _cloneProbability = Random.Range(0, 14);

            //第一波
            for (var i = 0; i < 1; ++i)
            {
                if (FirstwWaveNum <= MaxFirstWaveNum)
                {
                    if (_cloneProbability < 4)
                        Target = Instantiate(ZombiePrefab) as GameObject;
                    else if(_cloneProbability >9)
                        Target = Instantiate(SpiderPrefab) as GameObject;
                    else
                        Target = Instantiate(AxePrefab) as GameObject;
                    Target.transform.position = transform.position;
                    EnemyNumber++;
                    FirstwWaveNum++;
                }
                yield return new WaitForSeconds(CloneEnemySpeed);
            }

            yield return new WaitForSeconds(WaitWaveTime);

            //第二波
            for (var i = 0; i < 1; ++i)
            {
                if (SecondWaveNum <= MaxSecondWaveNum)
                {
                    if (_cloneProbability < 4)
                        Target = Instantiate(ZombiePrefab) as GameObject;
                    else if (_cloneProbability > 9)
                        Target = Instantiate(SpiderPrefab) as GameObject;
                    else
                        Target = Instantiate(AxePrefab) as GameObject;
                    Target.transform.position = transform.position;
                    EnemyNumber++;
                    SecondWaveNum++;
                }
                yield return new WaitForSeconds(CloneEnemySpeed);
            }

            yield return new WaitForSeconds(WaitWaveTime);

            //第三波
            for (var i = 0; i < 1; ++i)
            {
                if (ThirdWaveNum <= ThirdWaveNum)
                {
                    if (_cloneProbability < 4)
                        Target = Instantiate(ZombiePrefab) as GameObject;
                    else if (_cloneProbability > 9)
                        Target = Instantiate(SpiderPrefab) as GameObject;
                    else
                        Target = Instantiate(AxePrefab) as GameObject;
                    Target.transform.position = transform.position;
                    EnemyNumber++;
                    SecondWaveNum++;
                }
                yield return new WaitForSeconds(CloneEnemySpeed);
            }
        }
    }


    IEnumerator ScenePass()
    {
        yield return new WaitForSeconds(3f);
        while (ScenePassTime > 0)
        {
            yield return new WaitForSeconds(1f);
            print(ScenePassTime);
            ScenePassTime--;
        }
        ScenePassEffect.SetActive(true);

        yield return new WaitForSeconds(4f);

        if (Application.loadedLevelName == "Level1")
            Application.LoadLevel("Level2");
        else if (Application.loadedLevelName == "Level2")
            Application.LoadLevel("Level3");
        else if (Application.loadedLevelName == "Level3")
            Application.LoadLevel("Finish");
    }
}