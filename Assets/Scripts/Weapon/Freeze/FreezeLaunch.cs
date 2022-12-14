

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeLaunch : MonoBehaviour
{
    [SerializeField] private FreezeCreator creator;

    private float totalTime;
    private float cdTime;

    private void Start()
    {
        cdTime = creator.cdTime;
        totalTime = cdTime;
    }

    private void FixedUpdate()
    {
        cdTime = creator.cdTime;
        totalTime += Time.deltaTime;
        if (totalTime >= cdTime)
        {
            LauchFreeze();
            totalTime = 0;
        }
    }

    private void LauchFreeze()
    {
        creator.CreateFreeze();
    }
}
