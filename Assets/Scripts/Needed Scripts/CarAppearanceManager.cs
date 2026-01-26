using UnityEngine;

public class CarAppearanceManager : MonoBehaviour
{
    [Header("Car Bodies (only one active)")]
    public GameObject[] carBodies;

    [Header("Material")]
    public Renderer bodyRenderer;
    public SaveSystem saveSystem;

    private void Start()
    {
        SaveData data = saveSystem.LoadProfile(ActiveProfile.profileName);
        ApplyAppearance(data);
    }

    private void ApplyAppearance(SaveData data)
    {
        for (int i = 0; i < carBodies.Length; i++)
            carBodies[i].SetActive(i == data.carBodyIndex);

        Material bodyMat = bodyRenderer.material;
        bodyMat.color = data.bodyColor;
    }
}
