using UnityEngine;

public enum GimmickType { Trap, Key, Puzzle }

[CreateAssetMenu(fileName = "GimmickData", menuName = "Game/GimmickData")]
public class GimmickData : ScriptableObject {
    public string gimmickName;
    public GameObject gimmickPrefab;
    public float spawnProbability;
    public GimmickType type;
}