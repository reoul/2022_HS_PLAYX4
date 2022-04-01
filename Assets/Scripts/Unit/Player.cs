using UnityEngine;

public class Player : Unit
{
    [SerializeField]
    private PlayerHealthBar healthBar;


    private void Start()
    {
        healthBar.InitHealthBar(maxHealth,currentHealth);
    }

    private void Update() //디버깅용 :: 스페이스바 입력시 10의 데미지를 가함
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage(10f);
        }

    }

    override public void Damage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth < 0)
        {
            currentHealth = 0;
        }
        OnDamage();
    }

    private void OnDamage()
    {
        NotifyHealthBar();
    }

    private void NotifyHealthBar()
    {
        healthBar.ChageCurrentHealth(currentHealth);
    }
}
