using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] float textScrollSpeed = 30;

    [SerializeField] TextMeshProUGUI messageTMP;
    float visibleCharacters = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SetMessage(string _message)
    {
        messageTMP.text = _message;

        yield return StartCoroutine(ScrollText());
    }

    IEnumerator ScrollText()
    {

        while (visibleCharacters < messageTMP.text.Length)
        {
            visibleCharacters += Time.deltaTime * textScrollSpeed;
            messageTMP.maxVisibleCharacters = (int)visibleCharacters;

            yield return null;
        }
    }

    public IEnumerator ReadJoke()
    {
        yield return null;
    }

}
