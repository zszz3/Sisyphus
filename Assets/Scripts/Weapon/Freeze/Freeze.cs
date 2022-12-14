using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timers;
public class Freeze : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] private float continueTime;
    public float cdTime;

    private float totalTime;
    void Start()
    {
        totalTime = 0;
    }
    private void FixedUpdate()
    {
        totalTime += Time.deltaTime;
        if (totalTime >= continueTime)
        {
            totalTime = 0;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().Frezze();
        }
    }

}
