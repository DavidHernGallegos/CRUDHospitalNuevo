using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Hospital
    {
        public static Dictionary<string, object> GetAll()
        {
            ML.Hospital hospital = new ML.Hospital();
            Dictionary<string,object> diccionario = new Dictionary<string, object> { {"Hospital", hospital },{"Resultado", false },{"Mensaje", "" } };

            try
            {
                using(DL.CrudHospital1Context context = new DL.CrudHospital1Context())
                {
                    var query = (from Hospitales in context.Hospitals
                                 join Especialidades in context.Especialidads on Hospitales.IdEspecialidad equals Especialidades.IdEspecialidad
                                 select new
                                 {
                                     IdHospital = Hospitales.IdHospital,
                                     Nombre = Hospitales.Nombre,
                                     Direccion = Hospitales.Direccion,
                                     AñoConstruccion = Hospitales.AñoCostruccion,
                                     Capacidad = Hospitales.Capacidad,  
                                     IdEspecialidad = Especialidades.IdEspecialidad,
                                     NombreEspecialidad = Especialidades.Nombre

                                 }).ToList();


                    hospital.Hospitales = new List<object>();
                    if(query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Hospital obj = new ML.Hospital();
                            obj.IdHospital = item.IdHospital;
                            obj.Nombre = item.Nombre;
                            obj.Direccion = item.Direccion;
                            obj.AñoCostruccion = item.AñoConstruccion;
                            obj.Capacidad = item.Capacidad;
                            obj.Especialidad = new ML.Especialidad();
                            obj.Especialidad.IdEspecialidad = item.IdEspecialidad;
                            obj.Especialidad.Nombre = item.NombreEspecialidad;

                            hospital.Hospitales.Add(obj);

                            diccionario["Hospital"] = hospital;
                            diccionario["Resultado"] = true;
                            diccionario["Mensaje"] = "Se han cargado los datos";
                        }
                    }

                    else
                    {
                        diccionario["Resultado"] = false;
                        diccionario["Mensaje"] = "No se han cargado los datos";
                    }

                   
                }

            }catch (Exception e)
            {
                diccionario["Resultado"] = false;
                diccionario["Mensaje"] = "No se han cargado los datos";
            }

            return diccionario;
        }

        public static Dictionary<string, object> GetById(int idHospital)
        {
            ML.Hospital hospital = new ML.Hospital();
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Hospital", hospital }, { "Resultado", false }, { "Mensaje", "" } };

            try
            {
                using (DL.CrudHospital1Context context = new DL.CrudHospital1Context())
                {
                    var query = (from Hospitales in context.Hospitals
                                 join Especialidades in context.Especialidads on Hospitales.IdEspecialidad equals Especialidades.IdEspecialidad
                                 where Hospitales.IdHospital == idHospital
                                 select new
                                 {
                                     IdHospital = Hospitales.IdHospital,
                                     Nombre = Hospitales.Nombre,
                                     Direccion = Hospitales.Direccion,
                                     AñoConstruccion = Hospitales.AñoCostruccion,
                                     Capacidad = Hospitales.Capacidad,
                                     IdEspecialidad = Especialidades.IdEspecialidad,
                                     NombreEspecialidad = Especialidades.Nombre

                                 }).SingleOrDefault();


                  
                    if (query != null)
                    {
                      
                         
                            hospital.IdHospital = query.IdHospital;
                            hospital.Nombre = query.Nombre;
                            hospital.Direccion = query.Direccion;
                            hospital.AñoCostruccion = query.AñoConstruccion;
                            hospital.Capacidad = query.Capacidad;
                            hospital.Especialidad = new ML.Especialidad();
                            hospital.Especialidad.IdEspecialidad = query.IdEspecialidad;
                            hospital.Especialidad.Nombre = query.NombreEspecialidad;

                    

                            diccionario["Hospital"] = hospital;
                            diccionario["Resultado"] = true;
                            diccionario["Mensaje"] = "Se han cargado los datos";
                        
                    }

                    else
                    {
                        diccionario["Resultado"] = false;
                        diccionario["Mensaje"] = "No se han cargado los datos";
                    }


                }

            }
            catch (Exception e)
            {
                diccionario["Resultado"] = false;
                diccionario["Mensaje"] = "No se han cargado los datos";
            }

            return diccionario;
        }



        public static Dictionary<string, object> ADD(ML.Hospital hospital)
        {
         
       
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Mensaje", "" } };

            try
            {
                using (DL.CrudHospital1Context context = new DL.CrudHospital1Context())
                {

                    var query = context.Database.ExecuteSqlRaw($"ADDHospital '{hospital.Nombre}', '{hospital.Direccion}', '{hospital.AñoCostruccion}', {hospital.Capacidad} , {hospital.Especialidad.IdEspecialidad} ");

                    if(query >  0)
                    {
                        diccionario["Resultado"] = true;
                        diccionario["Mensaje"] = "Se han cargado los datos";
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                        diccionario["Mensaje"] = "No se han cargado los datos";
                    }

                }

            }
            catch (Exception e)
            {
                diccionario["Resultado"] = false;
                diccionario["Mensaje"] = "No se han cargado los datos" + e;
            }

            return diccionario;
        }

        public static Dictionary<string, object> Update(ML.Hospital hospital)
        {


            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Mensaje", "" } };

            try
            {
                using (DL.CrudHospital1Context context = new DL.CrudHospital1Context())
                {

                    var query = context.Database.ExecuteSqlRaw($"UpdateHospital {hospital.IdHospital},'{hospital.Nombre}', '{hospital.Direccion}', '{hospital.AñoCostruccion}', {hospital.Capacidad} , {hospital.Especialidad.IdEspecialidad} ");

                    if (query > 0)
                    {
                        diccionario["Resultado"] = true;
                        diccionario["Mensaje"] = "Se han actualizado los datos";
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                        diccionario["Mensaje"] = "No se han actualizado los datos";
                    }

                }

            }
            catch (Exception e)
            {
                diccionario["Resultado"] = false;
                diccionario["Mensaje"] = "No se han actualizado los datos" + e;
            }

            return diccionario;
        }
        public static Dictionary<string, object> Delete(int IdHospital)
        {


            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Mensaje", "" } };

            try
            {
                using (DL.CrudHospital1Context context = new DL.CrudHospital1Context())
                {

                    var query = context.Database.ExecuteSqlRaw($"DeleteHospital {IdHospital}");

                    if (query > 0)
                    {
                        diccionario["Resultado"] = true;
                        diccionario["Mensaje"] = "Se han eliminado los datos";
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                        diccionario["Mensaje"] = "No se han eliminado los datos";
                    }

                }

            }
            catch (Exception e)
            {
                diccionario["Resultado"] = false;
                diccionario["Mensaje"] = "No se han eliminado los datos" + e;
            }

            return diccionario;
        }


    }

}