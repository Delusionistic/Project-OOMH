using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
   public string LevelToLoad = "Level1";
   
   public void LoadLevel()
   {
      SceneManager.LoadScene(LevelToLoad);
   }
}
