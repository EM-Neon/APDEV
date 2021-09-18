using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class AssetBundleManager : MonoBehaviour
{

    public static AssetBundleManager assetInstance;
    public bool isStopped = false;
    public string BundlesRootPath
    {
        get
        {
#if UNITY_EDITOR
            return Application.streamingAssetsPath;
#elif UNITY_ANDROID
return Application.persistentDataPath;
#endif
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (assetInstance == null)
        {
            assetInstance = this;
        }
        else
        {
            Object.Destroy(gameObject);
        }
    }

    Dictionary<string, AssetBundle> LoadedBundles = new Dictionary<string, AssetBundle>();

    public AssetBundle LoadBundle (string bundleName)
    {
        if (LoadedBundles.ContainsKey(bundleName))
        {
            return LoadedBundles[bundleName];
        }

        AssetBundle ret = AssetBundle.LoadFromFile(Path.Combine(BundlesRootPath, bundleName));


        if(ret == null)
        {
            Debug.LogError($"Cannot load {bundleName} - does not exist");
        }
        else
        {
            LoadedBundles.Add(bundleName, ret);
        }
        return ret;
    }

    public T GetAsset<T>(string bundleName, string asset) where T : Object
    {
        T ret = null;

        AssetBundle bundle = LoadBundle(bundleName);

        if (bundle != null)
        {
            ret = bundle.LoadAsset<T>(asset);
        }

        return ret;
    }
}
