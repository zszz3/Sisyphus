using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_11 : Enemy
{
    new public void Start()
    {
        base.Start();
    }

    new public void FiexedUpdate()
    {
        base.FixedUpdate();
        Debug.Log(this.damage);
    }
}
