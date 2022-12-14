using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLauch : MonoBehaviour
{
    [SerializeField] private CircleCreator creator;

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
            LauchCircle();
            totalTime = 0;
        }
    }

    private void LauchCircle()
    {
        creator.CreateCircle();
    }
}
