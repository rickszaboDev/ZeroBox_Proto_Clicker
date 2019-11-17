using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleRoomData", menuName = "Rooms/Puzzle Room Data", order = 1)]
public class PuzzleRoomData : RoomData
{
    public int buttonsAmount = 0;
    public int cooldown = 0;
    public int timeToComplete = 0;
}