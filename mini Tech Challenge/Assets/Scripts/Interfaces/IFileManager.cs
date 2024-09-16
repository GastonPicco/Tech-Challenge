using System.Collections.Generic;

public interface IFileManager
{
    string GetDocumentsPath(string fileName);
    void SavePositionsToXml(List<Position> positions, string fileName);
    List<Position> LoadPositionsFromXml(string fileName);
    int GetTotalEmployeeCount(List<Position> positions);
    void DeleteFile(string fileName);
}
