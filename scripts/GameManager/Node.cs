using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    private Renderer rend;
    private Color startColor;
    public Vector3 positionOffset;

    public TMP_Text warningText; // Reference to your TextMeshPro text
    public GameObject warnText; // Reference to the GameObject containing the warning text
   //public GameObject warnToDisable;

    public TMP_Text CurrenywarningText; // Reference to TextMeshPro text
    public GameObject CurrencywarnObj; // Reference to the GameObject containing the warning text

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded1 = false;
    [HideInInspector]
    public bool isUpgraded2 = false;
    [HideInInspector]
    public bool isMaxUpgraded = false;

    private int currentUpgradeLevel = 0;

    BuildManager buildManager;

    void Start ()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
        
        // Hide the warning text initially
        HideWarningText();
    }

    public Vector3 GetBuildPosition ()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown ()
    {
        if (turret != null)
        {
            buildManager.SelectNode(this);
            Debug.Log("Can't build there!");
            return;
        }

        if (!buildManager.CanBuild){
            ShowWarningText("Please select a turret!");
           
            Debug.Log("Please select a turret");
            return;
        }
        
        // Build a turret
        BuildTurret(buildManager.GetTurretToBuild());
        buildManager.SelectTurretToBuild(null);

        
       
        HideWarningText(); // Hide the warning text after building
    }

     void BuildTurret (TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            ShowCurrencyWarningText("Not enough 'Bawang' to build!");
            Debug.Log("Not enough currency to build");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

         turretBlueprint = blueprint;


         Vector3 buildEffectPosition = GetBuildPosition();
    // Offset the build effect slightly above the node position
    buildEffectPosition += new Vector3(0f, 0.5f, 0f); // Adjust the Y value as needed

    // Instantiate the build effect with a rotation that faces upward
    GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, buildEffectPosition, Quaternion.Euler(-90, 0, 0));
    Destroy(effect, 3f);
        Debug.Log("Turret build! Money Left: " + PlayerStats.Money);

    isUpgraded1 = true;
   
    }

    public void UpgradeTurret1 ()
    {
       

        Vector3 buildEffectPosition = GetBuildPosition();
    // Offset the build effect slightly above the node position
    buildEffectPosition += new Vector3(0f, 0.5f, 0f); // Adjust the Y value as needed

    // Check if the turret is already at the max level
    if (isUpgraded2)
    {
        if (PlayerStats.Money < turretBlueprint.maxCost)
    {
        ShowCurrencyWarningText("Not enough 'Bawang' to upgrade!");
        Debug.Log("Not enough currency to upgrade");
        return;
    }

        PlayerStats.Money -= turretBlueprint.maxCost;
        // Handle max level upgrade
        if (turretBlueprint.maxPrefab != null)
        {
            // Get rid of the old turret
            Destroy(turret);

            // Build the max level turret
            GameObject maxTurret = (GameObject)Instantiate(turretBlueprint.maxPrefab, GetBuildPosition(), Quaternion.identity);
            turret = maxTurret;

            //buildEffect
            GameObject effectMax = (GameObject)Instantiate(buildManager.maxbuildEffect, buildEffectPosition, Quaternion.Euler(-90, 0, 0));
            Destroy(effectMax, 3f);

            isUpgraded1 = false;
            isUpgraded2 = false;
            isMaxUpgraded = true;
            currentUpgradeLevel = 2;
            // Optionally set isUpgraded to true if you have a max level flag
            Debug.Log("Turret Upgraded to Max Level! Money Left: " + PlayerStats.Money);
        }
       
    }
    else
    {
           if (PlayerStats.Money < turretBlueprint.upgradeCost)
    {
        ShowCurrencyWarningText("Not enough 'Bawang' to upgrade!");
        Debug.Log("Not enough currency to upgrade");
        return;
    }

        PlayerStats.Money -= turretBlueprint.upgradeCost;
        // Upgrade to the next level
        Destroy(turret);
        GameObject upgradedTurret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = upgradedTurret;

        //buildEffect
        GameObject effect = (GameObject)Instantiate(buildManager.upgradebuildEffect, buildEffectPosition, Quaternion.Euler(-90, 0, 0));
        Destroy(effect, 3f);

        isMaxUpgraded = false;
        isUpgraded1 = false;
        isUpgraded2 = true;
        currentUpgradeLevel = 1;
        Debug.Log("Turret Upgraded to Level 2! Money Left: " + PlayerStats.Money);
    }
    }

//Selling of Turret
    public void SellTurret ()
    {
        Vector3 buildEffectPosition = GetBuildPosition();
    // Offset the build effect slightly above the node position
        buildEffectPosition += new Vector3(0f, 0.5f, 0f); 

       int sellAmount = 0;

        if (currentUpgradeLevel == 0)
        {
            sellAmount = turretBlueprint.GetSellAmount1();
            
        }
        else if (currentUpgradeLevel == 1)
        {
            sellAmount = turretBlueprint.GetSellAmount2();
        }
        else if (currentUpgradeLevel == 2)
        {
            sellAmount = turretBlueprint.GetSellAmount3();
        }

        GameObject sellEffect = (GameObject)Instantiate(buildManager.sellEffect, buildEffectPosition, Quaternion.Euler(-90, 0, 0));
        Destroy(sellEffect, 3f);

        PlayerStats.Money += sellAmount;
        Destroy(turret);
        turretBlueprint = null;
        isUpgraded1 = false;
        isUpgraded2 = false;
        isMaxUpgraded = false;
        currentUpgradeLevel = 0; // Reset the upgrade level
    }

    void OnMouseEnter ()
    {
        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit ()
    {
        rend.material.color = startColor;
    }

    // Function to show the warning text
    void ShowWarningText(string text)
    {
        warningText.text = text;
        warnText.SetActive(true);
        CurrencywarnObj.SetActive(false);
    }

    // Function to hide the warning text
    void HideWarningText()
    {
        warnText.SetActive(false);
    }

    void ShowCurrencyWarningText(string text)
{
    CurrenywarningText.text = text;
    CurrencywarnObj.SetActive(true);
    warnText.SetActive(false);
}

// Function to hide the warning text
void HideCurrencyWarningText()
{
    CurrencywarnObj.SetActive(false);
}


}
