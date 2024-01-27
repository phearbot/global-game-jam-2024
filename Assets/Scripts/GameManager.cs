using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> Levels;

    [SerializeField]
    CinemachineVirtualCamera OutsideCamera;
    CinemachineVirtualCamera InsideCamera;


    int currentLevelIndex = 0;
    GameObject currentLevel = null;

    public enum STATE { intro, waiting, playing }
    STATE state = STATE.intro;

    // Start is called before the first frame update
    void Start()
    {
        SpawnLevel(0);
        //SpawnLevel(1);
        //SpawnLevel(2);

        StartCoroutine(ZoomIntoHead());
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

        // zoom into the guys head

        // give player control
    }

    public void EndLevel()
    {

    }

    IEnumerator ZoomIntoHead()
    {
        var dolly = OutsideCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        while (dolly.m_PathPosition < 1)
        {
            dolly.m_PathPosition += Time.deltaTime;

            yield return null;
        }

    }

    IEnumerator ZoomOutOfHead()
    {
        var dolly = OutsideCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        while (dolly.m_PathPosition > 0)
        {
            dolly.m_PathPosition -= Time.deltaTime;

            yield return null;
        }
    }

}
