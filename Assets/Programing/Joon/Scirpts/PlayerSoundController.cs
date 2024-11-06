using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] AudioClip landingClip;
    [SerializeField] AudioClip runClip;
    [SerializeField] AudioClip attack1Clip;
    [SerializeField] AudioClip attack2Clip;
    [SerializeField] AudioClip attack3Clip;
    [SerializeField] AudioClip dashClip;
    [SerializeField] AudioClip dieClip;
    [SerializeField] AudioClip defeatClip;

    [SerializeField] AudioSource runAudioSource;


    // AudioSource ������Ʈ
    private AudioSource audioSource;

    void Start()
    {
        // AudioSource �ʱ�ȭ
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // ���� ���� ���
    public void PlayLandingSound()
    {
        PlaySound(landingClip);
    }

    // �ȱ� ���� ���
    public void StartRunSound()
    {
        runAudioSource.enabled = true; // ����� �ҽ� Ȱ��ȭ
    }

    // �ȱ� ���� ����
    public void StopRunSound()
    {
        runAudioSource.enabled = false; // ����� �ҽ� ��Ȱ��ȭ
    }

    // ���� ���� ���
    public void PlayAttack1Sound()
    {
        PlaySound(attack1Clip);
    }

    public void PlayAttack2Sound()
    {
        PlaySound(attack2Clip);
    }

    public void PlayAttack3Sound()
    {
        PlaySound(attack3Clip);
    }

    public void PlayDashSound()
    {
        PlaySound(dashClip);
    }

    public void PlayDieSound()
    {
        PlaySound(dieClip);
    }

    public void PlayDefeatSound()
    {
        PlaySound(defeatClip);
    }



    // ���� ����� ���� ���� �޼���
    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
