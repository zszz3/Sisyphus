using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCreator : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject[] circlePrefab;
    public static int level;
    public int currentLevel;

    public float cdTime;         //不要在这里改

    private void Start()
    {
        level = 0;
        currentLevel = level;
    }

    private void FixedUpdate()
    {
        if (level != 0 && level != currentLevel)
        {
            cdTime = circlePrefab[level].GetComponent<MagicRunecircle>().cdTime;
            if(currentLevel != 0)
                circlePrefab[currentLevel].SetActive(false);
            currentLevel = level;
        }
        else
        {
            cdTime = 10;
        }
    }

    public void CreateCircle()
    {
        if (level != 0)
        {
            circlePrefab[level].SetActive(true);
        }
    }
}
