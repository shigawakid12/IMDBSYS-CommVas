using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CahayaganCRUD.Repository;

namespace CahayaganCRUD.Controllers
{
    public class BaseController : Controller
    {
        public dbsys32Entities db;
        public BaseRepository<user> userRepo;
        public BaseController()
        {
            db = new dbsys32Entities();
            userRepo = new BaseRepository<user>();
        }
    }
}