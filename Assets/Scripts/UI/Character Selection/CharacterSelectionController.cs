using UnityEngine;

public class CharacterSelectionController : MonoBehaviour
{
    [field: SerializeField] public Vector2 CharacterPosition { get; set; }
    public void SelectCharacter(GameObject selectedCharacter)
    {
        GameObject player = Instantiate(selectedCharacter, CharacterPosition, Quaternion.identity);
        DontDestroyOnLoad(player);
    }
}
