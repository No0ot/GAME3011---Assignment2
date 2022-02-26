using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPip : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 endPosition;
    public GameObject end;

    float downSpeed;
    public float downSpeedMin;
    public float downSpeedMax;

    public float upSpeedMin;
    public float upSpeedMax;

    public float lockWindow;

    public float upSpeed;

    bool canLock;
    bool locked;
    float lerpT = 0;

    public bool pickActive;
    public bool reverse;

    private void Awake()
    {
        startPosition = transform.position;
        endPosition = end.transform.position;
        Reset();
    }

    void Pick()
    {
        transform.position = Vector3.Lerp(startPosition, endPosition, lerpT);
        lerpT += (downSpeed * Time.deltaTime);
        if (transform.position == endPosition)
        {
            canLock = true;
            StartCoroutine(Stop());
        }
    }

    private void Update()
    {
        if (!locked)
        {
            if (pickActive)
            {
                if (!reverse)
                    Pick();
                else
                    ReturnToStartPosition();

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (canLock)
                    {
                        locked = true;
                        LockpickingMiniGameManager.Instance.setNumLockedPips(LockpickingMiniGameManager.Instance.numLockedPips + 1);
                    }
                    else
                    {
                        LockpickingMiniGameManager.Instance.FailPick();
                        Reset();
                    }

                }
            }
        }
    }

    void ReturnToStartPosition()
    {
        transform.position = Vector3.Lerp(endPosition, startPosition, lerpT);
        lerpT += (upSpeed * Time.deltaTime);
        if (transform.position == startPosition)
        {
            lerpT = 0;
            reverse = false;
            pickActive = false;
        }
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(lockWindow);
        if (!locked)
        {
            reverse = true;
            lerpT = 0;
            canLock = false;
            upSpeed = Random.Range(upSpeedMin, upSpeedMax);
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            pickActive = true;
            downSpeed = Random.Range(downSpeedMin, downSpeedMax);
        }
    }

    public void Reset()
    {
        if (locked)
            locked = false;

        canLock = false;
        pickActive = false;
        reverse = false;
        lerpT = 0;
        transform.position = startPosition;

        downSpeedMin = 5.0f  + (-(LockpickingMiniGameManager.Instance.lockpickingSkill / 50) + (LockpickingMiniGameManager.Instance.lockDifficulty / 5));
        downSpeedMax = 10.0f + (-(LockpickingMiniGameManager.Instance.lockpickingSkill / 25) + LockpickingMiniGameManager.Instance.lockDifficulty / 5);
        upSpeedMin = 1.5f - (LockpickingMiniGameManager.Instance.lockpickingSkill / 100);
        upSpeedMax = 3.0f - (LockpickingMiniGameManager.Instance.lockpickingSkill / 100);

        lockWindow = (0.30f - (0.025f * LockpickingMiniGameManager.Instance.lockDifficulty)) + (0.02f * (LockpickingMiniGameManager.Instance.lockpickingSkill / 20));
    }
}
