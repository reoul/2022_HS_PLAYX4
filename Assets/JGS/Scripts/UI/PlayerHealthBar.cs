/*=====================================================
 * 플레이어 체력 디버깅용 코드
 * 필요 없으면 지워도 됨
 =====================================================*/
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthBarBG;
    [SerializeField]
    private RectTransform healthBar;
    [SerializeField]
    private Player player;

    private float healthBarWidth;

    private float maxHealth;
    private float currentHealth;

    public void ChageCurrentHealth(float health)
    {
        currentHealth = health;
        healthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, healthBarWidth / (maxHealth / currentHealth));
    }

    public void InitHealthBar(float mHealth, float cHealth)
    {
        maxHealth = mHealth;
        currentHealth = cHealth;
    }

    private PlayerHealthBar()
    {
    }

    private void Start()
    {
        healthBarWidth = healthBarBG.sizeDelta.x;
    }

    private void Update()
    {
        healthBarBG.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(healthBarBG.sizeDelta.x, healthBar.sizeDelta.x,0.1f));
    }
}
