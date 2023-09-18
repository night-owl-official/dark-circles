using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BoolEvent : UnityEvent<bool> { }

public class BoolGameEventListener : MonoBehaviour {
    private void OnEnable() {
        GameEvent.RegisterListener(this);
    }

    private void OnDisable() {
        GameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(bool value) {
        Response?.Invoke(value);
    }

    public BoolGameEventSO GameEvent;
    public BoolEvent Response;
}
