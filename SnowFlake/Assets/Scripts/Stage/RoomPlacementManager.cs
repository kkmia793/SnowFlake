using System.Collections.Generic;
using UnityEngine;

public class RoomPlacementManager : MonoBehaviour {
    public GameObject startRoomPrefab;
    public GameObject goalRoomPrefab;
    public RoomData[] possibleRoomData;
    public Vector2Int StartRoomPosition { get; private set; }
    public Vector2Int GoalRoomPosition { get; private set; }

    public Room[,] PlaceRooms(int gridSize, int minRoomCount) {
        Room[,] mapGrid = new Room[gridSize, gridSize];
        List<Vector2Int> occupiedPositions = new List<Vector2Int>();

        StartRoomPosition = GetRandomPosition(gridSize);
        PlaceRoom(startRoomPrefab, StartRoomPosition, mapGrid, occupiedPositions);

        do {
            GoalRoomPosition = GetRandomPosition(gridSize);
        } while (Vector2Int.Distance(StartRoomPosition, GoalRoomPosition) < 3);

        PlaceRoom(goalRoomPrefab, GoalRoomPosition, mapGrid, occupiedPositions);

        while (occupiedPositions.Count < minRoomCount) {
            Vector2Int newPos = GetRandomPosition(gridSize);
            PlaceRoom(possibleRoomData[Random.Range(0, possibleRoomData.Length)].roomPrefab, newPos, mapGrid, occupiedPositions);
        }

        return mapGrid;
    }

    private void PlaceRoom(GameObject prefab, Vector2Int position, Room[,] mapGrid, List<Vector2Int> occupiedPositions) {
        if (occupiedPositions.Contains(position)) return;

        var newRoom = Instantiate(prefab, new Vector3(position.x * 15, 0, position.y * 15), Quaternion.identity);
        mapGrid[position.x, position.y] = newRoom.GetComponent<Room>();
        occupiedPositions.Add(position);
    }

    private Vector2Int GetRandomPosition(int gridSize) {
        return new Vector2Int(Random.Range(0, gridSize), Random.Range(0, gridSize));
    }
}