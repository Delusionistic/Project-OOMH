using Player;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController Player;
    private CinemachineCamera _camera;
    
    public void Awake()
    {
        Player = FindFirstObjectByType<PlayerController>();
        _camera = GetComponent<CinemachineCamera>();
        _camera.Follow = Player.transform;
    }
} 
