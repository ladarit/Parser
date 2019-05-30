using System;
using System.Collections.Generic;
using System.Linq;
using GovernmentParse.Services;
using Newtonsoft.Json;

namespace GovernmentParse.DataProviders
{
	public static class ConvocationDeterminant
	{
		public static readonly int ConvocationNumberForForm;

		static ConvocationDeterminant()
		{
			ConvocationNumberForForm = DetermineConvocation();
		}

		/// <summary>
		/// метод возвращает номер текущего созыва
		/// </summary>
		/// <returns></returns>
		public static int DetermineConvocation()
		{
			int convNumber = 0;
			var urls = new IniReader().AutoReadIni();
			var responce = HtmlProvider.GetResponse<string>("https://data.rada.gov.ua/ogd/mps/info/list.json", true);
			if (responce.Error == null && !string.IsNullOrEmpty(responce.ReceivedData))
			{
				var example = JsonConvert.DeserializeObject<Example>(responce.ReceivedData);
				var numbers = example.item.Select(i => int.Parse(i.id.Replace("mps-skl", ""))).ToList();
				convNumber = numbers.Max(n => n) + 1;
			}
			else
			{
				responce = HtmlProvider.GetResponse<string>(urls.DeputiesPage.Replace("fetch_mps?skl_id=", "p_deputat_list"));
				if (responce.Error == null && !string.IsNullOrEmpty(responce.ReceivedData))
				{
					var document = Converter.ConvertToHtmlDocument(responce.ReceivedData);
					var elemWithLink = document.DocumentNode.SelectNodes("//div[@class='col-half col-last']/ul/li").First();
					var convlink = elemWithLink.SelectSingleNode("a").Attributes["href"].Value;
					convNumber = int.Parse(convlink.Substring(convlink.Length - 1)) + 1;
				}
			}
			return convNumber;
		}


	}

	public class Item
	{
		public string id { get; set; }
		public string guid { get; set; }
		public string type { get; set; }
		public string title { get; set; }
		public string description { get; set; }
		public DateTime pubDate { get; set; }
		public string path { get; set; }
	}

	public class Example
	{
		public string id { get; set; }
		public string title { get; set; }
		public string language { get; set; }
		public DateTime creationDate { get; set; }
		public DateTime pubDate { get; set; }
		public DateTime lastBuildDate { get; set; }
		public string path { get; set; }
		public string format { get; set; }
		public string publisher { get; set; }
		public string creator { get; set; }
		public string manager { get; set; }
		public string managerPhone { get; set; }
		public string webMaster { get; set; }
		public string opendata { get; set; }
		public string category { get; set; }
		public string keywords { get; set; }
		public IList<Item> item { get; set; }
	}
}