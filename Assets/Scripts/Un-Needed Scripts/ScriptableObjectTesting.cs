using UnityEngine;

public class ScriptableObjectTesting : MonoBehaviour
{
    public CharacterSO goku;

    public CharacterData gokuData;

    private void Start()
    {
        gokuData = new CharacterData(goku);
        Concentrate();
    }

    public void Concentrate()
    {
        gokuData.stats.atk *= 2;
    }
}
