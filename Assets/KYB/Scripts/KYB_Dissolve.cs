using System;
using System.Net.Sockets;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Compilation;
using UnityEngine;

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
    private enum DissolveState
    {
        /// <summary>
        /// 감소
        /// </summary>
        Dec = -1,
        /// <summary>
        /// 변화 없음
        /// </summary>
        Nomal,
        /// <summary>
        /// 증가
        /// </summary>
        Inc
    }
    /// <summary>
    /// 디졸브 목표 시간
    /// </summary>
    public int DissolveSecond = 1;
    /// <summary>
    /// 디졸브 타이머
    /// </summary>
    private float _time = 0;
    /// <summary>
    /// 디졸브 상태
    /// </summary>
    private DissolveState _state = DissolveState.Nomal;
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
    private float _percent => _time / DissolveSecond;

    private BoxCollider _collider;
    private Material _material;
    private static readonly int CutoffHeight = Shader.PropertyToID("_CutoffHeight");
    private static readonly int DegeWidth = Shader.PropertyToID("_DegeWidth");
    private static readonly int NoiseStrength = Shader.PropertyToID("_NoiseStrength");

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _collider = GetComponent<BoxCollider>();
    }
    
    public void Update()
    {
        UpdateDissolve();
    }

    private void UpdateDissolve()
    {
        // 디졸브 중일때
        if (_state != DissolveState.Nomal)
        {
            Dissolve();
        }
        else //안끝났다면
        {
            TopBottom topBottom = GetDissolveTopBottom();
            SetHeight(_percent >= 1 ? topBottom.Top + 0.001f : topBottom.Bottom - 0.001f);
        }
    }
    
    /// <summary>
    /// 생성 디졸브 시작
    /// </summary>
    public void StartCreateDissolve()
    {
        _time = 0;
        _state = DissolveState.Inc;
    }

    /// <summary>
    /// 제거 디졸브 시작
    /// </summary>
    public void StartDestroyDissolve()
    {
        _time = DissolveSecond;
        _state = DissolveState.Dec;
    }
    
    /// <summary>
    /// 디졸브
    /// </summary>
    private void Dissolve()
    {
        // 박스 콜리더의 영역을 가져온다
        var bounds = GetComponent<BoxCollider>().bounds;
        _time += (int)_state * Time.deltaTime;
        if ((_percent % 1) == 0)
        {
            _time = (_state == DissolveState.Inc) ? DissolveSecond : 0;
            _state = DissolveState.Nomal;
        }

        TopBottom topBottom = GetDissolveTopBottom();
        float height = Mathf.Lerp(topBottom.Bottom, topBottom.Top, _percent);
        _material.SetFloat(CutoffHeight, height);
    }

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