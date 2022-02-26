using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockpickingMiniGameManager : MonoBehaviour
{
    public int numLockedPips;
    int numLockPicks;
    [SerializeField]
    int startingNumLockPicks;

    public float lockDifficulty;
    public float lockpickingSkill;

    public LockPick theLockpick;

    public static LockpickingMiniGameManager Instance;

    [SerializeField]
    LockPip[] lockPips;

    public TMP_Text timerText;
    public TMP_Text difficultyText;

    public DoorScript door;

    float time;
    float timeStart = 20.0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        lockPips = GetComponentsInChildren<LockPip>();
        lockDifficulty = 1;
        lockpickingSkill = 1;
        numLockPicks = startingNumLockPicks;
        time = timeStart;
    }

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            timerText.text = "0." + time;
        }
        else
        {
            time = 0f;
            StartCoroutine(Fail());
        }

        difficultyText.text = "Difficulty: " + lockDifficulty;
    }

    private void OnDisable()
    {
        ResetMinigame();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void FailPick()
    {
        theLockpick.BreakPick();
        //numLockPicks--;
        //if(numLockPicks <= 0)
        //{
        //    StartCoroutine(Fail());
        //}
    }

    public void ResetMinigame()
    {
        foreach(LockPip pip in lockPips)
        {
            pip.Reset();
        }
        numLockPicks = startingNumLockPicks;
        numLockedPips = 0;
        time = timeStart;
    }

    public void changeLockpickingSkill(float newskill)
    {
        lockpickingSkill = newskill;
        ResetMinigame();
    }

    public void changeLockDifficulty(float newdiff)
    {
        lockDifficulty = newdiff;
        ResetMinigame();
    }

    public void setNumLockedPips(int value)
    {
        numLockedPips = value;
        if(numLockedPips >= 5)
        {
            StartCoroutine(OpenDoor());
            
        }
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(1.0f);
        theLockpick.Spawn();
        door.Unlock();
        GameManager.Instance.LeaveLockpickingMinigame();
        yield return null;
    }

    IEnumerator Fail()
    {
        yield return new WaitForSeconds(1.0f);
        theLockpick.Spawn();
        GameManager.Instance.LeaveLockpickingMinigame();
        yield return null;
    }
}
