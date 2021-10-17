using System;
using System.Drawing;
using System.Threading.Tasks;
using BlazorLeaflet;
using BlazorLeaflet.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Storybranch.Components
{
	public class MapComponentBase : ComponentBase
	{
		[Inject]
		protected IJSRuntime JsRuntime { get; set; }

		[Parameter]
		public string MapName { get; set; } = "map";

		protected Map _map;
		private PointF _startAt = new PointF(47.5574007f, 16.3918687f);

		protected override void OnInitialized()
		{
			/*_map = new Map(JsRuntime);
			_map.Layers.Add(new TileLayer
			{
				UrlTemplate = "http://battosai.de/jedaya/map/tiles/{z}/{x}/{y}.jpg",
				Attribution = "",
			});*/
		}

		protected void InitMap()
		{
			//Console.WriteLine("initLeaflet");
			//JsRuntime.InvokeVoidAsync("initLeaflet", MapName);
			//StateHasChanged();
		}
	}
}
