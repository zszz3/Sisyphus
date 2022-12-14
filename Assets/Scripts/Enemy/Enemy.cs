using Timers;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //��ע����ͣ������ͣUpdate�е��ж������Դ�����ж�����fixedUpdate�С�
    //���˳�棬��Ϊ����ԭ�����update�У���Ҳ������ͣʱ�����ո�ȷ�ϼ��������һ�γ�̡�
    public GameObject dropTreasureBox;       //����               
    public GameObject dropCoin;              //���
    //�����public�ı����ǲ�ͬ������в�ͬ������
    public GameObject dropExp;               //���鱦ʯ
    public float moveSpeed;                  //�����ƶ��ٶ�
    public float damage;                     //�����˺�
    public float blood;                      //����Ѫ��

    [Header("�������ù�����ʣ�������1-100����")]
    [Header("����nullDrop < expDrop < coinDrop")]
    public float nullDrop;                    //ɶ������: 0% - nullDrop%
    public float expDrop;                     //���鱦ʯ�������: nullDrop% - expDrop%
    public float coinDrop;                    //��ҵ������: expDrop% - coinDrop%

    protected bool[] _canAttack = new bool[7] { true, true, true, true, true,true,true };
    private PlayerBlood playerBlood;                 
    private Color originalColor;             //���³�ʼ��ɫ���Ա����˱���������       
    private SpriteRenderer sr;
    public Animator animator;
    private Transform player;
    protected float flashTime;                 //���ﱻ��������ʱ��
    private Transform enemy;
    public bool jugde;                      //���ⷴ�������������㡣
    private float movespeed1;   //����
    [Header("�������ù�����ǿ�ļ������ǿ����ֵ")]
    [Header("���ҿ����趨���ֵ")]
    public float MonsterEnhancementInterval;//�����ǿʱ����
    public float MonsterIncreasesAttack;//�����ǿ��������ֵ
    public float MonsterSpeedIncreased;//�����ٶ�����ֵ
    public float MonsterHPIncreased;//��������ֵ����
    public float MonsterMaxAttack; //���﹥�������ֵ
    public float MonsterMaxSpeed;//�����ٶ����ֵ
    public float MonsterMaxHP;//��������ֵ���ֵ
    public float starttime;//�趨�����һ�γ��ֵ�ʱ��
    public float timers = 0;//�趨һ����ʱ��
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
        if (blood <= 0 && jugde == true)                 //�����ж�
        {

            ParticleController.Instance.AssignParticle(transform.position,0f,"cfx");
            jugde = false;
            animator.SetFloat("Running",10f);
            foreach (var child in GetComponentsInChildren<SpriteRenderer>())
            {
                if (child.gameObject != this.gameObject) child.enabled = false;
            }
            Invoke("Destroy", 1.0f);                  //������������ȫͳһ��0.5s�����������ǵȶ���������ϣ�ִ��destroy����������
        }
        if (blood > 0)                                   //δ����ʱ�����ƶ�
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
            Enhance();//������ǿ����
        }
    }

    public void TakeDamage(float playerDamage)   //��������
    {
        //ParticleController.Instance.AssignParticle(transform.position,0f, "Water");
        blood -= playerDamage*DamagePotions.buffs;
        FlashColor(flashTime);
    }

    static int GetRandomNumber(int length)//���һ�������е���
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
        Invoke("Recover", 2f);//2���ָ��ٶ�
    }

    protected void Slowdown()
    {
        ParticleController.Instance.AssignParticle(transform, "Water");
        moveSpeed = moveSpeed * 0.5f;
        Invoke("Recover", 2f);//2���ָ��ٶ�
    }

    protected void Repulsed()
    {
        moveSpeed = -2;
        Invoke("Recover", 2f);//2���ָ��ٶ�
    }

    protected void FreezeColor(float time)
    {
        foreach (var child in GetComponentsInChildren<SpriteRenderer>())
        {
            child.color = Color.blue;
        }
        Invoke("ResetColor", time);
    }     //���˱��

    protected void FlashColor(float time)
    {
        foreach (var child in GetComponentsInChildren<SpriteRenderer>())
        {
             child.color = Color.red;
        }
        Invoke("ResetColor",time);
    }     //���˱��

    protected void ResetColor()
    {
        foreach (var child in GetComponentsInChildren<SpriteRenderer>())
        {
            child.color = originalColor;
        }
    }               //���ص���ʼ��ɫ

    protected void Movement()
    {
        Vector2 vector = enemy.position - player.position;
        if(Mathf.Abs(vector.x)>0.8)
        {
            enemy.localScale = new Vector3(-(player.position.x - enemy.position.x) / Mathf.Max(Mathf.Abs(player.position.x - enemy.position.x), 1), 1, 1);
        }
        transform.Translate(-vector.normalized * moveSpeed * Time.fixedDeltaTime);
    }                 //�ƶ�����

    protected void OnTriggerEnter2D(Collider2D collision)              //�������ײ�����˺�
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

    protected void Destroy()    //������������gameobject�����Ҹ��ݸ��ʲ������鱦ʯ����ң����䡣
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

    protected void Enhance()//������ʱ���ǿ����
    {
        if (moveSpeed < MonsterMaxSpeed && MonsterMaxSpeed > 0)//�����ٶ�����
        {
            moveSpeed += MonsterSpeedIncreased;
            if (moveSpeed > MonsterMaxSpeed)//������ж���û�д������ֵ
            {
                moveSpeed = MonsterMaxSpeed;
            }
        }
        else if (MonsterMaxSpeed == 0)
        {
            moveSpeed += MonsterSpeedIncreased;
        }

        if (damage < MonsterMaxAttack && MonsterMaxAttack > 0)//���﹥�������ӣ��߼�ͬ�ٶ�
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
        if (blood < MonsterMaxHP && MonsterMaxHP > 0)//����Ѫ������
        {
            blood += MonsterHPIncreased;
            //Debug.Log("������������" + "�����ʱѪ��Ϊ" + blood);
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
