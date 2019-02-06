using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Events;
using Terraria.ModLoader;

namespace Events.NPCs.MeteorShower.MeteorWorm
{
	public class MWormTail : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ion Courser");
			Main.npcFrameCount[npc.type] = 1;
		}

		public override void SetDefaults()
		{

			npc.width = 10;
			npc.height =10;
			npc.damage = 20;
			npc.HitSound = SoundID.NPCHit7;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.defense = 12;
			npc.lifeMax = 1;
			npc.knockBackResist = 0.0f;
			npc.behindTiles = true;
			npc.noTileCollide = true;
			npc.netAlways = true;
			npc.noGravity = true;
			npc.buffImmune[24] = true;
			npc.dontCountMe = true;
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.Next(5) == 0)
				target.AddBuff(BuffID.OnFire, 200);

		}
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				npc.position.X = npc.position.X + (float)(npc.width / 2);
				npc.position.Y = npc.position.Y + (float)(npc.height / 2);
				npc.width = 10;
				npc.height = 10;
				npc.position.X = npc.position.X - (float)(npc.width / 2);
				npc.position.Y = npc.position.Y - (float)(npc.height / 2);
				for (int num621 = 0; num621 < 20; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 1.4f;
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
			if (npc.ai[3] > 0)
				npc.realLife = (int)npc.ai[3];
			if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
				npc.TargetClosest(true);
			if (Main.player[npc.target].dead && npc.timeLeft > 300)
				npc.timeLeft = 300;

			if (Main.netMode != 1)
			{
				if (!Main.npc[(int)npc.ai[1]].active)
				{
					npc.life = 0;
					npc.HitEffect(0, 10.0);
					npc.active = false;
					//NetMessage.SendData(28, -1, -1, "", npc.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
				}
			}

			if (npc.ai[1] < (double)Main.npc.Length)
			{
				Vector2 npcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);

				float dirX = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - npcCenter.X;
				float dirY = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - npcCenter.Y;
				npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;

				float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
				float dist = (length - (float)npc.width) / length;
				float posX = dirX * dist;
				float posY = dirY * dist;


				npc.velocity = Vector2.Zero;

				npc.position.X = npc.position.X + posX;
				npc.position.Y = npc.position.Y + posY;
			}
			return false;
		}
		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            EventsUtility.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/MeteorShower/MeteorWorm/MWormTail_Glow"));
        }
		public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[npc.type];
			Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
			Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(), drawColor, npc.rotation, origin, npc.scale, SpriteEffects.None, 0);
			return false;
		}
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
	}
}