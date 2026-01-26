using UnityEngine;

public class DontDie : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject.transform);
    }
}
