using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField]
    GameObject lockPickingMiniGame;
    [SerializeField]
    GameObject mainGame;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void StartLockpickingMinigame(DoorScript door)
    {
        lockPickingMiniGame.SetActive(true);
        lockPickingMiniGame.GetComponent<LockpickingMiniGameManager>().door = door;
        mainGame.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LeaveLockpickingMinigame()
    {
        lockPickingMiniGame.SetActive(false);
        mainGame.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

}
