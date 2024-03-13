using Edanoue.Rx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GetBoxGameRuleHUD : MonoBehaviour
{
    [SerializeField]
    private GetBoxGameMode gameMode;
    
    [SerializeField]
    private Text clearText;

    private readonly CompositeDisposable _disposableOnDestroy = new();
    
    
    // Start is called before the first frame update
    private void Awake()
    {
        // レベルクリア時の通知を購読
        gameMode.OnLevelClearObservable
            .Take(1)
            .Subscribe(this, (_, state) => state.OnLevelClear())
            .AddTo(_disposableOnDestroy);

        // 要求数、取得数が変化したときの通知を購読
        gameMode.CurrentBoxInfo
            .Subscribe(this, (value, state) => state.OnGetRequirement(value))
            .AddTo(_disposableOnDestroy);
    }

    private void OnDestroy()
    {
        _disposableOnDestroy.Dispose();
    }

    /// <summary>
    /// レベルクリア時に実行する処理
    /// </summary>
    private void OnLevelClear()
    {
        clearText.enabled = true;
        clearText.text = "Level Clear";
    }

    /// <summary>
    /// 目的のものを取得したときに実行する処理
    /// </summary>
    private void OnGetRequirement(Vector2Int value)
    {
        clearText.enabled = true;
        // 取得数 <= 要求数 のとき ゴール地点に到達できるテキストを画面上に表示する
        if (value.x >= value.y)
        {
            clearText.text = "You can extract.";
        }
        else
        {
            clearText.text = $"Obento: {value.x} of {value.y} acquired.";
        }
    }
}