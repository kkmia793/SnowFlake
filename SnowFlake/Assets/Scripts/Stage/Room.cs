using UnityEngine;

public enum DoorDirection { North, East, South, West }

public class Room : MonoBehaviour {
    public GameObject northDoor;
    public GameObject eastDoor;
    public GameObject southDoor;
    public GameObject westDoor;

    public void SetDoorActive(DoorDirection direction, bool isActive) {
        switch (direction) {
            case DoorDirection.North:
                northDoor.SetActive(isActive);
                break;
            case DoorDirection.East:
                eastDoor.SetActive(isActive);
                break;
            case DoorDirection.South:
                southDoor.SetActive(isActive);
                break;
            case DoorDirection.West:
                westDoor.SetActive(isActive);
                break;
        }
    }

    public void CloseDoor(DoorDirection direction) {
        SetDoorActive(direction, true);
    }

    public void OpenDoor(DoorDirection direction) {
        SetDoorActive(direction, false);
    }
}