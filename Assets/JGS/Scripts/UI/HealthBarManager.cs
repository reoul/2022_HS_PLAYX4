using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _healthBlock;

    [SerializeField]
    private int _maxHealth;

    private Stack<GameObject> _healthStack;

    private void Awake()
    {
        _healthStack = new Stack<GameObject>();
    }

    private void Start()
    {
        for (int i = 0; i < _maxHealth; i++)
        {
            GameObject _tempHealthBlock = GameObject.Instantiate(_healthBlock, this.transform);
            RectTransform _tempRect = _tempHealthBlock.GetComponent<RectTransform>();
            _tempRect.sizeDelta = new Vector2((20f / _maxHealth) * 65, _tempRect.sizeDelta.y);
            _tempHealthBlock.transform.localPosition = new Vector3(i * _tempRect.sizeDelta.x, 0, 0);
            _healthStack.Push(_tempHealthBlock);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            if(_healthStack.Count > 0)
            {
                GameObject _curHealthBlock = _healthStack.Pop();
                Destroy(_curHealthBlock);
            }
        }
    }
}
