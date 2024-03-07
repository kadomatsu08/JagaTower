using UnityEngine;

/// <summary>
/// 脱出地点クラス
/// このゲームではプレイヤーが条件を満たした上でこのエリアに入るとステージクリアとなる
/// </summary>
public class ExtractionArea : MonoBehaviour
{
    [SerializeField]
    private InLevelManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<FpsController>(out var player))
        {
            gameManager.TryExtract(player);
        }
    }
}