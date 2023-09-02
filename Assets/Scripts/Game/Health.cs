using UnityEngine;

public class Health : MonoBehaviour {
    #region Methods
    // Start is called before the first frame update
    private void Start() {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage) {
        currentHealth = Mathf.Max(0f, currentHealth - damage);
    }

    public virtual void HealUp(float amount) {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
    }
    #endregion

    #region Member variables
    [SerializeField]
    protected float maxHealth = 3f;

    protected float currentHealth;
    #endregion
}
