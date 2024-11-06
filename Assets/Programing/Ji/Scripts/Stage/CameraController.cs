using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player; // �÷��̾� ������Ʈ ����
    [SerializeField] CinemachineVirtualCamera LCam; // �÷��̾ ������ �ٶ� ��(isLeft = true) ����ϴ� ī�޶�
    [SerializeField] CinemachineVirtualCamera RCam; // �÷��̾ �������� �ٶ� ��(isLeft = false) ����ϴ� ī�޶�
    public bool isLeft;

    private void Update()
    {
        // ������ �ٶ󺸰� ������
        if (isLeft == true) 
        {
            LCam.Priority = 20; // LCam�� Ȱ��ȭ
        }
        // �������� �ٶ󺸰� ������
        else
        {
            LCam.Priority = 5; // RCam�� Ȱ��ȭ
        }
    }
}
