using System.Drawing;
using BlazorLeaflet;
using BlazorLeaflet.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Storybranch.Components
{
	public class MapToolBarComponentBase : ComponentBase
	{
		// --- Parameters ---
    [Parameter] EventCallback OnPlacePinDelegate {get; set;}

	
		// --- Injects ---

		// --- Properties ---

		// --- ComponentBase Overrides ---
		protected override void OnInitialized()
		{
		}

		// --- Methods ---
    protected void addObject() {
      OnPlacePinDelegate.InvokeAsync();
    }
	}
}