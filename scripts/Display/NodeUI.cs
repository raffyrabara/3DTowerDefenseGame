using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeUI : MonoBehaviour
{
    private Node target;

    public GameObject ui;
    public TMP_Text upgradeCost;
    public TMP_Text sellAmount;
    public GameObject maxbuttonDisable;
    public GameObject rangeIndicator;

    void Start()
    {
        maxbuttonDisable.SetActive(true);
    }    

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();
        if(target.isUpgraded2)
        {
            upgradeCost.text = target.turretBlueprint.maxCost.ToString();
            sellAmount.text = (target.turretBlueprint.upgradeCost/2).ToString();
            rangeIndicator.transform.localScale = new Vector3(target.turretBlueprint.range2 * 2, target.turretBlueprint.range2 * 2, target.turretBlueprint.range2 * 2);
        }
        else if(target.isMaxUpgraded)
        {
            maxbuttonDisable.SetActive(false);
            sellAmount.text = (target.turretBlueprint.maxCost/2).ToString();
            rangeIndicator.transform.localScale = new Vector3(target.turretBlueprint.range3 * 2, target.turretBlueprint.range3 * 2, target.turretBlueprint.range3 * 2);
        }
        else if(target.isUpgraded1)
        {
            upgradeCost.text = target.turretBlueprint.upgradeCost.ToString();
            sellAmount.text = (target.turretBlueprint.cost/2).ToString();
            rangeIndicator.transform.localScale = new Vector3(target.turretBlueprint.range1 * 2, target.turretBlueprint.range1 * 2, target.turretBlueprint.range1 * 2);
        }

        

        ui.SetActive(true);
        rangeIndicator.SetActive(true);
        
    }

    public void Hide ()
    {
        ui.SetActive(false);
        maxbuttonDisable.SetActive(true);
        rangeIndicator.SetActive(false);

    }

    public void Upgrade ()
    {

        target.UpgradeTurret1();
        BuildManager.instance.DeselectNode();
    }

    public void Sell ()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}

