using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public static Action OnTimeEnd;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI clockText;

    [Header("Settings")]
    [SerializeField] private float initialTime;
    private float currentTime;
    private float pausedTime;
    private bool canCountTime = true;

    #region Unity Methods
    private void OnEnable()
    {
        Enemy.OnGiveTime += AddTime;
        Spawner.OnClearAllEnemies += StopTime;
    }

    private void OnDisable()
    {
        Enemy.OnGiveTime -= AddTime;
        Spawner.OnClearAllEnemies -= StopTime;
    }

    public void Start()
    {
        StartCoroutine(TimeBurnDown());
    }
    #endregion

    private IEnumerator TimeBurnDown()
    {
        currentTime = initialTime;

        while(currentTime > 0 && canCountTime)
        {
            currentTime -= Time.deltaTime;
            clockText.text = Convert.ToInt16(currentTime).ToString();
            yield return null;
        }
        pausedTime = currentTime;
        if(currentTime <= 0) OnTimeEnd?.Invoke();
    }

    private void AddTime(float time)
    {
        currentTime += time;
    }

    public void StopTime()
    {
        canCountTime = false;
    }

    public void StartTime()
    {
        canCountTime = true;
        initialTime = pausedTime;
        StartCoroutine(TimeBurnDown());
    }
}
