using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestComboCounter : MonoBehaviour
{
    public TextMeshProUGUI TextField;

    public void OnUpdateText(int _value)
    {
        TextField.text = "Combo Count: " + _value;
    }
}
