
using Edanoue.Rx;
using UnityEngine;

/// <summary>
/// 全部箱を取るゲームモードで、箱の取得数を数えるクラス
/// </summary>
public class BoxCounter : MonoBehaviour
{
    [SerializeField]
    private GetBoxGameMode gameMode;
    
    private void Awake()
    {
        var bunbo = FindAndInitCubes();
        gameMode.setBunbo(bunbo);
        gameMode.setBunshi(0);
    }
    
    /// <summary>
    /// シーン上に存在する ExtructRequirementCube の個数を数え上げ、インタラクトの通知を購読する
    /// </summary>
    /// <returns> 実行時にシーン上に存在する ExtructRequirementCube の個数 </returns>
    private int FindAndInitCubes()
    {
        var requirements = FindObjectsByType<ExtructRequirementCube>(FindObjectsSortMode.None);
        foreach (var requirement in requirements)
        {
            requirement.OnInteractedObservable
                .Subscribe(this, (_, state) => state.gameMode.AddBunshi(1))
                .RegisterTo(destroyCancellationToken);
        }
        return requirements.Length;
    }
}
