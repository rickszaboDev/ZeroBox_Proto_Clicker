using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatRoomData", menuName = "Rooms/Combat Room Data", order = 1)]
public class CombatRoomData : RoomData
{
    public int maxEnemiesOnScreen = 0;
    public int cooldown = 0;
    public List<GameObject> EnemiesPool;
}
