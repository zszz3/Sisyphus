using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;                   //�����ƶ��ٶ�

    private Animator animator;        
    private float horizontalMove;
    private float verticalMove;

    [Header("CD��UI���")]
    public Image cdImage;

    [Header("Dash����")]
    public float dashCoolDown; //���cd
    public float dashSpeed;
    public float dashTime; //dashʱ��
    public bool isDashing;  //�ж�

    [SerializeField] private GameObject health;
    public static float _dashCoolDown;
    private float dashTimeLeft;  //���ʣ��ʱ��
    public static float lastDash;         //���ô˱���������ͣ�����������ܳ�档

    void Start()
    {
        _dashCoolDown = dashCoolDown;
        animator = GetComponent<Animator>();
        lastDash = 0 - dashCoolDown;
    }
    
    void Update()                         //�ո���
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                ReadyToDash();
            }
        }
    }

    private void FixedUpdate()            //�ƶ������ͳ��cd ui
    {
        // ParticleController.Instance.AssignParticle(transform, "Fire");
        //ParticleController.Instance.AssignParticle(transform, "Poision");
        cdImage.fillAmount -= 1.0f / dashCoolDown * Time.deltaTime;
        Dash();
        if (isDashing)
        {
            return;
        }
        Movement();
    }

    void Movement()                //��ɫ�ƶ���ת��
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        Vector2 position = transform.position;
        if(horizontalMove != 0)
        {
            position.x += horizontalMove * speed * Time.fixedDeltaTime;
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
        if(verticalMove != 0)
        {
            position.y += verticalMove * speed * Time.fixedDeltaTime;
        }
        animator.SetFloat("Running", Mathf.Abs(horizontalMove)+Mathf.Abs(verticalMove));
        transform.position = position;
    }

    void ReadyToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;
        cdImage.fillAmount = 1;
    }

    void Dash()
    {
        if (isDashing)
        {
            if(dashTimeLeft > 0)
            {
                health.GetComponent<BoxCollider2D>().enabled = false;
                Vector2 position = transform.position;
                if (horizontalMove == 0 && verticalMove == 0)  //�����ɫ��ֹ������ķ�����г�̡�
                {
                    position.x += this.transform.localScale.x * dashSpeed*Time.fixedDeltaTime;
                }
                else
                {
                    position.x += horizontalMove * dashSpeed*Time.fixedDeltaTime;
                    position.y += verticalMove * dashSpeed* Time.fixedDeltaTime;
                }
                transform.position = position;
                dashTimeLeft -= Time.fixedDeltaTime;
                ShadowPool.instance.GetFormPool();
            }
        }
        if (dashTimeLeft <= 0)
        {
            health.GetComponent<BoxCollider2D>().enabled = true;
            isDashing = false;
        }
    }

}
