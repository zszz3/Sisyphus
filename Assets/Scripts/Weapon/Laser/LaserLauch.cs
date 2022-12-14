using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLauch : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject[] Laser = new GameObject[8];
    [SerializeField] public static int level;
    public int currentLevel;
    private float cdTime;
    private float totalTime;

    void Start()
    {
        level = 0;
    }

    private void FixedUpdate()              //每隔一段时间产生回旋镖
    {
        cdTime = Laser[level].GetComponent<Laser>().cdTime;
        totalTime += Time.deltaTime;
        if (totalTime >= cdTime)
        {
            ShootLaser();
            totalTime = 0;
        }
    }

    void ShootLaser()
    {
        if (level >= 1) { 
        Laser[level].GetComponent<Laser>().flagx = 1;
        Laser[level].GetComponent<Laser>().flagy = 1;
        Instantiate(Laser[level], playerTransform.position, playerTransform.rotation);
        Laser[level].GetComponent<Laser>().flagx = -1;
        Laser[level].GetComponent<Laser>().flagy = -1;
        Instantiate(Laser[level], playerTransform.position, playerTransform.rotation);
        }
        if (level >= 2)
        {
            Laser[level].GetComponent<Laser>().flagx = 1;
            Laser[level].GetComponent<Laser>().flagy = -1;
            Instantiate(Laser[level], playerTransform.position, playerTransform.rotation);
            Laser[level].GetComponent<Laser>().flagx = -1;
            Laser[level].GetComponent<Laser>().flagy = 1;
            Instantiate(Laser[level], playerTransform.position, playerTransform.rotation);
        }

    }
}
