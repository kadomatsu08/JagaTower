# nullable enable

using UnityEngine;

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