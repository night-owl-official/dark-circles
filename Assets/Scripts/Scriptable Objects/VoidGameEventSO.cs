using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent_", menuName = "Gameplay/VoidGameEvent")]
public class VoidGameEventSO : ScriptableObject {
    public void RegisterListener(VoidGameEventListener listener) {
        if (!gameEventListeners.Contains(listener)) {
            gameEventListeners.Add(listener);
        }
    }

    public void UnregisterListener(VoidGameEventListener listener) {
        if (gameEventListeners.Contains(listener)) {
            gameEventListeners.Remove(listener);
        }
    }

    public void Raise() {
        for (int i = gameEventListeners.Count - 1; i >= 0; --i) {
            gameEventListeners[i].OnEventRaised();
        }
    }

    private readonly List<VoidGameEventListener> gameEventListeners = new();
}
