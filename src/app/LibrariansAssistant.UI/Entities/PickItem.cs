namespace LibrariansAssistant.UI.Entities;

/// <summary>
/// Represents a pick item.
/// </summary>
internal sealed class PickItem
{
    /// <summary>
    /// Gets or sets a value indicating whether the element is checked.
    /// </summary>
    public bool IsChecked { get; set; }

    /// <summary>
    /// Gets the item ID.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the item description.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PickItem"/> class.
    /// </summary>
    /// <param name="id">Item ID.</param>
    /// <param name="description">Item description.</param>
    public PickItem(int id, string? description) => (Id, Description) = (id, description);
}