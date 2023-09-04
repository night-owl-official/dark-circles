using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerHeartUI : MonoBehaviour {
    #region Methods
    public void SetFill(float value) {
        Assert.IsNotNull(fillImage, "Fill image not set for PlayerHeartUI");

        fillImage.fillAmount = GetRoundedFillValueInCorrectRange(value);
    }

    public float GetFillAmount() {
        Assert.IsNotNull(fillImage, "Fill image not set for PlayerHeartUI");

        return fillImage.fillAmount;
    }

    private float GetRoundedFillValueInCorrectRange(float value) {
        float newValue;

        if (value <= 0.3f) {
            newValue = 0;
        } else if (value > 0.3f && value <= 0.6f) {
            newValue = 0.5f;
        } else {
            newValue = 1f;
        }

        return newValue;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private Image fillImage;
    #endregion
}
