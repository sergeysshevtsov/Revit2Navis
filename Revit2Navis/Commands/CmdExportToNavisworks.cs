using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Nice3point.Revit.Toolkit.External;
using System.Diagnostics;
using System.IO;

namespace Revit2Navis.Commands;

[Transaction(TransactionMode.Manual)]
public class CmdExportToNavisworks : ExternalCommand
{
    public override void Execute()
    {
        View activeView = Document.ActiveView;

        if (activeView is not View3D view3D || view3D.IsTemplate || !view3D.IsValidObject)
        {
            TaskDialog.Show("View Check", "The active view is NOT a 3D view.");
            return;
        }

        var dialog = new Options.OptionsWindow { };
        new System.Windows.Interop.WindowInteropHelper(dialog)
        {
            Owner = Autodesk.Windows.ComponentManager.ApplicationWindow
        };
        dialog.ShowDialog();
        var dc = dialog.DataContext as Options.OptionWindowDataContext;

        if (dc.ExportModel)
        {
            Properties.Settings.Default.Save();
            NavisworksExportOptions options = new()
            {
                ExportParts = Properties.Settings.Default.ExportParts,
                ExportElementIds = Properties.Settings.Default.ExportElementIds,
                Parameters = (NavisworksParameters)Enum.Parse(typeof(NavisworksParameters), Properties.Settings.Default.NavisworksParameter),
                ConvertElementProperties = Properties.Settings.Default.ConvertElementProperties,
                ExportLinks = Properties.Settings.Default.ExportLinks,
                ExportRoomAsAttribute = Properties.Settings.Default.ExportRoomAsAttribute,
                ExportUrls = Properties.Settings.Default.ExportUrls,
                Coordinates = (NavisworksCoordinates)Enum.Parse(typeof(NavisworksCoordinates), Properties.Settings.Default.NavisworksCoordinate),
                DivideFileIntoLevels = Properties.Settings.Default.DivideFileIntoLevels,
                ExportScope = (NavisworksExportScope)Enum.Parse(typeof(NavisworksExportScope), Properties.Settings.Default.NavisworksExportScope),
                ExportRoomGeometry = Properties.Settings.Default.ExportRoomGeometry,
                FindMissingMaterials = Properties.Settings.Default.FindMissingMaterials
            };


            Document.Export(Path.GetTempPath(), activeView.Name + ".nwc", options);


            TaskDialog downloadDialog = new("NWC file is ready")
            {
                MainContent = $"Open NWC file",
                AllowCancellation = false
            };
            downloadDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink1,
                       "Open NWC file",
                       "This option will open NWC file.");
            downloadDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink2,
                       "Navigate to NWC file",
                       "This option will open NWC file directory.");
            downloadDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink3,
                       "Close");

            var pathToFile = Path.Combine(Path.GetTempPath(), activeView.Name + ".nwc");
            switch (downloadDialog.Show())
            {
                case TaskDialogResult.CommandLink1:
                    try
                    {
                        Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
                    }
                    catch (Exception ex)
                    {
                        TaskDialog.Show("Error", $"Failed to open file: {ex.Message}");
                    }
                    break;

                case TaskDialogResult.CommandLink2:
                    Process.Start("explorer.exe", $"/select,\"{pathToFile}\"");
                    break;

                default:
                    break;
            }
        }
    }
}