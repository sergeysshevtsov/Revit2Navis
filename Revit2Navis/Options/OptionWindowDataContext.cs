using System.Windows;
using System.Windows.Input;

namespace Revit2Navis.Options;
public class OptionWindowDataContext
{
    public OptionWindowDataContext()
    {
    }
    public ICommand CloseCommand => new RelayCommand<Window>(
        window =>
        {
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
