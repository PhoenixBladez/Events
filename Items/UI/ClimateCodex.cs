using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.UI
{
	class ClimateCodex : ModItem
	{
		public override void SetStaticDefaults()
		{
			// TODO
		}

		public override void SetDefaults()
		{
			// TODO
			item.useStyle = ItemUseStyleID.HoldingUp;
		}

		public override bool UseItem(Player player)
		{
			Main.PlaySound(SoundID.MenuOpen);
			Events.eventsUserInterface.SetState(Events.eventsUIState);
			// Events.eventsUserInterface.SetState(new global::Events.UI.EventsJournalUIState()); // This is for live testing OnInitialize
			return true;
		}
	}
}
