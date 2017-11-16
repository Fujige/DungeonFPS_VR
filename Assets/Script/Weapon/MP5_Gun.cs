namespace VRTK.Examples
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public class MP5_Gun : VRTK_InteractableObject
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
        public int currentCount = 30;
        public int shellpavel = 5;
        public int shellCount;
        //是否允许开枪
        public bool IsFire = true;
        public Steam_VR_Fire SteamVrFire;
        public GameObject FireLight;
        //弹匣
        public GameObject Magazine;
        public Animator Animator;
        //开火间隔
        public float FireTime;
        public float NextFireTime;

        //开火检测
        //public override void StartUsing(VRTK_InteractUse usingObject)
        //{
        //    base.StartUsing(usingObject);
            
        //    FireBullet();
        //}

        protected void Start()
        {
            shellCount = 30 * shellpavel;
            trackedObj = this.transform.parent.GetComponent<SteamVR_TrackedObject>();
            FireTime = 0.1f;
            ShotText.text = currentCount + "/" + shellCount;
            bullet = transform.Find("SMGBullet").gameObject;
            bullet.SetActive(false);
            SteamVrFire.enabled = true;
            Animator = GetComponent<Animator>();
        }

        void Update()
        {
            var device = SteamVR_Controller.Input((int)trackedObj.index);
            //按键控制开火效果
            if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger) && NextFireTime < Time.time)
            {
                NextFireTime = FireTime + Time.time;
                FireBullet();
            }
            AnimatorStateInfo _animatorInfo;
            _animatorInfo = Animator.GetCurrentAnimatorStateInfo(0);
            
            shellCount = 30 * shellpavel;
            //子弹数量监视
            ShotText.text = currentCount + "/" + shellCount;

            if (_animatorInfo.normalizedTime > 1 && _animatorInfo.IsName("Base Layer.Reload"))
            {
                Animator.SetBool("Reload", false);
                IsFire = true;
                SteamVrFire.enabled = true;
            }
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
                }
            }
            
        }

        //更换弹匣
        private void Inplay()
        {
            //动画状态机信息
            AnimatorStateInfo _animatorInfo;
            _animatorInfo = Animator.GetCurrentAnimatorStateInfo(0);

            if (currentCount < 30 && shellpavel >= 1)
            {
                SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
                {
                    IsFire = false;
                    //Debug.Log("按下手柄侧键");
                    Magazine.GetComponent<AudioSource>().Play();
                    currentCount = 30;
                    Animator.SetBool("Reload",true);
                    SteamVrFire.enabled = false;
                    shellpavel--;
                }
            }
        }
    }
}