using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;


/// <summary>
/// DealPlatform ������Ʈ�� �����ϴ� ��ũ��Ʈ
/// public void ShowPlatform() �Լ��� ����ϴ��� Ȥ�� DealPlatform�� �����Ͽ� Ȱ��ȭ ��Ű�� ���
/// ��� ����
/// </summary>
public class DealPlatform : MonoBehaviour
{

    [SerializeField] float timer; // �ν�����â���� �÷����� ���̴� �ð��� ����
    float nowtTimer;
    // Ư���ð� ���� �����̸鼭 ������� �ٶ�� ��� �ڷ�ƾ���� ����
    // �ڿ��������� ���� �߰��Ͽ����� ��ȹ�ǵ��� �ƴ϶�� /**/ǥ�� �ִ� ������ ����
    /**/
    [SerializeField] float spaceTime; // �������� Ÿ�̹�
    [SerializeField] float flashTime; // �����̴� �ð�
    [SerializeField] SpriteRenderer spriteRenderer; // ������ �̹���
    /**/

    /// <summary>
    /// ������Ʈ Ȱ��ȭ �� ����ð� ����
    /// </summary>
    private void OnEnable()
    {
        nowtTimer = timer;
        spriteRenderer.color = new Color(1, 1, 1, 1f); // �̹����� ���� 100% ����
    }
    private void Update()
    {
        nowtTimer -= Time.deltaTime; // Timer�� ����
        /**/
        if (nowtTimer < flashTime)
        {
            StartCoroutine(DisapperPlatform());
        }
        /**/
        if (nowtTimer < 0)
        {
            gameObject.SetActive(false); // ������Ʈ ��Ȱ��ȭ
            /**/
            StopCoroutine(DisapperPlatform());
            /**/
        }
    }

    /// <summary>
    /// PatternController.cs���� �÷��̾ ������ ������ �� �ֵ��� �����ð�����
    /// DealPlatform�� ǥ���ϴ� �Լ�
    /// </summary>
    public void ShowPlatform()
    {
        gameObject.SetActive(true);
    }

    /**/
    /// <summary>
    /// ������ �����̴� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator DisapperPlatform()
    {
        while(true)
        {
        spriteRenderer.color = new Color(1, 1, 1, 0.5f); // �̹����� ���� 50% ����
        yield return new WaitForSeconds(spaceTime); // �����ð� ����
        spriteRenderer.color = new Color(1, 1, 1, 1f); // �̹����� ���� 100% ����
        yield return new WaitForSeconds(spaceTime); // �����ð� ����
        }
    }
    /**/
}
