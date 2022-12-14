using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timers;

public class MissileLauch : MonoBehaviour
{
    [SerializeField] private MissileCreator creator;

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
            LauchMissile();
            totalTime = 0;
        }
    }

    private void LauchMissile()
    {
        creator.CreateMissile();
    }
}
