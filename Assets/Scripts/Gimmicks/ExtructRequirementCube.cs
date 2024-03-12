using UnityEngine;

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