//#define DEBUG_MODE

using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    #region Methods
    // Update is called once per frame
    private void Update() {
        MoveWhenPathClear();
    }

    private void MoveWhenPathClear() {
        Vector3 movementDirection = GetMovementDirection();

#if DEBUG_MODE
        Debug.DrawRay(transform.position,
            movementDirection * collisionMinDistance,
            Color.yellow);
#endif

        if (!Physics2D.Raycast(transform.position,
            movementDirection,
            collisionMinDistance,
            collisionMask)) {
            FollowCursor();
        }
    }

    private Vector3 GetMovementDirection() {
        return (GetMouseWorldPosition() - transform.position).normalized;
    }

    private void FollowCursor() {
        Vector3 newPosition =
            Vector3.Lerp(transform.position,
            GetMouseWorldPosition(),
            speed * Time.deltaTime);

        // Clamp the movement vector to keep its magnitude (speed) between current speed and max speed
        newPosition =
            transform.position +
            Vector3.ClampMagnitude(newPosition - transform.position, maxSpeed * Time.deltaTime);

        transform.position = newPosition;
    }

    private Vector3 GetMouseWorldPosition() {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePos.z = 0f;

        return worldMousePos;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private float speed = 0.5f;

    [SerializeField]
    private float maxSpeed = 5f;

    [Header("Collision Detection")]

    [SerializeField]
    private float collisionMinDistance = 1f;

    [SerializeField]
    private LayerMask collisionMask;
    #endregion
}
