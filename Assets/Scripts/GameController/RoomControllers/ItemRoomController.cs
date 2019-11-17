using System.Collections;
using UnityEngine;

public class ItemRoomController : RoomController, IDungeonController
{
    [Header("References")]
    [SerializeField] private int Itemer;

    protected override void CallHandler(RoomData data)
    {
        Debug.Log("Called Itemer!");
    }
}
