using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class DisplayUI : MonoBehaviour
{
    [SerializeField] PlayerStates playerStates;
    [SerializeField] TextMeshProUGUI UI_AmmoValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UI_AmmoValue.text = playerStates.getAmmo();
    }
}
