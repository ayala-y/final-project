using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class SkirtDAL : SkirtIDAL
    {
        SuitYouDbContext db;

        public SkirtDAL(SuitYouDbContext db)
        {
            this.db = db;
        }
        public Skirt GetSkirtById(int Id)
        {
            Skirt t = db.Skirts.FirstOrDefault(x => x.Id == Id);
            return t;
        }
        public Skirt GetSkirtByName(string Name)
        {
            Skirt s = db.Skirts.FirstOrDefault(x => x.ImgName == Name);
            return s;
        }
        public List<Skirt> GetAllSkirtsWithsameName(string Name)
        {
            return db.Skirts.Where(s => s.ImgName == Name).ToList();
        }
        public List<Skirt> GetAllSkirtsOfUser(int user_id)
        {
            return db.Skirts.Where(s => s.UserId == user_id).ToList();
        }

        public Skirt AddSkirt(Skirt s)
        {
            this.db.Skirts.Add(s);
            this.db.SaveChanges();
            return s;

        }

        public Skirt UpdateSkirt(Skirt s)
        {

            Skirt skirt = db.Skirts.FirstOrDefault(x => x.Id == s.Id);
            if (skirt != null&&skirt.UserId==s.UserId&&skirt.SkirtCutImgName==null)//פעם ראשונה עדכון שם גזרה
            {
                skirt.SkirtCutImgName = s.SkirtCutImgName;
                db.SaveChanges();
                return skirt;
            }
            if (skirt != null && skirt.UserId == s.UserId)//עדכון שם גזרה לפעמים נוספות
            {
                skirt.ImgName = s.ImgName;

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", skirt.SkirtCutImgName);
                if (File.Exists(path))
                {
                    string destinatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", s.SkirtCutImgName);
                    System.IO.File.Move(path,destinatePath);
                }
                skirt.SkirtCutImgName = s.SkirtCutImgName;
                db.SaveChanges();
            }
            return skirt;
        }
        public Skirt UpdateDetailsOfSkirt(Skirt s)
        {
            Skirt skirt=db.Skirts.FirstOrDefault(x=>x.Id == s.Id);
            if (skirt != null)
            {
                skirt.WaistCircumference = s.WaistCircumference;
                skirt.HipCircumference = s.HipCircumference;
                skirt.SkirtLength = s.SkirtLength;
                skirt.HeightHip=s.HeightHip;
                db.SaveChanges();
            }
            return skirt;
        }
        public void DeleteSkirt(int id, int skirt_id)
        {
            Skirt s = GetSkirtById(skirt_id);
            if (s != null)
            {
                if (id == s.UserId)
                {
                    if (s.SkirtCutImgName != null)
                    {
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", s.SkirtCutImgName);
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                    }

                    else
                    {
                        if(s.ImgName==null)
                        {
                            db.Remove(s);
                            db.SaveChanges();
                            return;
                        }
                    }

                    List<Skirt> lst = GetAllSkirtsWithsameName(s.ImgName);
                    if (lst.Count > 1)
                    {
                        db.Skirts.Remove(s);
                        db.SaveChanges();
                        return;
                    }
                    if (lst.Count == 1)
                    {
                        string name = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", s.ImgName);
                        if (File.Exists(name))
                        {
                            File.Delete(name);
                            db.Skirts.Remove(s);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }
        public void DeleteAllSkirtsOfUser(int id)
        {
            //מחיקת כל הגזרות של המשתמש
            List<Skirt> skirts = db.Skirts.Where(s => s.UserId == id).ToList();
            foreach (Skirt skirt in skirts)
            {
                if (skirt.SkirtCutImgName != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", skirt.SkirtCutImgName);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                }
            }

            //מחיקת תמונות החצאיות של המשתמש במקרה שלא שייכות לעוד משתמשים נוספים, אחרת הסרת החצאית של המשתמש ממאגר הנתונים מבלי למחוק את התמונה עצמה.
            foreach (Skirt skirt in skirts)
            {
                List<Skirt> lst = GetAllSkirtsWithsameName(skirt.ImgName);
                if (lst.Count > 1)
                {
                    db.Skirts.Remove(skirt);
                    db.SaveChanges();
                }
                else
                {
                    string[] files = Directory.GetFiles("wwwroot/images");
                    
                    for(int i=0;i<files.Length;i++) 
                    {
                        if (files[i]== "wwwroot/images\\" + skirt.ImgName)
                        {
                            File.Delete("wwwroot/images\\" + skirt.ImgName);
                            db.Skirts.Remove(skirt);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}

