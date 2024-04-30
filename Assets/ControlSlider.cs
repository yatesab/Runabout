using UnityEngine;

public class ControlSlider : MonoBehaviour
{
    public bool Grabbed { get; private set; }

    [Header("Joint")]
    [SerializeField] private ConfigurableJoint joint;
    [SerializeField] private float jointLimit;

    [Header("Slider")]
    [SerializeField] protected Transform slider;


    protected float maxDistance;
    protected float lowerLimit;

    // Start is called before the first frame update
    public void Start()
    {
        maxDistance = jointLimit * 2;
        lowerLimit = jointLimit * -1;

        // Setup joint
        SoftJointLimit JointLimit = new SoftJointLimit();
        JointLimit.limit = jointLimit;

        joint.linearLimit = JointLimit;
        joint.connectedAnchor = new Vector3(transform.position.x, slider.position.y, transform.position.z);
    }

    public void HandleGrabbed()
    {
        Grabbed = true;
    }

    public void HandleReleased()
    {
        Grabbed = false;
    }
}
