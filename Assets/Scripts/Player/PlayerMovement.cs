//#define DEBUG_MODE

using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntEvent : UnityEvent<int> { }
[System.Serializable]
public class BoolEvent : UnityEvent<bool> { }

public class PlayerMovement : MonoBehaviour {
    #region Methods
    public void ToggleMovement(bool set) {
        canPlayerMove = set;
    }

    // Update is called once per frame
    private void Update() {
        MoveWhenPathClear();
    }

    private void MoveWhenPathClear() {
        if (!canPlayerMove) {
            // Stop the running animation event
            onPlayerMove?.Invoke(false);
            return;
        }

        UpdateLatestWorldMousePosition();
        Vector3 movementDirection = GetMovementDirection();

#if DEBUG_MODE
        Debug.DrawRay(transform.position,
            movementDirection * collisionMinDistance,
            Color.yellow);
#endif

        if (!IsThereAnImpendingCollision(movementDirection)) {
            FollowCursor();

            // Start running animation event
            int animMoveDir = GetAnimationMoveDirection(movementDirection);
            onPlayerMove?.Invoke(true);
            onPlayerMoveDirection?.Invoke(animMoveDir);
        } else {
            // Stop running animation when colliding and can't move
            onPlayerMove?.Invoke(false);
        }

        if (HasPlayerReachedDestination()) {
            // Stop the running animation event
            onPlayerMove?.Invoke(false);
        }
    }

    private void UpdateLatestWorldMousePosition() {
        // When the mouse stops moving we stop updating its position
        if (!IsMouseMoving()) {
            return;
        }

        mouseFinalPositionWhenLastMoved = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseFinalPositionWhenLastMoved.z = 0f;
    }

    private bool IsMouseMoving() {
        bool mouseMoving = Input.mousePosition != previousMousePosition;

        // Keep previous mouse position up to date
        previousMousePosition = Input.mousePosition;

        return mouseMoving;
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

    private int GetAnimationMoveDirection(Vector3 moveDir) {
        float threshold = 0.01f;

        if (IsPlayerWalkingUp(threshold, moveDir)) {
            // Going upwards
            return 0;
        }
        
        if (IsPlayerWalkingDown(threshold, moveDir)) {
            // Going downwards
            return 2;
        } 
        
        if (IsPlayerWalkingRight(threshold, moveDir) || IsPlayerWalkingLeft(threshold, moveDir)) {
            // Going sideways
            return 1;
        }

        // Shouldn't get here
        return -1;
    }

    private bool IsPlayerWalkingUp(float moveThreshold, Vector3 direction) {
        return direction.y > moveThreshold && IsLeftGreaterThanRight(direction.y, direction.x);
    }

    private bool IsPlayerWalkingDown(float moveThreshold, Vector3 direction) {
        return direction.y < -moveThreshold && IsLeftGreaterThanRight(direction.y, direction.x);
    }

    private bool IsPlayerWalkingRight(float moveThreshold, Vector3 direction) {
        bool walkingRight = direction.x > moveThreshold &&
            IsLeftGreaterThanRight(direction.x, direction.y);

        if (walkingRight) {
            onPlayerWalkSideways?.Invoke(false);
        }

        return walkingRight;
    }

    private bool IsPlayerWalkingLeft(float moveThreshold, Vector3 direction) {
        bool walkingLeft = direction.x < -moveThreshold &&
            IsLeftGreaterThanRight(direction.x, direction.y);

        if (walkingLeft) {
            onPlayerWalkSideways?.Invoke(true);
        }

        return walkingLeft;
    }

    private bool IsLeftGreaterThanRight(float left, float right) {
        // This is used to check whether 'left' movement is more prominent than 'right' movement
        // We can pick the animation for the movement that is more impactful
        return Mathf.Abs(left) > Mathf.Abs(right);
    }

    private bool HasPlayerReachedDestination() {
        return transform.position == mouseFinalPositionWhenLastMoved;
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

    public IntEvent onPlayerMoveDirection;
    public BoolEvent onPlayerMove;
    public BoolEvent onPlayerWalkSideways;

    private Vector3 previousMousePosition;
    private Vector3 mouseFinalPositionWhenLastMoved;

    private bool canPlayerMove = true;
    #endregion
}
