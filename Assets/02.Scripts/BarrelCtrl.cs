using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    public GameObject expEffect;
    private int hitCount = 0;

    public Texture[] textures;
    public MeshRenderer  _renderer;

    void Start()
    {
        _renderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))  //if (coll.gameObject.tag == "BULLET")
        {
            hitCount += 1; //hitCount = hitCount + 1; // ++hitCount;
            if (hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }

    void ExpBarrel()
    {
        GameObject obj = Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(obj, 3.0f);
        
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 1500.0f);

        Destroy(this.gameObject, 4.0f);
    }

}
