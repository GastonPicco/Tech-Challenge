using System.Collections.Generic;
using Model;

namespace Interface
{
    public interface IPositionLoader
    {
        public List<Position> LoadPositionFromXml(string fileName);
    }

}