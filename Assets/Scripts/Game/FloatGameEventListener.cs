using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

public class FloatGameEventListener : MonoBehaviour {
    private void OnEnable() {
        GameEvent.RegisterListener(this);
    }

    private void OnDisable() {
        GameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(float value) {
        Response?.Invoke(value);
    }

    public FloatGameEventSO GameEvent;
    public FloatEvent Response;
}
