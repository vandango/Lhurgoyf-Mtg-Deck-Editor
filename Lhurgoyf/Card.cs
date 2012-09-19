using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toenda.Foundation;
using Toenda.Lhurgoyf.Utility;
using System.Text.RegularExpressions;

namespace Toenda.Lhurgoyf {
	public class Card {
		//public string Color { get; set; }
		//public List<Ability> Abilities { get; set; }

		private Card _transformedCard;
		private List<ManaItem> _listOfMana;
		private string _name;

		/// <summary>
		/// Gets or sets the id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name
		/// </summary>
		public string Name {
			get {
				return this._name.Replace("’", "'");
			}
			set {
				this._name = value;
			}
		}

		/// <summary>
		/// Gets or sets the casting cost
		/// </summary>
		public string CastingCost { get; set; }

		/// <summary>
		/// Gets or sets the type
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the rarity
		/// </summary>
		public Rarity Rarity { get; set; }

		/// <summary>
		/// Gets or sets the artist of the card image
		/// </summary>
		public string Artist { get; set; }

		/// <summary>
		/// Gets or sets the flavour text
		/// </summary>
		public string Flavour { get; set; }

		/// <summary>
		/// Gets or sets the power and toughness
		/// </summary>
		public string PowerThoughness { get; set; }

		/// <summary>
		/// Gets or sets the text
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Gets or sets the edition pictures
		/// </summary>
		public Dictionary<Edition, EditionImage> EditionPictures { get; set; }

		/// <summary>
		/// Gets or sets a list of colors
		/// </summary>
		public List<CardColor> Color { get; set; }

		/// <summary>
		/// Get the card object that represents the transformed card, if the card is transformable
		/// </summary>
		public Card TransformedCard {
			get {
				if(this._transformedCard == null) {
					string transformedName = this.GetTransformedName();

					if(!transformedName.IsNullOrTrimmedEmpty()) {
						List<Card> found = CardFinder.FindByName(transformedName);

						if(found != null
						&& found.Count > 0) {
							this._transformedCard = found[0];
						}
					}
				}

				return this._transformedCard;
			}
			set {
				this._transformedCard = value;
			}
		}

		/// <summary>
		/// Gets a value that indicates if the card is transformable
		/// </summary>
		public bool IsTransformable {
			get {
				return (this.GetTransformedName() != null);
			}
		}

		/// <summary>
		/// Gets the first image from the image list
		/// </summary>
		public EditionImage FirstEditionImageFromList {
			get {
				if(this.EditionPictures.Count > 0) {
					return this.EditionPictures.Values.First();
				}
				else {
					return null;
				}
			}
		}

		/// <summary>
		/// Gets the first edition from the edition list
		/// </summary>
		public Edition FirstEditionFromList {
			get {
				if(this.EditionPictures.Count > 0) {
					return this.EditionPictures.Keys.First();
				}
				else {
					return null;
				}
			}
		}

		/// <summary>
		/// Gets the converted casting cost
		/// </summary>
		public int ConvertedCastingCost {
			get {
				if(this.CastingCost != null) {
					string castingCost = this.CastingCost;

					// Phyrexian Mana
					if(castingCost.Contains("(")
					&& castingCost.Contains("/")
					&& castingCost.Contains(")")
					&& castingCost.Contains("P")) {
						castingCost = castingCost
							.Replace("(", "")
							.Replace("/", "")
							.Replace(")", "")
							.Replace("P", "");
					}

					// Multi-Change Color (like (R/WR/WR/W) )
					if(castingCost.Contains("(")
					&& castingCost.Contains("/")
					&& castingCost.Contains(")")) {
						Regex regex = new Regex("([RWUGB]/[RWUGB])");
						castingCost = regex.Replace(castingCost, "1");
						castingCost = castingCost.Replace("(", "").Replace(")", "");
					}

					int cc = 0;
					int ccc = 0;

					foreach(char c in castingCost) {
						if(!c.ToString().IsNumeric()) {
							if(c != 'X') {
								cc++;
							}
						}
						else {
							ccc += c.ToString().ToInt32();
						}
					}

					cc += ccc;

					return cc;
				}
				else {
					return 0;
				}
			}
		}

		/// <summary>
		/// Gets a list of mana which can be created by this card
		/// </summary>
		public List<ManaItem> CreatableMana {
			get {
				if(this.Text != null
				&& this._listOfMana.Count == 0) {
					this.FindManaItem();
				}

				return this._listOfMana;
			}
		}

		/// <summary>
		/// Gets an value indicating if the card can add mana to the mana pool
		/// </summary>
		public bool CanAddMana {
			get {
				if(this.CreatableMana.Count > 0) {
					return true;
				}
				else {
					return false;
				}
			}
		}

		/// <summary>
		/// Represents the special type of a card, this could be
		/// "Fetchland" for the fetchlands, "Mana Creature" for 
		/// mana generating creatures and other special types.
		/// </summary>
		public string SpecialType {
			get {
				if(this.Type != null
				&& this.Text != null) {
					string specialType = "";
					Regex regex;

					// Card Draw
					// Discard

					#region Selection Removal

					// Removal
					if(specialType.IsNullOrTrimmedEmpty()) {
						// Damnation
						// Wrath of God
						// Terror
						// Go for the Throat
						// Vindicate
						regex = new Regex(
							"(Destroy){1,}.*?(creature|permanent)",
							RegexOptions.IgnoreCase
						);

						// Geth's Verdict
						// All is Dust
						Regex sacRegex = new Regex(
							"(player sacrifices a){1,}.*?(creature|permanent)",
							RegexOptions.IgnoreCase
						);

						// Oblivion Ring
						// Path to Exile
						// Brittle Effigy
						Regex exileRegex = new Regex(
							"(exile){1,}.*?(target).*?(creature|permanent)",
							RegexOptions.IgnoreCase
						);

						// Disfigure
						// Dismember
						Regex reduceRegex = new Regex(
							"(creature)+.*?(-[0-9]/-[0-9])+",
							RegexOptions.IgnoreCase
						);

						// Black Sun's Zenith
						Regex massReduceRegex = new Regex(
							"(-[0-9]/-[0-9])+.*?(creature)+",
							RegexOptions.IgnoreCase
						);

						// Repeal
						// Into the Roil
						Regex bounceRegex = new Regex(
							"(Return){1,}.*?(creature|permanent)",
							RegexOptions.IgnoreCase
						);

						// Lightning Bolt
						// Arc Trail
						// Devil's Play
						// Starstorm
						Regex damageRegex = new Regex(
							"(deals)+.*?([0-9x])+.*?(damage)+.*?(creature)+",
							RegexOptions.IgnoreCase
						);

						if((
							regex.IsMatch(this.Text)
							|| sacRegex.IsMatch(this.Text)
							|| exileRegex.IsMatch(this.Text)
							|| reduceRegex.IsMatch(this.Text)
							|| massReduceRegex.IsMatch(this.Text)
							|| bounceRegex.IsMatch(this.Text)
							|| damageRegex.IsMatch(this.Text)
						)
						&& (
							this.MainType == "Sorcery"
							|| this.MainType == "Instant"
							|| this.MainType == "Artifact"
							|| this.MainType == "Enchantment"
						)
						&& (
							this.Name != "Cryptic Command"
						)) {
							specialType = "Removal";
						}
					}

					#endregion

					#region Selection Tutor

					// Tutor
					if(specialType.IsNullOrTrimmedEmpty()) {
						regex = new Regex(
							"(Search your library for a){1,}.*?(card).*?([A-Za-z0-9]).*?(Then shuffle your library){1,}",
							RegexOptions.IgnoreCase
						);

						if((
							this.Text.Contains("Transmute")
							|| regex.IsMatch(this.Text)
						)
						&& (
							this.MainType == "Sorcery"
							|| this.MainType == "Instant"
						)) {
							specialType = "Tutor";
						}
					}

					#endregion

					#region Selection Counterspell

					// Counterspell
					if(specialType.IsNullOrTrimmedEmpty()) {
						regex = new Regex(
							"(Counter target){1,}.*?(spell|ability)",
							RegexOptions.IgnoreCase
						);

						if(this.MainType == "Instant"
						&& regex.IsMatch(this.Text)
						&& !this.Text.Contains("Transmute")) {
							specialType = "Counterspell";
						}
					}

					#endregion

					#region Selection Basic Land

					// Basic Land
					if(specialType.IsNullOrTrimmedEmpty()) {
						if(this.Name == "Island"
						|| this.Name == "Plains"
						|| this.Name == "Swamp"
						|| this.Name == "Mountain"
						|| this.Name == "Forest"
						) {
							specialType = "Basic Land";
						}
					}

					#endregion

					#region Selection Dual Lands

					// Dual Lands
					if(specialType.IsNullOrTrimmedEmpty()) {
						if(
							// Real Dual
						this.Name == "Savannah"
						|| this.Name == "Tropical Island"
						|| this.Name == "Taiga"
						|| this.Name == "Plateau"
						|| this.Name == "Tundra"
						|| this.Name == "Vulcanic Island"
						|| this.Name == "Underground Sea"
						|| this.Name == "Badlands"
						|| this.Name == "Bayou"
						|| this.Name == "Scrubland"

						// Shock Duals
						|| this.Name == "Blood Crypt"
						|| this.Name == "Breeding Pool"
						|| this.Name == "Godless Shrine"
						|| this.Name == "Hallowed Fountain"
						|| this.Name == "Overgrown Tomb"
						|| this.Name == "Sacred Foundry"
						|| this.Name == "Steam Vents"
						|| this.Name == "Stomping Ground"
						|| this.Name == "Temple Garden"
						|| this.Name == "Watery Grave"
						) {
							specialType = "Dual Land";
						}
					}

					#endregion

					#region Selection Draw Lands

					// Draw
					if(specialType.IsNullOrTrimmedEmpty()) {
						if(this.Name == "Tolaria West"
						|| this.Name == "Bazaar of Baghdad"
						|| this.Name == "Cephalid Coliseum"
						|| this.Name == "Grim Backwoods"
						|| this.Name == "Horizon Canopy"
						|| this.Name == "Library of Alexandria"
						|| this.Name == "Mikokoro, Center of the Sea"
						|| this.Name == "Seaside Haven"

						// Cycle (1)
						|| this.Name == "Lonely Sandbar"
						|| this.Name == "Tranquil Thicket"
						|| this.Name == "Forgotten Cave"
						|| this.Name == "Secluded Steppe"
						|| this.Name == "Barren Moor"

						// Cycle (2)
						|| this.Name == "Blasted Landscape"
						|| this.Name == "Drifting Meadow"
						|| this.Name == "Polluted Mire"
						|| this.Name == "Remote Isle"
						|| this.Name == "Slippery Karst"
						|| this.Name == "Smoldering Crater"
						) {
							specialType = "Draw Land";
						}
					}

					#endregion

					#region Selection Artifact Lands

					// Artefact
					if(specialType.IsNullOrTrimmedEmpty()) {
						if(this.Name == "Seat of the Synod"
						|| this.Name == "Ancient Den"
						|| this.Name == "Great Furnace"
						|| this.Name == "Tree of Tales"
						|| this.Name == "Vault of Whispers"
						|| this.Name == "Darksteel Citadel"
						) {
							specialType = "Artifact Land";
						}
					}

					#endregion

					#region Selection All Mana Lands

					// All Mana Lands
					if(specialType.IsNullOrTrimmedEmpty()) {
						if(this.Name == "Reflecting Pool"
						|| this.Name == "City of Brass"
						|| this.Name == "Ancient Ziggurat"
						|| this.Name == "Command Tower"
						|| this.Name == "Exotic Orchard"
						|| this.Name == "Forbidden Orchard"
						|| this.Name == "Forsaken City"
						|| this.Name == "Gemstone Caverns"
						|| this.Name == "Glimmervoid"
						|| this.Name == "Grand Coliseum"
						|| this.Name == "Lotus Vale"
						|| this.Name == "Mirrodin's Core"
						|| this.Name == "Pillar of the Paruns"
						|| this.Name == "Primal Beyond"
						|| this.Name == "Rainbow Vale"
						|| this.Name == "Rhystic Cave"
						|| this.Name == "Rupture Spire"
						|| this.Name == "Tarnished Citadel"
						|| this.Name == "Thran Quarry"
						|| this.Name == "Undiscovered Paradise"
						|| this.Name == "Vivid Crag"
						|| this.Name == "Vivid Creek"
						|| this.Name == "Vivid Grove"
						|| this.Name == "Vivid Marsh"
						|| this.Name == "Vivid Meadow"
						) {
							specialType = "All Mana Land";
						}
					}

					#endregion

					#region Selection Filter Lands

					// Filter
					if(specialType.IsNullOrTrimmedEmpty()) {
						if(
							// Eventide
						this.Name == "Flooded Grove"
						|| this.Name == "Fetid Heath"
						|| this.Name == "Cascade Bluffs"
						|| this.Name == "Rugged Prairie"
						|| this.Name == "Twilight Mire"

						// Shadowmoor
						|| this.Name == "Wooded Bastion"
						|| this.Name == "Graven Cairns"
						|| this.Name == "Fire-Lit Thicket"
						|| this.Name == "Mystic Gate"
						|| this.Name == "Sunken Ruins"
						) {
							specialType = "Filter Land";
						}
					}

					#endregion

					#region Selection Land Destruction Lands

					// Land Destruction Lands
					if(specialType.IsNullOrTrimmedEmpty()) {
						if(this.Name == "Strip Mine"
						|| this.Name == "Wasteland"
						|| this.Name == "Tectonic Edge"
						|| this.Name == "Ghost Quarter"
						|| this.Name == "Dust Bowl"
						|| this.Name == "Rishadan Port"
						|| this.Name == "Wintermoon Mesa"
						) {
							specialType = "Land Destruction Lands";
						}
					}

					#endregion

					#region Selection Utility Lands

					// Utility Lands
					if(specialType.IsNullOrTrimmedEmpty()) {
						if(this.Name == "Maze of Ith"
						|| this.Name == "Karakas"
						|| this.Name == "Bojuka Bog"
						|| this.Name == "Halimar Depths"
						|| this.Name == "Tower of the Magistrate"
						|| this.Name == "Halimar Depths"
						|| this.Name == "Academy Ruins"
						|| this.Name == "Halimar Depths"
						|| this.Name == "Volrath's Stronghold"
						|| this.Name == "Riptide Laboratory"
						) {
							specialType = "Utility Land";
						}
					}

					#endregion

					#region Selection Manlands

					// Manlands
					if(specialType.IsNullOrTrimmedEmpty()) {
						if(
							// Worldwake
						this.Name == "Celestial Colonnade"
						|| this.Name == "Creeping Tar Pit"
						|| this.Name == "Lavaclaw Reaches"
						|| this.Name == "Raging Ravine"
						|| this.Name == "Stirring Wildwood"
						|| this.Name == "Dread Statuary"

						// All other
						|| this.Name == "Blinkmoth Nexus"
						|| this.Name == "Inkmoth Nexus"
						|| this.Name == "Faerie Conclave"
						|| this.Name == "Forbidding Watchtower"
						|| this.Name == "Gargoyle Castle"
						|| this.Name == "Ghitu Encampment"
						|| this.Name == "Mishra's Factory"
						|| this.Name == "Mutavault"
						|| this.Name == "Spawning Pool"
						|| this.Name == "Stalking Stones"
						|| this.Name == "Treetop Village"
						|| this.Name == "Zoetic Cavern"
						) {
							specialType = "Man Land";
						}
					}

					#endregion

					#region Selection Fetch Lands

					// Fetch Lands
					if(specialType.IsNullOrTrimmedEmpty()) {
						if(
						// Zendikar
						this.Name == "Marsh Flats"
						|| this.Name == "Arid Mesa"
						|| this.Name == "Scalding Tarn"
						|| this.Name == "Verdant Catacombs"
						|| this.Name == "Misty Rainforest"

						// Onslaught
						|| this.Name == "Flooded Strand"
						|| this.Name == "Windswept Heath"
						|| this.Name == "Wooded Foothills"
						|| this.Name == "Polluted Delta"
						|| this.Name == "Bloodstained Mire"

						// Double Prints
						|| this.Name == "Evolving Wilds"
						|| this.Name == "Terramorphic Expanse"
						|| this.Name == "Krosan Verge"
						|| this.Name == "Terminal Moraine"

						// Alara Panorama
						|| this.Name == "Bant Panorama"
						|| this.Name == "Esper Panorama"
						|| this.Name == "Grixis Panorama"
						|| this.Name == "Jund Panorama"
						|| this.Name == "Naya Panorama"

						// Mirage
						|| this.Name == "Bad River"
						|| this.Name == "Flood Plain"
						|| this.Name == "Grasslands"
						|| this.Name == "Mountain Valley"
						|| this.Name == "Rocky Tar Pit"
						) {
							specialType = "Fetch Land";
						}
					}

					#endregion

					#region Selection Mana Creature

					// Mana Creature
					if(specialType.IsNullOrTrimmedEmpty()) {
						if(
							// "Manaelf"
						this.Name == "Birds of Paradise"
						|| this.Name == "Noble Hierarch"
						|| this.Name == "Avacyn's Pilgrim"
						|| this.Name == "Boreal Druid"
						|| this.Name == "Dryad Arbor"
						|| this.Name == "Elves of Deep Shadow"
						|| this.Name == "Fyndhorn Elves"
						|| this.Name == "Joraga Treespeaker"
						|| this.Name == "Llanowar Elves"
						|| this.Name == "Orcish Lumberjack"

						// Special
						|| this.Name == "Lotus Cobra"
						|| this.Name == "Bloom Tender"
						|| this.Name == "Utopia Tree"
						|| this.Name == "Devoted Druid"
						|| this.Name == "Druid of the Anima"
						|| this.Name == "Elvish Archdruid"
						|| this.Name == "Fyndhorn Elder"
						|| this.Name == "Greenweaver Druid"
						|| this.Name == "Leaf Gilder"
						|| this.Name == "Llanowar Dead"
						|| this.Name == "Magus of the Library"
						|| this.Name == "Manakin"
						|| this.Name == "Metalworker"
						|| this.Name == "Millikin"
						|| this.Name == "Nantuko Elder"
						|| this.Name == "Orochi Sustainer"
						|| this.Name == "Overgrown Battlement"
						|| this.Name == "Priest of Titania"
						|| this.Name == "Radha, Heir to Keld"
						|| this.Name == "Rofellos, Llanowar Emissary"
						|| this.Name == "Scorned Villager"
						|| this.Name == "Scuttlemutt"
						|| this.Name == "Sea Scryer"
						|| this.Name == "Sisters of the Flame"
						|| this.Name == "Skyshroud Elf"
						|| this.Name == "Skyshroud Troopers"
						|| this.Name == "Steward of Valeron"
						|| this.Name == "Sunseed Nurturer"
						|| this.Name == "Urborg Elf"
						|| this.Name == "Vine Trellis"
						|| this.Name == "Viridian Joiner"
						|| this.Name == "Werebear"
						|| this.Name == "Wirewood Elf"
						|| this.Name == "Wall of Roots"

						// Myr
						|| this.Name == "Alloy Myr"
						|| this.Name == "Copper Myr"
						|| this.Name == "Gold Myr"
						|| this.Name == "Iron Myr"
						|| this.Name == "Leaden Myr"
						|| this.Name == "Palladium Myr"
						|| this.Name == "Plague Myr"
						|| this.Name == "Silver Myr"

						// Rest
						|| this.Name == "Elvish Harbinger"
						|| this.Name == "Gemhide Sliver"
						|| this.Name == "Harabaz Druid"
						|| this.Name == "Harvester Druid"
						|| this.Name == "Lotus Guardian"
						|| this.Name == "Mul Daya Channelers"
						|| this.Name == "Quirion Elves"
						|| this.Name == "Quirion Explorer"
						|| this.Name == "Scuttlemutt"
						|| this.Name == "Silhana Starfletcher"
						|| this.Name == "Sylvok Explorer"
						|| this.Name == "Vedalken Engineer"
						|| this.Name == "Vesper Ghoul"
						|| this.Name == "Wirewood Channeler"
						|| this.Name == "Blightsoil Druid"
						|| this.Name == "Drumhunter"
						) {
							specialType = "Mana Creature";
						}
					}

					#endregion

					if(specialType.IsNullOrTrimmedEmpty()) {
						specialType = "Zentral Type";
					}

					return specialType;
				}
				else {
					return "Zentral Type";
				}
			}
		}

		/// <summary>
		/// Represents the main type of a card
		/// </summary>
		public string MainType {
			get {
				if(this.Type != null) {
					string mainType = "";

					// Split on "-"
					if(this.Type.Contains("-")) {
						mainType = this.Type.Substring(0, this.Type.IndexOf("-")).Trim();
					}
					else {
						mainType = this.Type;
					}

					// "Enchannt XXX" == "Enchantment"
					if(mainType == "Enchant Player") {
						mainType = "Enchantment";
					}

					// Unhinged & Unglued: Scariest Creature You'll Ever See
					if(mainType == "Scariest Creature You'll Ever See") {
						mainType = "Creature";
					}

					// Dryad Arbor
					if(mainType.StartsWith("Land Creature")) {
						//mainType = "Land";
						mainType = "Creature";
					}

					// Split on " "
					if(mainType.Contains(" ")) {
						//mainType = tmpMainType.Substring(0, tmpMainType.IndexOf(" ")).Trim();
						mainType = mainType.Substring(mainType.LastIndexOf(" ")).Trim();
					}

					// Token
					if(mainType.IsNullOrTrimmedEmpty()) {
						mainType = "Token";
					}

					// "Summon" == old value for "Creature"
					if(mainType == "Summon") {
						mainType = "Creature";
					}

					// "Interrupt" == old value for "Instant"
					if(mainType == "Interrupt") {
						mainType = "Instant";
					}

					// Snow lands are also (basic) lands
					if(mainType.Contains("Snow")) {
						mainType = mainType.Replace("Snow", "").Trim();
					}

					// Unhinged & Unglued: Eaturecray
					if(mainType == "Eaturecray") {
						mainType = "Creature";
					}

					if(!mainType.Equals("Instant")
					&& !mainType.Equals("Sorcery")
					&& !mainType.Equals("Creature")
					&& !mainType.Equals("Artifact")
					&& !mainType.Equals("Land")
					&& !mainType.Equals("Token")
					&& !mainType.Equals("Vanguard")
					&& !mainType.Equals("Plane")
					&& !mainType.Equals("Scheme")
					&& !mainType.Equals("Enchantment")
					&& !mainType.Equals("Planeswalker")) {
						Console.WriteLine(mainType);
					}

					//if(mainType.Contains("Instant")) {
					//}

					return mainType;
				}
				else {
					return "";
				}
			}
		}

		/// <summary>
		/// Create a new instance of <see cref="Card"/>
		/// </summary>
		public Card() {
			this.EditionPictures = new Dictionary<Edition, EditionImage>();
			this.Color = new List<CardColor>();
			//this.Abilities = new List<Ability>();

			this._listOfMana = new List<ManaItem>();
		}

		/// <summary>
		/// Get a string that represents the color of the card
		/// </summary>
		/// <returns></returns>
		public string GetColorString() {
			StringBuilder str = new StringBuilder();

			if(this.Color != null
			&& this.Color.Count > 0) {
				foreach(CardColor cc in this.Color) {
					str.Append(cc.Symbol.ToString());
				}

				str.Append(" (");

				foreach(CardColor cc in this.Color) {
					str.Append(cc.Name);
					str.Append(", ");
				}

				str.Append(")");
			}

			str = str.Replace(", )", ")");
			str = str.Replace("; )", ")");
			str = str.Replace("; ; ", "; ");

			return str.ToString();
		}

		/// <summary>
		/// The the name of the transformed card, if the card is transformable
		/// </summary>
		/// <returns></returns>
		public string GetTransformedName() {
			// Innistrad
			switch(this.Name) {
				// Untransformed cards
				case "Cloistered Youth": return "Unholy Fiend";
				case "Thraben Sentry": return "Thraben Militia";
				case "Civilized Scholar": return "Homicidal Brute";
				case "Delver of Secrets": return "Insectile Aberration";
				case "Ludevic's Test Subject": return "Ludevic's Abomination";
				case "Bloodline Keeper": return "Lord of Lineage";
				case "Screeching Bat": return "Stalking Vampire";
				case "Hanweir Watchkeep": return "Bane of Hanweir";
				case "Instigator Gang": return "Wildblood Pack";
				case "Kruin Outlaw": return "Terror of Kruin Pass";
				case "Rechless Waif": return "Merciless Predator";
				case "Tormented Pariah": return "Rampaging Werewolf";
				case "Village Ironsmith": return "Ironfang";
				case "Daybreak Ranger": return "Nightfall Predator";
				case "Garruk Relentless": return "Garruk, the Veil-Cursed";
				case "Gatstaf Shepherd": return "Gatstaf Howler";
				case "Grizzled Outcasts": return "Krallenhorde Wantons";
				case "Mayor of Avabruck": return "Howlpack Alpha";
				case "Ulvenwald Mystics": return "Ulvenwald Primordials";
				case "Villagers of Estwald": return "Howlpack of Estwald";

				// Transformed cards
				case "Unholy Fiend": return "Cloistered Youth";
				case "Thraben Militia": return "Thraben Sentry";
				case "Homicidal Brute": return "Civilized Scholar";
				case "Insectile Aberration": return "Delver of Secrets";
				case "Ludevic's Abomination": return "Ludevic's Test Subject";
				case "Lord of Lineage": return "Bloodline Keeper";
				case "Stalking Vampire": return "Screeching Bat";
				case "Bane of Hanweir": return "Hanweir Watchkeep";
				case "Wildblood Pack": return "Instigator Gang";
				case "Terror of Kruin Pass": return "Kruin Outlaw";
				case "Merciless Predator": return "Rechless Waif";
				case "Rampaging Werewolf": return "Tormented Pariah";
				case "Ironfang": return "Village Ironsmith";
				case "Nightfall Predator": return "Daybreak Ranger";
				case "Garruk, the Veil-Cursed": return "Garruk Relentless";
				case "Gatstaf Howler": return "Gatstaf Shepherd";
				case "Krallenhorde Wantons": return "Grizzled Outcasts";
				case "Howlpack Alpha": return "Mayor of Avabruck";
				case "Ulvenwald Primordials": return "Ulvenwald Mystics";
				case "Howlpack of Estwald": return "Villagers of Estwald";
			}

			// Dark Ascension
			switch(this.Name) {
				// Untransformed cards
				case "Afflicted Deserter": return "Werewolf Ransacker";
				case "Chalice of Life": return "Chalice of Death";
				case "Chosen of Markov": return "Markov's Servant";
				case "Elbrus, the Binding Blade": return "Withengar Unbound";
				case "Hinterland Hermit": return "Hinterland Scourge";
				case "Huntmaster of the Fells": return "Ravager of the Fells";
				case "Wolfbitten Captive": return "Krallenhorde Killer";
				case "Lambholt Elder": return "Silverpelt Werewolf";
				case "Loyal Cathar": return "Unhallowed Cathar";
				case "Mondronen Shaman": return "Tovolar's Magehunter";
				case "Scorned Villager": return "Moonscarred Werewolf";
				case "Ravenous Demon": return "Archdemon of Greed";
				case "Soul Seizer": return "Ghastly Haunting";

				// Transformed cards
				case "Werewolf Ransacker": return "Afflicted Deserter";
				case "Chalice of Death": return "Chalice of Life";
				case "Markov's Servant": return "Chosen of Markov";
				case "Withengar Unbound": return "Elbrus, the Binding Blade";
				case "Hinterland Scourge": return "Hinterland Hermit";
				case "Ravager of the Fells": return "Huntmaster of the Fells";
				case "Krallenhorde Killer": return "Wolfbitten Captive";
				case "Silverpelt Werewolf": return "Lambholt Elder";
				case "Unhallowed Cathar": return "Loyal Cathar";
				case "Tovolar's Magehunter": return "Mondronen Shaman";
				case "Moonscarred Werewolf": return "Scorned Villager";
				case "Archdemon of Greed": return "Ravenous Demon";
				case "Ghastly Haunting": return "Soul Seizer";
			}

			return null;
		}

		/// <summary>
		/// Get a string that represents the current object
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			StringBuilder str = new StringBuilder();

			str.Append(this.Name);

			if(this.Type != null
			|| !this.CastingCost.IsNullOrTrimmedEmpty()
				//|| !this.Color.IsNullOrTrimmedEmpty()) {
			|| (this.Color != null && this.Color.Count > 0)) {
				str.Append(" (");

				if(!this.Type.IsNullOrTrimmedEmpty()) {
					str.Append(this.Type);
					str.Append("; ");
				}

				if(!this.CastingCost.IsNullOrTrimmedEmpty()) {
					str.Append(this.CastingCost);
					str.Append("; ");
				}

				//if(!this.Color.IsNullOrTrimmedEmpty()) {
				//    str.Append(this.Color);
				//    str.Append("; ");
				//}

				if(this.Color != null
				&& this.Color.Count > 0) {
					str.Append("; ");

					foreach(CardColor cc in this.Color) {
						str.Append(cc.Name);
						str.Append(", ");
					}
				}

				str.Append(")");

				str = str.Replace(", )", ")");
				str = str.Replace("; )", ")");
				str = str.Replace("; ; ", "; ");
			}

			return str.ToString();
		}

		/// <summary>
		/// Finds each mana item in the description and adds it to the list of creatable mana
		/// </summary>
		private void FindManaItem() {
			this._listOfMana.Clear();

			if(this.SpecialType == "Basic Land") {
				#region Basic Land

				if(this.Name == "Island") {
					this._listOfMana.Add(new ManaItem("U"));
				}
				else if(this.Name == "Plains") {
					this._listOfMana.Add(new ManaItem("W"));
				}
				else if(this.Name == "Swamp") {
					this._listOfMana.Add(new ManaItem("S"));
				}
				else if(this.Name == "Mountain") {
					this._listOfMana.Add(new ManaItem("M"));
				}
				else if(this.Name == "Forest") {
					this._listOfMana.Add(new ManaItem("G"));
				}

				#endregion
			}
			else if(this.SpecialType == "Dual Land") {
				#region Dual Land

				if(this.Name == "Savannah"
				|| this.Name == "Temple Garden") {
					this._listOfMana.Add(new ManaItem("W"));
					this._listOfMana.Add(new ManaItem("G"));
				}
				else if(this.Name == "Tropical Island"
				|| this.Name == "Breeding Pool") {
					this._listOfMana.Add(new ManaItem("U"));
					this._listOfMana.Add(new ManaItem("G"));
				}
				else if(this.Name == "Taiga"
				|| this.Name == "Stomping Ground") {
					this._listOfMana.Add(new ManaItem("R"));
					this._listOfMana.Add(new ManaItem("G"));
				}
				else if(this.Name == "Plateau"
				|| this.Name == "Sacred Foundry") {
					this._listOfMana.Add(new ManaItem("W"));
					this._listOfMana.Add(new ManaItem("R"));
				}
				else if(this.Name == "Tundra"
				|| this.Name == "Hallowed Fountain") {
					this._listOfMana.Add(new ManaItem("W"));
					this._listOfMana.Add(new ManaItem("U"));
				}
				else if(this.Name == "Vulcanic Island"
				|| this.Name == "Steam Vents") {
					this._listOfMana.Add(new ManaItem("R"));
					this._listOfMana.Add(new ManaItem("U"));
				}
				else if(this.Name == "Underground Sea"
				|| this.Name == "Watery Grave") {
					this._listOfMana.Add(new ManaItem("B"));
					this._listOfMana.Add(new ManaItem("U"));
				}
				else if(this.Name == "Badlands"
				|| this.Name == "Blood Crypt") {
					this._listOfMana.Add(new ManaItem("B"));
					this._listOfMana.Add(new ManaItem("R"));
				}
				else if(this.Name == "Bayou"
				|| this.Name == "Overgrown Tomb") {
					this._listOfMana.Add(new ManaItem("B"));
					this._listOfMana.Add(new ManaItem("G"));
				}
				else if(this.Name == "Scrubland"
				|| this.Name == "Godless Shrine") {
					this._listOfMana.Add(new ManaItem("B"));
					this._listOfMana.Add(new ManaItem("W"));
				}

				#endregion
			}
			else if(this.SpecialType == "Fetch Land") {
				#region Fetch Land

				if(this.Name == "Marsh Flats") {
					this._listOfMana.Add(new ManaItem("W"));
					this._listOfMana.Add(new ManaItem("B"));
				}
				else if(this.Name == "Arid Mesa") {
					this._listOfMana.Add(new ManaItem("W"));
					this._listOfMana.Add(new ManaItem("R"));
				}
				else if(this.Name == "Scalding Tarn") {
					this._listOfMana.Add(new ManaItem("U"));
					this._listOfMana.Add(new ManaItem("R"));
				}
				else if(this.Name == "Verdant Catacombs") {
					this._listOfMana.Add(new ManaItem("B"));
					this._listOfMana.Add(new ManaItem("G"));
				}
				else if(this.Name == "Misty Rainforest") {
					this._listOfMana.Add(new ManaItem("U"));
					this._listOfMana.Add(new ManaItem("G"));
				}
				else if(this.Name == "Flooded Strand") {
					this._listOfMana.Add(new ManaItem("U"));
					this._listOfMana.Add(new ManaItem("W"));
				}
				else if(this.Name == "Windswept Heath") {
					this._listOfMana.Add(new ManaItem("G"));
					this._listOfMana.Add(new ManaItem("W"));
				}
				else if(this.Name == "Wooded Foothills") {
					this._listOfMana.Add(new ManaItem("G"));
					this._listOfMana.Add(new ManaItem("R"));
				}
				else if(this.Name == "Polluted Delta") {
					this._listOfMana.Add(new ManaItem("B"));
					this._listOfMana.Add(new ManaItem("U"));
				}
				else if(this.Name == "Bloodstained Mire") {
					this._listOfMana.Add(new ManaItem("B"));
					this._listOfMana.Add(new ManaItem("R"));
				}

				//        // Double Prints
				//        || this.Name == "Evolving Wilds"
				//        || this.Name == "Terramorphic Expanse"
				//        || this.Name == "Krosan Verge"
				//        || this.Name == "Terminal Moraine"

				//        // Alara Panorama
				//        || this.Name == "Bant Panorama"
				//        || this.Name == "Esper Panorama"
				//        || this.Name == "Grixis Panorama"
				//        || this.Name == "Jund Panorama"
				//        || this.Name == "Naya Panorama"

				//        // Mirage
				//        || this.Name == "Bad River"
				//        || this.Name == "Flood Plain"
				//        || this.Name == "Grasslands"
				//        || this.Name == "Mountain Valley"
				//        || this.Name == "Rocky Tar Pit"

				#endregion
			}
			else if(this.SpecialType == "All Mana Land") {
				this._listOfMana.Add(new ManaItem("A"));
			}
			else {
				Regex regex = new Regex(
					"(add)+.*?(mana)+.*?(pool)+",
					RegexOptions.IgnoreCase
				);

				if(regex.IsMatch(this.Text)) {
					if(this.Text.Contains("{W}")) {
						this._listOfMana.Add(new ManaItem("W"));
					}

					if(this.Text.Contains("{U}")) {
						this._listOfMana.Add(new ManaItem("U"));
					}

					if(this.Text.Contains("{B}")) {
						this._listOfMana.Add(new ManaItem("B"));
					}

					if(this.Text.Contains("{R}")) {
						this._listOfMana.Add(new ManaItem("R"));
					}

					if(this.Text.Contains("{G}")) {
						this._listOfMana.Add(new ManaItem("G"));
					}
					
					if(this.Text.Contains("{C}")
					|| this.Text.Contains("{X}")) {
						this._listOfMana.Add(new ManaItem("C"));
					}
					
					//this._listOfMana = 
				}
			}
		}
	}
}
