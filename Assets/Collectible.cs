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
        //print($"Collision recoreded between " + this.gameObject.name + " & " + collision.gameObject.tag);
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
        // Audio hook here

        // Visual hook
        // Disable MeshRenderer
        mr.enabled = false;
        // Enable particle emission (disabled on gameobject to prevent firing at start)
        var em = ps.emission;
        em.enabled = true;
        // Particle emission
        ps.Play();
        // Destroy gameobject
        Destroy(this.gameObject, 1f);
    }

}
