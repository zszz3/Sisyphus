using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePotions : MonoBehaviour
{
    // Start is called before the first frame update
    public  float[] buff = { 1.0f, 1.1f, 1.3f, 1.5f, 1.6f, 1.7f };
    public static DamagePotions Instance { get; private set; }
    [SerializeField] public static int level;
    [SerializeField] public static float buffs;

    private int StartLevel = 1;
    void Start()
    {
        level = StartLevel;
        Instance = this;
        buffs = 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
    public void levelUP()
    {
        buffs = buff[level];
    }
}
