using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent_", menuName = "Gameplay/FloatGameEvent")]
public class FloatGameEventSO : ScriptableObject {
    public void RegisterListener(FloatGameEventListener listener) {
        if (!gameEventListeners.Contains(listener)) {
            gameEventListeners.Add(listener);
        }
    }

    public void UnregisterListener(FloatGameEventListener listener) {
        if (gameEventListeners.Contains(listener)) {
            gameEventListeners.Remove(listener);
        }
    }

    public void Raise(float value) {
        for (int i = gameEventListeners.Count - 1; i >= 0; --i) {
            gameEventListeners[i].OnEventRaised(value);
        }
    }

    private readonly List<FloatGameEventListener> gameEventListeners = new();
}
