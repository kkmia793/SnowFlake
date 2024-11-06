using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RoomData", menuName = "Game/RoomData")]
public class RoomData : ScriptableObject {
    public string roomName;
    public GameObject roomPrefab;
    public List<GimmickData> possibleGimmicks;
}