﻿using UnityEngine;
using System.Collections;


public class AssetLoadUtil
{
    public static readonly string PathURL =
#if UNITY_ANDROID 
        "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE 
        Application.dataPath + "/Raw/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR 
        "file://" + Application.dataPath + "/StreamingAssets/";
#else 
        string.Empty;
#endif
    /// <summary>
    /// 同步-加载asset资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetPath">全路径</param>
    /// <param name="objName"></param>
    /// <param name="callBack"></param>
    /// <returns></returns>
    public static T SyncLoadAsset<T>(string assetPath, string objName) where T : Object
    {
        assetPath = assetPath.ToLower();
        AssetBundle assetBundle = AssetBundle.LoadFromFile(assetPath);
        T data = assetBundle.LoadAsset<T>(objName);
        assetBundle.Unload(false);
        return data;
    }

    /// <summary>
    /// 同步-加载asset资源 从本地StreamingAssets目录下加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetPath"> ex:Texture/btn_icon </param>
    /// <param name="objName"></param>
    /// <returns></returns>
    public static T SyncLoadAssetFromLocalPath<T>(string assetPath, string objName) where T : Object
    {
        string localPath = Application.dataPath + "/StreamingAssets/";
        return SyncLoadAsset<T>(localPath + assetPath, objName);
    }

    /// <summary>
    /// 同步-加载asset资源 TextAsset类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetPath"></param>
    /// <param name="objName"></param>
    /// <param name="image"></param>
    public static TextAsset SyncLoadAssetToBytes(string assetPath, string objName)
    {
        TextAsset textAsset = SyncLoadAsset<TextAsset>(assetPath, objName);
        return textAsset;
    }

    /// <summary>
    /// 同步-加载asset资源 TextAsset类型 从本地StreamingAssets目录下加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetPath"></param>
    /// <param name="objName"></param>
    /// <param name="image"></param>
    public static TextAsset SyncLoadAssetToBytesFromLocalPath(string assetPath, string objName)
    {
        TextAsset textAsset = SyncLoadAssetFromLocalPath<TextAsset>(assetPath, objName);
        return textAsset;
    }

    /// <summary>
    /// 同步-加载asset TextAsset 资源并设置图片
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetPath"></param>
    /// <param name="objName"></param>
    /// <param name="image"></param>
    public static Texture2D SyncLoadAssetToBytesForTexture2D(string assetPath, string objName)
    {
        TextAsset textAsset = SyncLoadAssetToBytes(assetPath, objName);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(textAsset.bytes);
        return texture;
    }

    /// <summary>
    /// 同步-加载asset TextAsset 资源并设置图片 从本地StreamingAssets目录下加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetPath"></param>
    /// <param name="objName"></param>
    /// <param name="image"></param>
    public static Texture2D SyncLoadAssetToBytesForTexture2DFromLocalPath(string assetPath, string objName)
    {
        TextAsset textAsset = SyncLoadAssetToBytesFromLocalPath(assetPath, objName);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(textAsset.bytes);
        return texture;
    }

    /// <summary>
    /// 异步加载asset资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetPath"></param>
    /// <param name="objName"></param>
    /// <param name="callBack"></param>
    /// <returns></returns>
    public static IEnumerator AsyncLoadAsset<T>(string assetPath, string objName, ILoadCallBack<T> callBack) where T : UnityEngine.Object
    {
        assetPath = PathURL + assetPath;
        AssetBundleCreateRequest assetRequest = AssetBundle.LoadFromFileAsync(assetPath);
        yield return assetRequest;
        if (assetRequest == null && callBack != null)
            callBack.LoadFail("加载失败：指定assetPath下没有该资源");
        AssetBundleRequest objRequest = assetRequest.assetBundle.LoadAssetAsync<T>(objName);
        yield return objRequest;
        assetRequest.assetBundle.Unload(false);
        if (objRequest == null && callBack != null)
            callBack.LoadFail("加载失败：指定assetPath下没有该名字的obj");
        T obj = objRequest.asset as T;
        if (obj != null && callBack != null)
            callBack.LoadSuccess(obj);
    }

    /// <summary>
    /// 异步加载aaset资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetPath"></param>
    /// <param name="objName"></param>
    /// <param name="callBackSuccess"></param>
    /// <param name="callBackFail"></param>
    /// <returns></returns>
    public static IEnumerator AsyncLoadAsset<T>(string assetPath, string objName,System.Action<T> callBackSuccess, System.Action<string> callBackFail = null) where T : Object
    {
        assetPath = PathURL + assetPath;
        AssetBundleCreateRequest assetRequest = AssetBundle.LoadFromFileAsync(assetPath);
        yield return assetRequest;
        if (assetRequest == null)
        {
            callBackFail?.Invoke("加载失败：" + assetPath + "路径不存在");
        }
        else
        {
            AssetBundleRequest objRequest = assetRequest.assetBundle.LoadAssetAsync<T>(objName);
            yield return objRequest;
            if (objRequest == null)
            {
                callBackFail?.Invoke("加载失败：" + assetPath + "中没有名字为" + objName + "的资源");
            }
            else
            {
                T asset = objRequest.asset as T;
                callBackSuccess?.Invoke(asset);
            }
        }
    }
}