using UnityEngine;
using UnityEngine.Events;

public class CollisionEventHandler : MonoBehaviour
{
    public string tagToCheck;
    public UnityEvent OnCollision;
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != tagToCheck) return;
        OnCollision?.Invoke();
    }
}
