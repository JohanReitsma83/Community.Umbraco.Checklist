namespace Community.Umbraco.Checklist.Models.Dto;

public class ChecklistStatusReport
{
	public int Errors { get; set; }

	public int Warnings { get; set; }

	public int Success { get; set; }

	public int Total { get; set; }
}