using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryItem : MonoBehaviour
{
    public float destroytime;
    private PlayerBlood playerBlood;
    public float increaseBlood;

    void Start()
    {
        playerBlood = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBlood>();//获取人物血量组件
        Destroy(gameObject, destroytime);
    }
    private void FixedUpdate()
    {
        int times = CountdownTimer.Instance.second;


        if (times >= 0 && times < 300)
        {
            increaseBlood = 90;
        }
        else if (times >= 300 && times < 600)
        {
            increaseBlood = 120;
        }
        else
        {
            increaseBlood = 150;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            playerBlood.IncreasePlayer(increaseBlood);
            TipsUI.cherry = true;
            Destroy(gameObject);
        }
    }
}
