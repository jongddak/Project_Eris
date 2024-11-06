using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static PatternController;

public class Boss3Controller : MonoBehaviour
{
    [SerializeField] Transform[] shootingPoints;
    [SerializeField] GameObject[] lineRender;
    [SerializeField] GameObject player;

    [SerializeField] GameObject dealTimePlatform;


    public UnityEvent bulletShooting;
    public UnityEvent movePlatform;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    // �߻�� ������
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject missilePrefab;
   



    [SerializeField] float bossHp;  // ���� ü��
    [SerializeField] float patternTerm; // ���� ���ϰ� �ð� 
    [SerializeField] int patternCount = 0;
    [SerializeField] bool phasech;

    
    private int patternSelectNum;

    [SerializeField] float laserTerm = 0.7f; // ������ �߻� ����
    [SerializeField] int missileQty = 3; // �̻��� ����(�ִ� 4)
   // [SerializeField] bool onPhaseChange = false; // ������ �ٲ�� true
    Coroutine curCoroutine;
    private void Start()
    {
        
        curCoroutine = StartCoroutine(BossDo());

    }
    private void Update()
    {
       
        if (bossHp <= 0) 
        {
            Debug.Log("���� ���");
            StopCoroutine("BossDo");
            StartCoroutine(Died());
        }
    }
    IEnumerator BossDo() // ������ �ൿ. ���� 4�� �߾� �Ѿ˹߻� , �������߻� , �̻��� �߻� , ���� �̵�  
                        // ���� �ð�(���� �����ϰ�)���� ���� 1������ ����, ������ ��� �����ϰ� ���� �߾� ���� ����(��Ÿ��)
    {
        WaitForSeconds time = new WaitForSeconds(patternTerm);

        while (bossHp > 0)  // ü���� 0���� ũ�� �ݺ�
        {
            
            if (patternCount >= 3)
            {
                //  ������ 3�� �����ϸ� ��Ÿ��(�߾� ���� ����)
                Debug.Log("��Ÿ��");
                dealTimePlatform.SetActive(true);
                patternCount = 0;
            }
            else
            {
                patternSelectNum = Random.Range(0, 4);// 0 ~ 3
                switch (patternSelectNum) // ���� ���� ���� 
                {

                    case 0:
                        Debug.Log("�߾� �Ѿ�");
                        audioSource.clip = audioClips[2];
                        audioSource.Play();
                        bulletShooting?.Invoke();
                        break;
                    case 1:
                        Debug.Log("���� �̵�");
                        audioSource.clip = audioClips[0];
                        audioSource.Play();
                        movePlatform?.Invoke();
                        yield return time;
                        audioSource.clip = audioClips[1];
                        audioSource.Play();
                        break;
                    case 2:
                        Debug.Log("�̻���");
                        StartCoroutine(ShootingMissile());
                        break;
                    case 3:
                        Debug.Log("������");
                        StartCoroutine(ShootingLaser());
                        break;
                }
                patternCount++;
            }

            yield return time;
        }



        yield return new WaitForSeconds(1f);
    }

    IEnumerator ShootingLaser() //������ ����, 1�߾� 6���� �� 
    {
        for (int i = 0; i < 6; i++)
        {
            int rand = Random.Range(0, 6);
            lineRender[rand].SetActive(true);
            yield return new WaitForSeconds(1f);
            lineRender[rand].SetActive(false);
            audioSource.clip = audioClips[5];
            audioSource.Play();
            GameObject obj = Instantiate(laserPrefab, shootingPoints[rand].position, shootingPoints[rand].rotation);
            Destroy(obj,20f);
            yield return new WaitForSeconds(laserTerm);
        }

        yield return null;
    }
    IEnumerator ShootingMissile() // �̻��� ���� , �ѹ��� 3���� �� 
    {
        List<int> randNums = new List<int> { 0, 1, 2, 3, 4, 5 };  //  �ߺ� �ȵǴ� n�� �̴� ��� 
        List<int> pickNum = new List<int>();
        while (pickNum.Count < missileQty)
        {
            int rand = Random.Range(0, randNums.Count);
            if (randNums.Contains(rand) == true)
            {
                pickNum.Add(rand);
                randNums.Remove(rand);
            }
            if (pickNum.Count == missileQty)
            {
                
                break;

            }
        }
        for (int i = 0; i < pickNum.Count; i++) 
        {
            lineRender[pickNum[i]].SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < pickNum.Count; i++)
        {
            lineRender[pickNum[i]].SetActive(false);
        }
        audioSource.clip = audioClips[4];
        audioSource.Play();
        for (int i = 0; i < pickNum.Count; i++) 
        {   

            Instantiate(missilePrefab, shootingPoints[pickNum[i]].position, shootingPoints[pickNum[i]].rotation);
        }
    }
    //private void PhaseChange() // ���� ü�� ���ϸ� ������ ���� , ���� ���Ϻ��� �� �̹��� ���� 
    //{
    //    if (onPhaseChange == false) 
    //    {
    //        Debug.Log("������ ����");
    //        patternTerm = 4f; // �⺻ 6�� 
    //        laserTerm = 0.3f; // �⺻ 1�� 
    //        missileQty = 4;  // �⺻ 3�� 
    //        onPhaseChange =true;

    //        boss.sprite = bossPhase2img;
    //        backGroundimg.sprite = bossPhase2backimg;
    //        mid.color = new Color(1f, 0.698f, 0.698f, 1f);
    //    }
    //}

 


    public void TakeDamage(float damage) // ������Ʈ�� �̺�Ʈ�� ó���ϸ� �ɵ�
    {
        bossHp -= damage;

        // ������ ü���� 0 ���ϰ� �Ǹ� ���¸� Die�� ����
        if (bossHp <= 0)
        {
            StartCoroutine(Died());
        }
    }
    private void Die() 
    {
        // ���� ��� 

        if (phasech == false)
        {
            //1������ �� 
            GameManager.Instance.LoadSceneByName("Boss3DPhase");
        }
        else if (phasech == true) // 2������ �� 
        {
            DataManager.Instance.LoadGameData();
            DataManager.Instance.data.isUnlock[2] = true;
            DataManager.Instance.SaveGameData();
            GameManager.Instance.LoadSceneByName("Boss3DEnd");
        }
    }
    IEnumerator Died()
    {
        if (phasech == false)
        {
            //1������ �� 
            yield return new WaitForSeconds(0.5f);
            GameManager.Instance.LoadSceneByName("Boss3DPhase");
        }
        else if (phasech == true) // 2������ �� 
        {
            yield return new WaitForSeconds(0.5f);
            DataManager.Instance.LoadGameData();
            DataManager.Instance.data.isUnlock[2] = true;
            DataManager.Instance.SaveGameData();
            GameManager.Instance.LoadSceneByName("Boss3DEnd");
        }
    }
}
