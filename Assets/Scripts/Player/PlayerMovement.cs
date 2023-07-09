//#define DEBUG_MODE

using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BoolIntEvent : UnityEvent<bool, int> { }

public class PlayerMovement : MonoBehaviour {
    #region Methods
    // Update is called once per frame
    private void Update() {
        MoveWhenPathClear();
    }

    private void MoveWhenPathClear() {
        UpdateLatestWorldMousePosition();
        Vector3 movementDirection = GetMovementDirection();

#if DEBUG_MODE
        Debug.DrawRay(transform.position,
            movementDirection * collisionMinDistance,
            Color.yellow);
#endif

        if (!IsThereAnImpendingCollision(movementDirection)) {
            FollowCursor();
        }
    }

    private Vector3 GetMovementDirection() {
        return (mouseFinalPositionWhenLastMoved - transform.position).normalized;
    }

    private bool IsThereAnImpendingCollision(Vector3 moveDir) {
        return Physics2D.Raycast(transform.position,
            moveDir,
            collisionMinDistance,
            collisionMask).collider != null;
    }

    private void FollowCursor() {
        transform.position =
            Vector3.MoveTowards(transform.position,
            mouseFinalPositionWhenLastMoved,
            speed * Time.deltaTime);
    }

    private void UpdateLatestWorldMousePosition() {
        // When the mouse stops moving we stop updating its position
        if (!IsMouseMoving())
            return;

        mouseFinalPositionWhenLastMoved = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseFinalPositionWhenLastMoved.z = 0f;
    }

    private bool IsMouseMoving() {
        bool mouseMoving = Input.mousePosition != previousMousePosition;

        // Keep previous mouse position up to date
        previousMousePosition = Input.mousePosition;

        return mouseMoving;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private float speed = 0.5f;

    [Header("Collision Detection")]

    [SerializeField]
    private float collisionMinDistance = 1f;

    [SerializeField]
    private LayerMask collisionMask;

    public BoolIntEvent onPlayerMove;

    private Vector3 previousMousePosition;
    private Vector3 mouseFinalPositionWhenLastMoved;
    #endregion
}
