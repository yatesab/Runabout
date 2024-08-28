using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PullLever : MonoBehaviour
{
    [SerializeField] private float retractionSpeed = 0.3f;

    public float pullPercentage = 0;

    private Vector3 startPoint;
    private Coroutine resethandleRoutine;
    private bool isGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.05f, transform.localPosition.z);
        transform.localPosition = startPoint;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovementPercentage();
    }

    public void CalculateMovementPercentage()
    {
        pullPercentage = (transform.position.y - startPoint.y) / (0.05f);
    }

    public void GrabbedHandle()
    {
        isGrabbed = true;
        if (resethandleRoutine != null)
        {
            StopCoroutine(resethandleRoutine);
            resethandleRoutine = null;
        }
    }

    public void ReleaseHandle()
    {
        isGrabbed = false;
        resethandleRoutine = StartCoroutine(MoveHandle());
    }

    public IEnumerator MoveHandle()
    {
        while (transform.localPosition != startPoint)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPoint, retractionSpeed);
            yield return null;
        }
    }
}
