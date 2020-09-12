using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        Debug.Log("h="+h);    
    }
}
