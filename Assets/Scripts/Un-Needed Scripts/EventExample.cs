using UnityEngine;
using UnityEngine.Events;

public class EventExample : MonoBehaviour
{
    public UnityEvent onCoinCollected;
    public int coinCount;

    [ContextMenu("collect coin")]
    public void CollectCoin()
    {
        coinCount++;
        onCoinCollected?.Invoke();
    }
}
