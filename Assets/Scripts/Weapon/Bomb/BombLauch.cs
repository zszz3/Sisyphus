using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timers;

public class BombLauch : MonoBehaviour
{
    [SerializeField] private BombCreator creator;

    private float totalTime;
    private float cdTime;

    private void Start()
    {
        cdTime = creator.cdTime;
        totalTime = cdTime;
    }

    private void FixedUpdate()              //ÿ��һ��ʱ�����������
    {
        cdTime = creator.cdTime;
        totalTime += Time.deltaTime;
        if (totalTime >= cdTime)
        {
            LauchBomb();
            totalTime = 0;
        }
    }

    private void LauchBomb()
    {
        creator.CreateBomb();
    }
}
