﻿using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.UI.MainMenu.Skins;
using Code.Services;
using Code.Services.Progress;
using Code.Services.Progress.SaveLoad;
using Code.StaticData.Skins;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.UI.FinishWindow
{
  public class SkinProgressBar : MonoBehaviour
  {
    private const Ease FillTweenEase = Ease.OutQuad;

    [SerializeField] private GameObject[] _objectsToDisableIfPlayerHaveAllSkins;
    [SerializeField] private Image _nextSkinImage;
    [SerializeField] private Image _progressImage;
    [SerializeField] private float _progressMoveTime = 1f;

    private ISaveLoadService _saveLoadService;
    private ProgressService _progressService;
    private StaticDataService _staticDataService;

    private float _finalFillAmount;
    private Tween _fillTween;
    private List<SkinType> _openedSkinTypes;
    private UnlockableSkinConfig _nextSkinConfig;
    private NewSkinSceneService _newSkinSceneService;
    private bool _isAllSkinsUnlockedOnStart;

    [Inject]
    public void Construct(ProgressService progressService, ISaveLoadService saveLoadService,
      StaticDataService staticDataService, NewSkinSceneService newSkinSceneService)
    {
      _newSkinSceneService = newSkinSceneService;
      _staticDataService = staticDataService;
      _progressService = progressService;
      _saveLoadService = saveLoadService;

      _openedSkinTypes = _progressService.Progress.SkinData.OpenedSkins;
    }

    private void Awake()
    {
      _isAllSkinsUnlockedOnStart = AllSkinsUnlocked();

      if (_isAllSkinsUnlockedOnStart)
        HideSkinObjects();
      else
      {
        SetNextSkinSprite();
        _progressImage.fillAmount = _progressService.Progress.SkinData.NextSkinProgress;
      }
    }

    private bool AllSkinsUnlocked()
    {
      return _staticDataService.ForSkins().UnlockableSkins
        .All(unlockableSkinConfig => !PlayerDontHaveSkin(unlockableSkinConfig.SkinType));
    }

    private void OnDestroy() =>
      _fillTween.Kill();

    public async UniTask IncreaseProgress(float increaseAmount)
    {
      if (increaseAmount == 0 || _isAllSkinsUnlockedOnStart)
        return;

      await PlayAnimations(increaseAmount);
    }

    private async UniTask PlayAnimations(float increaseAmount)
    {
      if (!ProgressImageWillBeFilled(increaseAmount))
      {
        _fillTween = _progressImage.DOFillAmount(_finalFillAmount, _progressMoveTime).SetEase(FillTweenEase);
      }
      else
      {
        float fillDuration = _isAllSkinsUnlockedOnStart
          ? _progressMoveTime
          : _progressMoveTime * ((1 - _progressImage.fillAmount) / increaseAmount);
        _fillTween = _progressImage.DOFillAmount(1, fillDuration)
          .SetEase(FillTweenEase);
        await _fillTween;

        await _newSkinSceneService.ShowSkinScene(_nextSkinConfig.SkinType);
        await _newSkinSceneService.HideSkinScene();
        if (!AllSkinsUnlocked())
        {
          SetNextSkinSprite();
          _progressImage.fillAmount = 0;
          float lastPartDuration = _progressMoveTime - fillDuration;
          _fillTween = _progressImage.DOFillAmount(_finalFillAmount, lastPartDuration).SetEase(FillTweenEase);
        }
        else
        {
          HideSkinObjects();
        }
      }
    }

    public void CalculateAndSetProgress(float increaseAmount)
    {
      if (increaseAmount == 0 || _isAllSkinsUnlockedOnStart)
        return;

      if (!ProgressImageWillBeFilled(increaseAmount))
      {
        _finalFillAmount = _progressImage.fillAmount + increaseAmount;
      }
      else
      {
        _finalFillAmount = _progressImage.fillAmount + increaseAmount - 1;
        _progressService.Progress.SkinData.OpenedSkins.Add(_nextSkinConfig.SkinType);
      }

      _progressService.Progress.SkinData.NextSkinProgress = _finalFillAmount;
    }

    private bool ProgressImageWillBeFilled(float increaseAmount)
    {
      return _progressImage.fillAmount + increaseAmount >= 1;
    }

    private UnlockableSkinConfig FindOutNextSkin()
    {
      foreach (UnlockableSkinConfig unlockableSkinConfig in _staticDataService.ForSkins().UnlockableSkins)
      {
        if (PlayerDontHaveSkin(unlockableSkinConfig.SkinType))
          return unlockableSkinConfig;
      }

      throw new Exception("Cant find next skin config");
    }

    private bool PlayerDontHaveSkin(SkinType observableSkinType) =>
      _openedSkinTypes.All(skinType => skinType != observableSkinType);

    private void HideSkinObjects()
    {
      foreach (GameObject objectToDisableIfPlayerHaveAllSkin in _objectsToDisableIfPlayerHaveAllSkins)
        objectToDisableIfPlayerHaveAllSkin.SetActive(false);
    }

    private void SetNextSkinSprite()
    {
      _nextSkinConfig = FindOutNextSkin();
      _nextSkinImage.sprite = _nextSkinConfig.Icon;
    }
  }
}