using UnityEngine;

public abstract class Spawner : MonoBehaviour {
    #region Methods
    public abstract GameObject Spawn(Vector3 position);
    #endregion

    #region Member variables
    [SerializeField]
    protected GameObject spawnee;
    #endregion
}
