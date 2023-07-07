using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    #region Methods
    // Update is called once per frame
    private void Update() {
        FollowCursor();
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
    private float speed = 5f;
    #endregion
}
