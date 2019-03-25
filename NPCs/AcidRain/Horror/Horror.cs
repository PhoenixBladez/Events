using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Events;

namespace Events.NPCs.AcidRain.Horror
{
	public class Horror : ModNPC
	{
		int moveSpeed = 0;
		int moveSpeedY = 0;
		float HomeY = 120f;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nitric Horror");
			Main.npcFrameCount[npc.type] = 9;
		}

		public override void SetDefaults()
		{
			npc.width = 38;
			npc.height = 44;
			npc.damage = 60;
			npc.defense = 18;
			npc.noTileCollide = true;
			npc.lifeMax = 1600;
			banner = npc.type;
			bannerItem = mod.ItemType("HorrorBanner");
			npc.HitSound = SoundID.NPCHit19;
			npc.DeathSound = SoundID.NPCDeath22;
			npc.aiStyle = 22;
			aiType = NPCID.Wraith;
			npc.buffImmune[mod.BuffType("Acid")] = true;
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
		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter += 0.1f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
				if (Main.rand.Next(3) == 0)
			{
				target.AddBuff(BuffID.Poisoned, 600, true);
			}
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && MyWorld.activeEvents.Contains(EventID.acidRain) && !spawnInfo.playerSafe && !spawnInfo.invasion && !spawnInfo.sky && !Main.eclipse && !Main.bloodMoon && !NPC.AnyNPCs(mod.NPCType("Horror"))? 0.08f : 0f;
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
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				int d = 107;
				for (int k = 0; k < 20; k++)

				{
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				}

				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AcidEye"), 1f);	
				}
				
		}
		int shoottimer;
		float shootscale;
		int counters;
		public override bool PreAI()
		{

			Player player = Main.player[npc.target];
			shoottimer++;
			{
				if(shoottimer >= 400)
				{
					for (int k = 0; k < 20; k++)
					{
					Dust.NewDust(npc.position, npc.width, npc.height, 107, 2.5f * 1, -2.5f, 0, Color.White, 0.7f);
					Dust.NewDust(npc.position, npc.width, npc.height, 107, 2.5f * 1, -2.5f, 0, Color.White, 0.7f);
					}
					npc.position.X = player.position.X - Main.rand.Next(-500, 500); //Teleport in a corner of the screen
					npc.position.Y = player.position.Y + Main.rand.Next (-500, 500);
					for (int j = 0; j < 20; j++)
					{
					Dust.NewDust(npc.position, npc.width, npc.height, 107, 2.5f * 1, -2.5f, 0, Color.White, 0.7f);
					Dust.NewDust(npc.position, npc.width, npc.height, 107, 2.5f * 1, -2.5f, 0, Color.White, 0.7f);
					}
					shoottimer = 0;
					
				}
			}
			Player target = Main.player[npc.target];
			int distance = (int)Math.Sqrt((npc.Center.X - target.Center.X) * (npc.Center.X - target.Center.X) + (npc.Center.Y - target.Center.Y) * (npc.Center.Y - target.Center.Y));
			if (distance < 256)	
			{
				counters++;
				player.AddBuff(BuffID.Poisoned, 120); 
				player.lifeRegen -= 10;
				if (counters >= 60)
				{
					Vector2 direction = Main.player[npc.target].Center - npc.Center;
				direction.Normalize();
				direction.X *= 5f;
				direction.Y *= 5f;

				int amountOfProjectiles = 1;
				for (int i = 0; i < amountOfProjectiles; ++i)
				{
					float A = (float)Main.rand.Next(-150, 150) * 0.03f;
					float B = (float)Main.rand.Next(-150, 150) * 0.03f;
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X, direction.Y, mod.ProjectileType("HealingBolt"), 1, 1, Main.myPlayer, 0, 0);
				}
				counters = 0;
				}
			}				
			npc.spriteDirection = npc.direction;
			return true;
		}
	}
}
