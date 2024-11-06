using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public RoomPlacementManager roomPlacementManager;
    public DoorManager doorManager;
    public Pathfinding pathfinding;
    public int gridSize = 5;
    public int minRoomCount = 25;

    void Start() {
        GenerateMap();
    }

    public void GenerateMap() {
        var mapGrid = roomPlacementManager.PlaceRooms(gridSize, minRoomCount);
        doorManager.AdjustDoors(mapGrid);

        if (!pathfinding.IsPathAvailable(mapGrid, roomPlacementManager.StartRoomPosition, roomPlacementManager.GoalRoomPosition)) {
            Debug.LogError("No valid path found between start and goal! Regenerating map...");
            GenerateMap(); // 再生成
        }
    }
}