﻿namespace ML
{
    public class Hospital
    {

        public int IdHospital { get; set; }

        public string? Nombre { get; set; }

        public string? Direccion { get; set; }

        public string? AñoCostruccion { get; set; }

        public int? Capacidad { get; set; }

        public List<object> Hospitales { get; set; }

        public ML.Especialidad? Especialidad { get; set; }

    }
}