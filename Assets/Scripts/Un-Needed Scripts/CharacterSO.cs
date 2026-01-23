using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Scriptable Objects/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public string characterName;
    public Sprite portrait;
    public CharacterStats stats;
    
}

[Serializable]
public class CharacterStats
{
    public int maxHP;
    public int currentHP;
    public int maxSP;
    public int currentSP;
    public int atk;
    public int def;
    public int matk;
    public int mdef;
    public int spe;

    public CharacterStats(CharacterStats ref_)
    {
        maxHP = ref_.maxHP;
        maxSP = ref_.maxSP;

        currentHP = ref_.currentHP;
        currentSP = ref_.currentSP;

        atk = ref_.atk;
        def = ref_.def;
        matk = ref_.matk;
        mdef = ref_.mdef;
        spe = ref_.spe;
    }
}
