﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullerCtrl : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>(); 
        rb.AddRelativeForce(Vector3.forward * 2000.0f);   
    }

}
