using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rushshoot : MonoBehaviour
{
    //急行之靴，增加移动速度
    // Start is called before the first frame update
    [SerializeField] public PlayerController player;
    [SerializeField] public static int level;
    private int StartLevel = 1;
    public float[] speed = {7,7,9,11,13,15,17,19,21 };

    void Start()
    {
        level = StartLevel;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player.speed = speed[level];
    }
}
