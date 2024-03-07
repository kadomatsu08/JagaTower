using System;
using Edanoue.Rx;
using UnityEngine;

public class InLevelManager : MonoBehaviour
{
    private readonly Subject<bool> _onLevelClear = new();

    // TODO デバッグ用にtrueにしている
    private readonly bool _canExtract = true;

    /// <summary>
    /// ステージクリア時に通知するObservable
    /// </summary>
    public IObservable<bool> OnLevelClearObservable => _onLevelClear;

    private void OnDestroy()
    {
        _onLevelClear.Dispose();
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

    /// <summary>
    /// ステージクリア時の処理
    /// </summary>
    private void OnLevelClear()
    {
        // ステージクリア時にObserverに通知する
        _onLevelClear.OnNext(true);
    }
}