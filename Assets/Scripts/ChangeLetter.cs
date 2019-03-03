using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLetter : MonoBehaviour
{
    bool isSelected = false;
    int charId, maxCharId;
    bool hasChangedRecently = false;
    Text childText;
    
    // Start is called before the first frame update
    void Start()
    {
        charId = 'A';
        maxCharId = 'Z';
        childText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        var axis = Input.GetAxis("Vertical");
        if (isSelected)
        {
            if (axis > 0.01 && !hasChangedRecently)
            {
                charId++;
                if (charId>'Z')
                {
                    charId = 'A';
                }
                childText.text = ((char)charId).ToString();
                hasChangedRecently = true;
            }
            else if (axis < -0.01 && !hasChangedRecently)
            {
                charId--;
                if (charId < 'A')
                {
                    charId = 'Z';
                }
                childText.text = ((char)charId).ToString();
                hasChangedRecently = true;
            }
            if (Mathf.Abs(axis) < 0.0001f)
            {
                hasChangedRecently = false;
            }
        }
    }

    public void Select()
    {
        isSelected = true;
    }

    public void Deselect()
    {
        isSelected = false;
    }
}
