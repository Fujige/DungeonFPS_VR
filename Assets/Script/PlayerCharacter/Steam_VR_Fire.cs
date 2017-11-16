using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Steam_VR_Fire : MonoBehaviour
{
    private SteamVR_TrackedObject TrackdeObject;
    public GameObject HandGun;
    public GameObject SMG;

    [Header("手枪")]
    //开枪火光
    public GameObject Firelight;
    public GameObject MuzzleFire;
    //开火间隔
    private float FireTime;
    private float NextFireTime;
    //枪管
    public GameObject Barrel;
    //弹壳及弹壳抛出点
    public GameObject PistolShell;
    public GameObject PistolShellPoint;

    [Header("SMG")]
    public GameObject SMGFirelight;
    public GameObject SMGMuzzleFire;
    public GameObject SMGBarrel;
    public GameObject SMGPistolShell;
    public GameObject SMGPistolShellPoint;
    private float SMGFireTime;
    private float SMGNextFireTime;

    // Use this for initialization
    void Start()
    {

        TrackdeObject = GetComponent<SteamVR_TrackedObject>();
        FireTime = 0.2f;
        NextFireTime = 0f;
        SMGFireTime = 0.1f;
        MuzzleFire.SetActive(false);
    }


    void FixedUpdate()
    {
        var device = SteamVR_Controller.Input((int) TrackdeObject.index);
        //按键控制开火效果
        if ((HandGun.activeInHierarchy == true && NextFireTime < Time.time) && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            NextFireTime = FireTime + Time.time;
            //播放开枪音效
            HandGun.GetComponent<AudioSource>().Play();
            //火光
            Firelight.SetActive(true);
            //右手震动            
            //var deviceIndex2 = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
            device.TriggerHapticPulse(3000);
            MuzzleFire.SetActive(false);
            MuzzleFire.SetActive(true);

            //弹壳抛出
            GameObject _pistolShell =
                Instantiate(PistolShell, PistolShellPoint.transform.position,
                    PistolShell.transform.rotation) as GameObject;
            _pistolShell.GetComponent<Rigidbody>().AddForce(PistolShellPoint.transform.up * -8);
            Destroy(_pistolShell, 4.0f);

            StartCoroutine(BarrelMove());
        }

        if ((SMG.activeInHierarchy == true && SMGNextFireTime < Time.time) && device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            SMGNextFireTime = SMGFireTime + Time.time;
            SMG.GetComponent<AudioSource>().Play();
            SMGFirelight.SetActive(true);
            device.TriggerHapticPulse(3000);
            SMGMuzzleFire.SetActive(false);
            SMGMuzzleFire.SetActive(true);

            GameObject _pistolShell =
                Instantiate(SMGPistolShell, SMGPistolShellPoint.transform.position,
                    SMGPistolShell.transform.rotation) as GameObject;
            _pistolShell.GetComponent<Rigidbody>().AddForce(SMGPistolShellPoint.transform.right * -8);

            Destroy(_pistolShell, 4.0f);

            StartCoroutine(SMGBarrelMove());
        }

        //当扳机键抬起时关闭火光
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (HandGun.activeInHierarchy == true)
            {
                Firelight.SetActive(false);
            }

            if (SMG.activeInHierarchy == true)
            {
                SMGFirelight.SetActive(false);
            }
        }
    }

    //枪管
    IEnumerator BarrelMove()
    {
        Barrel.transform.localPosition = new Vector3(0.00251f, 0, 0);

        yield return new WaitForSeconds(0.1f);

        Barrel.transform.localPosition = Vector3.zero;
    }

    IEnumerator SMGBarrelMove()
    {
        SMGBarrel.transform.localPosition = new Vector3(0, 0, 3.021f);

        yield return new WaitForSeconds(0.1f);

        SMGBarrel.transform.localPosition = new Vector3(0, 7.162714e-17f, 2.6963f);
    }

}
