using UnityEngine;

/// <summary>
/// 脱出のために必要なオブジェクト
/// </summary>
public class ExtructRequirementCube : MonoBehaviour, IInteractableObject, IExtractRequirment
{
    [SerializeField]
    private ClearConditionManager clearConditionManager;

    public void MeetRequirement()
    {
        clearConditionManager.CountUpDoneRequirement();
    }

    public void OnInteracted()
    {
        MeetRequirement();
        Destroy(gameObject);
    }
}