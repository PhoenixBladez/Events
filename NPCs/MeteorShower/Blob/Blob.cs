using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Events.NPCs.MeteorShower.Blob
{
	public class Blob : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Panspermic Blob");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.width = 32;
			npc.height = 32;
			npc.damage = 29;
			npc.defense = 5;
			npc.lifeMax = 95;
			banner = npc.type;
			bannerItem = mod.ItemType("BlobBanner");
			npc.HitSound = SoundID.NPCHit19;
			npc.DeathSound = SoundID.NPCDeath22;
			npc.knockBackResist = .45f;
			npc.noGravity = true;
			npc.noTileCollide = true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				npc.position.X = npc.position.X + (float)(npc.width / 2);
				npc.position.Y = npc.position.Y + (float)(npc.height / 2);
				npc.width = 32;
				npc.height = 32;
				npc.position.X = npc.position.X - (float)(npc.width / 2);
				npc.position.Y = npc.position.Y - (float)(npc.height / 2);
				for (int num621 = 0; num621 < 20; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 107, 0f, 0f, 100, default(Color), 1f);
					Main.dust[num622].velocity *= .1f;
					Main.dust[num622].noGravity = true;
					
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.9f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
			
			}
		}
		public override void NPCLoot()
		{
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Gel, Main.rand.Next(1, 4));
			}
			if (Main.rand.Next(50) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Bezoar, 1);
			}
		}
	public override void FindFrame(int frameHeight)
		{
			npc.frameCounter += 0.25f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
			int counters;
		public override void AI()
		{
			{
								Player target = Main.player[npc.target];
				Player player = Main.player[npc.target];
				int distance = (int)Math.Sqrt((npc.Center.X - target.Center.X) * (npc.Center.X - target.Center.X) + (npc.Center.Y - target.Center.Y) * (npc.Center.Y - target.Center.Y));
				if (distance < 400)
				{
					counters++;
					
					npc.aiStyle = 5;
					aiType = NPCID.Moth;
					if (counters >= 30 && npc.life < 95)
					{
					npc.life += 1; 
					npc.HealEffect(1, true); 
					counters = 0;
					}
					player.AddBuff(BuffID.Poisoned, 120); 
				}
				else if (distance > 400)
				{
					npc.velocity.X *= .65f;
					npc.velocity.Y *= .65f;
				}
				if (Main.rand.NextFloat() < 0.131579f)
				{
				Dust dust;
				Vector2 position = npc.Center;
				dust = Terraria.Dust.NewDustPerfect(position, 16, new Vector2(0f, 4.421053f), 0, new Color(50,168,91), 1.447368f);
				dust.noGravity = true;
				dust.velocity *= .5f;
				dust.fadeIn = 0.5526316f;
				}
			}
			
		}
	}
}
