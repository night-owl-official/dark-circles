using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent_", menuName = "Gameplay/GameobjectGameEvent")]
public class GameobjectGameEventSO : ScriptableObject {
    public void RegisterListener(GameobjectGameEventListener listener) {
        if (!gameEventListeners.Contains(listener)) {
            gameEventListeners.Add(listener);
        }
    }

    public void UnregisterListener(GameobjectGameEventListener listener) {
        if (gameEventListeners.Contains(listener)) {
            gameEventListeners.Remove(listener);
        }
    }

    public void Raise(GameObject gameObject) {
        for (int i = gameEventListeners.Count - 1; i >= 0; --i) {
            gameEventListeners[i].OnEventRaised(gameObject);
        }
    }

    private readonly List<GameobjectGameEventListener> gameEventListeners = new();
}
