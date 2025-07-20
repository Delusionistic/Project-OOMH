//using System;
//using Unity.VisualScripting;
//using UnityEngine;

//namespace Level
//{
//    public class SceneLoader : MonoBehaviour
//    {
//        public string LevelToLoad = "";
        
//        public void LoadLevel()
//        {
//            UnityEngine.SceneManagement.SceneManager.LoadScene(LevelToLoad);
//        }

<<<<<<< HEAD:Assets/Scripts/Level/SceneLoader.cs
        private void OnCollisionEnter2D(Collision2D other)
        {
            //var player = other.gameObject.GetComponent<player.PlayerController>();
            //if (player != null)
            {
                LoadLevel();
            }
        }
    }
}
=======
//        private void OnCollisionEnter2D(Collision2D other)
//        {
//            var player = other.gameObject.GetComponent<Player.PlayerController>();
//            if (player != null)
//            {
//                LoadLevel();
//            }
//        }
//    }
//}
>>>>>>> UI:Assets/Level/SceneLoader.cs
