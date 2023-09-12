using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class VectorEvent : UnityEvent<Vector3> { }

public class VectorGameEventListener : MonoBehaviour {
    private void OnEnable() {
        GameEvent.RegisterListener(this);
    }

    private void OnDisable() {
        GameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(Vector3 value) {
        Response?.Invoke(value);
    }

    public VectorGameEventSO GameEvent;
    public VectorEvent Response;
}
