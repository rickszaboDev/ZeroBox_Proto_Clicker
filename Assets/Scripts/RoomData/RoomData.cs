using UnityEngine;

public class RoomData : ScriptableObject
{
    [HideInInspector] public RoomType Type;
    public RoomData NavNorthRoom;
    public RoomData NavSouthRoom;
    public RoomData NavEastRoom;
    public RoomData NavWestRoom;
}

public enum RoomType {
Combat,
Puzzle,
Item
}