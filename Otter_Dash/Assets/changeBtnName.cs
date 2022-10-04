using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class changeBtnName : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public int buttonIndex;
    private static DirectoryInfo directoryInfo;
    private FileInfo[] fileListInfo ;


    // Start is called before the first frame update
    void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        if (directoryInfo == null || fileListInfo == null)
        {
            directoryInfo = new DirectoryInfo(Application.persistentDataPath); //get directory info 
            fileListInfo = directoryInfo.GetFiles(
                "*.otter").OrderByDescending(p => p.LastAccessTime).ToArray(); // list of saved files
        }
        
        updateButtonsName(buttonIndex);
    }

    public void updateButtonsName(int buttonIndex)
    {
        if (buttonIndex < fileListInfo.Length)
        {
            buttonText.text = fileListInfo[buttonIndex].Name.Split("."[0])[0];
            tag = "occupied";
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBtnName()
    {
        
    }
}
