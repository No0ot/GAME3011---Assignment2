using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPick : MonoBehaviour
{
    [SerializeField]
    int lockpickCurrentLocationIndex;

    float lerpT = 0;
    bool tapAnimActive;
    bool reverse;
    bool moveAnimActive;
    [SerializeField]
    bool breakAnimActive;

    Vector3 spawnPosition;
    Vector3 tapPosition;
    Vector3 tapPositionEnd;

    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        lockpickCurrentLocationIndex = 0;
        spawnPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!tapAnimActive && !moveAnimActive && !breakAnimActive)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (lockpickCurrentLocationIndex < 4)
                {
                    Move(-2.5f);
                }
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (lockpickCurrentLocationIndex > 0)
                {
                    Move(2.5f);
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                tapAnimActive = true;
                tapPosition = transform.position;
                tapPositionEnd = new Vector3(tapPosition.x + 0.75f, tapPosition.y, tapPosition.z);
            }

        }

        if(moveAnimActive)
        {
            MoveLockpickIn();
        }

        if (tapAnimActive)
        {
            if (!reverse)
                Tap();
            else
                ReturnToStartPosition();
        }
    }

    public void Move(float distance)
    {
        moveAnimActive = true;
        lerpT = 0;

        if(distance < 0)
            lockpickCurrentLocationIndex++;
        else
            lockpickCurrentLocationIndex--;

        tapPosition = transform.position;
        tapPositionEnd = new Vector3(tapPosition.x, tapPosition.y, tapPosition.z + distance);
    }

    public void MoveLockpickIn()
    {
        transform.position = Vector3.Lerp(tapPosition, tapPositionEnd, lerpT);
        lerpT += (2 * Time.deltaTime);
        if (transform.position == tapPositionEnd)
        {
            moveAnimActive = false;
            lerpT = 0;
        }
    }

    void Tap()
    {
        transform.position = Vector3.Lerp(tapPosition, tapPositionEnd, lerpT);
        lerpT += (2 * Time.deltaTime);
        if (transform.position == tapPositionEnd)
        {
            reverse = true;
            lerpT = 0;
        }
    }

    private void ReturnToStartPosition()
    {
        transform.position = Vector3.Lerp(tapPositionEnd, tapPosition, lerpT);
        lerpT += (2 * Time.deltaTime);
        if (transform.position == tapPosition)
        {
            lerpT = 0;
            reverse = false;
            tapAnimActive = false;
        }
    }

    public void BreakPick()
    {
        if (!breakAnimActive)
        {
            animator.SetBool("Break", true);
            breakAnimActive = true;
        }
    }

    public void Spawn()
    {
        transform.position = spawnPosition;
        transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        animator.SetBool("Break", false);
        breakAnimActive = false;
        lockpickCurrentLocationIndex = 0;
    }
}
