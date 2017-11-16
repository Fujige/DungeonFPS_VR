using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    //动画状态机组件
    private Animator Animator;
    //导航网格
    private NavMeshAgent NavMeshAgent;

    //移动速度
    public float MoveSpeed;
    //丧尸生命值
    public float ZombieHealth;
    //伤害值
    public float Damage;
    //是否存活
    public bool Alive;
    //目标对象
    public GameObject Target;
    //攻击范围
    public float AttackRanage;

    //攻击时间
    private float AttackTime;
    private float NextAttackTime;

    //获取玩家对象及玩家血量
    public GameObject Player;
    public float PlayerHealth;

    // Use this for initialization
    void Start ()
    {
        //初始化变量
        //MoveSpeed = NavMeshAgent.speed;
        MoveSpeed = 1.0f;
        Alive = true;
        ZombieHealth = 50.0f;
        Damage = 20.0f;
        AttackRanage = 1.0f;
        AttackTime = 2.0f;
        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindWithTag("Player");
        Target = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {

        PlayerHealth = Player.GetComponent<Steam_VR_PlayerCharacter>().Health;

        //动画状态机信息
        AnimatorStateInfo _animatorInfo;
        _animatorInfo = Animator.GetCurrentAnimatorStateInfo(0);

        if (Alive == true)
	    {
	        if (Target != null)
	        {
	            NavMeshAgent.destination = Target.transform.position;
	            Animator.SetBool("Walk", true);

                //当敌人接近玩家并且进入到可攻击距离时进行攻击
	            if (Vector3.Distance(Player.transform.position, this.transform.position) < AttackRanage)
	            {
	                NavMeshAgent.Stop();
                    //transform.LookAt(Target.transform);
	                Animator.SetBool("Walk", false);
	                Animator.SetBool("Attack", true);
                }

                //播放攻击动画后对玩家造成伤害
	            if ((_animatorInfo.normalizedTime > 1 && _animatorInfo.IsName("Base Layer.Attack")))
	            {
	                Animator.SetBool("Attack", false);
                    if (NextAttackTime < Time.time)
	                {
	                    NextAttackTime = Time.time + AttackTime;
	                    PlayerHealth = PlayerHealth - Damage;
                        Player.GetComponent<Steam_VR_PlayerCharacter>().SendMessageUpwards("Damage", Damage);
                        if (PlayerHealth < 0)
	                        PlayerHealth = 0;
                        //通过外部设置玩家血量
	                    Player.GetComponent<Steam_VR_PlayerCharacter>().SetHealth(PlayerHealth);
	                    print(PlayerHealth);
                    }	                
	            }
            }
	    }

        //敌人血量为0时死亡
        if (ZombieHealth <= 0)
        {
            Alive = false;
            Animator.SetBool("Die", true);
            NavMeshAgent.Stop();
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(this.gameObject,5);
        }

        if ((_animatorInfo.normalizedTime > 1 && _animatorInfo.IsName("Base Layer.hit")))
        {
            Animator.SetBool("Hit", false);
        }

        if (ZombieHealth <= 0)
            ZombieHealth = 0;

    }

    //受到子弹撞击时减少血量
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            ZombieHealth -= 10;
            Animator.SetBool("Hit", true);
        }
        else if (other.gameObject.tag == "SMGBullet")
        {
            ZombieHealth -= 10;
            Animator.SetBool("Hit", true);
        }
        else if (other.gameObject.tag == "Dragonblade")
        {
            ZombieHealth -= 30;
            Animator.SetBool("Hit",true);
        }
        else
        {
            Animator.SetBool("Hit", false);
        }
    }

    //允许外部设置zombie血量
    public void SetZombieHealth(float ZombieHealth)
    {
        this.ZombieHealth = ZombieHealth;
    }
}
