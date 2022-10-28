namespace LibrariansAssistant.UserInterfaceLayer.Entities.WinFormsEntities;

internal sealed class PickItem
{
    public bool IsChecked { get; set; }

    public int Id { get; }

    public string? Description { get; }

    internal PickItem(int id, string? description) =>
        (Id, Description) = (id, description);
}