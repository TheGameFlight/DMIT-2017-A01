using System;
using UnityEngine;

[Serializable]
public class CharacterData
{
    public string characterName;
    public Sprite portrait;
    public CharacterStats stats;
    
    public CharacterData(CharacterSO config_)
    {
        characterName = config_.characterName;
        portrait = config_.portrait;

        stats = new CharacterStats(config_.stats);
    }
}
