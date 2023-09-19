using UnityEngine;
using UnityEngine.Assertions;

//[System.Serializable]
//public class WeightedItem {
//    [Range(0f, 100f)]
//    public float chance;

//    [HideInInspector]
//    public float weight;
//}

[System.Serializable]
public class WeightedGameObject {
    public GameObject prefab;

    [Range(0f, 100f)]
    public float chance;

    [HideInInspector]
    public float weight;
}

[System.Serializable]
public class WeightedValue {
    public float value;

    [Range(0f, 100f)]
    public float chance;

    [HideInInspector]
    public float weight;
}

public static class WeightedChanceItemSelector {
    public static GameObject GetRandomItemFromList(WeightedGameObject[] items) {
        Assert.IsTrue(items.Length > 0, "There are no weighted items to choose from: WeightedChanceItemSelector");

        float randomValue = Random.value * GetAccumulatedWeight(items);
        foreach (var item in items) {
            if (randomValue <= item.weight) {
                return item.prefab;
            }
        }

        return null;
    }

    public static float GetRandomItemFromList(WeightedValue[] items) {
        Assert.IsTrue(items.Length > 0, "There are no weighted items to choose from: WeightedChanceItemSelector");

        float randomValue = Random.value * GetAccumulatedWeight(items);
        foreach (var item in items) {
            if (randomValue <= item.weight) {
                return item.value;
            }
        }

        return float.NaN;
    }

    private static float GetAccumulatedWeight(WeightedGameObject[] items) {
        float accumulatedWeight = 0f;
        foreach (var item in items) {
            accumulatedWeight += item.chance;
            item.weight = accumulatedWeight;
        }

        return accumulatedWeight;
    }

    private static float GetAccumulatedWeight(WeightedValue[] items) {
        float accumulatedWeight = 0f;
        foreach (var item in items) {
            accumulatedWeight += item.chance;
            item.weight = accumulatedWeight;
        }

        return accumulatedWeight;
    }
}
