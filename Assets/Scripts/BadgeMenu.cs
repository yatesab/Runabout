using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeMenu : MonoBehaviour
{
    [SerializeField] private GameObject playerMenu;
    [SerializeField] private Transform characterBody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMenu()
    {
        playerMenu.SetActive(!playerMenu.activeSelf);

        if(playerMenu.activeSelf)
        {
            playerMenu.transform.localEulerAngles = new Vector3(playerMenu.transform.localEulerAngles.x, characterBody.localEulerAngles.y, playerMenu.transform.localEulerAngles.z);
        }
    }
}
