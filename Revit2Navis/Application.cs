using Nice3point.Revit.Toolkit.External;
using Revit2Navis.Commands;

namespace Revit2Navis;

[UsedImplicitly]
public class Application : ExternalApplication
{
    public override void OnStartup()
    {
        CreateRibbon();
    }

    private void CreateRibbon()
    {
        var panel = Application.CreatePanel("Export", "SHSS Tools");

        panel.AddPushButton<CmdExportToNavisworks>("Export to\nNavisworks")
            .SetImage("/Revit2Navis;component/Resources/Icons/nvs16.png")
            .SetLargeImage("/Revit2Navis;component/Resources/Icons/nvs32.png");
    }
}