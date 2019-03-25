using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.NPCs.AcidRain.Nimbus
{
	public class SulfuricNimbus : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sulfuric Nimbus");
			Main.npcFrameCount[npc.type] = 9;
		}

		public override void SetDefaults()
		{
			npc.damage = 51;
			npc.width = 46; //324
			npc.height = 40; //216
			npc.buffImmune[mod.BuffType("Acid")] = true;
			npc.defense = 26;
			npc.lifeMax = 320;
			npc.knockBackResist = 0.4f;
			npc.noGravity = true;
			npc.value = Item.buyPrice(0, 0, 5, 0);
			banner = npc.type;
			bannerItem = mod.ItemType("NimbusBanner");
			npc.HitSound = SoundID.NPCHit30;
			npc.DeathSound = SoundID.NPCDeath22;
		}

		public override void AI()
		{
			npc.TargetClosest(true);
			float num1164 = 4f;
			float num1165 = 0.75f;
			Vector2 vector133 = new Vector2(npc.Center.X, npc.Center.Y);
			float num1166 = Main.player[npc.target].Center.X - vector133.X;
			float num1167 = Main.player[npc.target].Center.Y - vector133.Y - 200f;
			float num1168 = (float)Math.Sqrt((double)(num1166 * num1166 + num1167 * num1167));
			if (num1168 < 20f)
			{
				num1166 = npc.velocity.X;
				num1167 = npc.velocity.Y;
			}
			else
			{
				num1168 = num1164 / num1168;
				num1166 *= num1168;
				num1167 *= num1168;
			}
			if (npc.velocity.X < num1166)
			{
				npc.velocity.X = npc.velocity.X + num1165;
				if (npc.velocity.X < 0f && num1166 > 0f)
				{
					npc.velocity.X = npc.velocity.X + num1165 * 2f;
				}
			}
			else if (npc.velocity.X > num1166)
			{
				npc.velocity.X = npc.velocity.X - num1165;
				if (npc.velocity.X > 0f && num1166 < 0f)
				{
					npc.velocity.X = npc.velocity.X - num1165 * 2f;
				}
			}
			if (npc.velocity.Y < num1167)
			{
				npc.velocity.Y = npc.velocity.Y + num1165;
				if (npc.velocity.Y < 0f && num1167 > 0f)
				{
					npc.velocity.Y = npc.velocity.Y + num1165 * 2f;
				}
			}
			else if (npc.velocity.Y > num1167)
			{
				npc.velocity.Y = npc.velocity.Y - num1165;
				if (npc.velocity.Y > 0f && num1167 < 0f)
				{
					npc.velocity.Y = npc.velocity.Y - num1165 * 2f;
				}
			}
			if (npc.position.X + (float)npc.width > Main.player[npc.target].position.X && npc.position.X < Main.player[npc.target].position.X + (float)Main.player[npc.target].width && npc.position.Y + (float)npc.height < Main.player[npc.target].position.Y && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && Main.netMode != 1)
			{
				npc.ai[0] += 4f;
				if (npc.ai[0] > 44f)
				{
					npc.ai[0] = 0f;
					int num1169 = (int)(npc.position.X + 10f + (float)Main.rand.Next(npc.width - 20));
					int num1170 = (int)(npc.position.Y + (float)npc.height + 4f);
					int num184 = 26;
					if (Main.expertMode)
					{
						num184 = 14;
					}
					Projectile.NewProjectile((float)num1169, (float)num1170, 0f, 5f, mod.ProjectileType("AcidRain"), num184, 0f, Main.myPlayer, 0f, 0f);
					return;
				}
			}
			if (Main.rand.NextFloat() < 0.131579f)
				{
				Dust dust;
				Vector2 position = npc.Center;
					int d = Dust.NewDust(npc.position, npc.width, npc.height + 10, 193, 0, 1f, 0, Color.Green, 0.7f);
					Main.dust[d].velocity *= .1f;
				}
		}

		public override void HitEffect(int hitDirection, double damage)
        {
			{
				int d = 193;
				for (int k = 0; k < 5; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
				}

				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
			}
			if (npc.life <= 0)
			{
				int d = 193;
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
				}

				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Green, 0.7f);
								Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AcidEye"), 1f);	
			}
		}
		public override void NPCLoot()
		{
			if (Main.rand.Next (25) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Razoreye"), 1);
			}
			if (Main.rand.Next (100) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Hornet"), 1);
			}
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && MyWorld.activeEvents.Contains(EventID.acidRain) && !spawnInfo.playerSafe && !spawnInfo.invasion && !spawnInfo.sky && !Main.eclipse && !Main.bloodMoon ? 0.4f : 0f;
		}
		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter += 0.15f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
	}
}