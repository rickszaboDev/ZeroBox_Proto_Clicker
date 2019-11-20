using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    protected RoomData CurrentRoomData;
    [SerializeField] protected DungeonController manager;

     private void OnEnable()
    {
        Spawner.OnClearAllEnemies += FinishRoom;
    }

    private void OnDisable()
    {
        Spawner.OnClearAllEnemies -= FinishRoom;
    }

    public void StartRoom(RoomData CurrentRoomData)
    {
        this.CurrentRoomData = CurrentRoomData;
        CallHandler(this.CurrentRoomData);
    }
    protected virtual void CallHandler(RoomData data){}
    protected void FinishRoom()
    {
        manager.SelectRoom();
    }
}