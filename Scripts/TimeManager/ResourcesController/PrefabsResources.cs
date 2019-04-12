using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewResources", menuName = "PrefabsResources")]
public class PrefabsResources : ScriptableObject
{

    public GameObject ProductProvider;
    public GameObject ProductionFieldUnit;
    public GameObject ProductionUnit;
    public GameObject Customer;

    public GameObject GoalItemView;
    public GameObject TimerRestrictionView;
    public GameObject CustomersRestrictionView;
    public GameObject CoinsView;
}
