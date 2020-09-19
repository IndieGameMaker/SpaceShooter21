using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    public GameObject expEffect;
    public AudioClip expSfx;

    private int hitCount = 0;

    public Texture[] textures;
    private MeshRenderer  _renderer;
    private AudioSource _audio;

    void Start()
    {
        _renderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
        _audio    = GetComponent<AudioSource>();

        int idx = Random.Range(0, textures.Length); //0, 1, 2
        _renderer.material.mainTexture = textures[idx];

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
        _audio.PlayOneShot(expSfx);

        GameObject obj = Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(obj, 3.0f);
        
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 1500.0f);

        Destroy(this.gameObject, 4.0f);
    }

}
