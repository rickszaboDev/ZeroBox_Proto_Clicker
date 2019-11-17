using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    protected RoomData CurrentRoomData;
    protected DungeonController manager;
     private void OnEnable()
    {
        Spawner.OnClearAllEnemies += FinishRoom;
    }

    private void OnDisable()
    {
        Spawner.OnClearAllEnemies -= FinishRoom;
    }

    public virtual void StartRoom(DungeonController manager, RoomData CurrentRoomData)
    {
        this.manager = manager;
        this.CurrentRoomData = CurrentRoomData;
        CallHandler(this.CurrentRoomData);
    }
    protected virtual void CallHandler(RoomData data){}
    protected void FinishRoom()
    {
        manager.SelectRoom();
    }
}