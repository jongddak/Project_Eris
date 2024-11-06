using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ������ �����ϸ� ������ ��ġ�� �÷��̾��� ũ�⿡ ���߾� ������ �缳���ϴ� ��ũ��Ʈ
/// ��溸�� �� �ٱ��ʿ� ������ ��ġ�� ������ �����Ͽ� ���
/// ����� ũ��� ������ ũ�⿡ ���� ������ ������ control���� �ν�����â���� �����Ͽ�
/// ������ �������� ���ġ�ǵ��� ������ �ʿ���
/// </summary>
public class CreatePlatform : MonoBehaviour
{
    [SerializeField] BoxCollider2D playerCollider; // �÷��̾��� �ڽ��ݶ��̴��� ũ�⿡ ���߾� ������ ������ ����
    [SerializeField] public Transform SetPos; // ���� ��ġ�� ���� ���� ��ġ ���� - �ٴ��� ����� ũ�� ���� �ٴڿ� ����
    public float space; // ������ ���� 
    [SerializeField] float control; // ������ ���� ������
    [SerializeField] public int num; // ������ ����
    GameObject[] child; // ��ġ�� ���� ������Ʈ�� �迭

    private void Awake()
    {
        num = gameObject.transform.childCount; // ����� ������ ����

        child = new GameObject[num]; // ��ġ�� ���� ������Ʈ �迭 ����
        for(int i = 0; i < child.Length; i++)
        {
            child[i] = gameObject.transform.GetChild(i).gameObject; // �ڽ� ������Ʈ�� �迭�� ��ġ
        }
        space = playerCollider.size.y * control; // ������ ����  = �÷��̾��� �ݶ��̴��� y�� * ������
    }

    private void Start()
    {
        for(int i = 0;i < child.Length;i++) // �迭�� ó������ ������
        {
            // �迭�� ������Ʈ�� ��ġ�� x���� �θ�� �����ϰ�
            // y���� �������� �������� ���� * Ƚ���� ��������
            child[i].transform.position = new Vector2(gameObject.transform.position.x, SetPos.position.y + space * i);
        }
    }
}
