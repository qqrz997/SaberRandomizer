using System.Threading.Tasks;
using SabersCore.Components;
using SabersCore.Models;
using SabersCore.Services;
using SiraUtil.Interfaces;
using UnityEngine;
using Zenject;

namespace SaberRandomizer.Game;

internal class RandoSaberModelController : SaberModelController, IColorable, IPreSaberModelInit
{
    [Inject] private readonly Task<SaberInstanceSet> saberSet = null!;
    [Inject] private readonly ITrailFactory trailFactory = null!;
    [Inject] private readonly ICustomSaberEventManagerHandler eventManagerHandler = null!;
    [Inject] private readonly ColorManager colorManager = null!;
    [Inject] private readonly GameplayCoreSceneSetupData gameplayCoreSceneSetupData = null!;
    
    private ISaber? saberInstance;
    private CustomSaberTrail[] customTrailInstances = [];
    private Color color;

    public Color Color
    {
        get => color;
        set => SetColor(value);
    }

    public bool PreInit(Transform parent, Saber saber)
    {
        transform.SetParent(parent, false);
        transform.position = parent.position;
        transform.rotation = parent.rotation;
        
        CustomSaberInit(saber);
        return false;
    }

    private async void CustomSaberInit(Saber saber)
    {
        var sabers = await saberSet;
        saberInstance = sabers.GetSaberForType(saber.saberType);
        
        if (saberInstance is null)
        {
            return;
        }

        saberInstance.SetParent(transform);
        
        eventManagerHandler.InitializeEventManager(saberInstance.GameObject, saber.saberType);

        customTrailInstances = trailFactory.AddTrailsTo(
            saberInstance,
            sabers.GetTrailsForType(saber.saberType),
            gameplayCoreSceneSetupData.playerSpecificSettings.saberTrailIntensity);
        
        SetColor(colorManager.ColorForSaberType(saber.saberType));
    }

    public void SetColor(Color color)
    {
        this.color = color;
        saberInstance?.SetColor(color);
        foreach (var trail in customTrailInstances)
        {
            trail.SetColor(color);
        }
    }
}
