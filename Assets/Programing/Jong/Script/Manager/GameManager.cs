using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Update() // �׽�Ʈ�� 
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Debug.Log("�Ͻ� ����");
            PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadSceneByName("ManagerTest2");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            LoadSceneByName("ManagerTest3");
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            LoadSceneByName("ManagerTest");
        }

    }

    public void LoadSceneByName(string sceneName) // �� �̸����� �ҷ����� , ���ε��� �̸����� �ҷ����� ���� 
    {
            SceneManager.LoadScene(sceneName);
    }

    public void PauseGame() // �Ͻ����� 
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
        }
    }
}
