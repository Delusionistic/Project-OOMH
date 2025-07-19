using UnityEngine;

public class HeadStats : MonoBehaviour
{
    public string headName;

    [Header("Buff Settings")]
    public float buffMoveSpeed = 0f;
    public float buffJumpPower = 0f;
    public float buffDuration = 0f; // 0 means permanent

    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnDrop()
    {
        // Optional: Play drop animation or enable physics
        Debug.Log("Head dropped: " + headName);
    }
}
