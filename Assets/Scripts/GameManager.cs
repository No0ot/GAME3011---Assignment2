using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int numLockedPips;
    [SerializeField]
    int numLockPicks;

    public int lockDifficulty;
    [Range(0.1f, 0.9f)]
    public int lockpickingSkill;

    public LockPick theLockpick;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void FailPick()
    {
        theLockpick.BreakPick();
        numLockPicks--;
        if(numLockPicks <= 0)
        {
            //LOSE GAME
            Debug.Log("LOSER");
        }
    }
}
