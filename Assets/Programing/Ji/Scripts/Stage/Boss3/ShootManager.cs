using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� ���ϰ� ������ �ʿ�
/// </summary>
public class ShootManager : MonoBehaviour
{
    [Header("Balance")]

    [SerializeField] int sameNum; // ���ÿ� ����� �Ѿ� ����
    [SerializeField] float shootTimer; // �Ѿ��� �߻�Ǵ� ���������� ����Ǵ� �ð�
    float nowShootTimer;
    [SerializeField] float spaceTime; // �Ѿ��� �߻�Ǵ� �ð� ����

    List<int> rotationList; // �߻��� �Ѿ� ȸ������ ����Ʈ�� ������ ��

    [SerializeField] public Transform point; // �߻�� ������ ��ġ
    private BulletPool bulletPool = new BulletPool();

    [SerializeField] float bulletSpeed; // �߻��� �Ѿ��� �ӵ�
    [SerializeField] public List<BulletObj> fireBulletPool; //�⺻ ����Ʈ

    [Header("BulletObj.cs")]
    [SerializeField] public List<BulletObj> nowBulletPool; // ����� �Ѿ��� ������Ʈ Ǯ
    public BulletObj nowBullet; // �߻��� �Ѿ�



    private void Start()
    {
        rotationList = new List<int>();
        // �������ڸ��� �����Ǵ� ����Ʈ�� ��������
        fireBulletPool = GameObject.Find("BulletPool").GetComponent<BulletPool>().bulletPoolList;
    }
    /*
    // ������ ���°� ���¿� ���� ������ ����ϵ��� ������ �ʿ�
    // �Ѿ��� ������ ������ŭ �߻�
    // ��, ȸ���� 360�� �����ϰ� �߻�
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(timerCounter());
        }
    }
    */

    public void Shooting()
    {
        StartCoroutine(timerCounter());
    }

        // �����ð�(shootTimer)���� ��������(spaceTime)���� ����ϱ�
    IEnumerator timerCounter()
    {
        nowShootTimer = shootTimer; // �ð��� ����
        while (nowShootTimer >= 0) // �ð��� �����ִ� ����
        {
            RandomRotation(); // ���� �Լ��� ���ؼ� ��ġ�� �ʰ� ���
            for (int i = 0; i < sameNum; i++) // ���ÿ� ��µǴ� ����(sameNum)�� �Բ� ����ϰ�
            {
                // �Ѿ��� ȸ���� RandomRotation()���� ���� ����Ʈ���� ���
                nowBullet = bulletPool.GetBulletObjPool(new Vector2(point.position.x, point.position.y),
                                                            Quaternion.Euler(0, 0, rotationList[i]), fireBulletPool);
                nowBulletPool = fireBulletPool;
                Fire(nowBullet, nowBulletPool);
            }
            yield return new WaitForSeconds(spaceTime); // ������ ���ݸ�ŭ ����
            nowShootTimer -= spaceTime;
        }
    }

    /// <summary>
    /// ������ ��ü �Ѿ��� ������ �´� ȸ������ ��ġ�� �ʰ� ����Ͽ� ����Ʈ�� �����ϴ� �Լ�
    /// </summary>
    private void RandomRotation()
    {
        rotationList.Clear(); // �Ź� ���� �� ����Ʈ�� �ʱ�ȭ �ʿ�
        for (int i = 0; i < sameNum; i++)
        {
            int num = Random.Range(0, 37) * 10; // ������ ���� ��� - 1���� ������ ��� ���̰� ũ���ʾ� ���Ƿ� 10���� ���̸� �α�� ����
            if (rotationList.Contains(num)) // ������ ���� ���� ��� �ٽ� ���ڸ� ���
            {
                i--;
                continue;
            }
            else
            {
                rotationList.Add(num);
            }
        }
    }


    /// <summary>
    /// �Ѿ��� �߻��ϴ� �Լ�
    /// </summary>
    /// <param name="bulletObj"></param>
    /// <param name="bulletPoolList"></param>
    public void Fire(BulletObj bulletObj, List<BulletObj> bulletPoolList)
    {
        bulletObj.gameObject.SetActive(true); // �Ѿ� Ȱ��ȭ
        bulletPoolList.RemoveAt(bulletPoolList.Count - 1); // ����Ʈ���� ������ �Ѿ� ����
        Boss3Bullet bullet = bulletObj.GetComponent<Boss3Bullet>(); // Boss3Bullet�� Bullet ������Ʈ�� �����ͼ�
        bullet.SetSpeed(bulletSpeed); // �Ѿ��� �ӵ��� �����Ͽ� ���
    }
}
