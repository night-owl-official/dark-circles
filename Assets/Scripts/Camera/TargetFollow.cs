using UnityEngine;
using UnityEngine.Assertions;

public class TargetFollow : MonoBehaviour {
    #region Methods
    // Start is called before the first frame update
    private void Start() {
        Assert.IsNotNull(target);
    }

    // Update is called once per frame
    private void LateUpdate() {
        FollowTargetSmoothly();
    }

    private void FollowTargetSmoothly() {
        Vector3 cameraPosition =
            Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);
        cameraPosition.z = transform.position.z;

        transform.position = cameraPosition;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private float followSpeed = 1f;

    [SerializeField]
    private Transform target = null;
    #endregion
}
