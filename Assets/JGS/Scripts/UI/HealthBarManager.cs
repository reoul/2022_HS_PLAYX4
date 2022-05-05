using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : Singleton<HealthBarManager>
{
    [SerializeField]
    private GameObject _healthBlock;

    [SerializeField]
    private int _maxHealth;

    private Stack<GameObject> _healthStack;

    private Color[] color;

    private void Awake()
    {
        _healthStack = new Stack<GameObject>();
    }

    private void Start()
    {
        color = new Color[2] { new Color(0.9960784f, 0.3035352f, 0.282353f), new Color(0.282353f, 0.5973283f, 0.9960784f) };

        _maxHealth = DataManager.Instance.Data.GolemMaxHealth;
        for (int i = 0; i < _maxHealth; i++)
        {
            GameObject _tempHealthBlock = GameObject.Instantiate(_healthBlock, this.transform);
            RectTransform _tempRect = _tempHealthBlock.GetComponent<RectTransform>();
            _tempHealthBlock.GetComponent<Image>().color = color[1];
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
                DistractDamage();
            }
        }
    }

    public void DistractDamage()
    {
        if(_healthStack.Peek().GetComponent<Image>().color == color[1])
        {
            _healthStack.Peek().GetComponent<Image>().color = color[0];
        }
        else
        {
            Destroy(_healthStack.Pop());
        }
    }
}
