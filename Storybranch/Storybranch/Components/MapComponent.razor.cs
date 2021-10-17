using System.Collections.Generic;
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

		// --- Protected Properties ---
		protected Map Map { get; set; }

		// --- Private Properties ---
		private List<Layer> MapLayers { get; set; } = new List<Layer>();
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
			CreateMapLayers();

			Map.OnInitialized += SetupLeafletMap;
		}

		// --- Public Methods ---

		// --- Protected Methods ---
		protected void TestZoomMap()
		{
			Map.FitBounds(new PointF(45.943f, 24.967f), new PointF(46.943f, 25.967f), maxZoom: 5f);
		}

		protected void TestPanToSomewhere()
		{
			Map.PanTo(new PointF(40.713185f, -74.0072333f), animate: true, duration: 10f);
		}

		// --- Private Methods ---
		private void CreateMapLayers()
		{
			// Tile layers.
			TileLayer backgroundTileLayer = new TileLayer()
			{
				UrlTemplate = "http://battosai.de/jedaya/map/tiles/{z}/{x}/{y}.jpg",
				Attribution = "...",
			};

			// Shape layers.
			Polygon polygonTestLayer = new Polygon()
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
			};
			Circle circleTestLayer = new Circle
			{
				Position = new LatLng(10f, 5f),
				Radius = 10f
			};

			// Add to list, the order is important!
			MapLayers.Add(backgroundTileLayer);
			MapLayers.Add(polygonTestLayer);
			MapLayers.Add(circleTestLayer);
		}

		private void SetupLeafletMap()
		{
			// Add layers to map.
			foreach (var layerToAdd in MapLayers)
			{
				Map.AddLayer(layerToAdd);
			}
		}
	}
}