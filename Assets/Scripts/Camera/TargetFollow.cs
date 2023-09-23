using UnityEngine;

public class TargetFollow : MonoBehaviour {
    #region Methods
    public void SetTarget(GameObject targetToFollow) {
        var cameraPosition = targetToFollow.transform.position;
        cameraPosition.z = -10f;
        transform.position = cameraPosition;

        target = targetToFollow.transform;
    }

    // Update is called once per frame
    private void LateUpdate() {
        FollowTargetSmoothly();
    }

    private void FollowTargetSmoothly() {
        if (!target) {
            SetTarget(FindObjectOfType<PlayerMovement>().gameObject);
        }

        Vector3 cameraPosition =
            Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);
        cameraPosition.z = transform.position.z;

        transform.position = cameraPosition;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private float followSpeed = 1f;

    private Transform target = null;
    #endregion
}
