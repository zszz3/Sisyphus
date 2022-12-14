using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleAttack : MonoBehaviour
{
    [SerializeField] private GameObject[] sickle = new GameObject[8];

    public static int level;

    private float totalTime;
    private float cdTime;

    void Start()
    {
        level =1;
        cdTime = sickle[level].GetComponent<Sickle>().cdTime;
        totalTime = cdTime;
    }

    private void FixedUpdate()              //每隔一段时间产生回旋镖
    {
        cdTime = sickle[level].GetComponent<Sickle>().cdTime;
        totalTime += Time.deltaTime;
        if (totalTime >= cdTime)
        {
            ShootSickle();
            totalTime = 0;
        }
    }

    void ShootSickle()
    {
        Instantiate(sickle[level], transform.position, transform.rotation);
    }
}
