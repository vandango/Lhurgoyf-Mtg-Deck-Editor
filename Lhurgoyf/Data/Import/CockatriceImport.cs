using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Toenda.Foundation;
using Toenda.Lhurgoyf.Utility;
using System.Net;
using System.Reflection;

namespace Toenda.Lhurgoyf.Data.Import {
	public static class CockatriceImport {
		public delegate void LoaderEventHandler(LoaderEventArgs args);
		public static event LoaderEventHandler LoaderResponse;

		public delegate void LoaderFinishEventhandler(LoaderFinishEventArgs args);
		public static event LoaderFinishEventhandler LoaderFinish;

		public static int MaxItems { get; private set; }

		private static XmlDocument _xmlDoc;

		static CockatriceImport() {
			StreamReader reader = new StreamReader(@"data\cards.xml");

			string buffer = reader.ReadToEnd();

			reader.Close();
			reader.Dispose();

			_xmlDoc = new XmlDocument();
			_xmlDoc.LoadXml(buffer);

			XmlNodeList setNodeList = _xmlDoc.SelectNodes("//cockatrice_carddatabase/sets/set");
			XmlNodeList cardNodeList = _xmlDoc.SelectNodes("//cockatrice_carddatabase/cards/card");

			MaxItems = setNodeList.Count + cardNodeList.Count;
		}

		public static void Run() {
			//StreamReader reader = new StreamReader(@"data\cards.xml");

			//string buffer = reader.ReadToEnd();

			//reader.Close();
			//reader.Dispose();

			//XmlDocument doc = new XmlDocument();
			//doc.LoadXml(buffer);

			int index = 0;
			bool setsLoaded = false;

			XmlNodeList mainNodeList = _xmlDoc.SelectNodes("//cockatrice_carddatabase");
			int globalIndex = 0;

			foreach(XmlNode node in mainNodeList) {
				if(node.HasChildNodes) {
					foreach(XmlNode sub in node.ChildNodes) {
						switch(sub.Name) {
							case "sets":
								/*
								 * SETS
								 * */
								if(sub.HasChildNodes) {
									foreach(XmlNode item in sub.ChildNodes) {
										XmlNode nameNode = item.ChildNodes[0];
										XmlNode longNameNode = item.ChildNodes[1];

										// correct some edition short names
										string shortName = nameNode.InnerText;

										//if(shortName == "COM") {
										//    shortName = "CMD";
										//}

										Edition edition = new Edition();
										edition.Id = index;
										edition.Name = longNameNode.InnerText;
										edition.Shortname = shortName;

										index++;

										Toenda.Lhurgoyf.Data.CardBase.Editions.Add(edition);

										// raise event
										if(LoaderResponse != null) {
											LoaderResponse(new LoaderEventArgs(
												null,
												edition,
												globalIndex,
												MaxItems
											));
										}
									}
								}
								break;

							case "cards":
								/*
								 * CARDS
								 * */
								if(!setsLoaded) {
									index = 0;
								}

								setsLoaded = true;

								if(sub.HasChildNodes) {
									foreach(XmlNode item in sub.ChildNodes) {
										if(item.HasChildNodes) {
											Card card = new Card();

											card.Id = index;
											//card.Color = "";

											foreach(XmlNode single in item.ChildNodes) {
												switch(single.Name) {
													case "name":
														card.Name = single.InnerText;
														break;

													case "set":
														string editionShortName = single.InnerText;

														//if(editionShortName == "COM") {
														//    editionShortName = "CMD";
														//}

														string picture = single.Attributes["picURL"].InnerText;

														List<Edition> editions = CardFinder.FindEditionByName(editionShortName);

														if(editions != null
														&& editions.Count > 0) {
															foreach(Edition edition in editions) {
																card.EditionPictures.Add(
																	edition,
																	new EditionImage() {
																		Url = new Uri(picture),
																		Edition = edition,
																		Card = card
																	}
																);

																//var ediPics =
																//    from ediItem in card.EditionPictures
																//    where ediItem.Key.Id == edition.Id
																//    && ediItem.Key.Name == edition.Name
																//    && ediItem.Key.Shortname == edition.Shortname
																//    select ediItem;

																//if(ediPics.Count() == 0) {
																//    card.EditionPictures.Add(
																//        edition,
																//        new EditionImage() {
																//            Url = new Uri(picture),
																//            //BitmapStream = file, 
																//            Edition = edition,
																//            Card = card
																//        }
																//    );
																//}
																//else {
																//    card.EditionPictures[edition] = new EditionImage() {
																//        Url = new Uri(picture),
																//        //BitmapStream = file,
																//        Edition = edition,
																//        Card = card
																//    };
																//}
															}
														}
														break;

													case "color":
														card.Color.Add(new CardColor() { Symbol = single.InnerText.ToEnum<ColorSymbol>() });
														break;

													case "manacost":
														card.CastingCost = single.InnerText;
														break;

													case "type":
														card.Type = single.InnerText;
														break;

													case "pt":
														card.PowerThoughness = single.InnerText;
														break;

													case "tablerow":
														break;

													case "text":
														card.Text = single.InnerText;
														break;

													default:
														break;
												}
											}

											index++;

											//card = AddAdditionalInformation(card);

											Toenda.Lhurgoyf.Data.CardBase.Cards.Add(card);

											// raise event
											if(LoaderResponse != null) {
												LoaderResponse(new LoaderEventArgs(
													card,
													null,
													globalIndex,
													MaxItems
												));
											}
										}
									}
								}
								break;
						}

						globalIndex++;
					}
				}
			}

			//XmlNodeList setNodeList = doc.SelectNodes("//cockatrice_carddatabase/sets/set");

			//foreach(XmlNode node in setNodeList) {
			//    if(node.HasChildNodes) {
			//        /*
			//         * Nodes look like this:
			//         * 
			//            <set>
			//                <name>5E</name>
			//                <longname>Fifth Edition</longname>
			//            </set>
			//         * */

			//        XmlNode nameNode = node.ChildNodes[0];
			//        XmlNode longNameNode = node.ChildNodes[1];

			//        // correct some edition short names
			//        string shortName = nameNode.InnerText;

			//        //if(shortName == "COM") {
			//        //    shortName = "CMD";
			//        //}

			//        Edition edition = new Edition();
			//        edition.Id = index;
			//        edition.Name = longNameNode.InnerText;
			//        edition.Shortname = shortName;

			//        index++;

			//        Toenda.Lhurgoyf.Data.CardBase.Editions.Add(edition);
			//    }
			//}

			//index = 0;

			//XmlNodeList cardNodeList = doc.SelectNodes("//cockatrice_carddatabase/cards/card");

			//foreach(XmlNode node in cardNodeList) {
			//    if(node.HasChildNodes) {
			//        /*
			//         * Nodes look like this:
			//         * 
			//            <card>
			//                <name>Counterspell</name>
			//                <set picURL="http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=14511&amp;type=card" picURLHq="" picURLSt="">6E</set>
			//                <set picURL="http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=185820&amp;type=card" picURLHq="" picURLSt="">DD2</set>
			//                <set picURL="http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=3898&amp;type=card" picURLHq="" picURLSt="">5E</set>
			//                <set picURL="http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=2148&amp;type=card" picURLHq="" picURLSt="">4E</set>
			//                <set picURL="http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=2500&amp;type=card" picURLHq="" picURLSt="">IA</set>
			//                <set picURL="http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=102&amp;type=card" picURLHq="" picURLSt="">A</set>
			//                <set picURL="http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=397&amp;type=card" picURLHq="" picURLSt="">B</set>
			//                <set picURL="http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=19570&amp;type=card" picURLHq="" picURLSt="">MM</set>
			//                <set picURL="http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=1196&amp;type=card" picURLHq="" picURLSt="">R</set>
			//                <set picURL="http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=11214&amp;type=card" picURLHq="" picURLSt="">7E</set>
			//                <set picURL="http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=20382&amp;type=card" picURLHq="" picURLSt="">ST</set>
			//                <set picURL="http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=25503&amp;type=card" picURLHq="" picURLSt="">ST2K</set>
			//                <set picURL="http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=4693&amp;type=card" picURLHq="" picURLSt="">TE</set>
			//                <set picURL="http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=699&amp;type=card" picURLHq="" picURLSt="">U</set>
			//                <color>U</color>
			//                <manacost>UU</manacost>
			//                <type>Instant</type>
			//                <tablerow>3</tablerow>
			//                <text>Counter target spell.</text>
			//            </card>
			//         * */

			//        Card card = new Card();

			//        card.Id = index;
			//        //card.Color = "";

			//        foreach(XmlNode sub in node.ChildNodes) {
			//            switch(sub.Name) {
			//                case "name":
			//                    card.Name = sub.InnerText;
			//                    break;

			//                case "set":
			//                    string editionShortName = sub.InnerText;

			//                    //if(editionShortName == "COM") {
			//                    //    editionShortName = "CMD";
			//                    //}

			//                    string picture = sub.Attributes["picURL"].InnerText;

			//                    List<Edition> editions = CardFinder.FindEditionByName(editionShortName);
								
			//                    if(editions != null
			//                    && editions.Count > 0) {
			//                        foreach(Edition edition in editions) {
			//                            var ediPics =
			//                                from item in card.EditionPictures
			//                                where item.Key.Id == edition.Id
			//                                && item.Key.Name == edition.Name
			//                                && item.Key.Shortname == edition.Shortname
			//                                select item;

			//                            //Assembly thisExe = Assembly.GetExecutingAssembly();
			//                            //Stream file = thisExe.GetManifestResourceStream("Toenda.Lhurgoyf.Resources.EmptyCard.bmp");

			//                            if(ediPics.Count() == 0) {
			//                                card.EditionPictures.Add(
			//                                    edition, 
			//                                    new EditionImage() { 
			//                                        Url = new Uri(picture),
			//                                        //BitmapStream = file, 
			//                                        Edition = edition, 
			//                                        Card = card
			//                                    }
			//                                );
			//                            }
			//                            else {
			//                                card.EditionPictures[edition] = new EditionImage() {
			//                                    Url = new Uri(picture),
			//                                    //BitmapStream = file,
			//                                    Edition = edition,
			//                                    Card = card
			//                                };
			//                            }
			//                        }
			//                    }
			//                    break;

			//                case "color":
			//                    card.Color.Add(new CardColor() { Symbol = sub.InnerText.ToEnum<ColorSymbol>() });
			//                    //card.Color += sub.InnerText;
			//                    break;

			//                case "manacost":
			//                    card.CastingCost = sub.InnerText;
			//                    break;

			//                case "type":
			//                    card.Type = sub.InnerText;
			//                    break;

			//                case "pt":
			//                    card.PowerThoughness = sub.InnerText;
			//                    break;

			//                case "tablerow":
			//                    break;

			//                case "text":
			//                    card.Text = sub.InnerText;
			//                    break;

			//                default:
			//                    break;
			//            }
			//        }

			//        index++;

			//        Toenda.Lhurgoyf.Data.CardBase.Cards.Add(card);

			//        //Console.WriteLine("card {0} of {1} added...", index, cardNodeList.Count);
			//    }
			//}

			if(LoaderFinish != null) {
				LoaderFinish(new LoaderFinishEventArgs("Card database successfull loaded!"));
			}
		}

		public static Card AddAdditionalInformation(Card card) {
			string response = GetUrlResponse("http://gatherer.wizards.com/pages/search/default.aspx?name=+[\""
				+ card.Name
				+ "\"]");

			// flavour
			if(response.Contains("<div class=\"label\">Flavor Text:</div>")) {
				int flavourStart = response.IndexOf(
					"<div class=\"cardtextbox\"><i>",
					response.IndexOf("<div class=\"label\">Flavor Text:</div>")
				) + 28;
				int flavourLength = response.IndexOf("</i></div></div></div>", flavourStart) - flavourStart;

				string flavour = response.Substring(flavourStart, flavourLength);
				card.Flavour = flavour;
			}

			// artist
			try {
				int artistStart = response.IndexOf("<a href=\"/Pages/Search/Default.aspx?action=advanced&amp;artist=[%22") + 67;
				int artistLength = response.IndexOf("%22]\">", artistStart) - artistStart;

				string artist = response.Substring(artistStart, artistLength);
				card.Artist = artist;
			}
			catch(Exception ex) {
				Console.WriteLine(ex.ToString());
			}

			// rarity
			if(response.Contains("<div class=\"label\">Rarity:</div>")) {
				int rarityStart = response.IndexOf(
					"<div class=\"label\">Rarity:</div><div class=\"value\">"
				) + 51;
				int rarityLength = response.IndexOf("</span></div></div>", rarityStart) - rarityStart;

				string rarity = response.Substring(rarityStart, rarityLength);

				rarity = rarity.Substring(rarity.IndexOf("'>") + 2);

				card.Rarity = rarity.ToEnum<Rarity>();
			}

			return card;
		}

		private static string GetUrlResponse(string url) {
			string content = null;

			WebRequest webRequest = WebRequest.Create(url);
			WebResponse webResponse = webRequest.GetResponse();
			StreamReader sr = new StreamReader(webResponse.GetResponseStream(), Encoding.ASCII);

			StringBuilder contentBuilder = new StringBuilder();
			while(-1 != sr.Peek()) {
				contentBuilder.Append(sr.ReadLine());
				contentBuilder.Append("\r\n");
			}
			content = contentBuilder.ToString();

			return content.ToString();
		}
	}
}
