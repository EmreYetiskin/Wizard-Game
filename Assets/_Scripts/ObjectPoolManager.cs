using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    public static GameObject SpawnObject(GameObject objectToSpawn,Vector3 spawnPosition,Quaternion spawnRotation)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookUpString == objectToSpawn.name);

        //Eger pool yoksa, yenisini yapmak için
        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookUpString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        //Havuzda inactive obje kontrolu
        GameObject spawnableObj = null;
        foreach(GameObject obj in pool.InactiveObject)
        {
            if (obj != null)
            {
                spawnableObj = obj;
                break;
            }
        }

        if (spawnableObj == null)
        {
            //Eger inactive obje yoksa, yeni bir tane yap
            spawnableObj = Instantiate(objectToSpawn,spawnPosition,spawnRotation);
        }

        else
        {
            //Eger inactive obje varsa 
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation= spawnRotation;
            pool.InactiveObject.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj; 
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        //Objenin isminin son 7 harfini çýkararak , isimdeki (Clone) dan kurtuluyorum
        string goName = obj.name.Substring(0, obj.name.Length - 7);

        PooledObjectInfo pool = ObjectPools.Find(p => p.LookUpString == goName);

        if (pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObject.Add(obj);
        }
    }
}
public class PooledObjectInfo
{
    public string LookUpString;
    public List<GameObject> InactiveObject=new List<GameObject>();
}