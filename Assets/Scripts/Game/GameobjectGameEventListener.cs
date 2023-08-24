using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameobjectEvent : UnityEvent<GameObject> { }

public class GameobjectGameEventListener : MonoBehaviour {
    private void OnEnable() {
        GameEvent.RegisterListener(this);
    }

    private void OnDisable() {
        GameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(GameObject gameObject) {
        Response?.Invoke(gameObject);
    }

    public GameobjectGameEventSO GameEvent;
    public GameobjectEvent Response;
}
