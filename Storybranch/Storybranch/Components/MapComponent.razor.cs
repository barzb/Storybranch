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
		[Inject] protected IJSRuntime _jsRuntime { get; set; }

		// --- Properties ---
		protected Map _map { get; set; }
		private Circle _circle { get; set; }
		private LatLng _startAt { get; set; } = new LatLng(47.5574007f, 16.3918687f);

		// --- ComponentBase Overrides ---
		protected override void OnInitialized()
		{
			_map = new Map(_jsRuntime)
			{
				Center = _startAt,
				Zoom = DefaultZoom,
				MaxZoom = MaxZoom,
				MinZoom = MinZoom
			};

			_map.OnInitialized += () =>
			{
				_map.AddLayer(new TileLayer
				{
					UrlTemplate = "http://battosai.de/jedaya/map/tiles/{z}/{x}/{y}.jpg",
					Attribution = "...",
				});

				_map.AddLayer(new Polygon
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

				_circle = new Circle
				{
					Position = new LatLng(10f, 5f),
					Radius = 10f
				};
				_map.AddLayer(_circle);
			};
		}

		// --- Methods ---
		private void ZoomMap()
		{
			_map.FitBounds(new PointF(45.943f, 24.967f), new PointF(46.943f, 25.967f), maxZoom: 5f);
		}

		private void PanToSomewhere()
		{
			_map.PanTo(new PointF(40.713185f, -74.0072333f), animate: true, duration: 10f);
		}
	}
}