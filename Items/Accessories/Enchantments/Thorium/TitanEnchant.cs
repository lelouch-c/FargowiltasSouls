using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class TitanEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return false;// ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }

        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titan Enchantment");
            Tooltip.SetDefault(
@"'Infused with primordial energy'
Any damage you take while at full HP is reduced by 90%
Briefly become invulnerable after striking an enemy
Critical strikes deal 10% more damage
Pressing the 'Encase' key will place you in an impenetrable shell
While encased, you can't use items or health potions, life regeneration is heavily reduced, and damage is nearly nullified
Leaving the shell will greatly lower your speed, damage reduction and damage briefly
Leaving the shell will prohibit the use of the shell again for 20 seconds
Your symphonic damage will empower all nearby allies with: Damage Reduction II");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 6;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //titanium
            player.GetModPlayer<FargoPlayer>(mod).TitaniumEffect();
            //crystal eye mask
            thoriumPlayer.critDamage += 0.1f;
            //abyssal shell
            thoriumPlayer.AbyssalShell = true;
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3DamageReduction = 2;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("TitanHeadgear"));
            recipe.AddIngredient(thorium.ItemType("TitanHelmet"));
            recipe.AddIngredient(thorium.ItemType("TitanMask"));
            recipe.AddIngredient(thorium.ItemType("TitanBreastplate"));
            recipe.AddIngredient(thorium.ItemType("TitanGreaves"));
            recipe.AddIngredient(null, "TitaniumEnchant");
            recipe.AddIngredient(thorium.ItemType("CrystalEyeMask"));
            recipe.AddIngredient(thorium.ItemType("AbyssalShell"));
            recipe.AddIngredient(thorium.ItemType("TunePlayerDamageReduction"));
            recipe.AddIngredient(thorium.ItemType("Executioner"));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
