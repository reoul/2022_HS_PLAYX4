using System;
using System.Net.Sockets;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.Serialization;

public class KYB_Dissolve : MonoBehaviour
{
    private struct TopBottom
    {
        public float Top;
        public float Bottom;

        public TopBottom(float top, float bottom)
        {
            Top = top;
            Bottom = bottom;
        }
    }
    public enum DissolveState
    {
        /// <summary>
        /// 감소
        /// </summary>
        Dec = -2,
        /// <summary>
        /// 투명인 상태
        /// </summary>
        Hide = -1,
        /// <summary>
        /// 온 몸이 다 보이는 상태
        /// </summary>
        Nomal = 1,
        /// <summary>
        /// 증가
        /// </summary>
        Inc = 2
    }
    
    /// <summary>
    /// 디졸브 목표 시간
    /// </summary>
    [SerializeField]
    private int _dissolveSecond = 1;
    /// <summary>
    /// 디졸브 타이머
    /// </summary>
    private float _time = 0;
    /// <summary>
    /// 디졸브 상태
    /// </summary>
    public DissolveState State = DissolveState.Hide;
    /// <summary>
    /// 콜리더 상단 위치
    /// </summary>
    private float _topPos => _collider.bounds.max.y;
    /// <summary>
    /// 콜리더 하단 위치
    /// </summary>
    private float _bottomPos => _collider.bounds.min.y;
    /// <summary>
    /// 디졸브 상태 퍼센트(몇퍼센트 디졸브 됐는지)
    /// </summary>
    private float _percent => _time / _dissolveSecond;

    private BoxCollider _collider;
    private Material _material;
    private static readonly int CutoffHeight = Shader.PropertyToID("_CutoffHeight");
    private static readonly int DegeWidth = Shader.PropertyToID("_DegeWidth");
    private static readonly int NoiseStrength = Shader.PropertyToID("_NoiseStrength");

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _collider = GetComponent<BoxCollider>();
        if (State == DissolveState.Nomal || State == DissolveState.Dec)
        {
            _time = _dissolveSecond;
        }
        else
        {
            _time = 0;
        }
    }
    
    public void Update()
    {
        UpdateDissolve();
    }

    private void UpdateDissolve()
    {
        // 디졸브 하지 않을때
        if (State == DissolveState.Nomal || State == DissolveState.Hide)
        {
            TopBottom topBottom = GetDissolveTopBottom();
            SetHeight(State == DissolveState.Nomal ? topBottom.Top + 0.001f : topBottom.Bottom - 0.001f);
        }
        else
        {
            Dissolve();
        }
    }
    
    /// <summary>
    /// 생성 디졸브 시작
    /// </summary>
    public void StartCreateDissolve()
    {
        _time = 0;
        State = DissolveState.Inc;
    }

    /// <summary>
    /// 제거 디졸브 시작
    /// </summary>
    public void StartDestroyDissolve()
    {
        _time = _dissolveSecond;
        State = DissolveState.Dec;
    }
    
    /// <summary>
    /// 디졸브
    /// </summary>
    private void Dissolve()
    {
        
        _time += ((int)State / 2) * Time.deltaTime;
        _time = Mathf.Clamp(_time, 0, _dissolveSecond);
        // 디졸드 다 됐는지 확인
        if ((_percent % 1) == 0)
        {
            State = State > DissolveState.Nomal ? DissolveState.Nomal : DissolveState.Hide;
        }

        TopBottom topBottom = GetDissolveTopBottom();
        float height = Mathf.Lerp(topBottom.Bottom, topBottom.Top, _percent);
        _material.SetFloat(CutoffHeight, height);
    }

    /// <summary>
    /// 오브젝트의 상단 위치와 하단 위치를 가져옮
    /// </summary>
    /// <returns></returns>
    private TopBottom GetDissolveTopBottom()
    {
        float degeWidth = _material.GetFloat(DegeWidth);
        float noiseStrength = _material.GetFloat(NoiseStrength);
        float bottom = _bottomPos + degeWidth - noiseStrength;
        float top = _topPos + degeWidth + noiseStrength;
        return new TopBottom(top, bottom);
    }
    
    /// <summary>
    /// 디졸브 높이 설정
    /// </summary>
    /// <param name="height"></param>
    private void SetHeight(float height)
    {
        _material.SetFloat(CutoffHeight, height);
    }
}