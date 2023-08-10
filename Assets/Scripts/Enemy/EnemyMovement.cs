using UnityEngine;
using UnityEngine.Assertions;

public class EnemyMovement : MonoBehaviour {
    #region Methods
    // Start is called before the first frame update
    private void Start() {
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    private void Update() {
        ChaseTarget();
    }

    private void ChaseTarget() {
        Assert.IsNotNull(target);

        Vector3 directionToTarget = (target.position - transform.position).normalized;
        transform.position += speed * Time.deltaTime * directionToTarget;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private float speed = 1.0f;

    private Transform target;
    #endregion
}
