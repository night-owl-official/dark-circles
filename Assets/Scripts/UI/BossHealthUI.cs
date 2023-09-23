using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour {
    #region Methods
    // Start is called before the first frame update
    private void Start() {
        ToggleHealthBar(false);
    }

    public void ToggleHealthBar(bool toggle) {
        foreach (var image in healthBarImages) {
            image.enabled = toggle;
        }
    }

    public void TakeDamage(float damage) {
        if (!fillImage) { return; }

        float scaledDamage = damage / bossMaxHealth;
        fillImage.fillAmount -= scaledDamage;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private float bossMaxHealth = 10f;

    [SerializeField]
    private Image fillImage;

    [SerializeField]
    private Image[] healthBarImages;
    #endregion
}
