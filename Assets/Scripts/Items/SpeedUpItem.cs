# nullable enable

using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// プレイヤーが取得するとスピードアップするアイテム
/// </summary>
public class SpeedUpItem : MonoBehaviour
{
    [SerializeField]
    private float speedUpValue = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<FpsController>(out var controller))
        {
            return;
        }
        
        // プレイヤーが接触したらスピードアップする
        controller.AddSpeed(speedUpValue);

        // オブジェクトを削除する
        Destroy(gameObject);
    }
}