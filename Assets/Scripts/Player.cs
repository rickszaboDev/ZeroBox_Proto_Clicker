using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : DamageReceiver
{
    public static Action OnPlayerDie;

    [SerializeField] private Transform lifeContent;

    [Header("Resources")]
    [SerializeField] private int damage;
    [SerializeField] private GameObject lifeHeart;

    [Header("Player Events")]
    [SerializeField] private UnityEvent onGetHit;

    private List<GameObject> spawedHearts = new List<GameObject>();

    #region Unity Methods
    public void OnEnable()
    {
        Enemy.OnDealDamage += TakeDamage;
    }

    public void OnDisable()
    {
        Enemy.OnDealDamage -= TakeDamage;
    }

    private void Start()
    {
        InitializeLifeHUD();
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                var target = hit.transform.gameObject.GetComponent<Enemy>();
                if(target != null)
                {
                    target.TakeDamage(damage);
                }
            }
        }
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var target = hit.transform.gameObject.GetComponent<Enemy>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
        }
#endif
    }

#endregion

    private void InitializeLifeHUD()
    {
        InitializeLife();
        for (int i = 0; i < life; i++)
        {
            var heart = Instantiate(lifeHeart, lifeContent) as GameObject;
            spawedHearts.Add(heart);
        }
    }

    protected override void Die()
    {
        base.Die();

        OnPlayerDie?.Invoke();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        for (int i = 0; i < damage; i++)
        {
            spawedHearts[i].SetActive(false);
            spawedHearts.RemoveAt(i);
        }

        onGetHit.Invoke();
    }

}
