using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectionPanel : MonoBehaviour
{
    [SerializeField] WordContainer wordContainerPrefab;

    public List<WordContainer> wordContainers;

    private void Awake()
    {
        //PurgeChildren();
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PurgeChildren()
    {
        print("purging children");
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        wordContainers = new List<WordContainer>();
    }

    public void AddWord(string word, Color color)
    {
        print("adding");
        WordContainer container = Instantiate(wordContainerPrefab, transform);
        wordContainers.Add(container);

        container.container.text = word;
        container.container.outlineColor = color;
        container.container.faceColor = color;

        container.fill.text = word;
        container.fill.enabled = false;

    }

    public void SetWordToCompleteByColorTag(string tag)
    {
        int index = 0;
        if (tag == "Collectible-Red")
            index = 0;
        else if (tag == "Collectible-Green")
            index = 1;
        else if (tag == "Collectible-Blue")
            index = 2;
        else if (tag == "Collectible-Yellow")
            index = 3;
        else
            print("you fucked up big time");

        wordContainers[index].fill.enabled = true;
    }

    public void ResetCollectedStates()
    {
        foreach (var word in wordContainers)
        {
            word.fill.enabled = false;
        }
    }

}
