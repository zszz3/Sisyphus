using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    private bool flyFlag;
    private Transform coin;
    private Transform player;
    private float moveSpeed;
    public float destroytime;

    private void Awake()
    {
        coin = GetComponent<Transform>();
    }

    void Start()
    {
        moveSpeed = 10;
        flyFlag = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("DelayDestroy", destroytime);//每隔一段时间销毁金币
    }

    private void FixedUpdate()
    {
        if (flyFlag == true)
        {
            Movement();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)          //与玩家碰撞
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CircleCollider2D")
        {
            flyFlag = true;
        }
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            CoinUI.currentCoinCount += 1;
            TipsUI.coin = true;
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        Vector2 vector = coin.position - player.position;
        transform.Translate(-vector.normalized * moveSpeed * Time.fixedDeltaTime);
    }
    void DelayDestroy()
    {
        Destroy(this.gameObject);
    }
}
