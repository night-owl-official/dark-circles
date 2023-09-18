using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomGenerator : MonoBehaviour {
    #region Methods
    public void RunCleanup() {
        allRoomsDictionary.Clear();
        playerRoomDictionary.Clear();
        bossRoomDictionary.Clear();
        treasureRoomDictionary.Clear();
        enemyRoomsDictionary.Clear();

        playerRoom?.ClearRoomData();
        bossRoom?.ClearRoomData();
        treasureRoom?.ClearRoomData();
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
        PickTreasureRoom();
        PickEnemyRooms();
    }

    private void PickPlayerRoom() {
        var keyValPair = allRoomsDictionary.ElementAt(Random.Range(0, allRoomsDictionary.Count));
        allRoomsDictionary.Remove(keyValPair.Key);
        playerRoomDictionary.Add(keyValPair.Key, keyValPair.Value);
    }

    private void PickBossRoom() {
        var farthestRoomStartPos = FindFarthestRoomFromPlayer();

        bossRoomDictionary.Add(farthestRoomStartPos, allRoomsDictionary[farthestRoomStartPos]);
        allRoomsDictionary.Remove(farthestRoomStartPos);
    }

    private void PickTreasureRoom() {
        if (Random.value >= treasureRoomSpawnProbability) {
            return;
        }

        var farthestRoomStartPos = FindFarthestRoomFromPlayer();

        treasureRoomDictionary.Add(farthestRoomStartPos, allRoomsDictionary[farthestRoomStartPos]);
        allRoomsDictionary.Remove(farthestRoomStartPos);
    }

    private Vector2Int FindFarthestRoomFromPlayer() {
        float biggestDistance = 0;
        Vector2Int longestDistanceStartPos = Vector2Int.zero;

        foreach (var roomStartPos in allRoomsDictionary.Keys) {
            float currentDistance =
                Vector2Int.Distance(playerRoomDictionary.ElementAt(0).Key, roomStartPos);
            if (currentDistance > biggestDistance) {
                biggestDistance = currentDistance;
                longestDistanceStartPos = roomStartPos;
            }
        }

        return longestDistanceStartPos;
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
        GenerateTreasureRoom();
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

    private void GenerateTreasureRoom() {
        if (treasureRoomDictionary.Count == 0) {
            return;
        }

        treasureRoom = new TreasureRoom(treasureRoomDictionary.ElementAt(0).Key,
            treasureRoomDictionary.ElementAt(0).Value,
            null,
            itemSpawner);

        treasureRoom.PlaceTreasure();
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
    private ItemSpawner itemSpawner;

    [SerializeField]
    private RoomDataSO bossRoomData;
    [SerializeField]
    private RoomDataSO[] enemyRoomsData;


    [SerializeField]
    [Range(0f, 1f)]
    private float treasureRoomSpawnProbability = 0.2f;
    [SerializeField]
    [Range(0f, 1f)]
    private float enemyRoomSpawnProbability = 0.8f;

    private PlayerRoom playerRoom;
    private BossRoom bossRoom;
    private TreasureRoom treasureRoom;
    private List<EnemyRoom> enemyRooms = new();

    private Dictionary<Vector2Int, HashSet<Vector2Int>> allRoomsDictionary = new();
    private Dictionary<Vector2Int, HashSet<Vector2Int>> playerRoomDictionary = new();
    private Dictionary<Vector2Int, HashSet<Vector2Int>> bossRoomDictionary = new();
    private Dictionary<Vector2Int, HashSet<Vector2Int>> treasureRoomDictionary = new();
    private Dictionary<Vector2Int, HashSet<Vector2Int>> enemyRoomsDictionary = new();
    #endregion
}
