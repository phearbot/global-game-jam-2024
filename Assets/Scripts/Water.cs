using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float scrollSpeed = 0.02f;
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        // water material offset moves (x/y) to add depth to level.
        rend.material.mainTextureOffset = new Vector2(offset, offset * 0.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if(collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.PlayerDeath();
        }
    }
}
