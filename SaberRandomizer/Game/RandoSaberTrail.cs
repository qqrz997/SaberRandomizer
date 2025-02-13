using SaberRandomizer.Models;
using SaberRandomizer.Utilities;
using UnityEngine;

namespace SaberRandomizer.Game;

internal class RandoSaberTrail : SaberTrail
{
    private readonly SaberMovementData customTrailMovementData = new();

    private Transform trailTop = null!;
    private Transform trailBottom = null!;
    private ITrailData trailData = null!;
    private bool didInit;
    
    public void Init(
        Transform trailTop,
        Transform trailBottom,
        ITrailData trailData)
    {
        this.trailTop = trailTop;
        this.trailBottom = trailBottom;
        this.trailData = trailData;
        
        gameObject.layer = 12;
        didInit = true;
    }
    
    public void SetColor(Color color)
    {
        _color = (trailData.UseCustomColor ? trailData.CustomColor : color) * trailData.ColorMultiplier;
        
        foreach (var trailMaterial in _trailRenderer._meshRenderer.materials)
        {
            trailMaterial.SetColor(MaterialProperties.Color, _color);
        }
    }
    
    private new void Awake()
    {
        _movementData = customTrailMovementData;
    }

    private void Update()
    {
        if (!gameObject.activeInHierarchy || !didInit) return;

        var topPos = trailTop.position;
        var bottomPos = trailBottom.position;
        
        customTrailMovementData.AddNewData(topPos, bottomPos, TimeHelper.time);
    }
}