using UnityEngine;

[CreateAssetMenu(fileName = "RoomData_", menuName = "Procedural Generation/Room Data")]
public class RoomDataSO : ScriptableObject {
    public int minEntities;
    public int maxEntities;
    public GameObject[] possibleEntities;
    public bool isEmpty;
}
