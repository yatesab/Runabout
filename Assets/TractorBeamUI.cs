using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TractorBeamUI : MonoBehaviour
{
    [SerializeField] private TractorBeam tractorBeam;
    [SerializeField] private GameObject togglePrefab;
    [SerializeField] private ToggleGroup toggleGroup;

    private List<GameObject> targets = new List<GameObject>();

    // Update is called once per frame
    void Start()
    {
        tractorBeam.scanAction += UpdateUI;
    }

    private void DestoryOldTargets()
    {
        foreach (GameObject target in targets)
        {
            Destroy(target);
        }

        targets.Clear();
    }

    private void UpdateUI()
    {
        DestoryOldTargets();

        foreach (Collider col in tractorBeam.Targets)
        {
            GameObject toggleObject = Instantiate(togglePrefab, toggleGroup.transform);

            toggleGroup.RegisterToggle(toggleObject.GetComponent<Toggle>());

            toggleObject.GetComponentInChildren<TMP_Text>().text = col.name;

            targets.Add(toggleObject);
        }
    }

    public void ActivateTractorBeam()
    {
        Toggle firstActive = toggleGroup.GetFirstActiveToggle();
        int index = targets.FindIndex(target => target == firstActive.gameObject);
        tractorBeam.StartTractorBeam(index);
    }
}
