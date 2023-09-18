using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent_", menuName = "Gameplay/BoolGameEvent")]
public class BoolGameEventSO : ScriptableObject {
    public void RegisterListener(BoolGameEventListener listener) {
        if (!gameEventListeners.Contains(listener)) {
            gameEventListeners.Add(listener);
        }
    }

    public void UnregisterListener(BoolGameEventListener listener) {
        if (gameEventListeners.Contains(listener)) {
            gameEventListeners.Remove(listener);
        }
    }

    public void Raise(bool value) {
        for (int i = gameEventListeners.Count - 1; i >= 0; --i) {
            gameEventListeners[i].OnEventRaised(value);
        }
    }

    private readonly List<BoolGameEventListener> gameEventListeners = new();
}
