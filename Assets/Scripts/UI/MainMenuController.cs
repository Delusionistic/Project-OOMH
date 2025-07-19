using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
   public string LevelToLoad = "Level One";
   
   public void LoadLevel()
   {
      SceneManager.LoadScene(LevelToLoad);
   }
}
