using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour {
    #region Methods
    private void Start() {
        FillUpHeartsList(basicHearts);
    }

    private void FillUpHeartsList(List<PlayerHeartUI> hearts) {
        var heartsArray = GetComponentsInChildren<PlayerHeartUI>();
        
        foreach(var heart in heartsArray) {
            hearts.Add(heart);
        }
    }

    public void DecreaseHealthUIByAmount(float amount) {
        PlayerHeartUI lastHeartAvailable = null;

        // Store the last heart in the list that has at least half health
        // and decrease it by the amount
        for (int i = basicHearts.Count - 1; i >= 0; --i) {
            if (basicHearts[i].GetFillAmount() > 0) {
                lastHeartAvailable = basicHearts[i];
                break;
            }
        }

        if (lastHeartAvailable) {
            lastHeartAvailable.SetFill(lastHeartAvailable.GetFillAmount() - amount);
        }
    }

    public void IncreaseHealthUIByAmount(float amount) {
        // Last heart in the list is full, can't increase health
        if (basicHearts[^1].GetFillAmount() == 1f) {
            return;
        }

        List<PlayerHeartUI> heartsToFill = new();

        // Store all the empty or half empty hearts into a new list
        for (int i = 0; i < basicHearts.Count; ++i) {
            if (basicHearts[i].GetFillAmount() < 1f) {
                heartsToFill.Add(basicHearts[i]);
            }
        }

        // At least one heart needs filling
        if (heartsToFill.Count > 0) {
            for (int i = 0; i < heartsToFill.Count; ++i) {
                // Increasing the heart's fill by half
                if (amount < 1f) {
                    heartsToFill[i].SetFill(heartsToFill[i].GetFillAmount() + amount);
                    break;
                }
                
                // Increasing the heart's fill fully
                if (amount == 1) {
                    // This heart is empty, can increase it to full
                    if (heartsToFill[i].GetFillAmount() < 0.5f) {
                        heartsToFill[i].SetFill(amount);
                        break;
                    }

                    // This heart is half empty
                    if (heartsToFill[i].GetFillAmount() < 1f) {
                        // When it's the last heart in the list, it can only
                        // be increased by half regardless of the amount
                        if (heartsToFill.Count == 1) {
                            heartsToFill[i].SetFill(heartsToFill[i].GetFillAmount() + 0.5f);
                            break;
                        }

                        // When it's not the last heart in the list,
                        // this heart will be increased by half, and then
                        // the next heart in the list will get the other half
                        heartsToFill[i].SetFill(heartsToFill[i].GetFillAmount() + 0.5f);
                        heartsToFill[i + 1].SetFill(heartsToFill[i + 1].GetFillAmount() + 0.5f);
                        break;
                    }
                }
            }
        }
    }
    #endregion

    #region Member variables
    private List<PlayerHeartUI> basicHearts = new();
    #endregion
}
