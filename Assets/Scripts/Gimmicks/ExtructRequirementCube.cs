using Edanoue.Rx;
using UnityEngine;

/// <summary>
/// 脱出のために必要なオブジェクト
/// </summary>
public class ExtructRequirementCube : MonoBehaviour, IInteractableObject
{
    // 箱が取られたときに通知するobservable
    private readonly Subject<Unit> _onInteractedSub = new();
    public Observable<Unit> OnInteractedObservable => _onInteractedSub;

    private void Awake()
    {
        _onInteractedSub.RegisterTo(destroyCancellationToken);
    }

    public void OnInteracted()
    {
        // インタラクトしたときに通知
        _onInteractedSub.OnNext(Unit.Default);

        Destroy(gameObject);
    }
}