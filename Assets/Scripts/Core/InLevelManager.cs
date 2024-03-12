using Edanoue.Rx;
using UnityEngine;

public class InLevelManager : MonoBehaviour
{
    [SerializeField]
    private ClearConditionManager clearConditionManager;

    private readonly Subject<bool> _onLevelClear = new();


    private bool _canExtract;

    /// <summary>
    /// ステージクリア時に通知するObservable
    /// </summary>
    public Observable<bool> OnLevelClearObservable => _onLevelClear;

    private void Start()
    {
        // new
        clearConditionManager.OnCleared
            .Subscribe(this, (_, state) => state.OnMeetRequire())
            .RegisterTo(destroyCancellationToken);
    }

    /// <summary>
    /// プレイヤーが脱出地点に到達した際に呼び出される
    /// </summary>
    /// <param name="component"></param>
    public void TryExtract(FpsController component)
    {
        if (_canExtract)
        {
            OnLevelClear();
        }
    }

    /// クリア条件を満たしたときの処理
    private void OnMeetRequire()
    {
        _canExtract = true;
    }

    /// <summary>
    /// ステージクリア時の処理
    /// </summary>
    private void OnLevelClear()
    {
        // ステージクリア時にObserverに通知する
        _onLevelClear.OnNext(true);
        _onLevelClear.OnCompleted();
    }
}