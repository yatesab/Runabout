using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SubsystemBar : MonoBehaviour
{
    public Button addButton;
    public Button subtractButton;

    [SerializeField] private GameObject[] defaultBars;
    [SerializeField] private GameObject[] extraBars;

    public SubsystemPower subsystemPower;

    private bool _hasChanges;

    public void Start()
    {
        _hasChanges = true;

        addButton.onClick.AddListener(AddBar);
        subtractButton.onClick.AddListener(RemoveBar);
    }

    public void Update()
    {
        if (_hasChanges)
        {
            UpdateBars();

            _hasChanges = false;
        }
    }

    private void UpdateBars()
    {
        int power = subsystemPower.PowerLevel;
        // Loop over extra bars
        for (int i = 0; i < 8; i++)
        {
            if(i < 4)
            {
                defaultBars[i].SetActive(i < power ? true : false);
            } else
            {
                extraBars[i - 4].SetActive(i < power ? true : false);
            }
        }
    }

    private void AddBar()
    {
        if(subsystemPower.AddToPower())
        {
            _hasChanges = true;
        }
    }

    private void RemoveBar()
    {
        if (subsystemPower.RemoveFromPower())
        {
            _hasChanges = true;
        }
    }
}
