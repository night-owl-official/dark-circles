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
        transform.position =
            Vector3.Lerp(transform.position,
            GetMouseWorldPosition(),
            speed * Time.deltaTime);
    }

    private Vector3 GetMouseWorldPosition() {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePos.z = 0f;

        return worldMousePos;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private float speed = 1f;

    [Header("Collision Detection")]

    [SerializeField]
    private float collisionMinDistance = 1f;

    [SerializeField]
    private LayerMask collisionMask;
    #endregion
}
