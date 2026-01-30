using System.Diagnostics;

namespace ScanbotSDK.MAUI.Example.Utils;

public class ImagePicker
{
    private static async Task<FileResult> PickImageAsync()
    {
        var options = new MediaPickerOptions
        {
            Title = "Select a photo",
            SelectionLimit = 1
        };
        var pickedList = await MediaPicker.Default.PickPhotosAsync(options);
        return pickedList.FirstOrDefault();
    }

    /// <summary>
    /// Picks image from the photos application.
    /// </summary>
    /// <returns>ImageSource object.</returns>
    public static async Task<ImageSource> PickImageAsSourceAsync()
    {
        try
        {
            var file = await PickImageAsync();
            if (file is null)
            {
                 //add error
                return null;
            }

            var stream = await file.OpenReadAsync();
                return ImageSource.FromStream(() => stream);
            
        }
        catch (Exception ex)
        {
            App.Navigation.CurrentPage.Alert("Error", $"Unable to pick image: {ex.Message}");
        }

        return null;
    }

    /// <summary>
    /// Picks image from the photos application.
    /// </summary>
    /// <returns>Image path string.</returns>
    public static async Task<string> PickImageAsPathAsync()
    {
        try
        {
            var file = await PickImageAsync();
            if (file?.FullPath is null)
            {
                //add error
                return null;
            }
                
            var path = file.FullPath; // for iOS it returns only the File name.
            if (!IsValidPath(path))
            {
                // iOS
                var stream = await file.OpenReadAsync();

                var extension = Path.GetExtension(file.FileName);
                if (string.IsNullOrEmpty(extension))
                    extension = ".jpg";

                // note: This is just for testing purpose and it is used for saving the image locally after picking the image from gallery.
                // path of the file.
                path = Path.Combine(FileSystem.CacheDirectory, "gallery-picked-items");
                Directory.CreateDirectory(path);

                // name of the file
                path = Path.Combine(path, file.FileName);
                await using var destinationStream = File.Create(path);
                await stream.CopyToAsync(destinationStream);
            }

            return path;
        }
        catch (Exception ex)
        {
            App.Navigation.CurrentPage.Alert("Error", $"Unable to pick image: {ex.Message}");
        }

        return null;
    }
        
    private static bool IsValidPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return false;

        try
        {
            var exist = File.Exists(path);
            return exist;
        }
        catch (Exception e)
        {
            Debug.WriteLine("The file could not be found. For more details:\n" + e.Message);
            return false;
        }
    }
}