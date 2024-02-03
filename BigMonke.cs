using MelonLoader;
using BTD_Mod_Helper;
using BigMonke;
using HarmonyLib;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Models.Towers;
using BTD_Mod_Helper.Api.ModOptions;
using Il2CppAssets.Scripts.Models;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.GenericBehaviors;

[assembly: MelonInfo(typeof(BigMonke.BigMonke), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace BigMonke;

public class BigMonke : BloonsTD6Mod
{
    public static readonly ModSettingDouble TowerScale = new(2.5)
    {
        max = 15,
        min = 0,
        slider = true,
        stepSize = 0.1f,
        requiresRestart = true,
        description = "Changes the scale of towers"
    };

    public static readonly ModSettingBool IncreaseRange = new(false)
    {
        requiresRestart = true,
        description = "Multiply the range by the tower scale?"
    };
    public static readonly ModSettingBool IncreaseRadius = new(false)
    {
        requiresRestart = true,
        description = "Multiply the radius of the tower by the tower scale? (Makes it so that the tower takes up more space)"
    };

    public override void OnGameModelLoaded(GameModel model)
    {
        foreach(var tower in model.towers)
        {
            tower.displayScale = TowerScale;
            if(IncreaseRange)
            {
                tower.range *= TowerScale;
                foreach(var attack in tower.GetAttackModels())
                {
                    attack.range *= TowerScale;
                }
            }
            if(IncreaseRadius)
            {
                tower.radius *= TowerScale;
            }

            foreach(var weapon in tower.GetWeapons())
            {
                weapon.projectile.scale = TowerScale;
            }
        }
    }
}