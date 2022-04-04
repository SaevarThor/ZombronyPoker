using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSwag", menuName = "Swag/SwaggySwagSwag")]
public class CardSwag : ScriptableObject
{
    public string[] FirstNames;
    public string[] LastNames;
    public string[] Flairs;
    public Sprite[] Pictures;
    public string[] Description;

    public string[] ZombieNames;
    public string[] ZombieLastNames;
    public string[] ZombieFliars;
    public Sprite[] ZombiePictures;
    public string[] ZombieDescription;
}
