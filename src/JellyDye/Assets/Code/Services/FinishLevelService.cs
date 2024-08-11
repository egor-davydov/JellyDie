﻿using Code.Gameplay.UI.FinishWindow;
using Code.Services.Factories.UI;
using Code.Services.Providers;
using DG.Tweening;
using UnityEngine;

namespace Code.Services
{
  public class FinishLevelService
  {
    private readonly PaintCountCalculationService _paintCountCalculationService;
    private readonly GreenButtonFactory _greenButtonFactory;
    private readonly CameraService _cameraService;
    private readonly ScreenshotService _screenshotService;
    private readonly WindowFactory _windowFactory;
    private readonly SyringeProvider _syringeProvider;
    private readonly HudProvider _hudProvider;

    private Texture2D _screenshotWithGround;
    private float? _countedPercentage;
    private FinishWindow _finishWindow;

    public bool CanFinish { get; private set; }

    public FinishLevelService(PaintCountCalculationService paintCountCalculationService,
      GreenButtonFactory greenButtonFactory, CameraService cameraService,
      ScreenshotService screenshotService, WindowFactory windowFactory,
      SyringeProvider syringeProvider, HudProvider hudProvider)
    {
      _paintCountCalculationService = paintCountCalculationService;
      _greenButtonFactory = greenButtonFactory;
      _cameraService = cameraService;
      _screenshotService = screenshotService;
      _windowFactory = windowFactory;
      _syringeProvider = syringeProvider;
      _hudProvider = hudProvider;
    }

    public void Initialize()
    {
      CanFinish = false;
      _countedPercentage = null;
      _finishWindow = null;
    }

    public void CheckPaint()
    {
      if (!_paintCountCalculationService.HasPaintOnAllMeshes())
        return;

      CanFinish = true;
      _greenButtonFactory.CreateFinishButton(_hudProvider.HudObject.transform);
    }

    public void FinishLevel()
    {
      _paintCountCalculationService.AsyncCalculatePaintPercentage((percentage)=>
      {
        if (_finishWindow == null)
          _countedPercentage = percentage;
        else
          _finishWindow.AnimatePercentageText(percentage);
      });
      Object.Destroy(_hudProvider.HudObject);
      Object.Destroy(_syringeProvider.SyringeObject);
      _cameraService.MoveToFinish().OnComplete(ShowPhotoFlash);
    }

    private void ShowPhotoFlash() =>
      _cameraService.ShowPhotoFlash(onFlashEnd: TakeScreenshot);

    private void TakeScreenshot() =>
      _screenshotService.TakeScreenshot(onTake: CreateFinishWindow);

    private void TakeScreenshotWithoutGround()
    {
      _screenshotWithGround = _screenshotService.ScreenshotTexture;
      _screenshotService.TakeScreenshot(onTake: CreateFinishWindow);
    }

    private async void CreateFinishWindow()
    {
      GameObject finishWindowObject = await _windowFactory.CreateFinishWindow();
      _finishWindow = finishWindowObject.GetComponent<FinishWindow>();
      _finishWindow.Initialize(_screenshotService.ScreenshotTexture);
      _finishWindow.AnimateWindowAppearance(() =>
      {
        if (_countedPercentage.HasValue)
          _finishWindow.AnimatePercentageText(_countedPercentage.Value);
      });
    }
  }
}