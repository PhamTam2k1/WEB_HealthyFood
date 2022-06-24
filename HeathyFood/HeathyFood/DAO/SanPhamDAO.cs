using HeathyFood.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HeathyFood.DAO
{
    public class SanPhamDAO
    {
        private FoodHeathyContext db = new FoodHeathyContext();
        public List<SanPham> getList(string status="All")
        {
            List<SanPham> list = null;
            switch (status)
            {
                case "Index":
                    list = db.SanPhams.Where(m => m.Active != 0).ToList();
                    break;
                case "Trash":
                    list = db.SanPhams.Where(m => m.Active == 0).ToList();
                    break;
                case "Active":
                    list = db.SanPhams.Where(m => m.Active == 1).ToList();
                    break;
                default:
                    {
                        list = db.SanPhams.ToList();
                        break;
                    }
                   
            }
                 
           
            return list;
        }
        public long getCount()
        {
            var count = db.SanPhams.Count();
            return count;
        }

        public SanPham getRow(int id)
        {
            var row = db.SanPhams.Where(s => s.MaSP == id).FirstOrDefault();
            return row;
        }
        public void Insert(SanPham row)
        {
            db.SanPhams.Add(row);
            db.SaveChanges();

        }
        public void Update(SanPham row)
        {
            db.Entry(row).State = EntityState.Modified;
            db.SaveChanges();

        }
        public void Delete(SanPham row)
        {
            db.SanPhams.Remove(row);
            db.SaveChanges();
        }
    }
}