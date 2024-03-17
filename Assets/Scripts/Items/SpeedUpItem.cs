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

    public void OnGotItem()
    {
        Destroy(gameObject);
    }
}