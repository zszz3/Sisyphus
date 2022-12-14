using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : MonoBehaviour
{
    public float destroyTime;
    public AudioClip pickupSFX;
    public float[] expNum;
    public float[] circlebuff = { 1.0f, 1.0f, 1.2f, 1.3f, 1.4f, 1.6f, 1.7f };
    private float expAdd;
    private bool flyFlag;
    private float moveSpeed;

    private Transform exp;
    private Transform player;
    
    private void Awake()
    {
        exp = GetComponent<Transform>();
    }

    void Start()
    {
        moveSpeed = 20;
        flyFlag = false;
        Destroy(gameObject, destroyTime);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        int times = CountdownTimer.Instance.second;

        if (flyFlag == true)
        {
            Movement();
        }
        
        if(times >= 0 && times < 300)
        {
            expAdd = expNum[0];
        }
        else if (times >= 300 && times <600)
        {
            expAdd = expNum[1];
        }
        else 
        {
            expAdd = expNum[2];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)  //ÓëÍæ¼ÒÅö×²
    {
        if(collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CircleCollider2D")
        {
            flyFlag = true;
        }

        if(collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            AudioSource.PlayClipAtPoint(pickupSFX, transform.position, 0.5f);
            //AudioController.Instance.PlayAudio("PickUp");
            if(CircleExp.level != 7)
                ExpUI.currentExp += expAdd*circlebuff[CircleExp.level];
            else
                ExpUI.currentExp += expAdd * circlebuff[CircleExp._level];
            TipsUI.exp = true;
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        Vector2 vector = exp.position - player.position;
        transform.Translate(-vector.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
