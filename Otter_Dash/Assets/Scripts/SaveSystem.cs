using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private const int MAX_AMMOUNT = 5; // max amount of files we can save in the game. Older files are deleted

    public static void savePlayer(PlayerAccount playerAccount)
    {

        BinaryFormatter formatter = new BinaryFormatter(); // used to turn playerdata to byte and viceversa
        string savePath = Application.persistentDataPath  +"/"+ playerAccount.playerAccountName + ".otter";

        DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath); //get directory info
        
        FileInfo[] fileListInfo = directoryInfo.GetFiles(
            "*.otter").OrderByDescending(p => p.LastAccessTime).ToArray(); // get all files in the directory
        FileStream stream;
        Debug.Log(fileListInfo.Length);

        if (fileListInfo.Length >= MAX_AMMOUNT) // if we have MAX_AMMOUNT saved files
        {
            
                if (!File.Exists(savePath)) // we check if we can override a file
                                                                // instead of creating a new file
                {
                    File.Delete(Application.persistentDataPath + 
                                "/" + fileListInfo[fileListInfo.Length-1].Name); //delete the oldest file
                }
        }
            
        stream = new FileStream(savePath, FileMode.Create); // override or create a new file
        PlayerData dataToSave = new PlayerData(playerAccount);
        formatter.Serialize(stream, dataToSave); // turn dataToSave to byte
        stream.Close();
    }

    public static PlayerData loadPlayer(string playerAccountName)
    {
        string loadpath = Application.persistentDataPath + "/" + playerAccountName + ".otter";
        PlayerData returnVal;

        if (File.Exists(loadpath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(loadpath, FileMode.Open);
            PlayerData playerData = formatter.Deserialize(stream) as PlayerData; // file back to player data
            stream.Close();
            returnVal = playerData;
        }
        else
        {
            Debug.Log("Specified file not found " +loadpath);
            returnVal = null;
        }

        return returnVal;
    }
    
}
