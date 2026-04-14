namespace Portfolio.Models;

public class SkillDomain
{
    public int? Id { get; set; }
    public int Number { get; set; }
    public string Icon { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public List<SkillItem> Items { get; set; } = [];
}

public class SkillItem
{
    public string Name { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
}

