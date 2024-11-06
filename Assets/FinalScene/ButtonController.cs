using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    // ���� ���� ��ư
    public void GameStart()
    {
        DataManager.Instance.LoadGameData(); // �ҷ�����
        DataManager.Instance.data.isUnlock[0] = false;
        DataManager.Instance.data.isUnlock[1] = false;
        DataManager.Instance.data.isUnlock[2] = false;
        DataManager.Instance.SaveGameData();
        GameManager.Instance.LoadSceneByName("Lobby");
    }

    // ���� �̾��ϱ� ��ư
    public void SaveGameStart()
    {
        DataManager.Instance.LoadGameData();
        GameManager.Instance.LoadSceneByName("Lobby");
    }

    // ���� ���� ��ư
    public void EndGame()
    {
        Application.Quit();
    }
}
