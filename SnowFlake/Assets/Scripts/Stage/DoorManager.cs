using UnityEngine;

public class DoorManager : MonoBehaviour {
    public void AdjustDoors(Room[,] mapGrid) {
        int gridSize = mapGrid.GetLength(0);
        
        // 中央9部屋の範囲を計算
        int innerStart = gridSize / 2 - 1;
        int innerEnd = gridSize / 2 + 1;

        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                Room room = mapGrid[x, y];
                if (room == null) continue;

                if (x >= innerStart && x <= innerEnd && y >= innerStart && y <= innerEnd) {
                    // 中央9部屋: 全てのドアを開く
                    room.OpenDoor(DoorDirection.North);
                    room.OpenDoor(DoorDirection.East);
                    room.OpenDoor(DoorDirection.South);
                    room.OpenDoor(DoorDirection.West);
                } else {
                    // 外側: 隣接する部屋がある場合のみドアを閉めない
                    if (x < gridSize - 1 && mapGrid[x + 1, y] != null)
                        room.OpenDoor(DoorDirection.East);
                    else
                        room.CloseDoor(DoorDirection.East);

                    if (y < gridSize - 1 && mapGrid[x, y + 1] != null)
                        room.OpenDoor(DoorDirection.North);
                    else
                        room.CloseDoor(DoorDirection.North);

                    if (x > 0 && mapGrid[x - 1, y] != null)
                        room.OpenDoor(DoorDirection.West);
                    else
                        room.CloseDoor(DoorDirection.West);

                    if (y > 0 && mapGrid[x, y - 1] != null)
                        room.OpenDoor(DoorDirection.South);
                    else
                        room.CloseDoor(DoorDirection.South);
                }
            }
        }
    }
}