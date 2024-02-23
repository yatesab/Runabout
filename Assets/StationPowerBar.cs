using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationPowerBar : MonoBehaviour
{
    [SerializeField] private GameObject[] defaultBars;

    private bool _hasChanges;
    private int _powerLevel;

    // Start is called before the first frame update
    void Start()
    {
        _hasChanges = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasChanges)
        {
            Debug.Log("Update");
            UpdateUI();

            _hasChanges = false;
        }
    }

    private void UpdateUI()
    {
        // Loop over extra bars
        for (int i = 0; i < 8; i++)
        {
            defaultBars[i].SetActive(i < _powerLevel ? true : false);
        }
    }

    public void UpdateBar(int power)
    {
        _powerLevel = power;
        _hasChanges = true;
    }
}
