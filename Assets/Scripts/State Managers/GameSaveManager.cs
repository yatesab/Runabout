using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class GameSaveManager
{
    public static void CreateNewPlayerData(Vector3 spawnLoaction)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(spawnLoaction, new Quaternion(0,0,0,0));

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SavePlayerData(Transform player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player.position, player.rotation);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/playerData";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }

    }

    public static void CreateNewShipData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/shipData";
        FileStream stream = new FileStream(path, FileMode.Create);

        ShipData data = new ShipData(new Vector3(0,0,0), new Quaternion(0,0,0,0));

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveShipData(Transform ship)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/shipData";
        FileStream stream = new FileStream(path, FileMode.Create);

        ShipData data = new ShipData(ship.position, ship.rotation);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ShipData LoadShipData()
    {
        string path = Application.persistentDataPath + "/shipData";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ShipData data = formatter.Deserialize(stream) as ShipData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }

    }
}
