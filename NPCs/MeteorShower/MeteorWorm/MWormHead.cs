using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Events;
using Terraria.ModLoader;

namespace Events.NPCs.MeteorShower.MeteorWorm

{
	public class MWormHead : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ion Courser");
			Main.npcFrameCount[npc.type] = 1;
		}

		public override void SetDefaults()
		{
			npc.lifeMax = 250;
			npc.damage = 55;
			npc.defense = 1;
			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.NPCHit7;
			npc.DeathSound = SoundID.NPCDeath1;
			banner = npc.type;
			bannerItem = mod.ItemType("MeteorWormBanner");
			npc.width = 30;
			npc.height = 20;
			npc.boss = false;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.buffImmune[24] = true;
			npc.noTileCollide = true;
			npc.behindTiles = true;
			npc.value = Item.buyPrice(0, 0, 1, 10);
			npc.npcSlots = 1f;
			npc.netAlways = true;
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.Next(5) == 0)
				target.AddBuff(BuffID.OnFire, 200);

		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Player player = spawnInfo.player;
			if (!(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust) && ((!Main.pumpkinMoon && !Main.snowMoon) || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && (SpawnCondition.GoblinArmy.Chance == 0))
			{
				return player.ZoneMeteor && MyWorld.activeEvents.Contains(EventID.Meteor) ? 0.051f : 0f;
			}
			return 0f;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				npc.position.X = npc.position.X + (float)(npc.width / 2);
				npc.position.Y = npc.position.Y + (float)(npc.height / 2);
				npc.width = 20;
				npc.height = 10;
				npc.position.X = npc.position.X - (float)(npc.width / 2);
				npc.position.Y = npc.position.Y - (float)(npc.height / 2);
				for (int num621 = 0; num621 < 10; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 1f;
					Main.dust[num622].noGravity = true;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
			}
		}

		public override bool PreAI()
		{
			if (Main.netMode != 1)
			{
				if (npc.ai[0] == 0)
				{
					npc.realLife = npc.whoAmI;
					int latestNPC = npc.whoAmI;

					int randomWormLength = Main.rand.Next(14, 18);
					for (int i = 0; i < randomWormLength; ++i)
					{
						latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 3, mod.NPCType("MWormBody"), npc.whoAmI, 0, latestNPC);
						Main.npc[(int)latestNPC].realLife = npc.whoAmI;
						Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
					}
					latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("MWormTail"), npc.whoAmI, 0, latestNPC);
					Main.npc[(int)latestNPC].realLife = npc.whoAmI;
					Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;

					npc.ai[0] = 1;
					npc.netUpdate = true;
				}
			}

			int minTilePosX = (int)(npc.position.X / 16.0) - 1;
			int maxTilePosX = (int)((npc.position.X + npc.width) / 16.0) + 2;
			int minTilePosY = (int)(npc.position.Y / 16.0) - 1;
			int maxTilePosY = (int)((npc.position.Y + npc.height) / 16.0) + 2;
			if (minTilePosX < 0)
				minTilePosX = 0;
			if (maxTilePosX > Main.maxTilesX)
				maxTilePosX = Main.maxTilesX;
			if (minTilePosY < 0)
				minTilePosY = 0;
			if (maxTilePosY > Main.maxTilesY)
				maxTilePosY = Main.maxTilesY;

			bool collision = false;
			for (int i = minTilePosX; i < maxTilePosX; ++i)
			{
				for (int j = minTilePosY; j < maxTilePosY; ++j)
				{
					if (Main.tile[i, j] != null && (Main.tile[i, j].nactive() && (Main.tileSolid[(int)Main.tile[i, j].type] || Main.tileSolidTop[(int)Main.tile[i, j].type] && (int)Main.tile[i, j].frameY == 0) || (int)Main.tile[i, j].liquid > 64))
					{
						Vector2 vector2;
						vector2.X = (float)(i * 16);
						vector2.Y = (float)(j * 16);
						if (npc.position.X + npc.width > vector2.X && npc.position.X < vector2.X + 16.0 && (npc.position.Y + npc.height > (double)vector2.Y && npc.position.Y < vector2.Y + 16.0))
						{
							collision = true;
							if (Main.rand.Next(100) == 0 && Main.tile[i, j].nactive())
								WorldGen.KillTile(i, j, true, true, false);
						}
					}
				}
			}
			if (!collision)
			{
				Rectangle rectangle1 = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
				int maxDistance = 1000;
				bool playerCollision = true;
				for (int index = 0; index < 255; ++index)
				{
					if (Main.player[index].active)
					{
						Rectangle rectangle2 = new Rectangle((int)Main.player[index].position.X - maxDistance, (int)Main.player[index].position.Y - maxDistance, maxDistance * 2, maxDistance * 2);
						if (rectangle1.Intersects(rectangle2))
						{
							playerCollision = false;
							break;
						}
					}
				}
				if (playerCollision)
					collision = true;
			}

			float speed = 4f;
			float acceleration = 0.08f;

			Vector2 npcCenter = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
			float targetXPos = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2);
			float targetYPos = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2);

			float targetRoundedPosX = (float)((int)(targetXPos / 16.0) * 16);
			float targetRoundedPosY = (float)((int)(targetYPos / 16.0) * 16);
			npcCenter.X = (float)((int)(npcCenter.X / 16.0) * 16);
			npcCenter.Y = (float)((int)(npcCenter.Y / 16.0) * 16);
			float dirX = targetRoundedPosX - npcCenter.X;
			float dirY = targetRoundedPosY - npcCenter.Y;

			float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
			if (!collision)
			{
				npc.TargetClosest(true);
				npc.velocity.Y = npc.velocity.Y + 0.11f;
				if (npc.velocity.Y > speed)
					npc.velocity.Y = speed;
				if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.4)
				{
					if (npc.velocity.X < 0.0)
						npc.velocity.X = npc.velocity.X - acceleration * 1.1f;
					else
						npc.velocity.X = npc.velocity.X + acceleration * 1.1f;
				}
				else if (npc.velocity.Y == speed)
				{
					if (npc.velocity.X < dirX)
						npc.velocity.X = npc.velocity.X + acceleration;
					else if (npc.velocity.X > dirX)
						npc.velocity.X = npc.velocity.X - acceleration;
				}
				else if (npc.velocity.Y > 4.0)
				{
					if (npc.velocity.X < 0.0)
						npc.velocity.X = npc.velocity.X + acceleration * 0.9f;
					else
						npc.velocity.X = npc.velocity.X - acceleration * 0.9f;
				}
			}
			else
			{
				if (npc.soundDelay == 0)
				{
					float num1 = length / 40f;
					if (num1 < 10.0)
						num1 = 10f;
					if (num1 > 20.0)
						num1 = 20f;
					npc.soundDelay = (int)num1;
					Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 1);
				}
				float absDirX = Math.Abs(dirX);
				float absDirY = Math.Abs(dirY);
				float newSpeed = speed / length;
				dirX = dirX * newSpeed;
				dirY = dirY * newSpeed;
				if (npc.velocity.X > 0.0 && dirX > 0.0 || npc.velocity.X < 0.0 && dirX < 0.0 || (npc.velocity.Y > 0.0 && dirY > 0.0 || npc.velocity.Y < 0.0 && dirY < 0.0))
				{
					if (npc.velocity.X < dirX)
						npc.velocity.X = npc.velocity.X + acceleration;
					else if (npc.velocity.X > dirX)
						npc.velocity.X = npc.velocity.X - acceleration;
					if (npc.velocity.Y < dirY)
						npc.velocity.Y = npc.velocity.Y + acceleration;
					else if (npc.velocity.Y > dirY)
						npc.velocity.Y = npc.velocity.Y - acceleration;
					if (Math.Abs(dirY) < speed * 0.2 && (npc.velocity.X > 0.0 && dirX < 0.0 || npc.velocity.X < 0.0 && dirX > 0.0))
					{
						if (npc.velocity.Y > 0.0)
							npc.velocity.Y = npc.velocity.Y + acceleration * 2f;
						else
							npc.velocity.Y = npc.velocity.Y - acceleration * 2f;
					}
					if (Math.Abs(dirX) < speed * 0.2 && (npc.velocity.Y > 0.0 && dirY < 0.0 || npc.velocity.Y < 0.0 && dirY > 0.0))
					{
						if (npc.velocity.X > 0.0)
							npc.velocity.X = npc.velocity.X + acceleration * 2f;
						else
							npc.velocity.X = npc.velocity.X - acceleration * 2f;
					}
				}
				else if (absDirX > absDirY)
				{
					if (npc.velocity.X < dirX)
						npc.velocity.X = npc.velocity.X + acceleration * 1.1f;
					else if (npc.velocity.X > dirX)
						npc.velocity.X = npc.velocity.X - acceleration * 1.1f;
					if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
					{
						if (npc.velocity.Y > 0.0)
							npc.velocity.Y = npc.velocity.Y + acceleration;
						else
							npc.velocity.Y = npc.velocity.Y - acceleration;
					}
				}
				else
				{
					if (npc.velocity.Y < dirY)
						npc.velocity.Y = npc.velocity.Y + acceleration * 1.1f;
					else if (npc.velocity.Y > dirY)
						npc.velocity.Y = npc.velocity.Y - acceleration * 1.1f;
					if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
					{
						if (npc.velocity.X > 0.0)
							npc.velocity.X = npc.velocity.X + acceleration;
						else
							npc.velocity.X = npc.velocity.X - acceleration;
					}
				}
			}
			npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;

			if (collision)
			{
				if (npc.localAI[0] != 1)
					npc.netUpdate = true;
				npc.localAI[0] = 1f;
			}
			else
			{
				if (npc.localAI[0] != 0.0)
					npc.netUpdate = true;
				npc.localAI[0] = 0.0f;
			}
			if ((npc.velocity.X > 0.0 && npc.oldVelocity.X < 0.0 || npc.velocity.X < 0.0 && npc.oldVelocity.X > 0.0 || (npc.velocity.Y > 0.0 && npc.oldVelocity.Y < 0.0 || npc.velocity.Y < 0.0 && npc.oldVelocity.Y > 0.0)) && !npc.justHit)
				npc.netUpdate = true;

			for (int index1 = 0; index1 < 5; ++index1)
                {
					float x = (npc.Center.X);
					float xnum2 = (npc.Center.X);
					float y = (npc.Center.Y);
					if (npc.direction == -1)
					{
						int index2 = Dust.NewDust(new Vector2(x, y), 1, 1, 6, 0.0f, 0.0f, 0, new Color(), 1f);
						Main.dust[index2].position.X = x;
						Main.dust[index2].position.Y = y;
						Main.dust[index2].scale = 1f;
						Main.dust[index2].velocity *= .4f;
						Main.dust[index2].noGravity = true;
						Main.dust[index2].noLight = false;
					}
					else if (npc.direction == 1)
					{
						int index2 = Dust.NewDust(new Vector2(xnum2, y), 1, 1, 6, 0.0f, 0.0f, 0, new Color(), 1f);
						Main.dust[index2].position.X = xnum2;
						Main.dust[index2].position.Y = y;
						Main.dust[index2].scale = 1f;
						Main.dust[index2].velocity *= .4f;
						Main.dust[index2].noGravity = true;
						Main.dust[index2].noLight = false;
					}
                }
			return false;				
		}
		public override void NPCLoot()
		{
			if (Main.rand.Next(4) == 0)			
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Meteorite, Main.rand.Next(1, 3));
			}
			if (Main.rand.Next(20) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MeteorStaff"), 1);
			}
	
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[npc.type];
			Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
			Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(), drawColor, npc.rotation, origin, npc.scale, SpriteEffects.None, 0);
			return false;
		}
		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            EventsUtility.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/MeteorShower/MeteorWorm/MWormHead_Glow"));
        }

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1f;
			return null;
		}
	}
}