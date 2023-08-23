using UnityEngine;
using UnityEngine.Events;

public class VoidGameEventListener : MonoBehaviour {
    private void OnEnable() {
        GameEvent.RegisterListener(this);
    }

    private void OnDisable() {
        GameEvent.UnregisterListener(this);
    }

    public void OnEventRaised() {
        Response?.Invoke();
    }

    public VoidGameEventSO GameEvent;
    public UnityEvent Response;
}
