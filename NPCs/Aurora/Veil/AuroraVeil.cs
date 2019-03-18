using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Events;

namespace Events.NPCs.Aurora.Veil
{
	public class AuroraVeil : ModNPC
	{
		int moveSpeed = 0;
		int moveSpeedY = 0;
		float HomeY = 120f;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aurora Veil");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.width = 20;
			npc.height = 64;
			npc.damage = 10;
			npc.defense = 0;
			npc.noTileCollide = true;
			npc.lifeMax = 55;
			banner = npc.type;
			bannerItem = mod.ItemType("HorrorBanner");
			npc.HitSound = SoundID.NPCHit19;
			npc.DeathSound = SoundID.NPCDeath22;
			npc.aiStyle = 22;
			aiType = NPCID.Wraith;
			npc.buffImmune[BuffID.Frostburn] = true;
			npc.value = 160f;
			npc.knockBackResist = .16f;
			npc.noGravity = true;
		}

		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter += 0.1f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life >= 0 || npc.life <= 0)
			{
				int d = 172;
				int d1 = 110;
				for (int k = 0; k < 20; k++)

				{
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
				}

				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
				}
				
		}
		int shoottimer;
		float shootscale;
		int counters;
		
		int name = 0;
		int amt = 1;
		public override void AI()
		{
			Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), 0.05f, 0.7f, 1f);
			{
                int dust = Dust.NewDust(npc.Center, npc.width, npc.height, 68);
				Main.dust[dust].velocity *= -1f;
				Main.dust[dust].noGravity = true;
				Vector2 vector2_1 = new Vector2((float) Main.rand.Next(-100, 101), (float) Main.rand.Next(-100, 101));
				vector2_1.Normalize();
				Vector2 vector2_2 = vector2_1 * ((float) Main.rand.Next(50, 100) * 0.04f);
				Main.dust[dust].velocity = vector2_2;
				vector2_2.Normalize();
				Vector2 vector2_3 = vector2_2 * 34f;
				Main.dust[dust].position = npc.Center - vector2_3;
			}
		}
		public override void NPCLoot()
		{
			if (Main.rand.Next(14) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AuroraOrb"));
			}
		}
		public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
		{
			if (item.melee)
			{
				name = mod.ProjectileType("AuroraCircle");
			}
			{
				Vector2 direction = Main.player[npc.target].Center - npc.Center;
				direction.Normalize();
				direction.X *= 2f;
				direction.Y *= 2f;

				int amountOfProjectiles = 1;
				for (int i = 0; i < amountOfProjectiles; ++i)
				{
					float A = (float)Main.rand.Next(-150, 150) * 0.03f;
					float B = (float)Main.rand.Next(-150, 150) * 0.03f;
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X, direction.Y, name, 4, 1, Main.myPlayer, 0, 0);
				}
				shoottimer = 0;
			}
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
						Player player = spawnInfo.player;
			return spawnInfo.spawnTileY < Main.rockLayer && MyWorld.activeEvents.Contains(EventID.aurora) && player.ZoneOverworldHeight && player.ZoneSnow && !spawnInfo.playerSafe && !spawnInfo.invasion && !spawnInfo.sky && !Main.eclipse && !NPC.AnyNPCs(mod.NPCType("AuroraVeil"))? 0.1f : 0f;
		}
		public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
		{
			if (projectile.magic == true || projectile.minion == true)
			{
				name = mod.ProjectileType("AuroraHoming");
				amt = 1;
			}
			else if (projectile.ranged = true || projectile.thrown == true)
			{
				name = mod.ProjectileType("AuroraBolt");
				amt = Main.rand.Next(1,3);
			}
			
			{
				Vector2 direction = Main.player[npc.target].Center - npc.Center;
				direction.Normalize();
				direction.X *= 2f;
				direction.Y *= 2f;

				int amountOfProjectiles = amt;
				for (int i = 0; i < amt; ++i)
				{
					float A = (float)Main.rand.Next(-15, 15) * 0.03f;
					float B = (float)Main.rand.Next(-15, 15) * 0.03f;
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B,name, npc.damage / 3 *2, 1, Main.myPlayer, 0, 0);
				}
				shoottimer = 0;
			}
		}
	}
}
