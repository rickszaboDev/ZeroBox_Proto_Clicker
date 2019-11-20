using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DungeonController : MonoBehaviour
{
    public static Action OnClearAllRooms;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private TextMeshProUGUI dungeonProgress;

    [Header("Settings")]
    [SerializeField] private int maxRooms;

    [Header("Controllers")]
    public List<RoomController> roomControllers;

    [Header("Events")]
    [SerializeField] private UnityEvent onFinishRoom;
    [SerializeField] private UnityEvent onEnterRoom;
    private int currentRoom = 1;
    public int CurrentRoom
    {
        get { return currentRoom; }
        set 
        {
            currentRoom = value;
            UpdateDungeonProgress();
        }
    }

    public RoomData CurrentRoomData;

    public static Action OnStartGame;

    #region Unity Methods
    

    private void Start()
    {
        StartRoom(CurrentRoomData);
    }
    #endregion

    public void SelectRoom()
    {
        if (currentRoom >= maxRooms)
        {
            OnClearAllRooms?.Invoke();
            return;
        } 

        if(currentRoom < maxRooms) currentRoom++;
        if (currentRoom <= maxRooms) onFinishRoom?.Invoke();
    }

    private void GoNorth(RoomData nextRoom)
    {
        transitionAnimator.SetFloat("TransitionDirection", (float)Direction.North);
        transitionAnimator.SetTrigger("DoTransition");
        StartRoom(nextRoom);
    }
    private void GoSouth(RoomData nextRoom)
    {
        transitionAnimator.SetFloat("TransitionDirection", (float)Direction.South);
        transitionAnimator.SetTrigger("DoTransition");
        StartRoom(nextRoom);
    }
    private void GoWest(RoomData nextRoom)
    {
        transitionAnimator.SetFloat("TransitionDirection", (float)Direction.West);
        transitionAnimator.SetTrigger("DoTransition");
        StartRoom(nextRoom);
    }
    private void GoEast(RoomData nextRoom)
    {
        transitionAnimator.SetFloat("TransitionDirection", (float)Direction.East);
        transitionAnimator.SetTrigger("DoTransition");
        StartRoom(nextRoom);
    }

    public void SetNorth(GameObject button)
    {
        var nextRoom = CurrentRoomData.NavNorthRoom; 
        button.SetActive(nextRoom != null);
        button.GetComponent<Button>().onClick.AddListener(() => GoNorth(nextRoom));
    }

    public void SetSouth(GameObject button)
    {
        var nextRoom = CurrentRoomData.NavSouthRoom;
        button.SetActive(nextRoom != null);
        button.GetComponent<Button>().onClick.AddListener(() => GoSouth(nextRoom));
    }
    public void SetWest(GameObject button)
    {
        var nextRoom = CurrentRoomData.NavWestRoom;
        button.SetActive(nextRoom != null);
        button.GetComponent<Button>().onClick.AddListener(() => GoWest(nextRoom));
    }
    public void SetEast(GameObject button)
    {
        var nextRoom = CurrentRoomData.NavEastRoom;
        button.SetActive(nextRoom != null);
        button.GetComponent<Button>().onClick.AddListener(() => GoEast(nextRoom));
    }

    private void StartRoom(RoomData data)
    {
        roomControllers[(int)data.Type].StartRoom(data);
        if(currentRoom != 1) onEnterRoom?.Invoke();
    }

    private void UpdateDungeonProgress()
    {
        dungeonProgress.text = $"{currentRoom}/{maxRooms}";
    }
}

public enum Direction
{
    North,
    West,
    East,
    South
}
