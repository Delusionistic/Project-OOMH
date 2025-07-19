using System.Collections;
using UnityEngine;

public class HeadSwap : MonoBehaviour
{
    [Header("Equipped Head")]
    private GameObject currentHead;
    private HeadStats currentStats;
    private Animator headAnimator;

    [Header("Buffs")]
    private float defaultMoveSpeed = 5f;
    private float defaultJumpPower = 5f;
    public float moveSpeed;
    public float jumpPower;

    [Header("Attach Point")]
    [SerializeField] private Transform headMountPoint;

    [Header("Tags")]
    public string headTag = "HeadItem";

    private void Start()
    {
        moveSpeed = defaultMoveSpeed;
        jumpPower = defaultJumpPower;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(headTag))
        {
            HeadStats head = collision.GetComponent<HeadStats>();
            if (head != null)
            {
                Debug.Log("Collected head: " + head.headName);
                EquipHead(head.gameObject);
            }
        }
    }

    public void EquipHead(GameObject newHead)
    {
        DropCurrentHead(); // Drop current head if any

        currentHead = newHead;
        currentStats = newHead.GetComponent<HeadStats>();

        if (currentStats == null)
        {
            Debug.LogWarning("No HeadStats component found on equipped head.");
            return;
        }

        // Attach head to player
        currentHead.transform.SetParent(headMountPoint);
        currentHead.transform.localPosition = Vector3.zero;
        currentHead.transform.localRotation = Quaternion.identity;

        headAnimator = currentHead.GetComponent<Animator>();

        ApplyBuffs(currentStats);
    }

    public void DropCurrentHead()
    {
        if (currentHead != null)
        {
            Debug.Log("Dropped head: " + currentHead.name);

            currentHead.transform.SetParent(null);
            currentHead.transform.position = transform.position + Vector3.right; // Drop slightly to the right

            HeadStats stats = currentHead.GetComponent<HeadStats>();
            if (stats != null)
            {
                stats.OnDrop();
            }

            currentHead = null;
            currentStats = null;
            ResetBuffs();
        }
    }

    private void ApplyBuffs(HeadStats stats)
    {
        if (stats == null) return;

        moveSpeed = stats.buffMoveSpeed > 0 ? stats.buffMoveSpeed : defaultMoveSpeed;
        jumpPower = stats.buffJumpPower > 0 ? stats.buffJumpPower : defaultJumpPower;

        if (stats.buffDuration > 0)
        {
            StartCoroutine(BuffTimer(stats.buffDuration));
        }
    }

    private IEnumerator BuffTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        ResetBuffs();
    }

    private void ResetBuffs()
    {
        moveSpeed = defaultMoveSpeed;
        jumpPower = defaultJumpPower;
    }
}
