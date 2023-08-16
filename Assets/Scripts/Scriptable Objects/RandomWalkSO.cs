using UnityEngine;

[CreateAssetMenu(fileName = "RandomWalkParameters_", menuName = "Procedural Generation/RandomWalkData")]
public class RandomWalkSO : ScriptableObject {
    public int iterations;
    public int walkLength;
    public bool startRandomlyEachIteration;
}
