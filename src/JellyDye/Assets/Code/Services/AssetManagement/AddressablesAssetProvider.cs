﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Code.StaticData.Token;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Code.Services.AssetManagement
{
  public class AddressablesAssetProvider : IAssetProvider
  {
    private Dictionary<string, AsyncOperationHandle> _completedCache;

    private readonly StaticDataService _staticDataService;
    private Func<CCDTokenConfig, bool> _ccdTokensPredicate;

    public AddressablesAssetProvider(StaticDataService staticDataService) =>
      _staticDataService = staticDataService;

    public void Initialize()
    {
      int levelPrefabsCount = _staticDataService.ForLevels().LevelConfigs.Length;
      int jellyMeshesCount = _staticDataService.ForLevels().LevelConfigs.Sum(x => x.JellyMeshConfigs.Count);
      int skinsCount = _staticDataService.ForSkins().SkinConfigs.Length;
      int assetsCount = levelPrefabsCount + jellyMeshesCount + skinsCount + 10;
      _completedCache = new(assetsCount);
      Addressables.InitializeAsync();
      CCDTokensStaticData ccdTokensStaticData = _staticDataService.ForCCDTokens();
      _ccdTokensPredicate = config => config.ProfileName == ccdTokensStaticData.ActiveProfileName;
      if (default != ccdTokensStaticData.Configs.FirstOrDefault(_ccdTokensPredicate))
        Addressables.WebRequestOverride += AddressablesWebRequestOverride;
    }

    public async UniTask<T> Load<T>(AssetReference assetReference) where T : Object =>
      await Load<T>(ConvertAssetReferenceToStringKey(assetReference));

    public async UniTask<T> Load<T>(string key) where T : Object
    {
      if (_completedCache.TryGetValue(key, out AsyncOperationHandle completedHandle))
        return await completedHandle.Convert<T>();

      AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
      _completedCache[key] = handle;

      return await handle;
    }

    public UniTask<GameObject> Instantiate(string address, Vector3 at) =>
      Addressables.InstantiateAsync(address, at, Quaternion.identity).ToUniTask();

    public UniTask<GameObject> Instantiate(string address, Transform under = null) =>
      Addressables.InstantiateAsync(address, under).ToUniTask();

    public void Cleanup()
    {
      foreach (AsyncOperationHandle handle in _completedCache.Values)
        Addressables.Release(handle);

      _completedCache.Clear();
    }

    public AsyncOperationHandle WarmUpAsset<T>(AssetReference assetReference) where T : Object =>
      WarmUpAsset<T>(ConvertAssetReferenceToStringKey(assetReference));

    public AsyncOperationHandle WarmUpAsset<T>(string key) where T : Object
    {
      Load<T>(key).Forget();
      return _completedCache[key];
    }

    private void AddressablesWebRequestOverride(UnityWebRequest overrideWebRequest)
    {
      CCDTokensStaticData ccdTokensStaticData = _staticDataService.ForCCDTokens();
      string token = ccdTokensStaticData.Configs.First(_ccdTokensPredicate).Token;
      string authorization = "Basic " + Authenticate("", token);
      overrideWebRequest.SetRequestHeader("Authorization", authorization);
    }

    private string Authenticate(string username, string password)
    {
      string authenticate = username + ":" + password;
      return Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(authenticate));
    }

    private string ConvertAssetReferenceToStringKey(AssetReference assetReference) =>
      (string)assetReference.RuntimeKey;
  }
}