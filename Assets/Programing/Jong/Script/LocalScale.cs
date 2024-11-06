using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalScale : MonoBehaviour
{
    [SerializeField] Transform Boss;
    private Vector3 originalScale;

    private void Awake()
    {
        Boss = GameObject.FindGameObjectWithTag("Boss").transform;
    }

    void Start()
    {
        // ����Ʈ�� �ʱ� ������ ����
        originalScale = transform.localScale;
    }

    void Update()
    {
        // �θ��� x �������� -1�̸� ����Ʈ�� �������� ������� ����
        if (Boss.localScale.x < 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = originalScale;
        }
    }
}
