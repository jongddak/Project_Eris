using UnityEngine;


/// <summary>
/// 몬스터의 공격 패턴 중 하나로
/// 랜덤으로 줄 발판들을 위 또는 아래로 움직이도록 설정
/// </summary>
public class PatternController : MonoBehaviour
{
    // 보스의 상태에 따라서
    // 보스의 체력이 25% 이상인 경우 한 개의 라인 씩 번갈아 가며 상하이동을 제어
    // 보스의 체력이 25% 이하인 경우 두 개의 라인 씩 번갈아 가며 상하이동을 제어

    [SerializeField] public bool isUpMove; // true 이면 위로 이동 - 각 공격패턴 선택 후 받아오도록 구현하기

    //보스의 현재 상태에 따라서 UpDate()에서 분기
    public enum BossState { normal, overdirve }
    // 보스의 체력이 25% 이상인 경우 - normal
    // 보스의 체력이 25% 이하인 경우 - overdive
    public BossState nowState; // 보스의 현재 상태

    private void Update()
    {
        switch (nowState)
        {
            case BossState.normal:

                break;
            case BossState.overdirve:

                break;
        }
    }

    public void setNormalChoice()
    {

    }

    public void setOverdirveChoice()
    {

    }





}
