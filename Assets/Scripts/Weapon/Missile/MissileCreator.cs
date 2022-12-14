using System.Collections;
using System.Collections.Generic;
using Timers;
using UnityEngine;

public class MissileCreator : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject[] missilePrefab = new GameObject[7];
    [SerializeField] public static int level;

    public float cdTime;

    private void Start()
    {
        level = 0;
    }

    private void FixedUpdate()
    {
        if (level != 0)
        {
            cdTime = missilePrefab[level].GetComponent<Missile>().cdTime;
        }
        else
        {
            cdTime = 10;
        }
    }

    public void CreateMissile()
    {
            var now = playerTransform.position;
            if (now.x == 0.0f) now.x = 0.0001f;
            if (now.y == 0.0f) now.y = 0.0001f;
        if(level!=0)
        {
            Instantiate(missilePrefab[level], now, Quaternion.identity);

        }
    }
}
