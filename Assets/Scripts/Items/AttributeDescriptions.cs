using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AttributeDescriptions : MonoBehaviour
{
    public TextMeshProUGUI number;
    public TextMeshProUGUI attributeName;
    // Start is called before the first frame update
    
    public void TextParameters(string _num, string _name)
    {
        number.text = _num;
        attributeName.text = _name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
