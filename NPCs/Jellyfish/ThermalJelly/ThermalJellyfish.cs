using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Events.NPCs.Jellyfish.ThermalJelly
{
	public class ThermalJellyfish : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Thermal Jellyfish");
		}

		public override void SetDefaults()
		{
			npc.width = 24;
			npc.height = 34;
			npc.damage = 34;
			npc.defense = 5;
			npc.lifeMax = 90;
			npc.HitSound = SoundID.NPCHit25;
			banner = npc.type;
			bannerItem = mod.ItemType("ThermalJellyfishBanner");
			npc.DeathSound = SoundID.NPCDeath28;
			npc.value = 200f;
			npc.noGravity = true;
			npc.knockBackResist = 0f;
			npc.aiStyle = 18;
			aiType = NPCID.BlueJellyfish;
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BlueJellyfish];
			aiType = NPCID.BlueJellyfish;
			animationType = NPCID.BlueJellyfish;
		}

		public override void AI()
		{
			Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), .5f, .25f, 0.12f);

			npc.spriteDirection = npc.direction;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.Next(4) == 0)
			{
				target.AddBuff(BuffID.Burning, 120, true);
			}
		}
		 public override void NPCLoot ()
		{
			Player player = Main.player[npc.target];
			float Speed = 0f; 
		    Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 4), npc.position.Y + (npc.height / 4));
			int damage = 35;  
			int time = 0;
			int type = mod.ProjectileType("ThermalJellyfish_Proj");
			float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
			int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
			Main.projectile[num54].friendly = true;
			Main.projectile[num54].hostile = false;
			time = 0;
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Glowstick, Main.rand.Next(0, 5));
			}
			if (Main.rand.Next(100) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.JellyfishNecklace, 1);
			}
			if (Main.rand.Next(50) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ThermalCore"), 1);
			}
		} 
		public override void HitEffect(int hitDirection, double damage)
        {
			{
				int d = 6;
				for (int k = 0; k < 5; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				}

				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
			}
			if (npc.life <= 0)
			{
				int d = 6;
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				}

				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
			}
		}
	}
}
