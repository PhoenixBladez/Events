using Terraria;
using Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.NPCs.AcidRain.Zombie
{

	public class HazmatZombie : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hazmat Zombie");
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Zombie];
		}

		public override void SetDefaults()
		{
			npc.width = 28;
			npc.height = 42;
			npc.damage = 40;
			npc.defense = 30;
			npc.lifeMax = 200;
			npc.HitSound = SoundID.NPCHit1;
			banner = npc.type;
			bannerItem = ItemID.ZombieBanner;
			npc.DeathSound = SoundID.NPCDeath2;
			npc.value = 460f;
			npc.buffImmune[mod.BuffType("Acid")] = true;
			npc.knockBackResist = 0.01f;
			npc.aiStyle = 3;
			aiType = NPCID.Zombie;
			animationType = NPCID.Zombie;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && MyWorld.activeEvents.Contains(EventID.acidRain) && !spawnInfo.playerSafe && !spawnInfo.invasion && !spawnInfo.sky && !Main.eclipse && !Main.dayTime ? 0.8f : 0f;
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			if(npc.life <= 0)
			{
			for (int i = 0; i < 10; i++)
			{
				int dustType = 193;
				int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
			}
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZombieHead"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZombieHand"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZombieHand"), 1f);
			}
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.Next(3) == 0)
			{
				target.AddBuff(BuffID.Poisoned, 600, true);
			}
		}
		public override void NPCLoot()
		{
			if (Main.rand.Next (15) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HazmatHelm"), 1);
			}
			if (Main.rand.Next (100) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Hornet"), 1);
			}
		}
		
	}
}
