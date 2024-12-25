using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{

    public GameObject interaction_Info_UI;
    Text interaction_text;

    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<Text>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            if (selectionTransform.GetComponent<InteractableObject>())
            {
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                interaction_Info_UI.SetActive(true);
            }
            else
            {
                interaction_Info_UI.SetActive(false);
            }

        }
    }
}