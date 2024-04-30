using UnityEngine;
using Unity.XR.CoreUtils;
using static Unity.XR.CoreUtils.XROrigin;

public class PlayerRecenter : MonoBehaviour
{
    private XROrigin xrOrigin;

    // Start is called before the first frame update
    void Start()
    {
        xrOrigin = GetComponent<XROrigin>();
    }

    public void RecenterPlayerRig()
    {
        xrOrigin.RequestedTrackingOriginMode = TrackingOriginMode.Device;

        xrOrigin.RequestedTrackingOriginMode = TrackingOriginMode.Floor;
    }
}
