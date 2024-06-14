using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TractorBeam : MonoBehaviour
{
    [SerializeField] private float tractorBeamScanRadius = 50f;
    [SerializeField] private TractorBeamUI tractorBeamUI;

    private List<Collider> grabColliders = new List<Collider>();
    private Vector3 tractorItemPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTractorBeam()
    {
        IEnumerable<Toggle> toggles = tractorBeamUI.ToggleGroup.ActiveToggles();

        tractorItemPosition = transform.InverseTransformPoint(toggles.ToList<Toggle>[0].transform.position);
    }

    public void ScanArea()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, tractorBeamScanRadius);

        grabColliders.Clear();
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Grab")
            {
                grabColliders.Add(hitCollider);
            }
        }

        tractorBeamUI.UpdateToggles(grabColliders);
    }
}
