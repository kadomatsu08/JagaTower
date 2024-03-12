using Edanoue.Rx;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField]
    private InLevelManager gameManager;

    [SerializeField]
    private ClearConditionManager clearConditionManager;
    
    [SerializeField]
    private Text clearText;

    private readonly CompositeDisposable _disposableOnDestroy = new();

    // Start is called before the first frame update
    private void Start()
    {
        // レベルクリア時の通知を購読
        gameManager.OnLevelClearObservable
            .Take(1)
            .Subscribe(this,(_, state) => state.OnLevelClear())
            .AddTo(_disposableOnDestroy);
        
        // クリア条件の変化を購読
        clearConditionManager.OnNumOfDoneChanged
            .Subscribe(this, (num, state) => state.OnGetRequirement(num))
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
    private void OnGetRequirement(int numOfDone)
    {
        clearText.enabled = true;
        clearText.text = $"Obento: {numOfDone} of {clearConditionManager.NumOfRequirements} acquired.";
    }
}