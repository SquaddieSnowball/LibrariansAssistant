using LibrariansAssistant.PresentationLayer.ViewServiceInterfaces;

namespace LibrariansAssistant.UserInterfaceLayer.Services.WinFormsServices.Implementations;

internal class DefaultMessageService : IMessageService
{
    public void ShowMessage(string message) =>
        MessageBox.Show(message, "Message", MessageBoxButtons.OK);

    public void ShowWarning(string warning) =>
        MessageBox.Show(warning, "Warning", MessageBoxButtons.OK);

    public void ShowError(string error) =>
        MessageBox.Show(error, "Error", MessageBoxButtons.OK);
}