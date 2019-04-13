using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Events.UI
{
	class UIEventDescription : UIPanel
	{
		private UIMessageBox messageBox;
		private int SelectedEvent = -1;

		public override void OnInitialize()
		{
			messageBox = new UIMessageBox("")
			{
				Width = { Pixels = -25, Precent = 1f },
				Height = { Pixels = -40, Precent = 1f },
				Top = { Pixels = 40 },
			};
			Append(messageBox);

			var messageBoxScrollbar = new UIScrollbar
			{
				Height = { Pixels = -30, Precent = 1f },
				Top = { Pixels = 30 },
				//VAlign = 0.5f,
				HAlign = 1f
			};
			messageBoxScrollbar.SetView(100f, 1000f);
			Append(messageBoxScrollbar);

			messageBox.SetScrollbar(messageBoxScrollbar);

			base.OnInitialize();
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			if (EventsJournalUIState.SelectedEvent == -1)
				return;

			if (EventsJournalUIState.SelectedEvent != SelectedEvent)
			{
				SelectedEvent = EventsJournalUIState.SelectedEvent;
				// TODO: wait until end to do hover on snippets, or make ItemTag alternate that doesn't call MouseText
				messageBox.SetText(EventID.descriptionString[SelectedEvent]);
			}
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(Events.Instance.GetTexture("Icons/" + SelectedEvent), dimensions.Position() + new Vector2(6, 6), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);

			Utils.DrawBorderString(spriteBatch, EventID.nameString[SelectedEvent], dimensions.Position() + new Vector2(46, 14), Color.White, 1, 0f, 0f, -1);
		}
	}
}
