using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    public Transform firePos;
    public GameObject bulletPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  //0: Left button  , 1: Right Button , 2: Middle
        {
            Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        }
    }
}
