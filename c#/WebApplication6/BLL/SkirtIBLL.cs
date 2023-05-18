using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace BLL

{
    public interface SkirtIBLL
    {
        Skirt GetSkirtById(int Id);
        Skirt GetSkirtByName(string Name);
        List<Skirt> GetAllSkirtsWithsameName(string Name);
        string GetTypeOfSkirtByName(string Name);
        List<Skirt> GetAllSkirtsOfUser(int user_id);
        Skirt AddSkirt(Skirt s);
        Skirt UpdateSkirt(Skirt s);
        Skirt UpdateDetailsOfSkirt(Skirt s);
        void DeleteSkirt(int id,int skirt_id);
        void DeleteAllSkirtsOfUser(int id);
    }
}
