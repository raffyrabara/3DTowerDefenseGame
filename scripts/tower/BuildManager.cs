using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

   

    void Awake ()
    {
        instance = this;
    }

    public GameObject buildEffect;
    public GameObject upgradebuildEffect;
     public GameObject sellEffect;

    public GameObject maxbuildEffect;

    private TurretBlueprint turretToBuild;
    private Node selectedNode;

 

    public NodeUI nodeUI;

    public bool CanBuild { get {return turretToBuild != null; }}
    public bool HasMoney { get {return PlayerStats.Money >= turretToBuild.cost; }}

  //  public GameObject GetTurretToBuild ()
 //   {
  //      return turretToBuild;
  //  }

    public void SelectNode (Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
   
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();

    
    }

    public void SelectTurretToBuild (TurretBlueprint turret)
    { 
        turretToBuild = turret;
        selectedNode = null;

        nodeUI.Hide();
        
     
    }

public TurretBlueprint GetTurretToBuild()
{
    return turretToBuild;
}

}
