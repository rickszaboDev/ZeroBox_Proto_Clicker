using System.Collections;
using UnityEngine;

public class CombatRoomController : RoomController, IDungeonController
{
    [Header("References")]
    [SerializeField] private Spawner spawner;
    
    protected override void CallHandler(RoomData data)
    {
        StartCoroutine(spawner.SpawnEnemies(data as CombatRoomData));
    }
}