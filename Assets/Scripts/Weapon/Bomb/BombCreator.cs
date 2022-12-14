using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCreator : MonoBehaviour
{   
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject[] bombPrefab = new GameObject[8];
    public static int level;
    public float cdTime;         //不要在这里改！！！

    private void Start()
    {
        level = 0;
    }

    private void FixedUpdate()
    {
        if (level != 0)
        {
            cdTime = bombPrefab[level].GetComponent<Bomb>().cdTime;
        }
        else
        {
            cdTime = 10;
        }
    }

    public void CreateBomb()
    {
        if (level != 0)
        {
            Instantiate(bombPrefab[level], playerTransform.position, Quaternion.identity);
        }
    }
}
