using UnityEngine;
using UnityEngine.Events;

public class DamageReceiver : Hitable
{
    [SerializeField] private int minInitialLife;
    [SerializeField] private int maxInitialLife;
    protected int life;

    [SerializeField] private UnityEvent OnDie;

    private void OnEnable()
    {
        InitializeLife();
    }

    protected void InitializeLife()
    {
        life = Random.Range(minInitialLife, maxInitialLife);
    }

    public virtual void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        OnDie?.Invoke();
        Destroy(gameObject);
    }
}
