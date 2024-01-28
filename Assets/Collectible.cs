using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    GameManager gm;

    // For item rotation
    public float rotationSpeed = 30f;
    public ParticleSystem ps;
    private float originalY;
    public MeshRenderer mr;


    void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
        ps = GetComponent<ParticleSystem>();
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateItem(); 
    }

    private void RotateItem()
    {
        // Rotate the object around its Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {   
        if (collision.gameObject.tag == "Player")
        {
            if (gm)
            {
                gm.ItemCollected(this.gameObject);
            }
            Collected();
        }
    }

    void Collected()
    {
        // Audio hook here:
        // Visual hook here:
        mr.enabled = false;
        var em = ps.emission;
        em.enabled = true;
        ps.Play();
        Destroy(this.gameObject, 1f);
    }

}
