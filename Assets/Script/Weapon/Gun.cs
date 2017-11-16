namespace VRTK.Examples
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public class Gun : VRTK_InteractableObject
    {
        SteamVR_TrackedObject trackedObj;
        //子弹
        private GameObject bullet;
        //子弹飞行速度
        private float bulletSpeed = 3000f;
        //子弹存活时间
        private float bulletLife = 5f;
        public Text ShotText;
        //一个弹匣的子弹数
        public int currentCount = 20;
        private int shellCount = 20;
        //是否允许开枪
        public bool IsFire = true;
        public Steam_VR_Fire SteamVrFire;
        public GameObject FireLight;

        //枪管
        public GameObject Barrel;
        //弹匣
        public GameObject Magazine;
        //开火检测
        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);
            FireBullet();
        }

        protected void Start()
        {
            trackedObj = this.transform.parent.GetComponent<SteamVR_TrackedObject>();
            ShotText.text = currentCount + "/" + shellCount;
            bullet = transform.Find("Bullet").gameObject;
            bullet.SetActive(false);
            SteamVrFire.enabled = true;
        }

        void Update()
        {
            //子弹数量监视
            ShotText.text = currentCount + "/" + shellCount;
            //更换弹匣
            Inplay();
        }
        
        //发射子弹
        private void FireBullet()
        {
            if (currentCount > 0 && IsFire == true)
            {
                GameObject bulletClone =
                    Instantiate(bullet, bullet.transform.position, bullet.transform.rotation) as GameObject;
                bulletClone.SetActive(true);
                Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
                rb.AddForce(bullet.transform.forward * bulletSpeed);
                Destroy(bulletClone, bulletLife);
                currentCount--;
                //当子弹数量小于或等于0时禁止开枪
                if (currentCount <= 0)
                {
                    currentCount = 0;
                    IsFire = false;
                    SteamVrFire.enabled = false;
                    FireLight.SetActive(false);
                    StartCoroutine(load());
                }
            }
            
        }

        //更换弹匣
        private void Inplay()
        {
            if (currentCount < 20)
            {
                SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
                {
                    IsFire = false;
                    //Debug.Log("按下手柄侧键");
                    Magazine.GetComponent<AudioSource>().Play();
                    currentCount = 20;
                    StartCoroutine(ReloadAnimation());
                }
            }
        }

        //协程 换弹动画
        IEnumerator ReloadAnimation()
        {
            Barrel.transform.localPosition = new Vector3(0.00251f, 0, 0);
            Magazine.transform.localPosition = new Vector3(0.00847f, -0.00055f, -0.00958f);
            SteamVrFire.enabled = false;
            FireLight.SetActive(false);

            yield return new WaitForSeconds(0.5f);
            Barrel.transform.localPosition = Vector3.zero * Time.deltaTime;
            Magazine.transform.localPosition = new Vector3(0.005972698f,0, -0.001205473f);
            IsFire = true;
            SteamVrFire.enabled = true;
        }

        //空仓挂机效果
        IEnumerator load()
        {
            yield return new WaitForSeconds(0.1f);
            Barrel.transform.localPosition = new Vector3(0.00251f, 0, 0);

        }
    }
}