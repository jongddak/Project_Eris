using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletObj : MonoBehaviour
{
    public BulletPool bulletPool;

    [SerializeField] public List<BulletObj> shootBulletPool; // �Ѿ��� ������Ʈ Ǯ ����

    [SerializeField] float returnTime; // �Ѿ��� ��ȯ�ð�
    [SerializeField] public float curTime; // �Ѿ��� ������ ���� �ð����� �Ѿ��� Ȱ��ȭ�Ǹ� ȸ���ð��� �����ϰ� ����

    // ShootManger.cs���� ��ȯ�ϱ� ���� ���
    List<BulletObj> returnPool; // ��ȯ�� �Ѿ��� ����Ʈ
    BulletObj returnObj; // ��ȯ�� BulletObj

    /// <summary>
    /// ��ü Ȱ��ȭ�� ���� shootManager.cs�� ���� �߻�� �� BulletObj ��ü�� ����Ʈ�� �����ϰ�
    /// ���� �ð��� returnTime���� Ÿ�̸Ӹ� ����
    /// </summary>
    private void OnEnable()
    {
        curTime = returnTime;
        // �Ѿ��� �߻� �� ShootManager���� �����Ǵ� ������Ʈ�� ����Ʈ ��������
        returnPool = GameObject.Find("ShootManager").GetComponent<ShootManager>().nowBulletPool;
        returnObj = GameObject.Find("ShootManager").GetComponent<ShootManager>().nowBullet;
    }

    private void Update()
    {
        curTime -= Time.deltaTime; // �ð� Ÿ�̸� ����

        if (curTime < 0)
        {
            ReturnBullet(returnObj, returnPool); // ȸ���� ����
        }
    }

    // �Ѿ��� �÷��̾�� �浹 ��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ReturnBullet(returnObj, returnPool); // ȸ���� ����
        }
    }

    // ��Ȱ�� �Ѿ˰� ��Ȱ�� ����Ʈ�� �޾Ƽ� ȸ���� ����
    public void ReturnBullet(BulletObj returnObj, List<BulletObj> bulletPools)
    {
        bulletPool.ReturnBulletObjPull(returnObj, bulletPools); // returnPool.cs�� �ִ� ReturnBulletPool()�� ����Ͽ�
        // ��ȯ�� ����
    }

}
