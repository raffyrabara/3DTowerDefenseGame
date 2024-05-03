using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint 
{
   public GameObject prefab;
   public int cost;

   public float range1;

   public GameObject upgradedPrefab;
   public int upgradeCost;
   public float range2;

   public GameObject maxPrefab;
   public int maxCost;
   public float range3;


   public int GetSellAmount1 ()
   {
      return cost / 2;
   }
    public int GetSellAmount2 ()
   {
      return upgradeCost / 2;
   }

    public int GetSellAmount3 ()
   {
      return maxCost / 2;
   }

   
   

   
}
