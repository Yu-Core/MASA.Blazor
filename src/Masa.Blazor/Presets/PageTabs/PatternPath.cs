﻿namespace Masa.Blazor.Presets;

internal record PatternPath
{
    public PatternPath(string absolutePath)
    {
        AbsolutePath = absolutePath;
        Pattern = "^" + absolutePath.ToLower() + "$";
        CreatedAt = DateTime.Now;
    }

    public PatternPath(string pattern, string absolutePath)
    {
        AbsolutePath = absolutePath;
        Pattern = pattern.ToLower();
        IsSelf = true;
        CreatedAt = DateTime.Now;
    }

    public string AbsolutePath { get; private set; }

    /// <summary>
    /// The regular expression pattern to match.
    /// </summary>
    public string Pattern { get; }

    /// <summary>
    /// Determine whether if the path is a self path.
    /// </summary>
    public bool IsSelf { get; }

    /// <summary>
    /// Use for keeping track of the order of the path, see #1535
    /// </summary>
    public DateTime CreatedAt { get; }

    public TabOptions? Options { get; set; }

    public bool IsCloseable => Options?.Closeable ?? true;

    public void UpdatePath(string path)
    {
        AbsolutePath = path;
    }

    public virtual bool Equals(PatternPath? other)
    {
        return Pattern == other?.Pattern && IsSelf == other?.IsSelf;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IsSelf, Pattern);
    }

    public void Deconstruct(out string pattern, out string absolutePath)
    {
        pattern = this.Pattern;
        absolutePath = this.AbsolutePath;
    }
}