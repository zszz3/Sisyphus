using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Vector3 scale;
    public float cdTime;
    public float damage;

    private float continueTime;
    private Animator anim;
    private float nowtime;
    private bool flag;

    void Start()
    {
        anim = GetComponent<Animator>();
        continueTime = 10;
        flag = false;
    }

    private void FixedUpdate()              //炸弹超过continueTime时间没有爆炸就自爆,装备升级可以加伤害，存在时间，范围 
    {
        if (scale != transform.localScale)
        {
            transform.localScale = scale;
        }
        nowtime += Time.fixedDeltaTime;
        if (nowtime > continueTime && flag == false)
        {
            flag = true;
            anim.SetFloat("running", 10f);
            Destroy(gameObject, 0.5f);
        }
    }

    public void Explod()
    {
        anim.SetFloat("running", 10.0f);
        Destroy(gameObject, 0.5f);
        flag = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !flag)
        {
            Explod();
        }
    }
}
