using System.Drawing;
using BlazorLeaflet;
using BlazorLeaflet.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Storybranch.Components
{
	public class MapComponentBase : ComponentBase
	{
		// --- Parameters ---
		[Parameter] public float MaxZoom { get; set; } = 10.0f;
		[Parameter] public float MinZoom { get; set; } = 1.0f;
		[Parameter] public float DefaultZoom { get; set; } = 2.0f;

		// --- Injects ---
		[Inject] protected IJSRuntime JsRuntime { get; set; }

		// --- Properties ---
		protected Map Map { get; set; }
		private Circle Circle { get; set; }
		private LatLng StartAt { get; set; } = new LatLng(47.5574007f, 16.3918687f);

		// --- ComponentBase Overrides ---
		protected override void OnInitialized()
		{
			Map = new Map(JsRuntime)
			{
				Center = StartAt,
				Zoom = DefaultZoom,
				MaxZoom = MaxZoom,
				MinZoom = MinZoom
			};

			Map.OnInitialized += () =>
			{
				Map.AddLayer(new TileLayer
				{
					UrlTemplate = "http://battosai.de/jedaya/map/tiles/{z}/{x}/{y}.jpg",
					Attribution = "...",
				});

				Map.AddLayer(new Polygon
				{
					Shape = new[]
					{
						new[]
						{
							new PointF(37f, -109.05f), new PointF(41f, -109.03f), new PointF(41f, -102.05f),
							new PointF(37f, -102.04f)
						}
					},
					Fill = true,
					FillColor = Color.Blue,
					Popup = new Popup
					{
						Content = "How are you doing,"
					}
				});

				Circle = new Circle
				{
					Position = new LatLng(10f, 5f),
					Radius = 10f
				};
				Map.AddLayer(Circle);
			};
		}

		// --- Methods ---
		private void ZoomMap()
		{
			Map.FitBounds(new PointF(45.943f, 24.967f), new PointF(46.943f, 25.967f), maxZoom: 5f);
		}

		private void PanToSomewhere()
		{
			Map.PanTo(new PointF(40.713185f, -74.0072333f), animate: true, duration: 10f);
		}
	}
}