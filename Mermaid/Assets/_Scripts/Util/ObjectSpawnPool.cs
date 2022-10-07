using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
// using Photon.Pun;
// using Photon.Realtime;

public class ObjectSpawnPool : MonoBehaviour
{
    public Transform ReParentTranform;
    public Object[] ObjectList;
    public int PreloadCount = 0;

    Dictionary<string, Object> _objectPool = new Dictionary<string, Object>();
    Dictionary<string, List<GameObject>> _objectDic = new Dictionary<string, List<GameObject>>();

    Dictionary<GameObject, bool> _spawnList = new Dictionary<GameObject, bool>();

    Dictionary<GameObject, Tween> _spawnDelayRemoveCoroutine = new Dictionary<GameObject, Tween>();

    public int SpawnCount
    {
        get
        {
            return _spawnList.Count;
        }
    }

    public List<GameObject> SpawnList { get => new List<GameObject>(_spawnList.Keys); }

    private void Awake()
    {
        try
        {
            if (null != ObjectList)
            {
                for (int i = 0; i < ObjectList.Length; i++)
                {
                    if (null == ObjectList[i])
                    {
                        continue;
                    }

                    AddObject(ObjectList[i].name, ObjectList[i]);
                    if (0 < PreloadCount)
                    {
                        //PreSpawn(ObjectList[i].name, PreloadCount);
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            LogManager.LogHexa("ObjectSpawnPool:" + name + "/" + e);
        }
    }

    public bool ContainsObject(string objname)
    {
        return _objectPool.ContainsKey(objname);
    }

    public void AddObject(string objname, string objPath)
    {
        if (false == _objectPool.ContainsKey(objname))
        {
            // var obj = ResourceLoad.GetObject(objPath + objname);
            // if (null == obj)
            // {
            //     return;
            // }

            //AddObject(objname, obj);
        }
    }

    public bool AddObject(string objname, Object obj)
    {
        if (null == obj)
        {
            LogManager.LogHexa("AddObject:" + objname);
            return false;
        }

        if (false == _objectPool.ContainsKey(objname))
        {
            _objectPool.Add(objname, obj);
        }

        return true;
    }

    // public void PreSpawn(string objName, int count)
    // {
    //     List<GameObject> objectList = null;
    //     if (false == _objectDic.TryGetValue(objName, out objectList))
    //     {
    //         objectList = new List<GameObject>();
    //         _objectDic.Add(objName, objectList);
    //     }

    //     GameObject spawnObj = null;
    //     Object obj;

    //     if (true == _objectPool.TryGetValue(objName, out obj))
    //     {
    //         for (int i = 0; i < count; i++)
    //         {
    //             //spawnObj = Instantiate(obj) as GameObject;
    //             spawnObj = PhotonNetwork.Instantiate(obj.name, Vector3.zero, Quaternion.identity) as GameObject;//    Instantiate(obj) as GameObject;
    //             LogManager.LogHexa("프리스폰 = " + obj.name);
    //             if (spawnObj != null)
    //             {
    //                 objectList.Add(spawnObj);

    //                 if (null != ReParentTranform)
    //                 {
    //                     spawnObj.transform.SetParent(ReParentTranform, false);
    //                 }
    //                 else
    //                 {
    //                     spawnObj.transform.SetParent(transform, false);
    //                 }

    //                 spawnObj.SetActive(false);
    //             }
    //             else
    //             {
    //                 spawnObj = Instantiate(obj) as GameObject;
    //                 objectList.Add(spawnObj);

    //                 if (null != ReParentTranform)
    //                 {
    //                     spawnObj.transform.SetParent(ReParentTranform, false);
    //                 }
    //                 else
    //                 {
    //                     spawnObj.transform.SetParent(transform, false);
    //                 }

    //                 spawnObj.SetActive(false);
    //             }

    //         }
    //     }
    // }

    public GameObject Spawn()
    {
        if (0 == _objectPool.Count)
        {
            return null;
        }

        var itr = _objectPool.Keys.GetEnumerator();
        itr.MoveNext();

        return Spawn(itr.Current);
    }

    public T Spawn<T>(string objName)
    {
        try
        {
            return Spawn(objName).GetComponent<T>();
        }
        catch
        {
            return (T)System.Convert.ChangeType(null, typeof(T));
        }
    }

    public GameObject Spawn(string objName)
    {
        GameObject spawnObj = null;

        if (false == _objectPool.ContainsKey(objName))
        {
            return null;
        }

        List<GameObject> objectLsit = null;
        if (false == _objectDic.TryGetValue(objName, out objectLsit))
        {
            objectLsit = new List<GameObject>();
            _objectDic.Add(objName, objectLsit);
        }

        try
        {
            spawnObj = objectLsit.Find((GameObject obj) =>
                    {
                        return false == _spawnList.ContainsKey(obj) && (false == obj.activeSelf);
                    });
        }
        catch (System.Exception e)
        {
            Debug.Log("Spawn:" + objName + " e:" + e);
        }

        if (null == spawnObj)
        {
            Object obj;
            if (true == _objectPool.TryGetValue(objName, out obj))
            {
                // if (obj. == "PhotonObj")
                // {
                    
                // }
                spawnObj = Instantiate(obj) as GameObject;
                //spawnObj = PhotonNetwork.Instantiate(obj.name, Vector3.zero, Quaternion.identity) as GameObject;
                //LogManager.LogHexa("스폰 = " + obj.name);
                if (spawnObj != null)
                {
                    objectLsit.Add(spawnObj);

                    if (null != ReParentTranform)
                    {
                        spawnObj.transform.SetParent(ReParentTranform, false);
                    }
                    else
                    {
                        spawnObj.transform.SetParent(transform, false);
                    }
                }
                // else
                // {
                //     spawnObj = Instantiate(obj) as GameObject;
                //     objectLsit.Add(spawnObj);

                //     if (null != ReParentTranform)
                //     {
                //         spawnObj.transform.SetParent(ReParentTranform, false);
                //     }
                //     else
                //     {
                //         spawnObj.transform.SetParent(transform, false);
                //     }
                // }
            }
        }

        spawnObj.SetActive(true);
        _spawnList.Add(spawnObj, true);

        return spawnObj;
    }

    // public GameObject HexaSpawn(string objName, string route = "")
    // {
    //     GameObject spawnObj = null;

    //     if (false == _objectPool.ContainsKey(objName))
    //     {
    //         return null;
    //     }

    //     List<GameObject> objectLsit = null;
    //     if (false == _objectDic.TryGetValue(objName, out objectLsit))
    //     {
    //         objectLsit = new List<GameObject>();
    //         _objectDic.Add(objName, objectLsit);
    //     }

    //     try
    //     {
    //         spawnObj = objectLsit.Find((GameObject obj) =>
    //                 {
    //                     return false == _spawnList.ContainsKey(obj) && (false == obj.activeSelf);
    //                 });
    //     }
    //     catch (System.Exception e)
    //     {
    //         Debug.Log("Spawn:" + objName + " e:" + e);
    //     }

    //     if (null == spawnObj)
    //     {
    //         Object obj;
    //         if (true == _objectPool.TryGetValue(objName, out obj))
    //         {
    //             if (route == "")
    //             {
    //                 spawnObj = PhotonNetwork.Instantiate(obj.name, Vector3.zero, Quaternion.identity) as GameObject;
    //                 LogManager.LogHexa("헥사스폰 = " + obj.name);
    //             }
    //             else
    //             {
    //                 spawnObj = PhotonNetwork.Instantiate(route + obj.name, Vector3.zero, Quaternion.identity) as GameObject;
    //                 LogManager.LogHexa("발사체 스폰 = " + obj.name);
    //             }

                
    //             if (spawnObj != null)
    //             {
    //                 objectLsit.Add(spawnObj);

    //                 if (null != ReParentTranform)
    //                 {
    //                     spawnObj.transform.SetParent(ReParentTranform, false);
    //                 }
    //                 else
    //                 {
    //                     spawnObj.transform.SetParent(transform, false);
    //                 }
    //             }
    //             else
    //             {
    //                 spawnObj = Instantiate(obj) as GameObject;
    //                 objectLsit.Add(spawnObj);

    //                 if (null != ReParentTranform)
    //                 {
    //                     spawnObj.transform.SetParent(ReParentTranform, false);
    //                 }
    //                 else
    //                 {
    //                     spawnObj.transform.SetParent(transform, false);
    //                 }
    //             }
    //         }
    //     }

    //     spawnObj.SetActive(true);
    //     _spawnList.Add(spawnObj, true);

    //     return spawnObj;
    // }

    public bool Despawn(GameObject spawnObj)
    {
        if (null == spawnObj)
        {
            return false;
        }

        Tween tween = null;
        if (_spawnDelayRemoveCoroutine.TryGetValue(spawnObj, out tween))
        {
            if (null != tween)
            {
                tween.Kill();
            }

            _spawnDelayRemoveCoroutine.Remove(spawnObj);
        }
        if (true == _spawnList.Remove(spawnObj))
        {
            spawnObj.transform.SetParent(transform, false);
            spawnObj.transform.position = Vector3.zero;
            spawnObj.SetActive(false);

            return true;
        }

        return false;
    }

    public void Despawn(GameObject spawnObj, float delay)
    {
        if (0.0f == delay)
        {
            Despawn(spawnObj);
        }
        else
        {
            Tween tween = null;
            if (_spawnDelayRemoveCoroutine.TryGetValue(spawnObj, out tween))
            {
                if (null != tween)
                {
                    tween.Kill();
                }

                _spawnDelayRemoveCoroutine.Remove(spawnObj);
            }

            tween = DOVirtual.DelayedCall(delay, () => Despawn(spawnObj));

            _spawnDelayRemoveCoroutine.Add(spawnObj, tween);
        }


    }

    public void DespawnAll()
    {
        foreach (var spawnDelay in _spawnDelayRemoveCoroutine)
        {
            spawnDelay.Value.Kill();
        }
        _spawnDelayRemoveCoroutine.Clear();

        foreach (var obj in _spawnList)
        {
            obj.Key.SetActive(false);
        }

        _spawnList.Clear();
    }

    public void DestroyAll()
    {
        foreach (var spawnDelay in _spawnDelayRemoveCoroutine)
        {
            spawnDelay.Value.Kill();
        }
        _spawnDelayRemoveCoroutine.Clear();

        foreach (var obj in _objectDic)
        {
            for (int i = 0; i < obj.Value.Count; i++)
            {
                Destroy(obj.Value[i]);
            }
        }

        _objectDic.Clear();
        _spawnList.Clear();
    }

    public void DeactiveDestroyAll()
    {
        foreach (var obj in _objectDic)
        {
            List<GameObject> removeList = new List<GameObject>();
            for (int i = 0; i < obj.Value.Count; i++)
            {
                if (false == obj.Value[i].activeSelf)
                {
                    removeList.Add(obj.Value[i]);
                }
            }

            for (int i = 0; i < removeList.Count; i++)
            {
                obj.Value.Remove(removeList[i]);
                Destroy(removeList[i]);
            }
        }

        Dictionary<string, Object> removeObjList = new Dictionary<string, Object>();
        List<GameObject> objectList = null;
        foreach (var obj in _objectPool)
        {
            if (true == _objectDic.TryGetValue(obj.Key, out objectList))
            {
                if (0 == objectList.Count)
                {
                    removeObjList.Add(obj.Key, obj.Value);
                }
            }
        }

        foreach (var obj in removeObjList)
        {
            _objectPool.Remove(obj.Key);
        }

        //Resources.UnloadUnusedAssets();
    }
}
