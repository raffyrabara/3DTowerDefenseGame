using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

public TurretBlueprint ballisticTurret;
public TurretBlueprint cannonTurret; 
public TurretBlueprint magicTurret; 

BuildManager buildManager;

void Start ()
{
    buildManager = BuildManager.instance;
}

public void SelectBallisticTurret ()
{
    buildManager.SelectTurretToBuild(ballisticTurret);
}
public void SelectCannonTurret ()
{
    buildManager.SelectTurretToBuild(cannonTurret);
}
public void SelectMagicTurret ()
{
    buildManager.SelectTurretToBuild(magicTurret);
}




}
