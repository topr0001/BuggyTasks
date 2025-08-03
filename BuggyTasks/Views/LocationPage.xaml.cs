using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;

namespace BuggyTasks.Views;

public partial class LocationPage : ContentPage
{
    public LocationPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = GetLocation();
    }

    private async Task GetLocation()
    {
        try
        {
            var location = await Geolocation.GetLastKnownLocationAsync();

            if (location != null)
            {
                Console.WriteLine($"Lat: {location.Latitude}, Long: {location.Longitude}");
                LocationLabel.Text = $"Lat: {location.Latitude}, Long: {location.Longitude}";
            }
            else
            {
                Console.WriteLine("No cached location available.");
                LocationLabel.Text = $"No cached location available.";
            }
        }
        catch (FeatureNotSupportedException)
        {
            await DisplayAlert("Error", "Location is not supported on this device.", "OK");
        }
        catch (FeatureNotEnabledException)
        {
            await DisplayAlert("Error", "Location services are not enabled.", "OK");
        }
        catch (PermissionException)
        {
            await DisplayAlert("Error", "Location permission is denied.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
        }
    }
}