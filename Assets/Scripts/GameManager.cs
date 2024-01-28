using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> Levels;

    [SerializeField] CinemachineVirtualCamera outsideCamera;
    [SerializeField] CinemachineVirtualCamera insideCamera;
    [SerializeField] Dialogue dialoguePanel;
    [SerializeField] CollectionPanel collectionPanel;
    [SerializeField] GameObject funnyGuy;

    int currentLevelIndex = 0;
    GameObject currentLevel = null;

    public enum STATE { intro, waiting, playing }
    STATE state = STATE.intro;

    // Collectible stats
    int itemsCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.FadeinBGM("BGM");

        collectionPanel.gameObject.SetActive(false);
        dialoguePanel.gameObject.SetActive(false);
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
        yield return new WaitForSeconds(.5f);
        dialoguePanel.gameObject.SetActive(true);

        // spawns level
        SpawnLevel(currentLevelIndex);
        currentLevelIndex++;

        // sets message with scrolling text
        yield return StartCoroutine(dialoguePanel.SetMessage("Hey there, why don't you hop in my head and help me think of a joke?"));
        yield return new WaitForSeconds(.5f);
        yield return StartCoroutine(dialoguePanel.SetMessage("What are you waiting for? Get in there!!"));

        // zoom into the guys head
        yield return StartCoroutine(ZoomIntoHead());

        // give player control
        dialoguePanel.gameObject.SetActive(false);
        collectionPanel.gameObject.SetActive(true);
        funnyGuy.SetActive(false);
        outsideCamera.enabled = false;
        state = STATE.playing;

    }

    public IEnumerator EndLevel()
    {
        state = STATE.waiting;

        yield return StartCoroutine(ZoomOutOfHead());
    }

    // Used when player falls out of arena
    public void RestartLevel()
    {
        // audio hook here
        Invoke("Reload", 0.5f);
    }

    // Will need to switch to current scene
    private void Reload()
    {
        SceneManager.LoadScene(1);
    }

    // Used for item collection
    public void ItemCollected(GameObject collectible)
    {
        print($"Collected: " + collectible.tag);
        itemsCollected += 1;
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
