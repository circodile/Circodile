using UnityEngine;

public class BoatManager : MonoBehaviour
{
    [Header("Boat Settings")]
    public float buildProgress;
    public float buildGoal;

    void AddBuildProgress(float amount)
    {
        buildProgress += amount;
    }

    bool isComplete()
    {
        return buildProgress >= buildGoal;
    }

}
