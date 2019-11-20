using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float rayX;
    public float rayY;

    public static Action OnClearAllEnemies;

    [Header("References")]
    [SerializeField] private List<Transform> spawnSpots;
    private List<Vector3> takenSpots;

    [Header("Enemies")]
    [SerializeField] private List<GameObject> enemies;

    [Header("Settings")]
    [SerializeField] private List<int> maxEnemiesOnScreenList;
    [SerializeField] private int waveLength;
    [SerializeField] private int cooldown;
    private int enemiesAlive;
    private int waveCounter;
    private int totalAmountOfEnemies = 0;
    public int EnemiesAlive 
    {
        get { return enemiesAlive; }
        set
        {
            enemiesAlive = value;
            if (enemiesAlive <= 0 && waveCounter <= 0) OnClearAllEnemies?.Invoke();
        }
    }

    #region Unity Methods
    private void Start()
    {
        //StartCoroutine(SpawnEnemies());
    }

    private void OnEnable()
    {
        Enemy.OnDie += SubtractAliveEnemie;
    }

    private void OnDisable()
    {
        Enemy.OnDie -= SubtractAliveEnemie;
    }

    private void Update()
    {
        var randomScreenPos = new Vector2(UnityEngine.Random.Range(0, Screen.width * 10), UnityEngine.Random.Range(0, Screen.height * 10));
        var ray = Camera.main.ScreenPointToRay(new Vector2(12,140));
        Debug.DrawRay(ray.origin, ray.direction);
    }
    #endregion

    #region Spawn Methods
    private void Spawn(int index)
    {
        var currentEnemy = Instantiate(enemies[index]);
        currentEnemy.transform.position = GetRandomPosition();
        EnemiesAlive++;
        waveCounter--;
    }
    public void StartSpawnEnemies(CombatRoomData data){
        StartCoroutine(SpawnEnemies(data));
    }

    public IEnumerator SpawnEnemies(CombatRoomData data)
    {
        enemies = data.EnemiesPool;
        waveLength = enemies.Count;
        maxEnemiesOnScreenList = data.maxEnemiesOnScreen;
        cooldown = data.cooldown;
        waveCounter = waveLength;

        var maxEnemiesOnScreenCounter = 0; 
        totalAmountOfEnemies = maxEnemiesOnScreenList[maxEnemiesOnScreenCounter];

        for (int i = 0; i < waveLength; i++)
        {
            var maxEnemiesOnScreen = maxEnemiesOnScreenList[maxEnemiesOnScreenCounter];
            
            if(totalAmountOfEnemies <= 0) {
                maxEnemiesOnScreenCounter++;
                totalAmountOfEnemies = maxEnemiesOnScreenList[maxEnemiesOnScreenCounter];
            }

            while (enemiesAlive >= maxEnemiesOnScreen)
            {
                yield return null;
            }

            yield return new WaitForSeconds(cooldown);
            Spawn(i);
        }
    }
    #endregion

    #region Events Callbacks
    private void SubtractAliveEnemie()
    {
        EnemiesAlive--;
        if(totalAmountOfEnemies > 0) totalAmountOfEnemies--;
    }

    private Vector3 GetRandomPosition()
    {
        var safeHorizontal = Screen.width * 0.1f;
        var safeVertical = Screen.height * 0.3f;

        var randomScreenPos = new Vector2(UnityEngine.Random.Range(safeHorizontal, Screen.width - safeHorizontal), UnityEngine.Random.Range(safeVertical, Screen.height - safeVertical));
        var ray = Camera.main.ScreenPointToRay(randomScreenPos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 1000, 1 << LayerMask.NameToLayer("CursedFloor")))
        {
            Debug.DrawRay(ray.origin, ray.direction);
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }

    }
    #endregion
}
