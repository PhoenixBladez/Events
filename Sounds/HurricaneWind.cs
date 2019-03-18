using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;

namespace Events.Sounds
{
	public class HurricaneWind : ModSound
	{
		public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
		{
			soundInstance = sound.CreateInstance();
			soundInstance.Volume = volume * .65f;
			soundInstance.Pan = pan;
			soundInstance.Pitch = Main.rand.Next(-10, 8) /30f;
			return soundInstance;

		}
	}
}
