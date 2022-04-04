using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class SexyBoyExit : MonoBehaviour
{
   public void Restart()
   {
      Destroy(GameManager.Instance.gameObject);
      SceneLoadingManager.LoadNewScene("MainMenu");
   }

   public void Quit()
   {
      Application.Quit();
   }
}
