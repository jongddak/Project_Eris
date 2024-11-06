using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BulletPool : MonoBehaviour
{
    [SerializeField] public List<BulletObj> bulletPoolList = new List<BulletObj>();
    [SerializeField] int size; // �Ѿ��� ����� ������Ʈ Ǯ�� ������
    [SerializeField] BulletObj prefab; // ������ �Ѿ� �������� �ν��Ͻ� â���� ����
    [SerializeField] Transform point; // �Ѿ��� �����Ǵ� ��ġ

    private void Awake()
    {
        // ������ ��ŭ�� �Ѿ��� �����ؼ� ����Ʈ�� ����
        for(int i = 0; i < size; i++)
        {
            BulletObj bullet = Instantiate(prefab);
            bullet.gameObject.SetActive(false); // ���� �� ��Ȱ��ȭ ���·� ������ ��
            bullet.transform.parent = transform;
            bulletPoolList.Add(bullet);
        }
        point = GameObject.Find("ShootManager").GetComponent<ShootManager>().point; // StootManager���� ������ �߻����� �ҷ�����
    }

    /// <summary>
    /// ShootManager���� ����ϸ�
    /// ����Ʈ�� ������ �Ѿ� ��ü�� ���ϴ� ��ġ�� ȸ���� �����ϴ� �Լ�
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="bullets"></param>
    /// <returns></returns>
    public BulletObj GetBulletObjPool(Vector2 position, Quaternion rotation, List<BulletObj> bullets)
    {
        // ����Ʈ�� �뷮�� �ڸ��� ���� ���
        if (bullets.Count > 0)
        {
            BulletObj fireBullet = bullets[bullets.Count - 1];
            fireBullet.transform.position = position;
            fireBullet.transform.rotation = rotation;
            fireBullet.transform.parent = null;
            fireBullet.bulletPool = this; // ��ȯ�� ������Ʈ�� �����ؾ���
                                          // BulletObj.cs���Ͽ� returnPool�� �����Ͽ� ���
            return fireBullet;
        }
        else
        {
            // ����� ź�� ���� ���
            BulletObj bullet = Instantiate(prefab,position,rotation);
            /*bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.transform.parent = null;*/
            return bullet; // ���� �����ؼ� �߻�
        }
    }

    public void ReturnBulletObjPull(BulletObj bullet, List<BulletObj> bulletPool)
    {
        bullet.gameObject.SetActive(false); // �Ѿ��� ��Ȱ��ȭ�ϰ�
        bullet.transform.parent = point; // ������ ��ġ�� �̵�����
        bulletPool.Add(bullet); // ������ ����Ʈ�� ���� �߰�
    }

}
