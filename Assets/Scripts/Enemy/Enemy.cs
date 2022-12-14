using Timers;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //请注意暂停不会暂停Update中的行动，所以大多数行动放在fixedUpdate中。
    //除了冲锋，因为卡顿原因放在update中，但也导致暂停时，按空格确认继续会进行一段冲刺。
    public GameObject dropTreasureBox;       //宝箱               
    public GameObject dropCoin;              //金币
    //下面的public的变量是不同怪物会有不同的数据
    public GameObject dropExp;               //经验宝石
    public float moveSpeed;                  //怪物移动速度
    public float damage;                     //怪物伤害
    public float blood;                      //怪物血量

    [Header("这里设置怪物掉率，请设置1-100数字")]
    [Header("并且nullDrop < expDrop < coinDrop")]
    public float nullDrop;                    //啥都不掉: 0% - nullDrop%
    public float expDrop;                     //经验宝石掉落概率: nullDrop% - expDrop%
    public float coinDrop;                    //金币掉落概率: expDrop% - coinDrop%

    protected bool[] _canAttack = new bool[7] { true, true, true, true, true,true,true };
    private PlayerBlood playerBlood;                 
    private Color originalColor;             //记下初始颜色，以便受伤变红后变回来。       
    private SpriteRenderer sr;
    public Animator animator;
    private Transform player;
    protected float flashTime;                 //怪物被攻击变红的时间
    private Transform enemy;
    public bool jugde;                      //避免反复进入死亡结算。
    private float movespeed1;   //备份
    [Header("这里设置怪物增强的间隔和增强属性值")]
    [Header("并且可以设定最大值")]
    public float MonsterEnhancementInterval;//怪物加强时间间隔
    public float MonsterIncreasesAttack;//怪物加强攻击力的值
    public float MonsterSpeedIncreased;//怪物速度增加值
    public float MonsterHPIncreased;//怪物生命值增加
    public float MonsterMaxAttack; //怪物攻击力最大值
    public float MonsterMaxSpeed;//怪物速度最大值
    public float MonsterMaxHP;//怪物生命值最大值
    public float starttime;//设定怪物第一次出现的时间
    public float timers = 0;//设定一个定时器
    protected void Awake()
    {
        enemy = transform;
    }

    protected void Start()
    {
        playerBlood = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBlood>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        originalColor = sr.color;
        flashTime = 0.25f;
        jugde = true;
        movespeed1 = moveSpeed;
       
    }

    protected void FixedUpdate()
    {
        if (blood <= 0 && jugde == true)                 //死亡判断
        {

            ParticleController.Instance.AssignParticle(transform.position,0f,"cfx");
            jugde = false;
            animator.SetFloat("Running",10f);
            foreach (var child in GetComponentsInChildren<SpriteRenderer>())
            {
                if (child.gameObject != this.gameObject) child.enabled = false;
            }
            Invoke("Destroy", 1.0f);                  //怪物死亡动画全统一是0.5s，所以这里是等动画播放完毕，执行destroy函数操作。
        }
        if (blood > 0)                                   //未死亡时进行移动
        {
            Movement();
        }
        float a = Time.time;
        timers = a-starttime;
        //Debug.Log(timers);
        if (timers >= MonsterEnhancementInterval)
        {
            starttime += MonsterEnhancementInterval;
            //timers = 0;
            Enhance();//调用增强函数
        }
    }

    public void TakeDamage(float playerDamage)   //怪物受伤
    {
        //ParticleController.Instance.AssignParticle(transform.position,0f, "Water");
        blood -= playerDamage*DamagePotions.buffs;
        FlashColor(flashTime);
    }

    static int GetRandomNumber(int length)//随机一个数组中的数
    {
            int index = Random.Range(0,length);
            //print(index);
            return index;
    }

    protected void Recover()
    {
        moveSpeed = movespeed1;
    }

    public void Frezze()
    {
        moveSpeed = 0;
        FreezeColor(2f);
        Invoke("Recover", 2f);//2秒后恢复速度
    }

    protected void Slowdown()
    {
        ParticleController.Instance.AssignParticle(transform, "Water");
        moveSpeed = moveSpeed * 0.5f;
        Invoke("Recover", 2f);//2秒后恢复速度
    }

    protected void Repulsed()
    {
        moveSpeed = -2;
        Invoke("Recover", 2f);//2秒后恢复速度
    }

    protected void FreezeColor(float time)
    {
        foreach (var child in GetComponentsInChildren<SpriteRenderer>())
        {
            child.color = Color.blue;
        }
        Invoke("ResetColor", time);
    }     //受伤变红

    protected void FlashColor(float time)
    {
        foreach (var child in GetComponentsInChildren<SpriteRenderer>())
        {
             child.color = Color.red;
        }
        Invoke("ResetColor",time);
    }     //受伤变红

    protected void ResetColor()
    {
        foreach (var child in GetComponentsInChildren<SpriteRenderer>())
        {
            child.color = originalColor;
        }
    }               //返回到初始颜色

    protected void Movement()
    {
        Vector2 vector = enemy.position - player.position;
        if(Mathf.Abs(vector.x)>0.8)
        {
            enemy.localScale = new Vector3(-(player.position.x - enemy.position.x) / Mathf.Max(Mathf.Abs(player.position.x - enemy.position.x), 1), 1, 1);
        }
        transform.Translate(-vector.normalized * moveSpeed * Time.fixedDeltaTime);
    }                 //移动函数

    protected void OnTriggerEnter2D(Collider2D collision)              //与玩家碰撞产生伤害
    {
        if (collision.gameObject.CompareTag("Sickle"))
        {
            if (!_canAttack[0]) return;
            TakeDamage(collision.GetComponent<Sickle>().damage);
            _canAttack[0] = false;
            TimersManager.SetTimer(this, 0.2f, CanAttack0);
        }
        if (collision.gameObject.CompareTag("Bomb"))
        {
            if (!_canAttack[1]) return;
            TakeDamage(collision.GetComponent<Bomb>().damage);
            _canAttack[1] = false;
            TimersManager.SetTimer(this, 1, CanAttack1);
        }
        if (collision.gameObject.CompareTag("Missile"))
        {
            if (!_canAttack[2]) return;
            TakeDamage(Missile._damage);
            _canAttack[2] = false;
            TimersManager.SetTimer(this, 0.05f, CanAttack2);
        }
        if (collision.gameObject.CompareTag("Circle"))
        {
            if (!_canAttack[3]) return;
            TakeDamage(collision.GetComponent<MagicRunecircle>().damage);
  
            _canAttack[3] = false;
            TimersManager.SetTimer(this, 0.25f, CanAttack3);
        }
        if (collision.gameObject.CompareTag("Laser"))
        {
            if (!_canAttack[4]) return;
            TakeDamage(collision.GetComponent<Laser>().damage);
            _canAttack[4] = false;
            TimersManager.SetTimer(this, 0.05f, CanAttack4);
        }
        if (collision.gameObject.CompareTag("Freeze"))
        {
            if (!_canAttack[5]) return;
            TakeDamage(collision.GetComponent<Freeze>().damage);
            _canAttack[5] = false;
            TimersManager.SetTimer(this, 0.2f, CanAttack5);
        }
        if (collision.gameObject.CompareTag("Health"))
        {
            if(!_canAttack[6]) return;
            player.GetComponent<PlayerBlood>().DamagePlayer(damage);
            _canAttack[6] = false;
            TimersManager.SetTimer(this, 0.5f, CanAttack6);
        }
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sickle"))
        {
            if (!_canAttack[0]) return;
            TakeDamage(collision.GetComponent<Sickle>().damage);
            _canAttack[0] = false;
            TimersManager.SetTimer(this, 0.2f, CanAttack0);
        }
        if (collision.gameObject.CompareTag("Bomb"))
        {
            if (!_canAttack[1]) return;
            TakeDamage(collision.GetComponent<Bomb>().damage);
            _canAttack[1] = false;
            TimersManager.SetTimer(this, 1, CanAttack1);
        }
        if (collision.gameObject.CompareTag("Missile"))
        {
            if (!_canAttack[2]) return;
            TakeDamage(Missile._damage);
            _canAttack[2] = false;
            TimersManager.SetTimer(this, 0.1f, CanAttack2);
        }
        if (collision.gameObject.CompareTag("Circle"))
        {
            if (!_canAttack[3]) return;
            TakeDamage(collision.GetComponent<MagicRunecircle>().damage);
            switch (CircleCreator.level)
            {
                case 1:
                    playerBlood.IncreasePlayer(0.5f);
                    break;
                case 2:
                    playerBlood.IncreasePlayer(0.6f);
                    break;
                case 3:
                    playerBlood.IncreasePlayer(0.7f);
                    break;
                case 4:
                    playerBlood.IncreasePlayer(0.8f);
                    break;
                case 5:
                    playerBlood.IncreasePlayer(0.9f);
                    break;
                case 6:
                    playerBlood.IncreasePlayer(1);
                    break;
                case 7:
                    playerBlood.IncreasePlayer(1.2f);
                    break;
                case 8:
                    playerBlood.IncreasePlayer(1.5f);
                    break;
                case 9:
                    playerBlood.IncreasePlayer(2f);
                    break;
                default:
                    break;
            }
            _canAttack[3] = false;
            TimersManager.SetTimer(this, 0.25f, CanAttack3);
        }
        if (collision.gameObject.CompareTag("Laser"))
        {
            if (!_canAttack[4]) return;
            TakeDamage(collision.GetComponent<Laser>().damage);
            _canAttack[4] = false;
            TimersManager.SetTimer(this, 0.1f, CanAttack4);
        }

        if (collision.gameObject.CompareTag("Freeze"))
        {
            if (!_canAttack[5]) return;
            TakeDamage(collision.GetComponent<Freeze>().damage);
            _canAttack[4] = false;
            TimersManager.SetTimer(this, 0.2f, CanAttack5);
        }
        if (collision.gameObject.CompareTag("Health"))
        {
            if (!_canAttack[6]) return;
            player.GetComponent<PlayerBlood>().DamagePlayer(damage);
            _canAttack[6] = false;
            TimersManager.SetTimer(this, 0.5f, CanAttack6);
        }
    }

    protected void CanAttack0()
    {
        _canAttack[0] = true;
    }

    protected void CanAttack1()
    {
        _canAttack[1] = true;
    }

    protected void CanAttack2()
    {
        _canAttack[2] = true;
    }

    protected void CanAttack3()
    {
        _canAttack[3] = true;
    }

    protected void CanAttack4()
    {
        _canAttack[4] = true;
    }

    protected void CanAttack5()
    {
        _canAttack[5] = true;
    }

    protected void CanAttack6()
    {
        _canAttack[6] = true;
    }

    protected void Destroy()    //怪物死亡消灭gameobject，并且根据概率产生经验宝石，金币，宝箱。
    {
        float a = Random.Range(0f, 100f);
        if (a < nullDrop)
        {
        }
        else if (a >= nullDrop && a < expDrop)
        {
            Instantiate(dropExp, transform.position, Quaternion.identity);
        }
        else if(a>=expDrop && a< coinDrop)
        {
            Instantiate(dropCoin, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(dropTreasureBox, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    protected void Enhance()//怪物随时间加强函数
    {
        if (moveSpeed < MonsterMaxSpeed && MonsterMaxSpeed > 0)//怪物速度增加
        {
            moveSpeed += MonsterSpeedIncreased;
            if (moveSpeed > MonsterMaxSpeed)//加完后判断有没有大于最大值
            {
                moveSpeed = MonsterMaxSpeed;
            }
        }
        else if (MonsterMaxSpeed == 0)
        {
            moveSpeed += MonsterSpeedIncreased;
        }

        if (damage < MonsterMaxAttack && MonsterMaxAttack > 0)//怪物攻击力增加，逻辑同速度
        {
            damage += MonsterIncreasesAttack;
            if (damage > MonsterMaxAttack)
            {
                damage = MonsterMaxAttack;
            }
        }
        else if (MonsterMaxAttack == 0)
        {
            damage += MonsterIncreasesAttack;
        }
        if (blood < MonsterMaxHP && MonsterMaxHP > 0)//怪物血量上涨
        {
            blood += MonsterHPIncreased;
            //Debug.Log("函数被调用了" + "怪物此时血量为" + blood);
            if (blood > MonsterMaxHP)
            {
                blood = MonsterMaxHP;
            }
        }
        else if (MonsterMaxHP == 0)
        {
            blood += MonsterHPIncreased;
        }

    }
}
