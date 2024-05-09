using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeMenu : MonoBehaviour
{
    [SerializeField] private GameObject playerMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu()
    {
        playerMenu.SetActive(true);
    }
}
