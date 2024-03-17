# nullable enable

using UnityEngine;

/// <summary>
/// プレイヤーが取得するとスピードアップするアイテム
/// </summary>
public class SpeedUpItem : MonoBehaviour
{
    public void OnGotItem()
    {
        Destroy(gameObject);
    }
}