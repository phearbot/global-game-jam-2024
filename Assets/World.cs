using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [TextArea]
    public string joke;

    public List<Collectible> collectibles;
    public List<string> words;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindAnyObjectByType<GameManager>();

        print("world start function");

        gm.collectionPanel.PurgeChildren();
        for (int i = 0; i < words.Count; i++)
        {
            Color col = collectibles[i].GetComponent<MeshRenderer>().material.color;
            gm.collectionPanel.AddWord(words[i], col);
        }

        gm.itemsInLevel = words.Count;
        gm.joke = joke;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
