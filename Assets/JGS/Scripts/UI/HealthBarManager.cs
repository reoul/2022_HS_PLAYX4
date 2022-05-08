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

    [SerializeField] private int _playerMaxHealth;
    public int PlayerHealth => _playerHealthStack.Count;
    public int GolemHealth => _healthStack.Count;

    private Stack<GameObject> _healthStack;
    private Stack<GameObject> _playerHealthStack;

    private Color[] color;
    private float baseWidth = 42.5f;

    public Transform bossTextTrans;
    public Transform playerTextTrans;

    public bool IsPlayerInvin;

    private void Awake()
    {
        _healthStack = new Stack<GameObject>();
        _playerHealthStack = new Stack<GameObject>();
    }

    private void Start()
    {
        _playerMaxHealth = DataManager.Instance.Data.PlayerMaxHP;
        Init();
    }

    public void Init()
    {
        color = new Color[2] { new Color(0.9960784f, 0.3035352f, 0.282353f), new Color(0.282353f, 0.5973283f, 0.9960784f) };

        _maxHealth = DataManager.Instance.Data.GolemMaxHealth;

        for (int i = 0; i < _maxHealth; i++)
        {
            GameObject _tempHealthBlock = GameObject.Instantiate(_healthBlock, bossTextTrans);
            RectTransform _tempRect = _tempHealthBlock.GetComponent<RectTransform>();
            _tempHealthBlock.GetComponent<Image>().color = color[1];
            _tempRect.sizeDelta = new Vector2((20f / _maxHealth) * baseWidth, _tempRect.sizeDelta.y);
            _tempHealthBlock.transform.localPosition = new Vector3(i * _tempRect.sizeDelta.x, 0, 0);
            _healthStack.Push(_tempHealthBlock);
        }

        for (int i = 0; i < _playerMaxHealth; i++)
        {
            GameObject _tempHealthBlock = GameObject.Instantiate(_healthBlock, playerTextTrans);
            RectTransform _tempRect = _tempHealthBlock.GetComponent<RectTransform>();
            _tempHealthBlock.GetComponent<Image>().color = color[0];
            _tempRect.sizeDelta = new Vector2((20f / _maxHealth) * baseWidth, _tempRect.sizeDelta.y);
            _tempHealthBlock.transform.localPosition = new Vector3(i * _tempRect.sizeDelta.x, 0, 0);
            _playerHealthStack.Push(_tempHealthBlock);
        }

        ActiveBossHP(false);
    }

    public void ActiveBossHP(bool active)
    {
        bossTextTrans.gameObject.SetActive(active);
        playerTextTrans.transform.localPosition += Vector3.up * (active ? -55 : 55 );
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_healthStack.Count > 0)
            {
                DistractDamage();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            DistractPlayerDamage();
        }
    }

    public void DistractDamage()
    {
        if (_healthStack.Count == 0)
        {
            return;
        }
        
        if (_healthStack.Peek().GetComponent<Image>().color == color[1])
        {
            _healthStack.Peek().GetComponent<Image>().color = color[0];
        }
        else
        {
            Destroy(_healthStack.Pop());
        }
    }

    public void DistractPlayerDamage()
    {
        if (IsPlayerInvin || PlayerHealth == 0)
        {
            return;
        }
        
        if (_playerHealthStack.Peek().GetComponent<Image>().color == color[1])
        {
            _playerHealthStack.Peek().GetComponent<Image>().color = color[0];
        }
        else
        {
            Destroy(_playerHealthStack.Pop());
        }
    }
}
