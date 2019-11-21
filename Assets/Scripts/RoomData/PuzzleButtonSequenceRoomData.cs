using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleRoomData", menuName = "Rooms/Puzzle Button Sequence Data", order = 1)]
public class PuzzleRoomButtonSequenceData : RoomData
{
    public int cooldown = 0;
    public int timeToComplete = 0;
    public bool randomSequence = false;

    public List<int> buttonSequence;
}