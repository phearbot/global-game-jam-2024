using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectionPanel : MonoBehaviour
{
    [SerializeField] WordContainer wordContainerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PurgeChildren();

        AddWord("chicken", Color.red);
        AddWord("road", Color.green);
        AddWord("side", Color.blue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PurgeChildren()
    { 
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    void AddWord(string word, Color color)
    {
        print("adding");
        WordContainer container = Instantiate(wordContainerPrefab, transform);

        container.container.text = word;
        container.container.outlineColor = color;
        container.container.faceColor = color;

        container.fill.text = word;
        container.fill.enabled = false;

    }
    
}
