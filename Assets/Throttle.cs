using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throttle : MonoBehaviour
{
    public float ThrottlePercentage { set; get; }
    public bool Grabbed { set; get; }

    public Vector3 ThrottlePosition { get { return transform.InverseTransformPoint(_throttleHandle.transform.position); } }

    [SerializeField] private Transform _throttleHandle;
    [SerializeField] private Transform _throttleCeiling;
    [SerializeField] private Transform _throttleFloor;
    [SerializeField] private float _throttleDeadZone = 0.02f;

    private float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        maxDistance = Vector3.Distance(_throttleHandle.position, _throttleCeiling.position);
    }

    // Update is called once per frame
    void Update()
    {
        float currentDistance = maxDistance - Vector3.Distance(_throttleHandle.position, _throttleCeiling.position);

        ThrottlePercentage = (currentDistance) / (maxDistance);
    }

    public void SyncThrottlePosition(Vector3 newHandlePosition)
    {
        _throttleHandle.localPosition = newHandlePosition;
    }

    public void ResetThrottle()
    {
        _throttleHandle.localPosition = _throttleFloor.localPosition;
    }
}
