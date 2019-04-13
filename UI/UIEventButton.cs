using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace Events.UI
{
	class UIEventButton : UIElement
	{
		internal Texture2D eventIcon;
		private int order;
		private float _visibilityActive = 1f;
		private float _visibilityInactive = 0.6f;

		public bool Selected => EventsJournalUIState.SelectedEvent == order;

		public UIEventButton(int order)
		{
			this.order = order;
			this.eventIcon = Events.Instance.GetTexture("Icons/" + order);
			this.Width.Set((float)this.eventIcon.Width, 0f);
			this.Height.Set((float)this.eventIcon.Height, 0f);
		}

		public override int CompareTo(object obj)
		{
			UIEventButton other = obj as UIEventButton;
			return order.CompareTo(other.order);
		}

		public override void Click(UIMouseEvent evt)
		{
			EventsJournalUIState.SelectedEvent = order;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			// optional selected background
			//if(Selected)
			//	spriteBatch.Draw(Main.magicPixel, dimensions.ToRectangle(), Color.LightBlue * 0.5f);
			spriteBatch.Draw(this.eventIcon, dimensions.Position(), Color.White * (base.IsMouseHovering || Selected ? this._visibilityActive : this._visibilityInactive));
		}

		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			Main.PlaySound(Terraria.ID.SoundID.MenuTick);
		}
	}
}
