using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> Levels;

    [SerializeField] CinemachineVirtualCamera outsideCamera;
    [SerializeField] CinemachineVirtualCamera insideCamera;
    [SerializeField] Dialogue dialoguePanel;

    int currentLevelIndex = 0;
    GameObject currentLevel = null;

    public enum STATE { intro, waiting, playing }
    STATE state = STATE.intro;

    // Start is called before the first frame update
    void Start()
    {
        
        //SpawnLevel(1);
        //SpawnLevel(2);

        StartCoroutine(StartLevel());
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnLevel(int levelIndex)
    {
        if (currentLevel != null)
            Destroy(currentLevel);

        currentLevelIndex = levelIndex;
        currentLevel = Instantiate(Levels[levelIndex]);
    }


    public IEnumerator StartLevel()
    {
        // spawns level
        SpawnLevel(currentLevelIndex);
        currentLevelIndex++;

        // sets message with scrolling text
        yield return StartCoroutine(dialoguePanel.SetMessage("oh boy we're going to get going!!"));

        // zoom into the guys head
        yield return StartCoroutine(ZoomIntoHead());

        // give player control
        state = STATE.playing;

    }

    public IEnumerator EndLevel()
    {
        state = STATE.waiting;

        yield return StartCoroutine(ZoomOutOfHead());
    }

    IEnumerator ZoomIntoHead()
    {
        var dolly = outsideCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        while (dolly.m_PathPosition < 1)
        {
            dolly.m_PathPosition += Time.deltaTime;

            yield return null;
        }

    }

    IEnumerator ZoomOutOfHead()
    {
        var dolly = outsideCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        while (dolly.m_PathPosition > 0)
        {
            dolly.m_PathPosition -= Time.deltaTime;

            yield return null;
        }
    }


}
