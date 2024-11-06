using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirrored : MonoBehaviour
{
    [SerializeField] GameObject player;

    // Update is called once per frame

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Mirror();
    }
    void Update()
    {
        
    }
    private void Mirror()
    {
        // �÷��̾ ������ ���ʿ� ������ ������ ��������, �����ʿ� ������ �������� �ٶ󺸰� ����
        if (player.transform.position.x < transform.position.x)
        {
            // ������ ������ �ٶ󺸵��� ��
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // ������ �������� �ٶ󺸵��� ��
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
