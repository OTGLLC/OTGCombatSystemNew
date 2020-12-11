using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestComboCounter : MonoBehaviour
{
    public TextMeshProUGUI TextField;
    public string Prefix;
    public void OnUpdateText(int _value)
    {
        TextField.text = Prefix + _value;
    }
}
