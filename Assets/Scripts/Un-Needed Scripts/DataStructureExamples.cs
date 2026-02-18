using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class DataStructureExamples : MonoBehaviour
{
    public List<int> integers = new List<int>();
    public Dictionary<int, Pokemon> pokedex = new Dictionary<int, Pokemon>();
    public List<Pokemon> pokedexButWorse = new List<Pokemon>();
    private void Start()
    {
    
        Pokemon mewtwo = new Pokemon();
        mewtwo.pokemonName = "Mewtwo";
        mewtwo.description = "mew but two";
        mewtwo.baseStats = new BaseStats(100, 50, 65, 120, 110, 100);
        pokedex.Add(151, mewtwo);
        Debug.Log(pokedex[151].pokemonName);

       

    }
}

public class Pokemon
{
    public string pokemonName;
    public string description;
    public Sprite sprite;
    public BaseStats baseStats;
}

public class Profile
{
    public string playerName;
    public int highscore;
    public Color color;
    public VehicleType vehicleType;

}

public enum VehicleType
{
    CART,
    BIKE,
    BOAT
}

public class BaseStats
{
    public int HP, ATK, DEF, SPATK, SPDEF, SPD;

    public BaseStats(int hp_, int atk_, int def_, int spatk_, int spdef_, int spd_)
    {
        HP = hp_;
        ATK = atk_;
        DEF = def_;
        SPATK = spatk_;
        SPDEF = spdef_;
        SPD = spd_;
    }
}
