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

    bool canLock;
    bool locked;
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
                    Reset();

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (canLock)
                    {
                        locked = true;
                    }
                    else
                    {
                        GameManager.Instance.FailPick();
                    }

                }
            }
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
        yield return new WaitForSeconds(0.55f);
        if (!locked)
        {
            reverse = true;
            lerpT = 0;
            canLock = false;
            upSpeed = Random.Range(0.5f, 2.0f);
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            pickActive = true;
            downSpeed = Random.Range(3.0f, 8.0f);
        }
    }
}
