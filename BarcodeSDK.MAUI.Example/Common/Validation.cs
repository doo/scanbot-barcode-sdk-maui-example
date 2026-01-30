namespace ScanbotSDK.MAUI.Example.Utils;

public class Validation
{
    public static async Task<PermissionStatus> CheckAndRequestCameraPermission()
    {
        PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (status == PermissionStatus.Granted)
            return status;

        if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
        {
            // Prompt the user to turn on in settings
            // On iOS once a permission has been denied it may not be requested again from the application
            await App.Navigation.CurrentPage.AlertAsync("Validation Issue: Camera permission", "Permission Denied. Please change permission in the application settings.");
            return status;
        }

        if (Permissions.ShouldShowRationale<Permissions.Camera>())
        {
            // Prompt the user with additional information as to why the permission is needed
            await App.Navigation.CurrentPage.AlertAsync("Validation Issue: Camera permission", "Please grant permission to access the camera in order to proceed.");
        }

        status = await Permissions.RequestAsync<Permissions.Camera>();

        return status;
    }

    public static async Task<bool> IsCameraPermissionValid()
    {
        var permissionStatus = await CheckAndRequestCameraPermission();
        return permissionStatus == PermissionStatus.Granted;
    }
}