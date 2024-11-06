using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShowList : MonoBehaviour
{
    [SerializeField] GameObject UITeamList; // ������ ����Ʈ UI
    [SerializeField] GameObject TeamBackground; // ������ ����Ʈ ��� �� ���
    [SerializeField] GameObject TeamText; // ������ �ؽ�Ʈ

    bool isClicked = false;

    private void Update()
    {
        Debug.Log(isClicked);
        if (!isClicked)
        {
            EndShow();
        }
    }

    public void Clicked()
    {
        isClicked = !isClicked;
    }
    public void StartShow()
    {
        UITeamList.SetActive(true); 
        TeamBackground.SetActive(true);
        TeamText.SetActive(true);
    }
    public void EndShow()
    {
        UITeamList.SetActive(false);
        TeamBackground.SetActive(false);
        TeamText.SetActive(false);
    }

}
