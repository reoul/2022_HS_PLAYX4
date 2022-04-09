using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureButton : MonoBehaviour
{
    private ButtonUI _buttonClass;

    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        _buttonClass = new StartMeasureClass();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            _buttonClass = _buttonClass.Execute();
        }
    }
}

public interface ButtonUI
{
    ButtonUI Execute();
}

public class StartMeasureClass : MonoBehaviour, ButtonUI
{
    public ButtonUI Execute()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        PlayerFloor.Instance.StartMeasure();
        return new StopMeasureClass();
    }
}
public class StopMeasureClass : MonoBehaviour, ButtonUI
{
    public ButtonUI Execute()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        PlayerFloor.Instance.StopMeasure();
        return new StartMeasureClass();
    }
}