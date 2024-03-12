// Purpose: クリア条件を管理するクラス

using Edanoue.Rx;
using UnityEngine;
using Unit = Edanoue.Rx.Unit;

/// <summary>
/// クリア条件の一種
/// 要求を一定数満たすことで脱出可能となる
/// </summary>
public class ClearConditionManager : MonoBehaviour, IClearCondition
{
    // 数値をインスペクタ上で入れるのちょっと気に食わない
    [SerializeField]
    private int numOfRequirement = 2;

    private readonly Subject<Unit> _onClearedSub = new();

    private readonly Subject<int> _onNumOfDoneChangedSub = new();

    // 要求の数が変わったら通知するObservable
    public Observable<int> OnNumOfDoneChanged => _onNumOfDoneChangedSub;

    // 要求を満たしたときに通知するObservable
    public Observable<Unit> OnCleared => _onClearedSub;

    // 分子
    public int NumOfDone { get; private set; }

    // 分母
    public int NumOfRequirements => numOfRequirement;

    private void Awake()
    {
        _onNumOfDoneChangedSub.RegisterTo(destroyCancellationToken);
        _onClearedSub.RegisterTo(destroyCancellationToken);
    }

    public void CountUpDoneRequirement()
    {
        NumOfDone++;
        _onNumOfDoneChangedSub.OnNext(NumOfDone);
        if (NumOfDone >= numOfRequirement)
        {
            _onClearedSub.OnNext(Unit.Default);
        }
    }
}

/// <summary>
/// クリア条件を表すインターフェース
/// </summary>
public interface IClearCondition
{
    // 本当は通知関係のメソッドはここに出したい
}