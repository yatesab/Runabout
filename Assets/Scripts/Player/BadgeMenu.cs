using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadgeMenu : MonoBehaviour
{
    [SerializeField] private GameObject playerMenu;
    [SerializeField] private Transform characterBody;
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject defaultCanvas;
    [SerializeField] private Dropdown YawDropdown;
    [SerializeField] private Dropdown PitchDropdown;
    [SerializeField] private Dropdown RollDropdown;

    public void Awake()
    {

    }

    private void UpdateControlUI(Dropdown selectedDropdown, SHIP_MOVEMENT_TYPE settingType)
    {
        if (PlayerConditionManager.instance.leftMovementLeftRightSetting == settingType)
        {
            selectedDropdown.value = 0;
        }else if (PlayerConditionManager.instance.leftMovementLeftRightSetting == settingType)
        {
            selectedDropdown.value = 1;
        }
        else if(PlayerConditionManager.instance.leftMovementLeftRightSetting == settingType)
        {
            selectedDropdown.value = 2;
        }
        else if(PlayerConditionManager.instance.leftMovementLeftRightSetting == settingType)
        {
            selectedDropdown.value = 3;
        }
    }

    public void ToggleMenu()
    {
        playerMenu.SetActive(!playerMenu.activeSelf);

        if(playerMenu.activeSelf)
        {
            playerMenu.transform.localEulerAngles = new Vector3(playerMenu.transform.localEulerAngles.x, characterBody.localEulerAngles.y, playerMenu.transform.localEulerAngles.z);
        }
    }

    public void ToggleSettingsPage()
    {
        defaultCanvas.SetActive(!defaultCanvas.activeSelf);
        settingsCanvas.SetActive(!settingsCanvas.activeSelf);
    }

    private void ResetControlSetting(SHIP_MOVEMENT_TYPE settingType)
    {
        if (PlayerConditionManager.instance.leftMovementLeftRightSetting == settingType)
        {
            PlayerConditionManager.instance.leftMovementLeftRightSetting = SHIP_MOVEMENT_TYPE.NONE;
        }
        else if (PlayerConditionManager.instance.leftMovementUpDownSetting == settingType)
        {
            PlayerConditionManager.instance.leftMovementUpDownSetting = SHIP_MOVEMENT_TYPE.NONE;
        }
        else if (PlayerConditionManager.instance.leftStickLeftRightSetting == settingType)
        {
            PlayerConditionManager.instance.leftStickLeftRightSetting = SHIP_MOVEMENT_TYPE.NONE;
        }
        else if (PlayerConditionManager.instance.leftStickUpDownSetting == settingType)
        {
            PlayerConditionManager.instance.leftStickUpDownSetting = SHIP_MOVEMENT_TYPE.NONE;
        }
        else if (PlayerConditionManager.instance.rightMovementLeftRightSetting == settingType)
        {
            PlayerConditionManager.instance.rightMovementLeftRightSetting = SHIP_MOVEMENT_TYPE.NONE;
        }
        else if (PlayerConditionManager.instance.rightMovementUpDownSetting == settingType)
        {
            PlayerConditionManager.instance.rightMovementUpDownSetting = SHIP_MOVEMENT_TYPE.NONE;
        }
        else if (PlayerConditionManager.instance.rightStickLeftRightSetting == settingType)
        {
            PlayerConditionManager.instance.rightStickLeftRightSetting = SHIP_MOVEMENT_TYPE.NONE;
        }
        else if (PlayerConditionManager.instance.rightStickUpDownSetting == settingType)
        {
            PlayerConditionManager.instance.rightStickUpDownSetting = SHIP_MOVEMENT_TYPE.NONE;
        }
    }

    private void SetLeftRightControlSetting(int index, SHIP_MOVEMENT_TYPE settingType)
    {
        switch (index)
        {
            case 0:
                PlayerConditionManager.instance.leftMovementLeftRightSetting = settingType;
                break;
            case 1:
                PlayerConditionManager.instance.leftStickLeftRightSetting = settingType;
                break;
            case 2:
                PlayerConditionManager.instance.rightMovementLeftRightSetting = settingType;
                break;
            case 3:
                PlayerConditionManager.instance.rightStickLeftRightSetting = settingType;
                break;
        }
    }

    private void SetUpDownControlSetting(int index, SHIP_MOVEMENT_TYPE settingType)
    {
        switch (index)
        {
            case 0:
                PlayerConditionManager.instance.leftMovementLeftRightSetting = settingType;
                break;
            case 1:
                PlayerConditionManager.instance.leftStickLeftRightSetting = settingType;
                break;
            case 2:
                PlayerConditionManager.instance.rightMovementLeftRightSetting = settingType;
                break;
            case 3:
                PlayerConditionManager.instance.rightStickLeftRightSetting = settingType;
                break;
        }
    }

    public void ChangeYawControls(int index)
    {
        ResetControlSetting(SHIP_MOVEMENT_TYPE.YAW);

        SetLeftRightControlSetting(index, SHIP_MOVEMENT_TYPE.YAW);
    }

    public void SetYawControl(int index)
    {
        ResetControlSetting(SHIP_MOVEMENT_TYPE.YAW);

        SetLeftRightControlSetting(index, SHIP_MOVEMENT_TYPE.YAW);
    }

    public void ChangePitchControls(int index)
    {
        ResetControlSetting(SHIP_MOVEMENT_TYPE.PITCH);

        SetUpDownControlSetting(index, SHIP_MOVEMENT_TYPE.PITCH);
    }

    public void ChangeRollControls(int index)
    {
        ResetControlSetting(SHIP_MOVEMENT_TYPE.ROLL);

        SetLeftRightControlSetting(index, SHIP_MOVEMENT_TYPE.ROLL);
    }

    public void ChangeStrafeControls(int index)
    {
        ResetControlSetting(SHIP_MOVEMENT_TYPE.STRAFE);

        SetUpDownControlSetting(index, SHIP_MOVEMENT_TYPE.STRAFE);
    }
}
