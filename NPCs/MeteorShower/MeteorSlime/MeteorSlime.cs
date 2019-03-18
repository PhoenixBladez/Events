using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Events;

namespace Events.NPCs.MeteorShower.MeteorSlime
{
	public class MeteorSlime : ModNPC
	{		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Slime");
			Main.npcFrameCount[npc.type] = 6;
		}
		public override void SetDefaults()
		{
			npc.width = 34;
			npc.height = 24;
			npc.damage = 28;
			npc.defense = 10;
			npc.lifeMax = 80;
			banner = npc.type;
			bannerItem = mod.ItemType("MeteorSlimeBanner");
			npc.HitSound = SoundID.NPCHit7;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 60f;
			npc.knockBackResist = .45f;
			npc.aiStyle = 1;
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BlueSlime];
			aiType = NPCID.BlueSlime;
			animationType = NPCID.BlueSlime;
		}
		public override void AI()
		{
			Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), .25f, .57f, .85f);
			npc.spriteDirection = npc.direction;
		}
		public override void NPCLoot()
		{
			if (Main.rand.Next(4) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Meteorite, Main.rand.Next(1, 4));
			}
			if (Main.rand.Next(18) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AstralStaff"), 1);
			}
			if (Main.rand.Next(50) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MeteorStaff"), 1);
			}
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && MyWorld.activeEvents.Contains(EventID.Meteor) && !Main.dayTime && !spawnInfo.playerSafe && !spawnInfo.invasion && !spawnInfo.sky && !Main.eclipse ? 0.12f : 0f;
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
            EventsUtility.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/MeteorShower/MeteorSlime/MeteorSlime_Glow"));
        }
		public override void HitEffect(int hitDirection, double damage)
        {
		    for (int i = 0; i < 10; i++) ;
            if (npc.life <= 0)
            {	 
				Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 14);
			     Vector2 direction = Main.player[npc.target].Center - npc.Center;
				direction.Normalize();
				direction.X *= 4f;
				direction.Y *= 4f;

				int amountOfProjectiles = Main.rand.Next(2, 4);
				for (int i = 0; i < amountOfProjectiles; ++i)
				{
					float A = (float)Main.rand.Next(-150, 150) * 0.01f;
					float B = (float)Main.rand.Next(-150, 150) * 0.01f;
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, 538, (int)(npc.damage / 2), 1, Main.myPlayer, 0, 0);
				}
			}
			if (npc.life <= 0)
			{
				int d = 172;
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
