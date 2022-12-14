using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCreator : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject[] FreezePrefab = new GameObject[8];
    public static int level;
    public int currentLevel;

    public float cdTime;         //不要在这里改
    // Start is called before the first frame update
    private void Start()
    {
        level = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (level != 0 && level != currentLevel)
        {
            cdTime = FreezePrefab[level].GetComponent<Freeze>().cdTime;
            FreezePrefab[currentLevel].SetActive(false);
            currentLevel = level;
        }
        else
        {
            cdTime = 5;
        }
    }

    public void CreateFreeze()
    {
        if (level != 0) {
            FreezePrefab[level].SetActive(true);
        }
    }
}
