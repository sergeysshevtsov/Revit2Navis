using System.Windows;
using System.Windows.Input;

namespace Revit2Navis.Options;
public class OptionWindowDataContext
{
    public ICommand RestoreCommand => new RelayCommand<Window>(
      window =>
      {
          Properties.Settings.Default.ExportParts = false;
          Properties.Settings.Default.ExportElementIds = true;
          Properties.Settings.Default.NavisworksParameter = "All";
          Properties.Settings.Default.ConvertElementProperties = false;
          Properties.Settings.Default.ExportLinks = true;
          Properties.Settings.Default.ExportRoomAsAttribute = true;
          Properties.Settings.Default.ExportUrls = true;
          Properties.Settings.Default.NavisworksCoordinate = "Internal";
          Properties.Settings.Default.DivideFileIntoLevels = false;
          Properties.Settings.Default.NavisworksExportScope = "Model";
          Properties.Settings.Default.ExportRoomGeometry = true;
          Properties.Settings.Default.FindMissingMaterials = true;
          Properties.Settings.Default.Save();
          window?.Close();
      });


    public ICommand CloseCommand => new RelayCommand<Window>(
        window =>
        {
            Properties.Settings.Default.Save();
            window?.Close();
        });

    public ICommand ExportCommand => new RelayCommand<Window>(
        window =>
        {
            ExportModel = true;
            window?.Close();
        });

    public List<string> NavisworksParameters { get; set; } = ["All", "Elements", "None"];
    public List<string> NavisworksCoordinates { get; set; } = ["Internal", "Shared"];
    public List<string> NavisworksExportScopes { get; set; } = ["Model", "SelectedElements", "View"];
    public bool ExportModel { get; set; } = false;
}
