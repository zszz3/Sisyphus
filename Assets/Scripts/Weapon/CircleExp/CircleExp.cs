using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleExp : MonoBehaviour
{
    //吸收圆环 ，增加吸收经验圆环的范围
    public CircleCollider2D Circle;
    public static int level;
    public static int _level;
    private int StartLevel = 1;
    public float[] radius = { 4, 4, 5, 6, 7, 8, 9, 10000};
    private int currentLevel;

    void Start()
    {
        level = StartLevel;
    }

    void Update()
    {
        Circle.radius = radius[level];
    }

    public void MaxCircleExp()
    {
        _level = level;
        currentLevel = level;
        level = 7;
        Invoke("ResetLevel", 5f);
    }

    private void ResetLevel()
    {
        level = currentLevel;
    }

}
