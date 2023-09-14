using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomGenerator : MonoBehaviour {
    #region Methods
    public void RunCleanup() {
        allRoomsDictionary.Clear();
        playerRoomDictionary.Clear();
        bossRoomDictionary.Clear();
        enemyRoomsDictionary.Clear();

        playerRoom?.ClearRoomData();
        bossRoom?.ClearRoomData();
        foreach (var room in enemyRooms) {
            room?.ClearRoomData();
        }
    }

    public void GenerateRooms(Dictionary<Vector2Int, HashSet<Vector2Int>> rooms) {
        allRoomsDictionary = new(rooms);
        SetRoomsDictionaries();

        InstantiateRooms();
    }

    private void SetRoomsDictionaries() {
        PickPlayerRoom();
        PickBossRoom();
        PickEnemyRooms();
    }

    private void PickPlayerRoom() {
        var keyValPair = allRoomsDictionary.ElementAt(Random.Range(0, allRoomsDictionary.Count));
        allRoomsDictionary.Remove(keyValPair.Key);
        playerRoomDictionary.Add(keyValPair.Key, keyValPair.Value);
    }

    private void PickBossRoom() {
        float biggestDistance = 0;
        Vector2Int longestDistanceStartPos = Vector2Int.zero;

        foreach (var roomStartPos in  allRoomsDictionary.Keys) {
            float currentDistance =
                Vector2Int.Distance(playerRoomDictionary.ElementAt(0).Key, roomStartPos);
            if (currentDistance > biggestDistance) {
                biggestDistance = currentDistance;
                longestDistanceStartPos = roomStartPos;
            }
        }

        bossRoomDictionary.Add(longestDistanceStartPos, allRoomsDictionary[longestDistanceStartPos]);
        allRoomsDictionary.Remove(longestDistanceStartPos);
    }

    private void PickEnemyRooms() {
        foreach (var room in allRoomsDictionary) {
            if (Random.value <= enemyRoomSpawnProbability) {
                enemyRoomsDictionary.Add(room.Key, room.Value);
            }
        }
    }

    private void InstantiateRooms() {
        GeneratePlayerRoom();
        GenerateBossRoom();
        GenerateEnemyRooms();
    }

    private void GeneratePlayerRoom() {
        playerRoom = new PlayerRoom(playerRoomDictionary.ElementAt(0).Key,
            playerRoomDictionary.ElementAt(0).Value,
            null,
            playerSpawner);

        playerRoom.PlacePlayer();
    }

    private void GenerateBossRoom() {
        bossRoom = new BossRoom(bossRoomDictionary.ElementAt(0).Key,
            bossRoomDictionary.ElementAt(0).Value,
            bossRoomData,
            enemySpawner);

        bossRoom.PlaceBoss();
    }

    private void GenerateEnemyRooms() {
        foreach (var room in enemyRoomsDictionary) {
            var enemyRoom = new EnemyRoom(room.Value,
                enemyRoomsData[0],
                enemySpawner);

            if (!enemyRooms.Contains(enemyRoom)) {
                enemyRooms.Add(enemyRoom);
            }

            enemyRoom.PlaceEnemies();
        }
    }
    #endregion

    #region Member variables
    [SerializeField]
    private PlayerSpawner playerSpawner;
    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private RoomDataSO bossRoomData;
    [SerializeField]
    private RoomDataSO[] enemyRoomsData;

    [SerializeField]
    [Range(0f, 1f)]
    private float enemyRoomSpawnProbability = 0.8f;

    private PlayerRoom playerRoom;
    private BossRoom bossRoom;
    private List<EnemyRoom> enemyRooms = new();

    private Dictionary<Vector2Int, HashSet<Vector2Int>> allRoomsDictionary = new();
    private Dictionary<Vector2Int, HashSet<Vector2Int>> playerRoomDictionary = new();
    private Dictionary<Vector2Int, HashSet<Vector2Int>> bossRoomDictionary = new();
    private Dictionary<Vector2Int, HashSet<Vector2Int>> enemyRoomsDictionary = new();
    #endregion
}
