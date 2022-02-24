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
    float moveValue;

    Vector3 tapPosition;
    Vector3 tapPositionEnd;
    // Start is called before the first frame update
    void Start()
    {
        lockpickCurrentLocationIndex = 0;
        moveValue = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!tapAnimActive && !moveAnimActive)
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
}
