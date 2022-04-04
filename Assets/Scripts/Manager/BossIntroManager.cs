using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIntroManager : MonoBehaviour
{
    public void StartFight(){
        SceneLoadingManager.LoadNewScene("BossFight");
    }
}
