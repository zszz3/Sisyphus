using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;                   //人物移动速度

    private Animator animator;        
    private float horizontalMove;
    private float verticalMove;

    [Header("CD的UI组件")]
    public Image cdImage;

    [Header("Dash参数")]
    public float dashCoolDown; //冲锋cd
    public float dashSpeed;
    public float dashTime; //dash时长
    public bool isDashing;  //判断

    [SerializeField] private GameObject health;
    public static float _dashCoolDown;
    private float dashTimeLeft;  //冲锋剩余时间
    public static float lastDash;         //利用此变量控制暂停升级死亡不能冲锋。

    void Start()
    {
        _dashCoolDown = dashCoolDown;
        animator = GetComponent<Animator>();
        lastDash = 0 - dashCoolDown;
    }
    
    void Update()                         //空格冲锋
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                ReadyToDash();
            }
        }
    }

    private void FixedUpdate()            //移动、冲锋和冲锋cd ui
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

    void Movement()                //角色移动、转向
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
                if (horizontalMove == 0 && verticalMove == 0)  //如果角色静止，向朝向的方向进行冲刺。
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
