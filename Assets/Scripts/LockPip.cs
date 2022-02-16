using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPip : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 endPosition;
    public GameObject end;

    public float downSpeed;
    public float upSpeed;

    bool canStop;
    float lerpT = 0;

    public bool pickActive;
    public bool reverse;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = end.transform.position;
    }

    void Pick()
    {
        transform.position = Vector3.Lerp(startPosition, endPosition, lerpT);
        lerpT += (downSpeed * Time.deltaTime);
        if (transform.position == endPosition)
        {
            StartCoroutine(Stop());
        }
    }

    private void Update()
    {
        if (pickActive)
        {
            if (!reverse)
                Pick();
            else
                Reset();
        }
        
    }

    void Reset()
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
        yield return new WaitForSeconds(0.1f);
        reverse = true;
        lerpT = 0;
        yield return null;
    }


}
