using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

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

    private void OnEnable()
    {
        
    }

    public void SetMessage(string _message)
    {
        messageTMP.text = _message;

        StartCoroutine(ScrollText());
    }

    IEnumerator ScrollText()
    {

        while (visibleCharacters < messageTMP.text.Length)
        {
            messageTMP.maxVisibleCharacters = (int)visibleCharacters;

            yield return null;
        }
    }
}
