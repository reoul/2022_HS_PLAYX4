using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// VR 컨트롤러 핸드 타입
/// </summary>
public enum HandType
{
    Left,
    Right,
    LeftRight
}

public class VRController : MonoBehaviour
{
    public SteamVR_Input_Sources HandType;  //모두 사용, 왼손, 오른손
    public SteamVR_Action_Boolean GrabAction;
    
    public SteamVR_Action_Vibration HapticAction;

    private int frequency = 300;

    public void Vibration(int frequency)
    {
        frequency = Mathf.Clamp(frequency, 0, 60);
        Pulse(0.1f, frequency, 75, HandType);
    }

    private void Awake() {
        //StartCoroutine(aaaa());
    }

    private void Update()
    {
        if (GetTrigger())
        {
            //Vibration();
        }
        if(GetTriggerDown())
        {
            Debug.Log("트리거 버튼이 한번 누른 상태");
        }
        /*if (GetTriggerUp())
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gameObject.AddComponent<Rigidbody>();
            gameObject.transform.position = this.transform.position;
            gameObject.GetComponent<Rigidbody>().AddForce(VRControllerManager.Instance.direction * 10, ForceMode.Impulse);
        }*/
    }

    IEnumerator aaaa()
    {
        int i = 0;
        while (true)
        {
            Debug.Log(i);
            Pulse(2, i, 75, HandType);
            yield return new WaitForSeconds(2);
            i+=10;
        }
    }

    /// <summary>
    /// 진동
    /// </summary>
    /// <param name="duration">진동 지속 시간</param>
    /// <param name="frequency">진동 크기 0~320</param>
    /// <param name="amplitude">촉각 작용의 강도 0-1</param>
    /// <param name="source"></param>
    private void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        HapticAction.Execute(0,duration, frequency, amplitude, source);
    }

    /// <summary>
    /// 트리거 버튼을 지속적으로 누르고 있는 상태
    /// </summary>
    /// <returns>트리거 상태</returns>
    public bool GetTrigger()
    {
        return GrabAction.GetState(HandType);
    }

    /// <summary>
    /// 트리거 버튼이 한번 누른 상태
    /// </summary>
    /// <returns>트리거 상태</returns>
    public bool GetTriggerDown()
    {
        return GrabAction.GetStateDown(HandType);
    }

    /// <summary>
    /// 트리거 버튼이 한번 눌렀다가 땐 상태
    /// </summary>
    /// <returns>트리거 상태</returns>
    public bool GetTriggerUp()
    {
        return GrabAction.GetStateUp(HandType);
    }
}
