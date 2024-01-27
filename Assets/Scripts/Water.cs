using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        print($"collided with " + collision.gameObject.tag);
        if(collision.gameObject.tag == "Player")
        {
            // audio hook here
            Invoke("Reload", 1.59f);
        }
    }

    
    // Move this to GameManager
    // hard spawn for temporary fix when falling off arena
    void Reload()
    {
        SceneManager.LoadScene(1);
    }

}
