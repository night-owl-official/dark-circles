using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent_", menuName = "Gameplay/VectorGameEvent")]
public class VectorGameEventSO : ScriptableObject {
    public void RegisterListener(VectorGameEventListener listener) {
        if (!gameEventListeners.Contains(listener)) {
            gameEventListeners.Add(listener);
        }
    }

    public void UnregisterListener(VectorGameEventListener listener) {
        if (gameEventListeners.Contains(listener)) {
            gameEventListeners.Remove(listener);
        }
    }

    public void Raise(Vector3 value) {
        for (int i = gameEventListeners.Count - 1; i >= 0; --i) {
            gameEventListeners[i].OnEventRaised(value);
        }
    }

    private readonly List<VectorGameEventListener> gameEventListeners = new();
}
