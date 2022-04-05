using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Localization;

namespace GearonArsenal.Content.Currencies {
	public class Doubloon : CustomCurrencySingleCoin {
		public Doubloon(int coinItemID, long currencyCap, string CurrencyTextKey) : base(coinItemID, currencyCap) {
			this.CurrencyTextKey = CurrencyTextKey;
			CurrencyTextColor = Color.Gold;
		}
	}
}