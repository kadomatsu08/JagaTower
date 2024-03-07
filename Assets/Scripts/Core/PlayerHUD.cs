using Edanoue.Rx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField]
    private InLevelManager gameManager;

    [SerializeField]
    private Text clearText;

    private readonly CompositeDisposable _disposableOnDestroy = new();

    // Start is called before the first frame update
    private void Start()
    {
        // レベルクリア時の通知を購読
        gameManager.OnLevelClearObservable
            .Take(1)
            .Subscribe(_ => OnLevelClear())
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
}