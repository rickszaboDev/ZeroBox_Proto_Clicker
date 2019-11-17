using System.Collections;
using UnityEngine;

public class Bomb : Enemy
{
    [Header("Bomb Properties")]
    [SerializeField] private float secondsToDestroy;

    private void Start()
    {
        StartCoroutine(AutoDestruct());
    }

    private IEnumerator AutoDestruct()
    {
        yield return new WaitForSeconds(secondsToDestroy);
        Die();
    }
}
