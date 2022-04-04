using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Flair {
    public string flair;
    public int damage;
    public int health;
}
[CreateAssetMenu(fileName = "CardSwag", menuName = "Swag/SwaggySwagSwag")]
public class CardSwag : ScriptableObject
{
    public string[] FirstNames;
    public string[] LastNames;
    public Flair[] Flairs;
    public Sprite[] Pictures;
    public string[] Description;

    public string[] ZombieNames;
    public string[] ZombieLastNames;
    public Flair[] ZombieFliars;
    public Sprite[] ZombiePictures;
    public string[] ZombieDescription;
}
