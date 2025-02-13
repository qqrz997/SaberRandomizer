using System.Threading.Tasks;
using CustomSaber;
using IPA.Utilities.Async;
using SaberRandomizer.App;
using SaberRandomizer.Models;
using SaberRandomizer.Utilities;
using SiraUtil.Interfaces;
using UnityEngine;
using Zenject;
using static SaberRandomizer.Utilities.CustomSaberUtils;

namespace SaberRandomizer.Game;

internal class RandoSaberModelController : SaberModelController, IPreSaberModelInit, IColorable
{
    [Inject] private readonly SaberEventService saberEventService = null!;
    [Inject] private readonly TrailFactory trailFactory = null!;
    [Inject] private readonly Task<SaberInstanceSet> saberInstanceSet = null!;
    [Inject] private readonly GameplayCoreSceneSetupData gameplayCoreSceneSetupData = null!;
    [Inject] private readonly ColorManager colorManager = null!;

    private DefaultSaberColorer? defaultSaberColorer;
    
    private GameObject? saberInstance;
    private Material[] colorableMaterials = [];
    private RandoSaberTrail[] customTrailInstances = [];
    private Color color;

    public Color Color
    {
        get => color;
        set => SetColor(value);
    }
    
    public bool PreInit(Transform parent, Saber saber)
    {
        transform.SetParent(parent, false);
        UnityMainThreadTaskScheduler.Factory.StartNew(() => Init(saber.saberType));
        return false;
    }

    private async Task Init(SaberType saberType)
    {
        var saberSet = await saberInstanceSet;
        
        saberInstance = saberSet.SaberForType(saberType);
        saberInstance.SetLayerRecursively(12);
        saberInstance.transform.SetParent(transform, false);

        var trailData = saberSet.TrailForType(saberType);
        var trailIntensity = gameplayCoreSceneSetupData.playerSpecificSettings.saberTrailIntensity;
        customTrailInstances = trailFactory.AddTrailsTo(saberInstance, trailData, trailIntensity);
        
        var eventManager = saberInstance.GetComponentOrAdd<EventManager>();
        saberEventService.InitializeEventManager(eventManager, saberType);
        
        colorableMaterials = GetColorableSaberMaterials(saberInstance);

        defaultSaberColorer = saberInstance.GetComponentInChildren<DefaultSaberColorer>();
        
        SetColor(colorManager.ColorForSaberType(saberType));
    }

    private void SetColor(Color color)
    {
        this.color = color;
        
        foreach (var trail in customTrailInstances)
        {
            trail.SetColor(color with { a = gameplayCoreSceneSetupData.playerSpecificSettings.saberTrailIntensity });
        }

        foreach (var material in colorableMaterials)
        {
            material.SetColor(MaterialProperties.Color, color);
        }

        if (defaultSaberColorer != null)
        {
            defaultSaberColorer.SetColor(color);
        }
    }

    private void OnDestroy()
    {
        if (saberInstance != null)
        {
            Destroy(saberInstance);
        }
    }
}