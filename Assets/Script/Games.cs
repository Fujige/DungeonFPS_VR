using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Examples;

public class Games : MonoBehaviour {
    SteamVR_TrackedObject trackedObj;

  //  FixedJoint joint;

    //是否获取到了抢
    bool Gun = false;

    public GameObject GunMesh;
    public Steam_VR_Fire ControllerTrigger;


	// Use this for initialization
	void Start () {
        trackedObj = this.transform.parent.GetComponent<SteamVR_TrackedObject>();

    }
	
	// Update is called once per frame
	void Update () {
    
    }
    //拾取枪支
    void OnTriggerStay(Collider collider)
    {
        if (collider.name != "Gun")
        {
            return;
        }
        else
        {
            SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Grip))
            {
                //固定关节
                //     joint = this.gameObject.AddComponent<FixedJoint>();
                //     joint.connectedBody = collider.GetComponent<Rigidbody>();
                GunMesh.SetActive(true);
                ControllerTrigger.enabled = true;
                this.gameObject.SetActive(false);
                collider.gameObject.SetActive(false);
            }
        }
    }
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.name != "Gun")
        {
            return;
        }
        Gun = true;
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.name != "Gun")
        {
            return;
        }
        Gun = false;
    }
}
