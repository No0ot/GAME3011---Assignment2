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
    void Start()
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
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (lockpickCurrentLocationIndex < 4)
                {
                    moveAnimActive = true;
                    lerpT = 0;
                    lockpickCurrentLocationIndex++;
                    tapPosition = transform.position;
                    tapPositionEnd = new Vector3(tapPosition.x, tapPosition.y, tapPosition.z - 2.5f);
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (lockpickCurrentLocationIndex > 0)
                {
                    moveAnimActive = true;
                    lerpT = 0;
                    lockpickCurrentLocationIndex--;
                    tapPosition = transform.position;
                    tapPositionEnd = new Vector3(tapPosition.x , tapPosition.y, tapPosition.z + 2.5f);
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                tapAnimActive = true;
                tapPosition = transform.position;
                tapPositionEnd = new Vector3(tapPosition.x + 0.75f, tapPosition.y, tapPosition.z);
            }

        }

        if(moveAnimActive)
        {
            Move();
        }

        if (tapAnimActive)
        {
            if (!reverse)
                Tap();
            else
                Reset();
        }
    }

    public void Move()
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

    private void Reset()
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
        animator.SetBool("Break", true);
        breakAnimActive = true;
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
