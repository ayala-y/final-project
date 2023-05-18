using BLL;
using Entity;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Drawing;
using System.Text;
using DAL;
using System.Buffers.Text;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkirtController : ControllerBase
    {
        SkirtIBLL skirtIBLL;
        public IWebHostEnvironment _environment;

        public SkirtController(SkirtIBLL skirtIBLL, IWebHostEnvironment _environment)
        {
            this.skirtIBLL = skirtIBLL;
            this._environment = _environment;
        }

        [HttpGet]
        [Route("GetSkirtById")]
        public Skirt GetSkirtById(int Id)
        {
            return this.skirtIBLL.GetSkirtById(Id);
        }
        [HttpGet]
        [Route("GetSkirtByName")]
        public Skirt GetSkirtByName(string Name)
        {
            return this.skirtIBLL.GetSkirtByName(Name);
        }


        [HttpGet]
        [Route("GetTypeOfSkirtByName")]
        public string GetTypeOfSkirtByName(string Name)
        {
            return this.skirtIBLL.GetTypeOfSkirtByName(Name);
        }

        [HttpGet]
        [Route("GetAllSkirtsWithsameName")]
        public List<Skirt> GetAllSkirtsWithsameName(string Name)
        {
            return this.skirtIBLL.GetAllSkirtsWithsameName(Name);
        }
        [HttpGet]
        [Route("GetAllSkirtsOfUser")]
        public List<Skirt> GetAllSkirtsOfUser(int user_id)
        {
            return this.skirtIBLL.GetAllSkirtsOfUser(user_id);
        }


        [HttpGet("{imageName}")]
        public IActionResult Get(string imageName)
        {
            Byte[] b;
            if (imageName == null)
            {
                return Content("Hi there is no type value given. Please enter picturefromtext or hostedimagefile in type parameter in url");
            }
            else
            {
                b = System.IO.File.ReadAllBytes(_environment.WebRootPath + "//images//" + imageName);
            }
            return File(b, "image/png");
        }

        [HttpPost]
        [Route("Post")]
        public Skirt Post([FromForm] Skirt objFile)
        {
            string projectDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            string[] files = Directory.GetFiles(projectDirectory, objFile.files.FileName, SearchOption.AllDirectories);
            if (files.Length > 0)
            {
                List<Skirt> lst = GetAllSkirtsWithsameName(objFile.files.FileName);

                foreach (Skirt skirt in lst)
                {
                    if (skirt.UserId == objFile.UserId)
                    {
                        Skirt s1=GetSkirtByName(skirt.ImgName);


                        objFile.Id = s1.Id;    
                        return this.skirtIBLL.UpdateDetailsOfSkirt(objFile);
                    }
                }

                objFile.ImgName = objFile.files.FileName;
                return this.skirtIBLL.AddSkirt(objFile);

            }

            Skirt obj = new Skirt();
            try
            {
                obj.Id = objFile.Id;
                obj.WaistCircumference = objFile.WaistCircumference;
                obj.HipCircumference = objFile.HipCircumference;
                obj.SkirtLength = objFile.SkirtLength;
                obj.HeightHip = objFile.HeightHip;
                obj.UserId = objFile.UserId;
                obj.files = objFile.files;

                obj.ImgName = "\\images\\" + objFile.files.FileName;
                if (objFile.files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\images"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\images\\");
                    }
                    string path = _environment.WebRootPath + "\\images\\" + objFile.files.FileName;
                    using (FileStream filestream = System.IO.File.Create(path))
                    {
                        objFile.files.CopyTo(filestream);
                        filestream.Flush();
                        obj.ImgName = objFile.files.FileName;
                    }

                    return this.skirtIBLL.AddSkirt(obj);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }

        }



        [HttpPost]
        public Skirt PostCut(string []arr)
        {

            Skirt obj = new Skirt();
            try
            {
                obj.Id = Convert.ToInt32(arr[0]);
                obj.UserId = Convert.ToInt32(arr[1]);
                obj.WaistCircumference = 0;
                obj.HipCircumference = 0;
                obj.SkirtLength = 0;
                obj.HeightHip = 0;
                obj.ImgName =  arr[3];

                Guid uuid = Guid.NewGuid();
                string fileName = uuid.ToString();
                obj.SkirtCutImgName = fileName;

                List<Skirt> lst = GetAllSkirtsOfUser((int)obj.UserId);
                foreach (Skirt skirt in lst)
                {
                    if(skirt.ImgName == obj.ImgName&&skirt.SkirtCutImgName!=null)
                    {
                        return this.skirtIBLL.UpdateSkirt(obj);
                    }

                }

                // Remove the data URI scheme and extract the base64-encoded string
                string base64String = Convert.ToString(arr[2]).Split(',')[1];

                // Convert the base64-encoded string to a byte array
                byte[] bytes = Convert.FromBase64String(base64String);
                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);

                }

                string filePath = Path.Combine("wwwroot/images", fileName);
                image.Save(filePath);

                return this.skirtIBLL.UpdateSkirt(obj);

            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        [HttpPut]
        [Route("Put")]
        public void Put(Skirt s)
        {
            this.skirtIBLL.UpdateSkirt(s);
        }

        [HttpDelete]
        [Route("delete/userAndSkirtName")]
        public void Delete(int id,int skirt_id)
        {
            this.skirtIBLL.DeleteSkirt(id, skirt_id);
        }

        [HttpDelete]
        [Route("DeleteAllSkirtsOfUser")]
        public void DeleteAllSkirtsOfUser(int id)
        {
            this.skirtIBLL.DeleteAllSkirtsOfUser(id);
        }
    }
}
