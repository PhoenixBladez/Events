using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Events;

namespace Events.NPCs.Storm.Elemental
{
	public class VoltaicElemental : ModNPC
	{
		int moveSpeed = 0;
		int moveSpeedY = 0;
		float HomeY = 120f;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Voltaic Elemental");
			Main.npcFrameCount[npc.type] = 1;
		}

		public override void SetDefaults()
		{
			npc.width = 50;
			npc.height = 46;
			npc.damage = 70;
			npc.defense = 20;
			npc.noTileCollide = false;
			npc.lifeMax = 265;
			npc.HitSound = SoundID.NPCHit41;
			banner = npc.type;
			bannerItem = mod.ItemType("VoltaicElementalBanner");
			npc.DeathSound = SoundID.NPCDeath43;
			npc.aiStyle = 22;
			aiType = NPCID.Wraith;
			npc.value = 1060f;
			npc.knockBackResist = .16f;
			npc.noGravity = true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
            var effects = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame,
                             drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            EventsUtility.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/Storm/Elemental/VoltaicElemental_Glow"));
        }
		
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.Next(3) == 0)
			{
				target.AddBuff(BuffID.Electrified, 240, true);
			}
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				npc.position.X = npc.position.X + (float)(npc.width / 2);
				npc.position.Y = npc.position.Y + (float)(npc.height / 2);
				npc.width = 30;
				npc.height = 30;
				npc.position.X = npc.position.X - (float)(npc.width / 2);
				npc.position.Y = npc.position.Y - (float)(npc.height / 2);
				for (int num621 = 0; num621 < 10; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 226, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 1f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
					int num623 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 109, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num623].velocity *= 1f;
					Main.dust[num623].scale = 0.5f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num623].scale = 0.5f;
						Main.dust[num623].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
			}
		}
		int shoottimer;
		float shootscale;
		public override bool PreAI()
		{
			bool expertMode = Main.expertMode;
			shoottimer++;
			{
				if(shoottimer == 240 || shoottimer == 250 | shoottimer >= 260)
				{
					Main.PlaySound(2, (int)npc.Center.X, (int)npc.Center.Y, 122);
					Vector2 direction = Main.player[npc.target].Center - npc.Center;
						direction.Normalize();
						direction.X *= 12f;
						direction.Y *= 12f;

						int amountOfProjectiles = 1;
						for (int i = 0; i < amountOfProjectiles; ++i)
						{
							float A = (float)Main.rand.Next(-50, 50) * 0.02f;
							float B = (float)Main.rand.Next(-50, 50) * 0.02f;
							int somedamage = expertMode ? 22 : 35;
							int p = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, mod.ProjectileType("Volt"), somedamage, 1, Main.myPlayer, 0, 0);
							Main.projectile[p].hostile = true;
						}
						if (shoottimer >= 260)
						{
						shoottimer = 0;
						}
				}
			}
			for (int index1 = 0; index1 < 6; ++index1)
            {
				float x = (npc.Center.X - 4);
				float xnum2 = (npc.Center.X +4);
				float y = (npc.Center.Y + 6);
				if (npc.direction == -1)
				{
					int index2 = Dust.NewDust(new Vector2(x, y), 1, 1, 226, 0.0f, 0.0f, 0, new Color(), 1f);
					Main.dust[index2].position.X = x;
					Main.dust[index2].position.Y = y;
					Main.dust[index2].scale = .5f;
					Main.dust[index2].velocity *= 0f;
					Main.dust[index2].noGravity = true;
					Main.dust[index2].noLight = false;
				}
				else if (npc.direction == 1)
				{
					int index2 = Dust.NewDust(new Vector2(xnum2, y), 1, 1, 226, 0.0f, 0.0f, 0, new Color(), 1f);
					Main.dust[index2].position.X = xnum2;
					Main.dust[index2].position.Y = y;
					Main.dust[index2].scale = .5f;
					Main.dust[index2].velocity *= 0f;
					Main.dust[index2].noGravity = true;
					Main.dust[index2].noLight = false;
				}
            }		
			Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), .25f, .57f, .85f);
			npc.spriteDirection = npc.direction;
			return true;
		}
		public override void NPCLoot()
		{
			if (Main.rand.Next(6) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.CrystalShard, 1);
			}
			if (Main.rand.Next(12) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoltStaff"), 1);
			}
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && MyWorld.activeEvents.Contains(EventID.Lightning) && Main.hardMode && Main.raining && !spawnInfo.playerSafe && !spawnInfo.invasion && !spawnInfo.sky && !Main.eclipse ? 0.01f : 0f;
		}
	}
}
