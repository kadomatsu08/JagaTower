using Edanoue.Rx;
using UnityEngine;

public class GetBoxGameMode : MonoBehaviour
{
    private readonly ReactiveProperty<Vector2Int> _currentBoxInfoReactiveProperty = new(Vector2Int.zero);

    private readonly Subject<Unit> _onLevelClearSub = new();
    private          bool          _canExtract;

    /// <summary>
    /// ステージクリア時に通知するObservable
    /// </summary>
    public Observable<Unit> OnLevelClearObservable => _onLevelClearSub;

    /// <summary>
    /// 要求数、取得数のどちらかが変化した際に通知するObservable
    /// </summary>
    public ReadOnlyReactiveProperty<Vector2Int> CurrentBoxInfo => _currentBoxInfoReactiveProperty;

    public void setBunbo(int value)
    {
        _currentBoxInfoReactiveProperty.Value = new Vector2Int(_currentBoxInfoReactiveProperty.Value.x, value);
    }

    public void AddBunshi(int value = 1)
    {
        _currentBoxInfoReactiveProperty.Value
            = new Vector2Int(_currentBoxInfoReactiveProperty.Value.x + value, _currentBoxInfoReactiveProperty.Value.y);

        if (_currentBoxInfoReactiveProperty.Value.x <= _currentBoxInfoReactiveProperty.Value.y)
        {
            _canExtract = true;
        }
    }

    /// <summary>
    /// プレイヤーが脱出地点に到達した際に呼び出される
    /// </summary>
    /// <param name="component"></param>
    public void TryExtract(FpsController component)
    {
        if (_canExtract)
        {
            LevelClear();
        }
    }

    private void LevelClear()
    {
        // ステージクリア時にObserverに通知する
        _onLevelClearSub.OnNext(Unit.Default);
        _onLevelClearSub.OnCompleted();
    }
}