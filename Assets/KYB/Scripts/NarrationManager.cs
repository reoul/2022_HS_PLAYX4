﻿using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class NarrationClips
{
    /// <summary>환영합니다. 이곳은 Sector V 훈련소 입니다.</summary>
    public AudioClip Intro1;
    /// <summary>이곳에서는 몬스터를 활로 잡는 기본적인 훈련을 진행하게 될 것입니다.</summary>
    public AudioClip Intro2;
    /// <summary>발 아래를 보시면, 3개의 칸이 존재합니다.</summary>
    public AudioClip Intro3;
    /// <summary>이것은 플레이어가 실제 움직일 수 있는 범위입니다.</summary>
    public AudioClip Intro4;
    /// <summary>활동 범위를 측정하기 위해 왼쪽이나 오른쪽으로 한발자국 움직여 주세요.</summary>
    public AudioClip CheckMove;
    /// <summary>앞에 보이는 활을 잡고 스타트 타겟을 겨눠주세요.</summary>
    public AudioClip Shot1;
    /// <summary>이제 활 시위를 놓아 화살을 쏘면 됩니다.</summary>
    public AudioClip Shot2;
    /// <summary>훈련 완수 조건은 제한 시간동안 몬스터를 쓰러뜨려 목표 점수 이상을 획득하는 것입니다.</summary>
    public AudioClip Condition;
    /// <summary>훈련을 시작하겠습니다.</summary>
    public AudioClip StartTraning;
    /// <summary>LEVEL 1이 시작되었습니다.</summary>
    public AudioClip StartLevel1;
    /// <summary>LEVEL 2가 시작되었습니다.</summary>
    public AudioClip StartLevel2;
    /// <summary>마지막 레벨을 시작하겠습니다.</summary>
    public AudioClip StartLevel3;
    /// <summary>이 레벨부터는 직접 움직여서 적의 공격을 피할 수 있습니다.</summary>
    public AudioClip MoveAvoid;
    /// <summary>적의 약점 표시를 화살로 쏘아 점수를 획득하세요.</summary>
    public AudioClip GetScore;
    /// <summary>현재 레벨에서 주어진 시간은 1분입니다.</summary>
    public AudioClip Remain1Min;
    /// <summary>현재 레벨에서 주어진 시간은 1분 30초입니다.</summary>
    public AudioClip Remain1Min30Second;
    /// <summary>현재 레벨에서 주어진 시간은 2분입니다.</summary>
    public AudioClip Remain2Min;
    /// <summary>현재 레벨에서 주어진 시간은 2분 30초입니다.</summary>
    public AudioClip Remain2Min30Second;
    /// <summary>현재 레벨에서 주어진 시간은 3분 30초입니다.</summary>
    public AudioClip Remain3Min30Second;
    /// <summary>현재 레벨에서 주어진 시간은 4분입니다.</summary>
    public AudioClip Remain4Min;
    /// <summary>제한 시간 내에 골렘을 쓰러뜨려 미션을 완수하세요.</summary>
    public AudioClip ConditionGolem;
    /// <summary>목표점수까지 50% 남았습니다.</summary>
    public AudioClip Persent50;
    /// <summary>레벨 클리어.</summary>
    public AudioClip LevelClear;
    /// <summary>잠시 후 다음 훈련이 시작됩니다.</summary>
    public AudioClip NextTraning;
    /// <summary>훈련에 실패하였습니다.</summary>
    public AudioClip FailedTraning;
    /// <summary>다시 도전하세요.</summary>
    public AudioClip TryAgain;
    /// <summary>훈련을 완수하였습니다.</summary>
    public AudioClip SuccessTraning;
    /// <summary>다음 sector에서 훈련을 진행해주세요.</summary>
    public AudioClip NextSector;
    /// <summary>고생하셨습니다.</summary>
    public AudioClip ThankYouEffort;
    /// <summary>스코어 보드에 있는 당신의 랭킹을 확인해 보세요.</summary>
    public AudioClip ScoreBoard;
}

public class NarrationManager : Singleton<NarrationManager>
{
    public NarrationClips NarrationClips;
    private AudioSource _audioSource;
    public AudioClip LastNarrationClip { get; private set; }
    public bool IsCheckFlag = false;
    private bool _isFirst = true;

    private WaitForEndOfFrame _waitEndFrame = new WaitForEndOfFrame();

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(NarrationCoroutine());
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            IsCheckFlag = false;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            ScoreSystem.Score += 100;
        }
    }

    public void PlayVoice(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        LastNarrationClip = audioClip;
        _audioSource.Play();
        Debug.Log($"{audioClip.name} 사운드가 플레이 됩니다");
    }

    public IEnumerator PlayVoiceCoroutine(AudioClip audioClip)
    {
        PlayVoice(audioClip);
        yield return new WaitForSeconds(audioClip.length+0.2f);
    }
    private IEnumerator NarrationCoroutine()
    {
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.Intro1));
        //yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.Intro2));
        //yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.Intro3));
        //yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.Intro4));
        PlayVoice(NarrationClips.CheckMove);
        yield return StartCoroutine(CheckMoveCoroutine());
        // 활 소환
        PlayVoice(NarrationClips.Shot1);
        yield return StartCoroutine(CheckFlagCoroutine());
        PlayVoice(NarrationClips.Shot2);
        yield return StartCoroutine(CheckFlagCoroutine());
        // 스테이지 1 before 이동
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.StartTraning));
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.Condition));
        // 스테이지 1 이동
        StageManager.Instance.NextStage();
        // 스테이지 주변 환경 등장
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.StartLevel1));
        // 시간 표현
        yield return StartCoroutine(PlayVoiceCoroutine(GetTimeAudio(60)));
        StartCoroutine(CheckScorePercent50Coroutine());
        // 클리어 했는지 표시
        yield return StartCoroutine(CheckTrainingResult());
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.LevelClear));
        // 스테이지 2 before 이동
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.NextTraning));
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.MoveAvoid));
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.GetScore));
        // 스테이지 2 이동
        StageManager.Instance.NextStage();
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.StartLevel2));
        yield return StartCoroutine(PlayVoiceCoroutine(GetTimeAudio(150)));
        yield return StartCoroutine(CheckScorePercent50Coroutine());
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.Persent50));
        yield return StartCoroutine(CheckTrainingResult());
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.LevelClear));
        // 스테이지 3 before 이동
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.NextTraning));
        yield return new WaitForSeconds(3f);
        StageManager.Instance.NextStage();
        // 스테이지 3 이동
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.StartLevel3));
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.ConditionGolem));
        yield return StartCoroutine(PlayVoiceCoroutine(GetTimeAudio(210)));
        yield return StartCoroutine(CheckTrainingResult());
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.LevelClear));
        
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.SuccessTraning));
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.NextSector));
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.ThankYouEffort));
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.ScoreBoard));
        yield return null;
    }

    private void FailedTraining()
    {
        StopAllCoroutines();
        StartCoroutine(FailedTrainingCoroutine());
    }

    private IEnumerator FailedTrainingCoroutine()
    {
        StageManager.Instance.ChangeToEnding();
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.FailedTraning));
        yield return StartCoroutine(PlayVoiceCoroutine(NarrationClips.TryAgain));
    }

    /// <summary>
    /// 플레이어가 움직였는지 체크
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckMoveCoroutine()
    {
        while (PlayerFloor.Instance.PlayerCurFloor == 1)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// 플래그 확인 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckFlagCoroutine()
    {
        IsCheckFlag = true;
        while (IsCheckFlag)
        {
            yield return _waitEndFrame;
        }
    }

    private IEnumerator CheckScorePercent50Coroutine()
    {
        while (((float)ScoreSystem.Score / StageManager.Instance._curStage.GoalScore) < 0.5f)
        {
            yield return _waitEndFrame;
        }
        PlayVoice(NarrationClips.Persent50);
    }

    private IEnumerator CheckTrainingResult()
    {
        while (!StageManager.Instance._curStage.IsFinish)
        {
            yield return _waitEndFrame;
        }
        if ((float) ScoreSystem.Score / StageManager.Instance._curStage.GoalScore < 1)
        {
            FailedTraining();
        }
    }

    private AudioClip GetTimeAudio(int time)
    {
        switch (time)
        {
            case 60:
                return NarrationClips.Remain1Min;
            case 90:
                return NarrationClips.Remain1Min30Second;
            case 120:
                return NarrationClips.Remain2Min;
            case 150:
                return NarrationClips.Remain2Min30Second;
            case 210:
                return NarrationClips.Remain3Min30Second;
            case 240:
                return NarrationClips.Remain4Min;
            default:
                return NarrationClips.Remain1Min;
        }
    }
}