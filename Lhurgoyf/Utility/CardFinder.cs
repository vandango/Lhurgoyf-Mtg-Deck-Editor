using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toenda.Lhurgoyf.Data;
using Toenda.Foundation;

namespace Toenda.Lhurgoyf.Utility {
	public static class CardFinder {
		public static List<Card> FindByName(string name) {
			CardFinderEngine cardFinderEngine = new CardFinderEngine(
				name, null, null, null, null, null, null
			);

			return cardFinderEngine.Find();
		}

		public static List<Card> FindByNameAndEdition(string name, string editionShortName) {
			CardFinderEngine cardFinderEngine = new CardFinderEngine(
				name, null, null, null, null, null, editionShortName
			);

			return cardFinderEngine.Find();
		}

		public static List<Card> Find(string name) {
			CardFinderEngine cardFinderEngine = new CardFinderEngine(
				name, "", name, null, "", name, ""
			);

			return cardFinderEngine.Find();
		}

		public static List<Card> Find(
			string name, string castingCost, string type,
			List<CardColor> colors, string powerThoughness, 
			string text, string editionShortName, bool linkByAND = false
		) {
			CardFinderEngine cardFinderEngine = new CardFinderEngine(
				name, castingCost, type, colors, powerThoughness, text, editionShortName
			);

			return cardFinderEngine.Find(linkByAND);
		}

		public static List<Edition> FindEditionByName(string name) {
			List<Edition> list = new List<Edition>();

			foreach(Edition item in CardBase.Editions) {
				//if(item.Name.Contains(name)
				//|| 
				if(item.Shortname.Equals(name)) {
					list.Add(item);
				}
			}

			return list;
		}

		internal class CardFinderEngine {
			private string _name;
			private string _castingCost;
			private string _type;
			private List<CardColor> _colors;
			private string _powerThoughness;
			private string _text;
			private string _editionShortName;

			public CardFinderEngine(
				string name, string castingCost, string type, 
				List<CardColor> colors, string powerThoughness,
				string text, string editionShortName
			) {
				this._name = name;
				this._castingCost = castingCost;
				this._type = type;
				this._colors = colors;
				this._powerThoughness = powerThoughness;
				this._text = text;
				this._editionShortName = editionShortName;

				// clean names now
				this._name = this._name.Replace("Æ", "AE");
				this._name = this._name.Replace("/", "//");
			}

			public List<Card> Find(bool linkByAND = false) {
				List<Card> list = new List<Card>();

				List<string> searchWords = new List<string>();

				//searchWords = this._name.Split(" ");
				searchWords.Add(this._name);

				if(this._name.Contains("'")) {
					searchWords.Add(this._name.Replace("'", "’"));
				}
				else if(this._name.Contains("’")) {
					searchWords.Add(this._name.Replace("’", "'"));
				}

				foreach(string text in searchWords) {
					/*
					 * Find by NAME and EDITION
					 */
					if(this._colors == null
					&& this._castingCost.IsNullOrTrimmedEmpty()
					&& this._type.IsNullOrTrimmedEmpty()
					&& this._text.IsNullOrTrimmedEmpty()
					&& this._powerThoughness.IsNullOrTrimmedEmpty()
					&& !this._editionShortName.IsNullOrTrimmedEmpty()) {
						List<Card> tmp = new List<Card>();

						foreach(Card card in CardBase.Cards) {
							if(card.Name.ToLower().Equals(text.ToLower())) {
								if(!tmp.Contains(card)) {
									tmp.Add(card);
								}
							}
						}

						foreach(Card card in tmp) {
							foreach(KeyValuePair<Edition, EditionImage> pair in card.EditionPictures) {
								if(pair.Key.Shortname.Equals(this._editionShortName)
								|| (
									(pair.Key.Shortname == "COM" || pair.Key.Shortname == "CMD")
									&& (this._editionShortName == "COM" || this._editionShortName == "CMD")
								)) {
									Card card2 = new Card();

									card2.Artist = card.Artist;
									card2.CastingCost = card.CastingCost;
									card2.Color = card.Color;
									card2.Flavour = card.Flavour;
									card2.Id = card.Id;
									card2.Name = card.Name;
									card2.PowerThoughness = card.PowerThoughness;
									card2.Rarity = card.Rarity;
									card2.Text = card.Text;
									card2.Type = card.Type;

									card2.EditionPictures.Add(pair.Key, pair.Value);

									list.Add(card2);
								}
							}
						}
					}
					else {
						/*
						 * Find by NAME
						 */
						if(this._colors == null
						&& this._castingCost.IsNullOrTrimmedEmpty()
						&& this._type.IsNullOrTrimmedEmpty()
						&& this._text.IsNullOrTrimmedEmpty()
						&& this._powerThoughness.IsNullOrTrimmedEmpty()
						&& this._editionShortName.IsNullOrTrimmedEmpty()) {
							foreach(Card card in CardBase.Cards) {
								if(card.Name.ToLower().Equals(text.ToLower())) {
									if(!list.Contains(card)) {
										list.Add(card);
									}
								}
							}
						}
						else {
							if(this._colors == null
							&& this._castingCost.IsNullOrTrimmedEmpty()
							&& this._powerThoughness.IsNullOrTrimmedEmpty()) {
								foreach(Card card in CardBase.Cards) {
									// some properties "equals"

									// name equals and contain
									if(card.Name.ToLower().Equals(text.ToLower())
									|| card.Name.ToLower().Contains(text.ToLower())
									//|| card.Name.ToLower().Equals(this._name.ToLower())
									//|| card.Name.ToLower().Contains(this._name.ToLower())
									// type equals and contain
									|| card.Type.ToLower().Equals(this._type.ToLower())
									|| card.Type.ToLower().Contains(this._type.ToLower())
									// maintype equals and contain
									|| card.MainType.ToLower().Equals(this._type.ToLower())
									|| card.MainType.ToLower().Contains(this._type.ToLower())
									// text equals and contain
									|| card.Text.ToLower().Equals(text.ToLower())
									|| card.Type.ToLower().Contains(this._type.ToLower())) {
										if(!list.Contains(card)) {
											list.Add(card);
										}
									}
								}
							}
							else {
								if(linkByAND) {
									/*
									 * Search using AND
									 * */
									foreach(Card card in CardBase.Cards) {
										if((!this._name.IsNullOrTrimmedEmpty() && card.Name.ToLower().Contains(text.ToLower()))
										&& (!this._castingCost.IsNullOrTrimmedEmpty() && card.CastingCost.ToLower().Contains(this._castingCost.ToLower()))
										&& (!this._type.IsNullOrTrimmedEmpty() && card.Type.ToLower().Contains(this._type.ToLower()))
										&& (!text.IsNullOrTrimmedEmpty() && card.Text.ToLower().Contains(text.ToLower()))) {
											bool colorEquals = false;

											if(this._colors != null
											&& this._colors.Count > 0
											&& this._colors.Count == card.Color.Count) {
												int countColors = 0;

												foreach(CardColor color in this._colors) {
													if((
														from item in card.Color
														where item.Symbol == color.Symbol
														select item
													).Count() > 0) {
														countColors++;
													}
												}

												if(countColors == card.Color.Count) {
													colorEquals = true;
												}
											}

											if(colorEquals) {
												if(this._powerThoughness.Contains("/")) {
													if(card.PowerThoughness.Contains(this._powerThoughness)) {
														if(!list.Contains(card)) {
															list.Add(card);
														}
													}
												}
												else if(!this._powerThoughness.IsNullOrTrimmedEmpty()) {
													if(card.PowerThoughness.Contains(this._powerThoughness + "/")) {
														if(!list.Contains(card)) {
															list.Add(card);
														}
													}
												}
												else {
													if(!list.Contains(card)) {
														list.Add(card);
													}
												}
											}
										}
									}
								}
								else {
									/*
									 * Search using OR
									 * */
									foreach(Card card in CardBase.Cards) {
										if((!this._name.IsNullOrTrimmedEmpty() && card.Name.ToLower().Contains(text.ToLower()))
										|| (!this._castingCost.IsNullOrTrimmedEmpty() && card.CastingCost.ToLower().Contains(this._castingCost.ToLower()))
										|| (!this._type.IsNullOrTrimmedEmpty() && card.Type.ToLower().Contains(this._type.ToLower()))
										|| (!text.IsNullOrTrimmedEmpty() && card.Text.ToLower().Contains(text.ToLower()))) {
											bool colorEquals = true;

											if(this._colors != null
											&& this._colors.Count > 0) {
												colorEquals = false;

												foreach(CardColor color in this._colors) {
													if((
														from item in card.Color
														where item.Symbol == color.Symbol
														select item
													).Count() > 0) {
														colorEquals = true;
														break;
													}
												}
											}

											if(colorEquals) {
												if(this._powerThoughness.Contains("/")) {
													if(card.PowerThoughness.Contains(this._powerThoughness)) {
														list.Add(card);
													}
												}
												else if(!this._powerThoughness.IsNullOrTrimmedEmpty()) {
													if(card.PowerThoughness.Contains(this._powerThoughness + "/")) {
														list.Add(card);
													}
												}
												else {
													list.Add(card);
												}
											}
										}
									}
								}
							}
						}
					}
				}

				return list;
			}
		}
	}
}
