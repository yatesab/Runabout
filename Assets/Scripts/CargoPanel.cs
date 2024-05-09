using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CargoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text cargoList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateList(List<Item> items)
    {
        foreach (var item in items)
        {
            cargoList.text += item.Name + "\n";
        }
    }
}
