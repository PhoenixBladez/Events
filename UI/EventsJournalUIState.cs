using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Events.UI
{
	class EventsJournalUIState : UIState
	{
		private UIPanel mainPanel;
		internal UIEventDescription eventDescription;
		private Terraria.ModLoader.UI.Elements.UIGrid weatherList;
		private Terraria.ModLoader.UI.Elements.FixedUIScrollbar weatherListScrollbar;

		private UIEventButton[] eventButtons;
		internal static int SelectedEvent = -1; // make this a property?

		public override void OnInitialize()
		{
			mainPanel = new UIDragableElement();
			mainPanel.Left.Set(300f, 0f);
			mainPanel.Top.Set(300f, 0f);
			mainPanel.Width.Set(500f, 0f);
			mainPanel.Height.Set(300f, 0f);
			mainPanel.BackgroundColor = new Color(73, 94, 171);
			Append(mainPanel);

			Texture2D closeTexture = Events.Instance.GetTexture("UI/closeButton");
			UIImageButton closeButton = new UIImageButton(closeTexture);
			closeButton.Left.Set(-13, 1f);
			closeButton.Top.Set(-2, 0f);
			closeButton.Width.Set(22, 0f);
			closeButton.Height.Set(22, 0f);
			closeButton.OnClick += new MouseEvent(CloseButtonClicked);
			mainPanel.Append(closeButton);

			UIText label = new UIText("Journal");
			mainPanel.Append(label);

			var weatherListPanel = new UIPanel();
			weatherListPanel.Left.Set(0, 0f);
			weatherListPanel.Top.Set(22, 0f);
			weatherListPanel.Width.Set(0, .35f);
			weatherListPanel.Height.Set(-22, 1f);
			mainPanel.Append(weatherListPanel);

			weatherList = new Terraria.ModLoader.UI.Elements.UIGrid();
			weatherList.Width.Set(-20, 1);
			weatherList.Height.Set(0, 1);
			weatherList.ListPadding = 12f;
			weatherListPanel.Append(weatherList);

			weatherListScrollbar = new Terraria.ModLoader.UI.Elements.FixedUIScrollbar(Events.eventsUserInterface);
			weatherListScrollbar.SetView(100f, 1000f);
			weatherListScrollbar.Height.Set(0, 1f);
			weatherListScrollbar.Left.Set(6, 0f);
			weatherListScrollbar.HAlign = 1f;
			weatherList.SetScrollbar(weatherListScrollbar);
			weatherListPanel.Append(weatherListScrollbar);

			eventDescription = new UIEventDescription();
			eventDescription.Left.Set(6, .35f);
			eventDescription.Top.Set(20, 0f); // or use 14
			eventDescription.Width.Set(-6, .65f);
			eventDescription.Height.Set(-20, 1f);
			mainPanel.Append(eventDescription);

			eventButtons = new UIEventButton[20];
			for (int i = 0; i < 20; i++)
			{
				eventButtons[i] = new UIEventButton(i);
				weatherList.Add(eventButtons[i]);
			}

			mainPanel.OnScrollWheel += OnScrollWheel_FixHotbarScroll;
		}

		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.PlaySound(SoundID.MenuClose);
			Events.eventsUserInterface.SetState(null);
		}

		// A hack to fix scroll bar usage scrolling the item hotbar
		internal static void OnScrollWheel_FixHotbarScroll(UIScrollWheelEvent evt, UIElement listeningElement)
		{
			Main.LocalPlayer.ScrollHotbar(Terraria.GameInput.PlayerInput.ScrollWheelDelta / 120);
		}
	}
}
