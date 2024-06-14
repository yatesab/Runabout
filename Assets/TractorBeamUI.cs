using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TractorBeamUI : MonoBehaviour
{
    [SerializeField] private GameObject grabItemPrefab;
    
    public ToggleGroup ToggleGroup { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ToggleGroup = GetComponent<ToggleGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateToggles(List<Collider> grabColliders)
    {
        DestroyAllToggles();

        foreach(Collider col in grabColliders)
        {
            GameObject item = Instantiate(grabItemPrefab, ToggleGroup.transform);
            ToggleGroup.RegisterToggle(item.GetComponent<Toggle>());
        }
    }

    private void DestroyAllToggles()
    {
        while (ToggleGroup.transform.childCount != 0)
        {
            Destroy(ToggleGroup.transform.GetChild(0).gameObject);
        }
    }
}
