using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerPanel : MonoBehaviour
{
    [Header("Heat Limits")]
    public float heatLimit = 60f;

    [Header("Transporter System")]
    [SerializeField] private TransporterSystem transporterSystem;
    [SerializeField] private Button transporterButton;
    [SerializeField] private Button transporterPatchButton;
    [SerializeField] private Button transporterFixButton;
    [SerializeField] private TMP_Text transporterPowerLevel;

    [Header("Propulsion System")]
    [SerializeField] private PropulsionSystem propulsionSystem;
    [SerializeField] private Button propulsionButton;
    [SerializeField] private Button propulsionPatchButton;
    [SerializeField] private Button propulsionFixButton;
    [SerializeField] private TMP_Text propulsionPowerLevel;

    [Header("Shields System")]
    [SerializeField] private ShieldSystem shieldSystem;
    [SerializeField] private Button shieldButton;
    [SerializeField] private Button shieldPatchButton;
    [SerializeField] private Button shieldFixButton;
    [SerializeField] private TMP_Text shieldPowerLevel;

    private ShipSystem patchInSystem;
    private float shieldPatchHeat = 0f;
    private float propulsionPatchHeat = 0f;
    private float transporterPatchHeat = 0f;

    // Start is called before the first frame update
    void Start()
    {
        transporterButton.onClick.AddListener(PatchInTransporterSystem);
        transporterPatchButton.onClick.AddListener(PatchToTransportSystem);

        propulsionButton.onClick.AddListener(PatchInPropulsionSystem);
        propulsionPatchButton.onClick.AddListener(PatchToPropulsionSystem);

        shieldButton.onClick.AddListener(PatchInShieldsSystem);
        shieldPatchButton.onClick.AddListener(PatchToShieldSystem);

        UpdateScreen();
    }

    // Update is called once per frame
    void Update()
    {
        HandleSystemTimer(transporterSystem, transporterPatchHeat);
        HandleSystemTimer(propulsionSystem, propulsionPatchHeat);
        HandleSystemTimer(shieldSystem, shieldPatchHeat);
    }

    public void UpdateScreen()
    {
        transporterPowerLevel.text = transporterSystem.PowerLevel.ToString();
        propulsionPowerLevel.text = propulsionSystem.PowerLevel.ToString();
        shieldPowerLevel.text = shieldSystem.PowerLevel.ToString();
    }
    
    private void HandleSystemTimer(ShipSystem shipSystem, float systemHeat)
    {
        if(shipSystem.isPatched)
        {
            systemHeat += Time.deltaTime;

            if(systemHeat > heatLimit)
            {
                shipSystem.RemovePatchToSystem();
                shipSystem.PatchedSystem.RemovePatchInSystem();

                UpdateScreen();
            }
        } else if (systemHeat > 0f)
        {
            systemHeat -= Time.deltaTime;
        }
    }

    private void HandlePatchInSystem(ShipSystem shipSystem, float systemHeat)
    {
        if(patchInSystem != null && !shipSystem.isPatched && systemHeat <= 0f) 
        {
            shipSystem.PatchToSystem();
            shipSystem.PatchedSystem = patchInSystem;

            patchInSystem.PatchInSystem();

            UpdateScreen();
        }        
    }

    private void PatchInTransporterSystem()
    {
        HandlePatchInSystem(transporterSystem, transporterPatchHeat);
    }

    private void PatchInPropulsionSystem()
    {
        HandlePatchInSystem(propulsionSystem, propulsionPatchHeat);
    }

    private void PatchInShieldsSystem()
    {
        HandlePatchInSystem(shieldSystem, shieldPatchHeat);
    }

    private void PatchToTransportSystem()
    {
        patchInSystem = transporterSystem;
    }

    private void PatchToPropulsionSystem()
    {
        patchInSystem = propulsionSystem;
    }

    private void PatchToShieldSystem()
    {
        patchInSystem = shieldSystem;
    }
}
