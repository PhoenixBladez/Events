using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.NPCs.Ashstorm.AshSpawn
{
	public class AshSpawn : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ash Spawn");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.width = 40;
			npc.height = 50;
			npc.damage = 56;
			npc.defense = 6;
			npc.lifeMax = 360;
			banner = npc.type;
			bannerItem = mod.ItemType("AshSpawnBanner");
			npc.HitSound = SoundID.NPCHit11;
			npc.DeathSound = SoundID.NPCDeath2;
			npc.value = 1130f;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.knockBackResist = 0f;
			npc.aiStyle = 3;
			aiType = 508;
		}
				        int frame = 0;
		int timer = 0;
		public override bool PreAI()
		{
			/*if (npc.wet)
			{
				npc.noGravity = true;
			}
			else
			{
				npc.noGravity = false;	
			}*/
			 timer++;
                if(timer == 5)
                {
                    frame++;
                    timer = 0;
                }
                if(frame == 6) //if you only have 6 frames for animation
                {
                    frame = 1;
                }
			/*if (npc.velocity.Y != 0f)
			{
				frame = 2;
			}*/
			int dust = Dust.NewDust(npc.position, npc.width, npc.height, 6);
            Main.dust[dust].noGravity = true;	
			npc.spriteDirection = npc.direction;
			return true;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && MyWorld.activeEvents.Contains(EventID.ashStorm) && !spawnInfo.playerSafe && !spawnInfo.invasion && !spawnInfo.sky && !Main.eclipse ? 1.2f : 0f;
		}
		public override void HitEffect(int hitDirection, double damage)
		{
				bool expertMode = Main.expertMode;
			if (Main.rand.Next (3) == 0)
			{
			Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, mod.ProjectileType("ThermalJellyfish_Proj"), npc.damage, 1, Main.myPlayer, 0, 0);
			Vector2 direction = Main.player[npc.target].Center - npc.Center;
			direction.Normalize();
			direction.X *= 6f;
			direction.Y *= 6f;

			int amountOfProjectiles = Main.rand.Next(0, 2);
				for (int i = 0; i < amountOfProjectiles; ++i)
				{
					float A = (float)Main.rand.Next(-100, 100) * 0.01f;
					float B = (float)Main.rand.Next(-100, 100) * 0.01f;
					int somedamage = expertMode ? 20 : 34;
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ProjectileID.Fireball, somedamage, 1, Main.myPlayer, 0, 0);

				}
			}
				int d = 0;
				int d1 = 6;
				for (int k = 0; k < 5; k++)
				{
				{
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				}

				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				}
			if (npc.life <= 0)
			{				
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				}

				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AshLeg2"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AshLeg2"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SpawnHead"), 1f);	
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AshSpawnGore"), 1f);					
			
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.Next(2) == 0)
			{
				target.AddBuff(BuffID.Burning, 120, true);
			}
		}
		public override void NPCLoot()
		{
			if (Main.rand.Next(4) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HeartStone"));
			}
			if (Main.rand.Next(30) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BloodskaalBlade"));
			}
		}
        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = frameHeight * frame;
        }
	}
}