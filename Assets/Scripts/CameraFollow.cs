using UnityEngine;

using System.Collections;

public class CameraFollow : MonoBehaviour

{
    public GameObject player;
    public Vector3 playerOffset;

    // Use this for initialization
    void Start()
    {
        playerOffset = transform.position - player.transform.position;
    }

    

    // Update is called once per frame
    void Update()
    {   
        transform.LookAt(player.transform);
        transform.position = player.transform.position + playerOffset;
    }
}
