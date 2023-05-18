using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Buffers.Text;

namespace BLL
{
    public class SkirtBLL : SkirtIBLL
    {
        SkirtIDAL skirtIDAL;
        

        public SkirtBLL(SkirtIDAL skirtIDAL)
        {
            this.skirtIDAL = skirtIDAL;
        }

        public Skirt GetSkirtById(int Id)
        {
            return skirtIDAL.GetSkirtById(Id);
        }
        public Skirt GetSkirtByName(string Name)
        {
            return skirtIDAL.GetSkirtByName(Name);
        }
        public List<Skirt> GetAllSkirtsWithsameName(string Name)
        {
            return skirtIDAL.GetAllSkirtsWithsameName(Name);
        }
        public string GetTypeOfSkirtByName(string Name)
        {
            //Environment.CurrentDirectory + "\\images\\" + Name//@"C:\Users\USER\Desktop\אילה\פרויקט גמר\c#\WebApplication6\WebApplication6\wwwroot\images\" + Name
            Bitmap img = new Bitmap(@"C:\Users\USER\Desktop\אילה\פרויקט גמר\c#\WebApplication6\WebApplication6\wwwroot\images\" + Name);
            return Algorithm.Algorithm.algorithms(img);
        }
        public List<Skirt> GetAllSkirtsOfUser(int user_id)
        {
            return skirtIDAL.GetAllSkirtsOfUser(user_id);
        }
      
        public Skirt AddSkirt(Skirt s)
        {
            return skirtIDAL.AddSkirt(s);
        }

        public Skirt UpdateSkirt(Skirt s)
        {
            return skirtIDAL.UpdateSkirt(s);
        }
        public Skirt UpdateDetailsOfSkirt(Skirt s)
        {
            return skirtIDAL.UpdateDetailsOfSkirt(s);
        }
        public void DeleteSkirt(int id, int skirt_id)
        {
            skirtIDAL.DeleteSkirt( id, skirt_id);
        }
        public void DeleteAllSkirtsOfUser(int id)
        {
            skirtIDAL.DeleteAllSkirtsOfUser(id);
        }
    }
}
