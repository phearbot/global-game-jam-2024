using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Water : MonoBehaviour
{  

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
