using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {
    public bool IsPathAvailable(Room[,] grid, Vector2Int start, Vector2Int goal) {
        return FindPath(grid, start, goal).Count > 0;
    }

    public List<Vector2Int> FindPath(Room[,] grid, Vector2Int start, Vector2Int goal) {
        var openSet = new PriorityQueue<Vector2Int>();
        var cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        var gScore = new Dictionary<Vector2Int, float>();
        var fScore = new Dictionary<Vector2Int, float>();

        int gridSize = grid.GetLength(0);

        foreach (var pos in GetAllPositions(gridSize)) {
            gScore[pos] = float.MaxValue;
            fScore[pos] = float.MaxValue;
        }

        gScore[start] = 0;
        fScore[start] = Heuristic(start, goal);

        openSet.Enqueue(start, fScore[start]);

        while (openSet.Count > 0) {
            Vector2Int current = openSet.Dequeue();

            if (current == goal) {
                return ReconstructPath(cameFrom, current);
            }

            foreach (var neighbor in GetNeighbors(grid, current, gridSize)) {
                float tentativeGScore = gScore[current] + 1;

                if (tentativeGScore < gScore[neighbor]) {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, goal);

                    if (!openSet.Contains(neighbor)) {
                        openSet.Enqueue(neighbor, fScore[neighbor]);
                    }
                }
            }
        }

        return new List<Vector2Int>();
    }

    private float Heuristic(Vector2Int a, Vector2Int b) {
        return Vector2Int.Distance(a, b);
    }

    private List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current) {
        var totalPath = new List<Vector2Int> { current };
        while (cameFrom.ContainsKey(current)) {
            current = cameFrom[current];
            totalPath.Insert(0, current);
        }
        return totalPath;
    }

    private IEnumerable<Vector2Int> GetAllPositions(int gridSize) {
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                yield return new Vector2Int(x, y);
            }
        }
    }

    private IEnumerable<Vector2Int> GetNeighbors(Room[,] grid, Vector2Int pos, int gridSize) {
        var directions = new List<Vector2Int> {
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0)
        };

        foreach (var dir in directions) {
            Vector2Int neighbor = pos + dir;
            if (neighbor.x >= 0 && neighbor.x < gridSize && neighbor.y >= 0 && neighbor.y < gridSize && grid[neighbor.x, neighbor.y] != null) {
                yield return neighbor;
            }
        }
    }
}