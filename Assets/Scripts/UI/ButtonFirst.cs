using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFirst : MonoBehaviour
{
    void Start()
    {
        GetComponent<UnityEngine.UI.Button>().Select();
    }

    private void FixedUpdate()
    {
        GetComponent<UnityEngine.UI.Button>().Select();
    }
}
