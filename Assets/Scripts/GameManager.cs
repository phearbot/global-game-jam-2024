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

    int currentLevelIndex = 0;
    GameObject currentLevel = null;

    public enum STATE { intro, waiting, playing }
    STATE state = STATE.intro;

    // Collectible stats
    int itemsCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnLevel(0);
        //SpawnLevel(1);
        //SpawnLevel(2);

        StartLevel();
        //StartCoroutine(ZoomIntoHead());

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


    public void StartLevel()
    {
        // should start outside the guys head

        // funny guy should say something
        dialoguePanel.SetMessage("oh boy we're going to get going");

        // zoom into the guys head

        // give player control


    }

    public void EndLevel()
    {

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
