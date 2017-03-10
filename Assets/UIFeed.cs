using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFeed : MonoBehaviour {
    public Text selectedUnitsTextField;
    public Text currentObjectiveTextField;

    public UnitSelector unitSelector;
    
    void Update () {
        List<Unit> selectedUnits = unitSelector.selectedUnits;

        string unitRoster = "";
        string objective = "";
        foreach (Unit u in selectedUnits)
        {
            unitRoster += "- " + u.name + "\n";

            GameObject wp = u.GetComponent<WaypointMovement>().currentWaypoint;
            if (wp != null)
            {
                objective = "SECURE AREA:\n" + wp.transform.position.ToString();
            }
        }
        selectedUnitsTextField.text = unitRoster;
        currentObjectiveTextField.text = objective;
    }
}
