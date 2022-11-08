using LibrariansAssistant.PresentationLayer.ViewServiceInterfaces;

namespace LibrariansAssistant.UserInterfaceLayer.Services.WinFormsServices.Implementations;

internal class WinFormsMessageService : IMessageService
{
    public void ShowMessage(string message) =>
        MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

    public void ShowWarning(string warning) =>
        MessageBox.Show(warning, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

    public void ShowError(string error) =>
        MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
}