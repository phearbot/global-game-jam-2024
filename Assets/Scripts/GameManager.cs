using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;

//public enum STATE { intro, waiting, playing }

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;

    [SerializeField]
    List<GameObject> Levels;

    [SerializeField] CinemachineVirtualCamera outsideCamera;
    [SerializeField] CinemachineVirtualCamera insideCamera;
    [SerializeField] Dialogue dialoguePanel;
    public CollectionPanel collectionPanel;
    [SerializeField] GameObject funnyGuy;

    int currentLevelIndex = 0;
    GameObject currentLevel = null;


    //public STATE state = STATE.intro;
    public bool playerCanMove = false;

    // Collectible stats
    int itemsCollected = 0;
    public int itemsInLevel = -1;

    public string joke = "";

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

        FindObjectOfType<Player>().transform.position = new Vector3(0, 5.5f, 0);

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
        // state = STATE.playing;
        playerCanMove = true;

    }

    public IEnumerator EndLevel()
    {
        // state = STATE.waiting;
        playerCanMove = false;
        yield return StartCoroutine(ZoomOutOfHead());
        yield return new WaitForSeconds(.5f);
        yield return StartCoroutine(dialoguePanel.SetMessage("Oh, I remember now!"));
        yield return new WaitForSeconds(.5f);
        yield return StartCoroutine(dialoguePanel.SetMessage(joke));
        PlayRandomLaugh();
        yield return new WaitForSeconds(6.5f);
        yield return StartCoroutine(dialoguePanel.SetMessage("Ha ha... That one was pretty good."));


        yield return StartLevel();
    }

    // Used when player falls out of arena
    public void RestartLevel()
    {
        // audio hook here
        Invoke("Reload", 1f);
    }

    // Will need to switch to current scene
    private void Reload()
    {
        //itemsCollected = 0;
        //collectionPanel.ResetCollectedStates();

        // respawn the collectibles
        // respawn the player

        GameObject _player = Instantiate(playerPrefab);
        _player.transform.position = new Vector3(0, 5.5f, 0);


        // SceneManager.LoadScene(1);
    }

    // Used for item collection
    public void ItemCollected(GameObject collectible)
    {
        print($"Collected: " + collectible.tag);
        itemsCollected += 1;
        collectionPanel.SetWordToCompleteByColorTag(collectible.tag);


        if (itemsCollected == itemsInLevel)
        {
            StartCoroutine(EndLevel());
        }
    }

    IEnumerator ZoomIntoHead()
    {
        var dolly = outsideCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        while (dolly.m_PathPosition < 1)
        {
            dolly.m_PathPosition += Time.deltaTime;

            yield return null;
        }
        outsideCamera.enabled = false;
    }

    IEnumerator ZoomOutOfHead()
    {
        outsideCamera.enabled = true;
        funnyGuy.SetActive(true);
        dialoguePanel.gameObject.SetActive(true);
        dialoguePanel.ZeroOutMessage();

        var dolly = outsideCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        while (dolly.m_PathPosition > 0)
        {
            dolly.m_PathPosition -= Time.deltaTime;

            yield return null;
        }
    }

    void PlayRandomLaugh()
    {
        int selector = Random.Range(0, 4);

        if (selector == 0)
            AudioManager.instance.PlaySubclip("laugh", 1f, 6f);
        else if (selector == 1)
            AudioManager.instance.PlaySubclip("laugh", 7f, 13f);
        else if (selector == 2)
            AudioManager.instance.PlaySubclip("laugh", 14f, 20f);
        else if (selector == 3)
            AudioManager.instance.PlaySubclip("laugh", 20f, 26f);
    }


}
