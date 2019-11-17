using System;
using UnityEngine;

public class Enemy : DamageReceiver
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [Header("Settings")]
    [SerializeField] protected float timeGiverAmount;
    [SerializeField] protected int damage;
    [SerializeField] protected bool dealDamageOnClick;

    public static Action OnDie;
    public static Action<int> OnDealDamage;
    public static Action<float> OnGiveTime;

    protected override void Die()
    {
        var death = Instantiate(ParticleSpawner.instance.death) as GameObject;
        death.transform.position = transform.position;

        OnDie?.Invoke();
        OnGiveTime?.Invoke(timeGiverAmount);

        base.Die();
    }

    protected override void OnClicked()
    {
        base.OnClicked();

        if (animator != null) animator.SetTrigger("Hit");
        if (dealDamageOnClick) OnDealDamage?.Invoke(damage);
    }
}
