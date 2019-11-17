using System.Collections;
using UnityEngine;

public class PuzzleRoomController : RoomController, IDungeonController
{
    [Header("References")]
    [SerializeField] private int Puzzler;

    protected override void CallHandler(RoomData data)
    {
        Debug.Log("Called Puzzler!");
    }
}
