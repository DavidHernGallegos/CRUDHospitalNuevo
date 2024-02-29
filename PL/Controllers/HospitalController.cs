using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class HospitalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetALL()
        {
            Dictionary<string, object> diccionario = BL.Hospital.GetAll();

            bool respuesta = (bool)diccionario["Resultado"];
            string mensaje = (string)diccionario["Mensaje"];
            ML.Hospital hospital = (ML.Hospital)diccionario["Hospital"];
            if(respuesta)
            {
                return View(hospital);
            }
            else
            {
                ViewBag.Mensaje = mensaje;  
                return PartialView("Modal");
            }


        }

        [HttpGet]
        public IActionResult Form(int? idHospital)
        {
            ML.Hospital hospital = new ML.Hospital();
            if (idHospital != null) 
            {
                Dictionary<string, object> diccionario = BL.Hospital.GetById(idHospital.Value);
                bool respuesta = (bool)diccionario["Resultado"];
                if (respuesta)
                {
                    hospital = (ML.Hospital)diccionario["Hospital"];
                    return View(hospital);
                }
               
            }
            else
            {

            }
            return View(hospital);
        }

        [HttpPost]
        public IActionResult Form(ML.Hospital hospital)
        {
            if (hospital.IdHospital > 0)
            {
                Dictionary<string, object> DiccionarioAgregar = BL.Hospital.Update(hospital);
                bool respuesta = (bool)DiccionarioAgregar["Resultado"];
                string mensaje = (string)DiccionarioAgregar["Mensaje"];
                if (respuesta)
                {
                    ViewBag.Mensaje = mensaje;
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = mensaje;
                    return PartialView("Modal");
                }
                
            }
            else
            {
                Dictionary<string, object> DiccionarioAgregar = BL.Hospital.ADD(hospital);
                bool respuesta = (bool)DiccionarioAgregar["Resultado"];
                string mensaje = (string)DiccionarioAgregar["Mensaje"];
                if (respuesta)
                {
                    ViewBag.Mensaje = mensaje;
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = mensaje;
                    return PartialView("Modal");
                }
            }
        }

        [HttpGet]
        public IActionResult Delete(int idHospital)
        {
            Dictionary<string, object> diccionarioEliminar = BL.Hospital.Delete(idHospital);
            bool respuesta = (bool)diccionarioEliminar["Resultado"];
            string mensaje = (string)diccionarioEliminar["Mensaje"];

            if(respuesta)
            {
                ViewBag.Mensaje = mensaje;
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Mensaje = mensaje;
                return PartialView("Modal");
            }

        }
    }
}
