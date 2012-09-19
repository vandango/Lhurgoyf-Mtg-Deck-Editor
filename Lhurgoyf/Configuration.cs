using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Toenda.Foundation;

namespace Toenda.Lhurgoyf {
	public static class Configuration {
		public static List<string> LastFiles { get; set; }
		public static bool ShowImage { get; set; }

		static Configuration() {
			LastFiles = new List<string>();

			StreamReader reader = new StreamReader(@"data\config.xml");

			string buffer = reader.ReadToEnd();

			reader.Close();
			reader.Dispose();

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(buffer);

			XmlNodeList fileNodeList = doc.SelectNodes("//lhurgoyf/lastFiles/file");

			foreach(XmlNode node in fileNodeList) {
				if(File.Exists(node.InnerText)) {
					LastFiles.Add(node.InnerText);
				}
			}

			XmlNodeList configNodeList = doc.SelectNodes("//lhurgoyf/config");

			foreach(XmlNode node in configNodeList) {
				if(node.HasChildNodes) {
					foreach(XmlNode sub in node.ChildNodes) {
						switch(sub.Name) {
							case "showImage":
								ShowImage = sub.InnerText.IsExpressionTrue();
								break;

							default:
								break;
						}
					}
				}
			}
		}

		public static void Save() {
			//<?xml version="1.0" encoding="UTF-8"?>
			//<lhurgoyf>
			//    <lastFiles>
			//        <file>C:\data\Dropbox\MTG\Type 1.5 - Legacy\GW Maverick.dec</file>
			//    </lastFiles>
			//    <config>
			//        <showImage>1</showImage>
			//    </config>
			//</lhurgoyf>

			StringBuilder str = new StringBuilder();

			str.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			str.AppendLine("<lhurgoyf>");
			str.AppendLine("\t<lastFiles>");

			if(LastFiles != null 
			&& LastFiles.Count > 0) {
				foreach(string file in LastFiles) {
					str.Append("\t\t<file>");
					str.Append(file);
					str.Append("</file>");
					str.AppendLine();
				}
			}

			str.AppendLine("\t</lastFiles>");
			str.AppendLine("\t<config>");

			// showImage
			str.Append("\t\t<showImage>");
			str.Append((ShowImage ? "1" : "0"));
			str.Append("</showImage>");
			str.AppendLine();

			str.AppendLine("\t</config>");
			str.AppendLine("</lhurgoyf>");

			StreamWriter writer = new StreamWriter(@"data\config.xml", false);
			writer.Write(str.ToString());
			writer.Flush();
			writer.Close();
			writer.Dispose();
		}
	}
}
